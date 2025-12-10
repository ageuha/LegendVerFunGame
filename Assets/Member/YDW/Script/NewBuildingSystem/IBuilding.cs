using Member.YDW.Script.BuildingSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public interface IBuilding
    {
        public bool IsActive { get; }
        public BuildingDataSO BuildingData { get; }

        public void Initialize(BuildingDataSO buildingData);
    }
}