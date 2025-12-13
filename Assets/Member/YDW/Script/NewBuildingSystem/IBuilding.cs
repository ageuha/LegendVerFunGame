using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public interface IBuilding
    {
        public bool IsActive { get; }
        public BuildingDataSO BuildingData { get; }
        public void InitializeBuilding(BuildingDataSO buildingData);
    }
}