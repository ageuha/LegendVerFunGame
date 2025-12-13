using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Inventory;

namespace YTH.Code.Craft
{    
    public class CraftingMaterialSlot : InventorySlot
    {
        [SerializeField] private CraftingTableMateiralChangeEventChannel craftingTableMateiralChangeEventChannel;
        private CraftingTable m_CraftingTable;

        protected override void OnTransformChildrenChanged()
        {
            Logging.Log("OnTransformChildrenChanged");
            base.OnTransformChildrenChanged();
        }

        public override void OnPointerClick(PointerEventData eventData)
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
                        craftingTableMateiralChangeEventChannel.Raise(new Empty());
                    }
                    else if(eventData.button == PointerEventData.InputButton.Right)
                    {
                        InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(transform);
                        newItem.Initialize(m_InventoryManager, m_InventoryManager.HoldItem.Item, 1);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;
                        newItem.CountChanged += OnChangeItemCount;
                        m_InventoryManager.HoldItem.RemoveStack(1);
                        craftingTableMateiralChangeEventChannel.Raise(new Empty());
                    }
                }
            }
        }

        private void OnChangeItemCount()
        {
            craftingTableMateiralChangeEventChannel.Raise(new Empty());
        }
    }
}
