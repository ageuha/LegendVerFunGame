using System;
using Member.YDW.Script.BuildingSystem.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingItemTest : MonoBehaviour, IBuildingItem
    {
        [field:SerializeField]public BuildingSOEvents EventSO { get; private set; }
        [field:SerializeField]public Sprite Icon { get; private set; }

        private void Start()
        {
            OnBuildingGhost();
        }

        public void OnBuildingGhost()
        {
            EventSO.GhostEventSO.Raise(new BuildingGhostEvent(Icon,true));
        }

        public void OffBuildingGhost()
        {
            EventSO.GhostEventSO.Raise(new BuildingGhostEvent(Icon,false));
        }
        
    }
        
}