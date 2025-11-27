using System;

namespace _02._Member.YTH.Code.Item
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
