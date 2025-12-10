using UnityEngine;
using YTH.Code.Inventory;

namespace YTH.Code.Interface
{    
    public interface IExtension
    {
        public bool CheckCondition(InventoryItem inventoryItem);
    }
}
