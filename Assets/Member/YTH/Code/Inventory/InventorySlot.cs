using Code.Core.GlobalStructs;
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
            if (transform.childCount == 0)
            {
                if(m_InventoryManager.HoldItem != null)
                {    
                    m_InventoryManager.HoldItem.parentAfterDrag = transform;
                    m_InventoryManager.HoldItem.PickDown();
                    inventoryItemPickDownEventChannel.Raise(new Empty());
                }
            }
        }
    }
}
