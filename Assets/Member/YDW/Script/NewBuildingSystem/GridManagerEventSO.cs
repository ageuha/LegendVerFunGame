using Code.Events;
using Code.GridSystem.Objects;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    [CreateAssetMenu(fileName = "GridManagerEvent", menuName = "BuildingSystem/GridManagerEvent", order = 0)]
    public class GridManagerEventSO : EventChannel<GridManagerEventValue>
    {
        
    }
}