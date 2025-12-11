using System;
using System.Collections.Generic;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using JetBrains.Annotations;
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
        [SerializeField] private List<CraftingMaterialSlot> craftingMaterialSlots = new List<CraftingMaterialSlot>(gridSize);
        [SerializeField] private List<InventorySlot> slots;
        [SerializeField] private CraftingResultSlot resultSlot;
        [SerializeField] private List<RecipeSO> recipeList;

        private const int gridSize = 9;
        private InventoryManager m_InventoryManager;
        private JsonSaveManager<InventoryData> m_InventoryJsonSaveManager;
        private int m_ItemCount = 0;
        private RecipeSO m_Recipe;

        private void Awake()
        {
            inventoryAddEventChannel.OnEvent += AddItem;
            inventoryManagerEventChannel.OnEvent += Initialize;
            craftingTableMateiralChangeEventChannel.OnEvent += Info;
            //craftEventChannel.OnEvent += Craft;
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
            //craftEventChannel.OnEvent -= Craft;
        }

        
        private void Initialize(InventoryManager inventoryManager)
        {
            this.m_InventoryManager = inventoryManager;

            foreach (var slot in slots)
            {
                slot.Initialize(m_InventoryManager);
            }

            foreach (var slot in craftingMaterialSlots)
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

            for (int i = 0; i < slots.Count && remain > 0; i++)
            {
                InventorySlot slot = slots[i];
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
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].InventoryItem == null)
                    return slots[i];
            }
            return null;
        }

        public void Info(Empty empty)
        {
            foreach (var r in recipeList)
            {
                m_ItemCount = 0; 
                if (CanMake(r))
                {
                    if (resultSlot.InventoryItem != null)
                    {
                        Logging.Log(m_ItemCount);
                        resultSlot.InventoryItem.SetCount(m_ItemCount);
                    }
                    else
                    {    
                        InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(resultSlot.transform);
                        newItem.Initialize(m_InventoryManager, r.Result, m_ItemCount);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;
                    }
                    Logging.Log($"만들 수 있는 레시피 : {r.Result.ItemName}");
                    m_Recipe = r;
                    return;
                }
                else
                {
                    Logging.Log($"만들 수 있는 레시피 : 없음");
                    m_Recipe = null;
                    if (resultSlot.InventoryItem != null)
                    {
                        PoolManager.Instance.Factory<InventoryItem>().Push(resultSlot.InventoryItem);
                    }
                }
            }   
        }

        public bool CanMake(RecipeSO currentRecipe)
        {
            if (currentRecipe == null) return false;
            if (m_ItemCount > 0) m_ItemCount = 0;

            for (int i = 0; i < gridSize; i++)
            {
                ItemDataSO requiredMaterial = currentRecipe.Materials[i];
                InventoryItem item = craftingMaterialSlots[i].InventoryItem;
                

                if (item == null)
                {
                    Logging.Log("Null 값임");
                    if (requiredMaterial == null) continue;
                    else
                    {
                        Logging.Log("못 만듬");
                        return false;
                    }
                }
                else
                {
                    if (requiredMaterial != item.Item) return false;
                    m_ItemCount = Math.Max(m_ItemCount, item.Count);
                }
            }

            Logging.Log($"{currentRecipe.Result} {m_ItemCount}개");
            return true;
        }

        public void TryMake()
        {
            if(CanMake(m_Recipe))
            {
                for (int i = 0; i < m_Recipe.Materials.Length; i++)
                {
                    if(craftingMaterialSlots[i].InventoryItem != null) continue;
                    craftingMaterialSlots[i].InventoryItem.RemoveStack(m_ItemCount);
                }
                Logging.Log("제작을 했습니다.");
            }
            else
            {
                Logging.LogWarning("제작을 할 수 없습니다.");
            }
        }

        public void Craft(Empty empty)
        {
            TryMake();
        }   
    }
}
