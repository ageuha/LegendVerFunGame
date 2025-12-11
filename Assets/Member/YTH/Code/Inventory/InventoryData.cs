using System;
using System.Collections.Generic;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    [Serializable]
    public class InventoryData
    {
        public List<ItemData> InventoryItems;
        public ItemData TotemItem;

        public InventoryData(List<ItemData> inventoryItems, ItemData totemItem)
        {
            this.InventoryItems = inventoryItems;
            this.TotemItem = totemItem;
        }
    }
}
