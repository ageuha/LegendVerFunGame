using Member.YDW.Script.BuildingSystem;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class ShabbyHouseBuilding : BoundsBuilding, IBuilding , IWaitable
    {
        public bool IsActive { get; private set; }
        public bool IsWaiting { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            SpriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = BuildingData.Image;
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
            }
            IsWaiting = waiting;
        }
    }
}