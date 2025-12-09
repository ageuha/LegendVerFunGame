using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Inventory
{
    public class InventoryTotemSlot : InventorySlot, IExtension
    {
        public bool CheckCondition(InventorySlot inventorySlot)
        {
            return false;
        }
    }
}
