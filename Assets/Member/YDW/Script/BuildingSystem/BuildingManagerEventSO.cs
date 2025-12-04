using Code.Events;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingManagerEvent", menuName = "BuildingSystem/BuildingManager", order = 0)]
    public class BuildingManagerEventSO : EventChannel<BuildingManagerEvent>
    {
        
    }
}