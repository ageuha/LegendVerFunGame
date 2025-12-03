
using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{
    public class PlayerInventory : InventoryData
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void AddItem(ItemDataSO itemData, int amount = 1)
        {
            if (!CanAddItem(itemData, amount)) return;

            int remain = amount;

            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.IsEmpty) continue;
                if (item.itemData != itemData) continue;
                if (item.IsFullStack) continue;

                item.itemData ??= itemData;

                int added = item.GetRemainAmount();
                int count = Mathf.Min(added, remain);
                int overflow = item.AddStack(count);

                remain -= count;
                remain += overflow;
                
                if (remain <= 0) return;
            }

            while (GetRemainSlotCount() > 0 && remain > 0)
            {
                var item = GetEmptySlot();

                item ??= new(itemData, 0);
                item.itemData ??= itemData;

                int added = item.GetRemainAmount();
                int count = Mathf.Min(added, remain);
                int overflow = item.AddStack(count);

                remain -= count;
                remain += overflow;

                if (remain <= 0) return;
            }
        
        }

        public override bool CanAddItem(ItemDataSO itemData, int amount)
        {
            if (itemData == null || amount <= 0) 
            {
                Logging.LogWarning("공간이 존재하지 않거나, 넣을 아이템이 존재하지 않습니다.");
                return false;
            }

            int remain = amount;
            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.IsEmpty) continue;
                if (item.itemData != itemData) continue;
                if (item.IsFullStack) continue;

                int free = item.GetRemainAmount();
                if (remain <= free) return true;

                remain -= free;
                return remain <= 0;
            }

            remain -= GetRemainSlotCount() * itemData.MaxStack;
            return remain <= 0;
        }

        public override void RemoveItem(ItemDataSO itemData, int amount)
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
