using System.Collections.Generic;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.PathFinder;

namespace Member.YDW.Script.EventStruct
{
    public enum BuildingManagerEventType
    {
        AddBuilding,
        RemoveBuilding,
    }
    public struct BuildingManagerEvent
    {
        public BuildingManagerEventType EventType;
        public List<NodeData>  PositionNode;
        public BuildingDataSO Data;
        public IBuilding building;

        public BuildingManagerEvent(BuildingManagerEventType eventType, List<NodeData> position, BuildingDataSO data,  IBuilding building)
        {
            EventType = eventType;
            PositionNode = position;
            Data = data;
            this.building = building;

        }
    }
}