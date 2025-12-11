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
    public class CraftingTableObj : UnitBuilding, IBuilding, IInteractable
    {
        [SerializeField] private CraftingInteractEventChannel craftingInteractEventChannel;
        
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        private SpriteRenderer _spriteRenderer;
        
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteRenderer.sprite = BuildingData.Image;
        }

        public void Interaction(InteractionContext context)
        {
            craftingInteractEventChannel.Raise(new Empty());
            Logging.Log("Crafting Table Interacted");
        }
    }
}