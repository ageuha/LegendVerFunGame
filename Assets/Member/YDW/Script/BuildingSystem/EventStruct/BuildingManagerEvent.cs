using Member.YDW.Script.PathFinder;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.EventStruct
{
    public enum BuildingManagerEventType
    {
        AddBuilding,
        RemoveBuilding,
        GetBuilding,
    }
    public struct BuildingManagerEvent
    {
        public BuildingManagerEventType EventType;
        public NodeData  PositionNode;
        public BuildingDataSO Data;
        public IBuilding building;

        public BuildingManagerEvent(BuildingManagerEventType eventType, NodeData position, BuildingDataSO data,  IBuilding building)
        {
            EventType = eventType;
            PositionNode = position;
            Data = data;
            this.building = building;

        }
    }
}