namespace Member.YDW.Script.BuildingSystem
{
    public interface IBuilding
    {
        public ICooldownBar  CooldownBar { get; }
        
        public BuildingDataSO BuildingData { get; }

        public void Initialize(BuildingDataSO buildingData);
    }
}