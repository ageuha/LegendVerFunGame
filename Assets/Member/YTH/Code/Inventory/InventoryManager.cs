using System.Collections.Generic;
using Code.Core.GlobalStructs;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {
        [field:SerializeField] public InventoryItem HoldItem { get; private set; }
        public List<InventorySlot> inventorySlots;

        [SerializeField] private InventoryItem inventoryItemPrefab;
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;

        private int m_SelectedSlot = -1;

        private void Start()
        {
            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
            }

            ChangeSelectedSlot(0);
            
            inventoryAddEventChannel.OnEvent += AddItem;
            inventoryItemPickUpEventChannel.OnEvent += PickUp;
            inventoryItemPickDownEventChannel.OnEvent += PickDown;
        }

        private void OnDestroy()
        {
            inventoryAddEventChannel.OnEvent -= AddItem;
            inventoryItemPickUpEventChannel.OnEvent -= PickUp;
        }

        private void ChangeSelectedSlot(int value)
        {
            if (m_SelectedSlot >= 0)
            {
                inventorySlots[m_SelectedSlot].UnSelect();
            }

            inventorySlots[value].Select();
            m_SelectedSlot = value;
        }

        private void PickUp(InventoryItem item)
        {
            HoldItem = item;
        }

        private void PickDown(Empty empty)
        {
            HoldItem = null;
        }

        public void AddItem(ItemDataSO item)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null && itemInSlot.Item == item && itemInSlot.Count < item.MaxStack)
                {
                    itemInSlot.AddStack();
                    return;
                }
            }

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return;
                }
            }

            return;
        }

        private void SpawnNewItem(ItemDataSO item, InventorySlot slot)
        {
            InventoryItem newItem = Instantiate(inventoryItemPrefab, slot.transform);
            newItem.Initialize(this, item);
        }

        public ItemDataSO GetSelectedItem(bool use)
        {
            InventorySlot slot = inventorySlots[m_SelectedSlot];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                if (use) itemInSlot.RemoveStack();

                return itemInSlot.Item;
            }

            return null;
        }

    }
}
