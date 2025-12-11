using System;
using Member.YTH.Code.Item;

namespace YTH.Code.Item
{
    [Serializable]
    public class ItemData
    {
        public ItemDataSO Item;
        public int Count;

        public ItemData(ItemDataSO item, int count)
        {
            this.Item = item;
            this.Count = count;
        }
    }
}
