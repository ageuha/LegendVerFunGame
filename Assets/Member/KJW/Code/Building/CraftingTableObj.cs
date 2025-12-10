using Code.Core.GlobalStructs;
using Member.JJW.Code.Interface;
using Member.KJW.Code.EventChannel;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.NewBuildingSystem;
using UnityEngine;

namespace Member.KJW.Code.Building
{
    public class CraftingTableObj : MonoBehaviour, IBuilding, IInteractable
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
        }
    }
}