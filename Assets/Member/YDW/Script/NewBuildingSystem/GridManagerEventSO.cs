using Code.Events;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    [CreateAssetMenu(fileName = "GridManagerEvent", menuName = "BuildingSystem/GridManagerEvent", order = 0)]
    public class GridManagerEventSO : EventChannel<GridManagerEventValue>
    {
        
    }
}