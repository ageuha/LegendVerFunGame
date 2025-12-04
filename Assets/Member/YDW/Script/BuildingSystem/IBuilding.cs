using System.Collections.Generic;
using Member.YDW.Script.PathFinder;

namespace Member.YDW.Script.BuildingSystem
{
    public interface IBuilding
    {
        public ICooldownBar  CooldownBar { get; }
        
        public List<NodeData> CurrentNodeData { get; }
        
        public BuildingDataSO BuildingData { get; }

        public void Initialize(BuildingDataSO buildingData, List<NodeData> nodeData);

        public void DestroyedBuilding();
    }
}