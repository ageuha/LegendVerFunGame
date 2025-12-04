using Code.Events;
using UnityEngine;
using YTH.Code.Inventory;
using YTH.Code.Item;

[CreateAssetMenu(fileName = "InventoryEventChannel", menuName = "EventChannel/InventoryEventChannel", order = 1)]
public class InventoryEventChannel : EventChannel<InventoryItem>
{
    
}
