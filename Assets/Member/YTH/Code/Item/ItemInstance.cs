using System;

namespace YTH.Item
{    
    [Serializable]
    public class ItemInstance
    {
        public ItemSO itemSO;
        public int amount;

        public ItemInstance(ItemSO itemSO, int amount)
        {
            this.itemSO = itemSO;
            this.amount = amount;
        }
    }
}
