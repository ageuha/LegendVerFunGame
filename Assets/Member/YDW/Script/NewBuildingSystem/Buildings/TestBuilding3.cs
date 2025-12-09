using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class TestBuilding3 : BoundsBuilding, IBuilding
    {
        public BuildingDataSO BuildingData { get; private set; }
        private SpriteRenderer _spriteRenderer;
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = BuildingData.Image;
        }

        public  void DestroyedBuilding()
        {
            
        }
    }
}