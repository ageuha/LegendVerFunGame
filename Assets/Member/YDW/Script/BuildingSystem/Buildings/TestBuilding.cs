using Code.Core.Pool;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class TestBuilding : PoolableObject, IBuilding
    {
        public ICooldownBar CooldownBar { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
        }
    }
}