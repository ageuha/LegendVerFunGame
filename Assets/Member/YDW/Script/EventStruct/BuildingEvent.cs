using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.PathFinder;

namespace Member.YDW.Script.EventStruct
{
    public struct BuildingEvent
    {
        public NodeData buildNode;
        public BuildingDataSO buildingData;

        public BuildingEvent(NodeData buildNode, BuildingDataSO buildingData)
        {
            this.buildNode = buildNode;
            this.buildingData = buildingData;
        }
    }
}