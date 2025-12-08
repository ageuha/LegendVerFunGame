using Code.Events;
using UnityEngine;
using YTH.Code.Item;

[CreateAssetMenu(fileName = "InventoryChangeEventChannel", menuName = "EventChannel/InventoryChangeEventChannel", order = 2)]
public class InventoryChangeEventChannel : EventChannel<InventoryItem[,]>
{
    
}
