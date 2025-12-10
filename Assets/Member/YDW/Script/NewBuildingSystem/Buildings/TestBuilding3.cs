using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class TestBuilding3 : BoundsBuilding, IBuilding , IWaitable
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
            IsActive = true;
        }
        
        public void SetWaiting(bool waiting)
        {
            IsWaiting =  waiting;
        }
    }
}