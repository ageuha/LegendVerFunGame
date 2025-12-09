using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Inventory;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private List<InventorySlot> defaultMaterials = new List<InventorySlot>(gridSize);
        [SerializeField] private List<InventorySlot> Slots;
        [SerializeField] private List<RecipeSO> recipeList;

        private const int gridSize = 9;
        private CraftingSystem m_CraftingSystem = new();
        private InventoryManager m_InventoryManager;

        private void Awake()
        {
            inventoryManagerEventChannel.OnEvent += Initialize;
        }

        private void OnDestroy()
        {
            inventoryManagerEventChannel.OnEvent -= Initialize;
        }

        
        private void Initialize(InventoryManager inventoryManager)
        {
            this.m_InventoryManager = inventoryManager;

            foreach (var slot in Slots)
            {
                slot.Initialize(m_InventoryManager);
            }
        }

        [ContextMenu("Info")]
        public void Info()
        {
            for (int i = 0; i < gridSize; i++)
            {
                var item = defaultMaterials[i].GetInventoryItem();
                if(item != null)
                {
                    Logging.Log(i);
                    m_CraftingSystem.SetItem(item, i);
                }
            }

            foreach (var r in recipeList)
            {
                if(m_CraftingSystem.CanMake(r))
                {
                    Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                    return;
                }
            }   
        }

        [ContextMenu("Craft")]
        public void Craft()
        {
            for (int i = 0; i < gridSize; i++)
            {
                m_CraftingSystem.SetItem(defaultMaterials[i].GetInventoryItem(), i);
            }

            foreach (var r in recipeList)
            {
                if(m_CraftingSystem.TryMake(r))
                {
                    Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                    return;
                }
            }   
            
            Logging.LogWarning("만들 수 있는 제작법이 없습니다.");

        }   
    }
}
