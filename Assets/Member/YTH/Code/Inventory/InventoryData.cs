using System;
using System.Collections.Generic;
using YTH.Code.Inventory;

namespace YTH.Code.Inventorys
{    
    [Serializable]
    public class InventoryData
    {
        public List<InventoryItem> InventoryItems;
        public InventoryItem TotemItem;

        public InventoryData(List<InventoryItem> inventoryItems, InventoryItem totemItem)
        {
            this.InventoryItems = inventoryItems;
            this.TotemItem = totemItem;
        }
    }
}
