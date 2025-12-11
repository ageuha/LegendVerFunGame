using Code.Events;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    [CreateAssetMenu(fileName = "InventoryAddEventChannel", menuName = "EventChannel/InventoryAddEventChannel")]
    public class InventoryAddEventChannel : EventChannel<ItemData>
    {
        
    }
}
