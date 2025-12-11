using System;
using System.Collections.Generic;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using UnityEngine;
using YTH.Code.Inventory;
using YTH.Code.Inventorys;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private InventoryChangeEventChannel inventoryChangeEventChannel;
        [SerializeField] private List<InventorySlot> Materials = new List<InventorySlot>(gridSize);
        [SerializeField] private List<InventorySlot> Slots;
        [SerializeField] private InventorySlot resultSlot;
        [SerializeField] private List<RecipeSO> recipeList;

        private const int gridSize = 9;
        private CraftingSystem m_CraftingSystem = new();
        private InventoryManager m_InventoryManager;
        private JsonSaveManager<InventoryData> m_InventoryJsonSaveManager;

        private void Awake()
        {
            inventoryChangeEventChannel.OnEvent += UpdateInventory;
            inventoryManagerEventChannel.OnEvent += Initialize;
        }

        private void Start()
        {
            m_InventoryJsonSaveManager = new("Inventory.json");
        }

        private void OnDestroy()
        {
            inventoryChangeEventChannel.OnEvent -= UpdateInventory;
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

        public void OpenCrafting()
        {
            
        }
        
        public void UpdateInventory(Empty empty)
        {
            InventoryData inventoryData = m_InventoryJsonSaveManager.LoadSaveData();

            if (inventoryData == null)
            {
                Logging.Log("널");
                return;
            }

            for (int i = 0; i < Slots.Count; i++)
            {
                if (inventoryData.InventoryItems[i] != null)
                {
                    Logging.Log("UI");
                    InventoryItem newItem = PoolManager.Instance.Factory<InventoryItem>().Pop(Slots[i].transform);
                    newItem.transform.localScale = Vector3.one;
                    newItem.transform.localPosition = Vector3.zero;
                    newItem.Initialize(m_InventoryManager, inventoryData.InventoryItems[i].Item, inventoryData.InventoryItems[i].Count);
                }
            }
        }

        [ContextMenu("Info")]
        public void Info()
        {
            for (int i = 0; i < gridSize; i++)
            {
                var item = Materials[i].InventoryItem;
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
                m_CraftingSystem.SetItem(Materials[i].InventoryItem, i);
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
