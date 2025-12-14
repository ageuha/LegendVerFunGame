using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.GridSystem.Objects;
using Member.JJW.Code.Interface;
using Member.KJW.Code.EventChannel;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.NewBuildingSystem;
using UnityEngine;

namespace Member.KJW.Code.Building
{
    public class CraftingTableObj : UnitBuilding, IBuilding, IWaitable, IInteractable
    {
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        public void InitializeBuilding(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = BuildingData.Image;
        }

        public void Interaction(InteractionContext context)
        {
            if (IsWaiting) return;
            context.EventChannel.Raise(new Empty());
            Logging.Log("Crafting Table Interacted");
        }

        public bool IsWaiting { get; private set; }
        public void SetWaiting(bool waiting)
        {
            IsWaiting = waiting;
        }
    }
}