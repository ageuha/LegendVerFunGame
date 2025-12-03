using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.EventStruct
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