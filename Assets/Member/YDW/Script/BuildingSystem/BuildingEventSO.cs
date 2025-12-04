using Code.Events;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingEvent", menuName = "BuildingSystem/BuildingEvent", order = 0)]
    public class BuildingEventSO : EventChannel<BuildingEvent>
    {
    }
}