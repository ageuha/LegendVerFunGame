using Code.Core.Pool;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class TestBuilding : PoolableObject, IBuilding
    {
        public ICooldownBar CooldownBar { get; }
        public BuildingDataSO BuildingData { get; }
        public void Initialize(BuildingDataSO buildingData)
        {
            throw new System.NotImplementedException();
        }
    }
}