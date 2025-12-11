using Code.Events;
using UnityEngine;

namespace Member.KJW.Code.EventChannel
{
    [CreateAssetMenu(fileName = "BuildingGhostFlagEventChannel", menuName = "EventChannel/BuildingGhostFlagEventChannel", order = 0)]
    public class BuildingGhostFlagEventChannel : EventChannel<bool>
    {
        
    }
}