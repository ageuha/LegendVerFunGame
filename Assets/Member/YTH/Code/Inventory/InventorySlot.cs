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
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [SerializeField] private Image image;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unSelectedColor;
        private InventoryManager m_InventoryManager;

        public void Initialize(InventoryManager inventoryManager)
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


        public void OnPointerClick(PointerEventData eventData)
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
                        Logging.Log("InventorySlot 1 PickDown Right Click");
                        InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(transform);
                        newItem.Initialize(m_InventoryManager, m_InventoryManager.HoldItem.Item, 1);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;
                        m_InventoryManager.HoldItem.RemoveStack(1);
                    }
                }
            }
        }

        public InventoryItem GetInventoryItem()
        {
            return GetComponentInChildren<InventoryItem>();
        }
    }
}
