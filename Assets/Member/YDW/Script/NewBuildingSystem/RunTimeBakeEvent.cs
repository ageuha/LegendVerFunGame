using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public enum RunTimeBakeEventType
    {
        Set,
        Delete
    }
    public struct RunTimeBakeEvent
    {
        public readonly RunTimeBakeEventType runTimeBakeEventType;
        public readonly Vector2Int buildingPosition;
        public readonly Vector2Int buildingSize;

        public RunTimeBakeEvent(RunTimeBakeEventType runTimeBakeEventType, Vector2Int buildingWorldPos,  Vector2Int buildingSize)
        {
            this.runTimeBakeEventType =  runTimeBakeEventType;
            buildingPosition = buildingWorldPos;
            this.buildingSize = buildingSize;
            
        }
        
    }
}