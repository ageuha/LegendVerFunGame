
using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{
    public class PlayerInventory : InventoryData
    {
        [SerializeField] private InventoryEventChannel inventoryEventChannel;
        protected override void Awake()
        {
            base.Awake();
            inventoryEventChannel.OnEvent += AddItem;
        }

        private void OnDestroy()
        {
            inventoryEventChannel.OnEvent -= AddItem;
        }

        public override void AddItem(InventoryItem inventoryItem)
        {
            if (!CanAddItem(inventoryItem)) return;

            int remain = inventoryItem.stackSize;

            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.IsEmpty) continue;
                if (item.itemData != inventoryItem.itemData) continue;
                if (item.IsFullStack) continue;

                item.itemData ??= inventoryItem.itemData;

                int added = item.GetRemainAmount();
                int count = Mathf.Min(added, remain);
                int overflow = item.AddStack(count);

                Logging.Log($"Added : {added}, Count : {count}, Overflow : {overflow}");

                remain -= count;
                remain += overflow;
                
                if (remain <= 0) return;
            }

            while (GetRemainSlotCount() > 0 && remain > 0)
            {
                var item = GetEmptySlot();

                item ??= new(inventoryItem.itemData, 0);
                item.itemData ??= inventoryItem.itemData;

                int added = item.GetRemainAmount();
                int count = Mathf.Min(added, remain);
                int overflow = item.AddStack(count);

                remain -= count;
                remain += overflow;

                if (remain <= 0) return;
            }
        
        }

        public override bool CanAddItem(InventoryItem inventoryItem)
        {
            if (inventoryItem.itemData == null || inventoryItem.stackSize <= 0) 
            {
                Logging.LogWarning("공간이 존재하지 않거나, 넣을 아이템이 존재하지 않습니다.");
                return false;
            }

            int remain = inventoryItem.stackSize;
            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.IsEmpty) continue;
                if (item.itemData != inventoryItem.itemData) continue;
                if (item.IsFullStack) continue;

                int free = item.GetRemainAmount();
                if (remain <= free) return true;

                remain -= free;
            }

            remain -= GetRemainSlotCount() * inventoryItem.itemData.MaxStack;
            return remain <= 0;
        }

        public override void RemoveItem(InventoryItem inventoryItem)
        {
        }

        public override int GetRemainSlotCount()
        {
            int emptySlotCount = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == null)
                {
                    emptySlotCount++;
                    continue;
                }
                if (inventory[i].IsEmpty)
                {
                    emptySlotCount++;
                    continue;
                }
            }

            return emptySlotCount;
        }
    }
}
