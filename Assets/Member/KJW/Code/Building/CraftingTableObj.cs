using System.Collections.Generic;
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
    public class CraftingTableObj : BoundsBuilding, IBuilding, IWaitable, IInteractable
    {
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        public List<SpriteRenderer> SpriteRenderers { get; private set; } = new();
        
        public void InitializeBuilding(BuildingDataSO buildingData)
        {
            SpriteRenderers.Clear();
            BuildingData = buildingData;
            foreach (Transform item in transform.root)
            {
                if (item.TryGetComponent(out SpriteRenderer s))
                SpriteRenderers.Add(s);
            }
            
            foreach (SpriteRenderer item in SpriteRenderers)
            {
                item.sprite = buildingData.Image;
            }

            Initialize(buildingData.BuildingSize,buildingData.MaxHealth);
            timer.StartTimer(this,cooldownBar,buildingData.BuildTime,this,true);
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