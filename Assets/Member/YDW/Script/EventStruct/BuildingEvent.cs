using Code.GridSystem.Objects;
using UnityEngine;
using Member.YDW.Script.BuildingSystem;

namespace Member.YDW.Script.EventStruct
{
    public struct BuildingEvent
    {
        public Vector2Int buildCellPosition;
        public Vector2 buildingWorldPosition;
        public GridObject gridObject;

        public BuildingEvent(Vector2Int buildCellPosition,Vector2 buildingWorldPosition, GridObject gridObject)
        {
            this.buildCellPosition = buildCellPosition;
            this.buildingWorldPosition = buildingWorldPosition;
            this.gridObject = gridObject;
        }
    }
}