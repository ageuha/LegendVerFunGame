using System;
using UnityEngine;

namespace YTH.Code.Item
{    
    [Serializable]
    public class InventoryItem
    {
        public ItemDataSO itemData;
        public int Count;
        public Vector2Int inventoryIndex;

        public bool IsFullStack => Count >= itemData.MaxStack;
        public bool IsEmpty => Count <= 0;

        public InventoryItem(ItemDataSO itemData, int stackSize, Vector2Int index)
        {
            this.itemData = itemData;
            this.Count = stackSize;
            this.inventoryIndex = index;
        }

        public void AddStack(int amount)
        {
            Count += amount;
        }

        public void RemoveStack(int amount = 1)
        {
            Count -= amount;

            if (Count < 0)
            {
                Count = 0;
            }
        }
    }
}
