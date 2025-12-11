using System;
using System.Collections.Generic;

namespace YTH.Code.Inventory
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
