using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Enum;
using YTH.Code.Interface;

namespace YTH.Code.Inventory
{
    public class InventoryTotemSlot : InventorySlot, IExtension
    {
        [SerializeField] private InventoryTotmeSlotChangeEventChannel inventoryTotmeSlotChangeEventChannel;
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            if (m_InventoryManager.HoldItem != null)
            {
                if (transform.childCount == 0)
                {
                    if (CheckCondition(m_InventoryManager.HoldItem))
                    {    
                        if(eventData.button == PointerEventData.InputButton.Left)
                        {    
                            m_InventoryManager.HoldItem.parentAfterDrag = transform;
                            m_InventoryManager.HoldItem.PickDown();
                            inventoryItemPickDownEventChannel.Raise(new Empty());
                            inventoryTotmeSlotChangeEventChannel.Raise(new Empty());
                        }
                        else if(eventData.button == PointerEventData.InputButton.Right)
                        {
                            InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(transform);
                            newItem.Initialize(m_InventoryManager, m_InventoryManager.HoldItem.Item, 1);
                            newItem.transform.localScale = Vector3.one;
                            newItem.transform.localPosition = Vector3.zero;
                            m_InventoryManager.HoldItem.RemoveStack(1);
                            inventoryTotmeSlotChangeEventChannel.Raise(new Empty());
                        }
                    }
                }
            }
        }
        public bool CheckCondition(InventoryItem inventoryItem)
        {
            return ItemType.Totem == m_InventoryManager.HoldItem.Item.ItemType;
        }
    }
}
