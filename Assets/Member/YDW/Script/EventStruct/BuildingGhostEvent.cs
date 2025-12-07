using Member.YDW.Script.BuildingSystem;

namespace Member.YDW.Script.EventStruct
{
    public struct BuildingGhostEvent
    {
        public bool OnOff;
        public BuildingDataSO buildingDataSO;

        public BuildingGhostEvent(BuildingDataSO dataSO, bool onOff)
        {
            OnOff = onOff;
            buildingDataSO = dataSO;
        }
    }
}