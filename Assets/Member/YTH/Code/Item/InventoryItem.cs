using System;

namespace YTH.Code.Item
{    
    [Serializable]
    public class InventoryItem
    {
        public ItemDataSO itemData;
        public int stackSize;

        public bool IsFullStack => stackSize >= itemData.MaxStack;

        public InventoryItem(ItemDataSO itemData, int stackSize)
        {
            this.itemData = itemData;
            this.stackSize = stackSize;
        }

        public int AddStack(int amount)
        {
            int remainCount = 0;
            stackSize += amount;

            if (stackSize > itemData.MaxStack)
            {
                remainCount = stackSize - itemData.MaxStack;
                stackSize = itemData.MaxStack;
            }

            return remainCount;
        }

        public void RemoveStack(int amount = 1)
        {
            stackSize -= amount;

            if (stackSize < 0)
            {
                stackSize = 0;
            }
        }
    }
}
