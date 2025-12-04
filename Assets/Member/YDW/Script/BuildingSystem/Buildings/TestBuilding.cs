using System.Collections.Generic;
using Code.Core.Pool;
using Code.EntityScripts;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class TestBuilding : HealthSystem, IBuilding
    {
        //추후 스탯을 SO로 받아올 예정? 임.
        public List<NodeData> CurrentNodeData { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public void Initialize(BuildingDataSO buildingData, List<NodeData> currentNodeData)
        {
            BuildingData = buildingData;
            CurrentNodeData = currentNodeData;
            //추후 HealthSystem을 초기화.
        }

        public void DestroyedBuilding()
        {
            
        }
    }
}