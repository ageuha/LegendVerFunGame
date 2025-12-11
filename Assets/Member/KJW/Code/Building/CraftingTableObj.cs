using Code.Core.GlobalStructs;
using Code.Core.Utility;
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
        public SpriteRenderer SpriteRenderer { get; private set; }
        
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = BuildingData.Image;
        }

        public void Interaction(InteractionContext context)
        {
            craftingInteractEventChannel.Raise(new Empty());
            Logging.Log("Crafting Table Interacted");
        }
    }
}