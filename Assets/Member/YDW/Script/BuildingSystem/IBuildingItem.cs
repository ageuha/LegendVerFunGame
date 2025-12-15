namespace Member.YDW.Script.BuildingSystem
{
    public interface IBuildingItem
    {
        public BuildingSOEvents EventSO { get; }
        public BuildingDataSO BuildingData { get; }
        
        public void OnBuildingGhost();
        
        public void OffBuildingGhost();
    }
}