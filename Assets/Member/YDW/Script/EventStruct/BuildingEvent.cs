using Code.GridSystem.Objects;
using UnityEngine;

namespace Member.YDW.Script.EventStruct
{
    public struct BuildingEvent
    {
        public Vector2Int buildCellPosition;
        public GridObject gridObject;

        public BuildingEvent(Vector2Int buildCellPosition, GridObject gridObject)
        {
            this.buildCellPosition = buildCellPosition;
            this.gridObject = gridObject;
        }
    }
}