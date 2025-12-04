using System.Collections.Generic;
using Code.Core.Pool;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class TestBuilding : PoolableObject, IBuilding
    {
        public ICooldownBar CooldownBar { get; private set; }
        public List<NodeData> CurrentNodeData { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public void Initialize(BuildingDataSO buildingData, List<NodeData> currentNodeData)
        {
            BuildingData = buildingData;
            CurrentNodeData = currentNodeData;
        }

        public void DestroyedBuilding()
        {
            
        }
    }
}