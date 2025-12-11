using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YTH.Code.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        [field:SerializeField] public InventoryItem InventoryItem { get; private set; }
        [SerializeField] protected InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [SerializeField] private InventoryChangeEventChannel inventoryChangeEventChannel;
        [SerializeField] private Image image;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unSelectedColor;
        [SerializeField] protected InventoryManager m_InventoryManager;

        protected virtual void OnTransformChildrenChanged()
        {
            InventoryItem = GetComponentInChildren<InventoryItem>();
        }

        public virtual void Initialize(InventoryManager inventoryManager)
        {
            this.m_InventoryManager = inventoryManager;
            UnSelect();
        }

        public void Select()
        {
            image.color = selectedColor;
        }

        public void UnSelect()
        {
            image.color = unSelectedColor;
        }


        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (m_InventoryManager.HoldItem != null)
            {
                if (transform.childCount == 0)
                {
                    if(eventData.button == PointerEventData.InputButton.Left)
                    {    
                        m_InventoryManager.HoldItem.parentAfterDrag = transform;
                        m_InventoryManager.HoldItem.PickDown();
                        inventoryItemPickDownEventChannel.Raise(new Empty());
                    }
                    else if(eventData.button == PointerEventData.InputButton.Right)
                    {
                        InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(transform);
                        newItem.Initialize(m_InventoryManager, m_InventoryManager.HoldItem.Item, 1);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;
                        m_InventoryManager.HoldItem.RemoveStack(1);
                    }
                    inventoryChangeEventChannel.Raise(new Empty());
                }
            }
        }
    }
}
