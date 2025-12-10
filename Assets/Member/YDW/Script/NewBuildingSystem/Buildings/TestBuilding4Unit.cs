using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class TestBuilding4Unit : UnitBuilding , IBuilding , IWaitable
    {
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        private SpriteRenderer _spriteRenderer;
        public bool IsWaiting { get; private set; }
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = BuildingData.Image;
            
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
                Logging.Log("IsActive Building");
            }
            IsWaiting = waiting;
        }

        private void OnDestroy()
        {
            Logging.Log("Oh.. Im Destroyed");
        }
    }
}