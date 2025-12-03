using System;
using Member.YDW.Script.BuildingSystem.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingItemTest : MonoBehaviour, IBuildingItem
    {
        [field:SerializeField]public BuildingSOEvents EventSO { get; private set; }
        
        [field:SerializeField]public BuildingDataSO BuildingData { get; private set; }

        private void Start()
        {
            OnBuildingGhost();
        }

        public void OnBuildingGhost()
        {
            EventSO.GhostEventSO.Raise(new BuildingGhostEvent(BuildingData,true));
        }

        public void OffBuildingGhost()
        {
            EventSO.GhostEventSO.Raise(new BuildingGhostEvent(BuildingData,false));
        }
        
    }
        
}