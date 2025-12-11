using Code.Events;
using Member.YTH.Code.Item;
using UnityEngine;

namespace YTH.Code.Inventory
{    
    [CreateAssetMenu(fileName = "InventoryAddEventChannel", menuName = "EventChannel/InventoryAddEventChannel")]
    public class InventoryAddEventChannel : EventChannel<ItemData>
    {
        
    }
}
