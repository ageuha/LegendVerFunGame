using Code.Events;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    [CreateAssetMenu(fileName = "BuildingGhostEvent", menuName = "BuildingSystem/Event", order = 0)]
    public class BuildingGhostEventSO : EventChannel<BuildingGhostEvent>
    {
        
    }
}