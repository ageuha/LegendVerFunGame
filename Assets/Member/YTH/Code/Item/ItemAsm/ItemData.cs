using System;
using Member.YTH.Code.Item;

namespace YTH.Code.Item
{
    [Serializable]
    public class ItemData
    {
        public int ItemID;
        public int Count;

        public ItemData(int itemID, int count)
        {
            this.ItemID = itemID;
            this.Count = count;
        }
    }
}
