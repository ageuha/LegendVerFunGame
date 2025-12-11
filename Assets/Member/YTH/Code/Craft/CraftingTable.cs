using System;
using System.Collections.Generic;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using Member.YTH.Code.Item;
using UnityEngine;
using YTH.Code.Inventory;
using YTH.Code.Item;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private CraftEventChannel craftEventChannel;
        [SerializeField] private CraftingTableMateiralChangeEventChannel craftingTableMateiralChangeEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
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
            inventoryAddEventChannel.OnEvent += AddItem;
            inventoryManagerEventChannel.OnEvent += Initialize;
            craftingTableMateiralChangeEventChannel.OnEvent += Info;
            craftEventChannel.OnEvent += Craft;
        }

        private void Start()
        {
            m_InventoryJsonSaveManager = new("Inventory.json");
        }

        private void OnDestroy()
        {
            inventoryAddEventChannel.OnEvent -= AddItem;
            inventoryManagerEventChannel.OnEvent -= Initialize;
            craftingTableMateiralChangeEventChannel.OnEvent -= Info;
            craftEventChannel.OnEvent -= Craft;
        }

        
        private void Initialize(InventoryManager inventoryManager)
        {
            this.m_InventoryManager = inventoryManager;

            foreach (var slot in Slots)
            {
                slot.Initialize(m_InventoryManager);
            }

            foreach (var slot in Materials)
            {
                slot.Initialize(m_InventoryManager);
            }
            
            resultSlot.Initialize(m_InventoryManager);
        }

        public void OpenCrafting()
        {
            
        }
        
        public void AddItem(ItemData item)
        {
            int remain = item.Count;

            for (int i = 0; i < Slots.Count && remain > 0; i++)
            {
                InventorySlot slot = Slots[i];
                InventoryItem itemInSlot = slot.InventoryItem;

                if (itemInSlot != null && itemInSlot.Item == item.Item && itemInSlot.Count < item.Item.MaxStack)
                {
                    remain = itemInSlot.AddStack(remain);
                }
            }

            while (remain > 0)
            {
                InventorySlot emptySlot = FindFirstEmptySlot();
                if (emptySlot == null)
                {
                    return;
                }

                int add = Mathf.Min(item.Item.MaxStack, remain);
                SpawnNewItem(item.Item, emptySlot, add);
                remain -= add;
            }
        }

        private void SpawnNewItem(ItemDataSO item, InventorySlot slot, int count = 1)
        {
            InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(slot.transform);
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;
            newItem.Initialize(m_InventoryManager, item, count);
        }

        private InventorySlot FindFirstEmptySlot()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].InventoryItem == null)
                    return Slots[i];
            }
            return null;
        }

        public void Info(Empty empty)
        {
            for (int i = 0; i < gridSize; i++)
            {
                var item = Materials[i].InventoryItem;
                if(item != null)
                {
                    m_CraftingSystem.SetItem(item, i);
                }
            }

            foreach (var r in recipeList)
            {
                if(m_CraftingSystem.CanMake(r))
                {
                    InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(resultSlot.transform);
                    newItem.Initialize(m_InventoryManager, r.Result, 1);
                    newItem.transform.localScale = Vector3.one;
                    newItem.transform.localPosition = Vector3.zero;
                    Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                    return;
                }
            }   
        }

        public void Craft(Empty empty)
        {
            for (int i = 0; i < gridSize; i++)
            {
                m_CraftingSystem.SetItem(Materials[i].InventoryItem, i);
            }

            foreach (var r in recipeList)
            {
                if(m_CraftingSystem.TryMake(r))
                {
                    PoolManager.Instance.Factory<InventoryItem>().Push(resultSlot.InventoryItem);
                    return;
                }
            }  
        }   
    }
}
