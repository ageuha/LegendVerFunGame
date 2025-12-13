using System;
using System.Collections.Generic;
using System.Linq;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using Member.KJW.Code.EventChannel;
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
        [SerializeField] private InventorySaveEventChannel inventorySaveEventChannel;
        [SerializeField] private CraftingInteractEventChannel craftingInteractEventChannel;
        [SerializeField] private CraftingCloseEventChannel craftingCloseEventChannel;
        [SerializeField] private List<CraftingMaterialSlot> craftingMaterialSlots = new List<CraftingMaterialSlot>(gridSize);
        [SerializeField] private List<InventorySlot> slots;
        [SerializeField] private CraftingResultSlot resultSlot;
        [SerializeField] private List<RecipeSO> recipeList;
        [SerializeField] private GameObject craftingTable;

        private const int gridSize = 9;
        private InventoryManager m_InventoryManager;
        private JsonSaveManager<InventoryData> m_InventoryJsonSaveManager;
        private int m_ItemCount = 0;
        private RecipeSO m_Recipe;

        private void Awake()
        {
            inventoryManagerEventChannel.OnEvent += Initialize;
            craftingTableMateiralChangeEventChannel.OnEvent += Info;
            craftingInteractEventChannel.OnEvent += OpenCraftingTable;
            craftEventChannel.OnEvent += TryMake;
        }

        private void Start()
        {
            m_InventoryJsonSaveManager = new("Inventory.json");
        }

        private void OnDestroy()
        {
            inventoryManagerEventChannel.OnEvent -= Initialize;
            craftingTableMateiralChangeEventChannel.OnEvent -= Info;
            craftingInteractEventChannel.OnEvent -= OpenCraftingTable;
            craftEventChannel.OnEvent -= TryMake;
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

        public void OpenCraftingTable(Empty empty)
        {
            inventorySaveEventChannel.Raise(new Empty());
            CraftingInventoryLoad();
            craftingTable.SetActive(true);
            m_InventoryManager.Open();
        }

        public void CloseCraftingTable()
        {
            foreach (var slot in craftingMaterialSlots)
            {
                if (slot.InventoryItem != null)
                {    
                    AddItem(new ItemData(slot.InventoryItem.Item.ItemID, slot.InventoryItem.Count));
                    PoolManager.Instance.Factory<InventoryItem>().Push(slot.InventoryItem);
                }
            }

            CraftingInventorySave();
            craftingTable.SetActive(false);
            craftingCloseEventChannel.Raise(new Empty());
            m_InventoryManager.MainInventoryClose();
        }

        public void AddItem(ItemData item)
        {
            int remain = item.Count;

            for (int i = 0; i < slots.Count && remain > 0; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem itemInSlot = slot.InventoryItem;
                ItemDataSO itemDataSO = GetItemDataSO.Instance.ItemDataListSO[item.ItemID];

                if (itemInSlot != null && itemInSlot.Item == itemDataSO && itemInSlot.Count < itemDataSO.MaxStack)
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

                ItemDataSO itemDataSO = GetItemDataSO.Instance.ItemDataListSO[item.ItemID];

                int add = Mathf.Min(itemDataSO.MaxStack, remain);
                SpawnNewItem(itemDataSO, emptySlot, add);
                remain -= add;
            }
        }
        
        private void CraftingInventoryLoad()
        {
            InventoryData inventoryData = m_InventoryJsonSaveManager.LoadSaveData();

            if(inventoryData == null)
            {
                Logging.LogWarning("인벤토리 로드 실패함");
                return;
            }
            
            for(int i = 0; i < slots.Count; i++)
            {
                if (slots[i].InventoryItem != null)
                {
                    Logging.Log("인벤토리 로드할 때 아이템 삭제함");
                    PoolManager.Instance.Factory<InventoryItem>().Push(slots[i].InventoryItem);
                }

                if (inventoryData.InventoryItems[i].ItemID != 0)
                {
                    Logging.Log("인벤토리 로드할 때 아이템 추가함");
                    ItemDataSO item = GetItemDataSO.Instance.ItemDataListSO[inventoryData.InventoryItems[i].ItemID];
                    SpawnNewItem(item, slots[i], inventoryData.InventoryItems[i].Count);
                }
            }
            Logging.Log("인벤토리 로드");
        }

        private void CraftingInventorySave()
        {
            InventoryItem inventoryItem = m_InventoryManager.totemSlot.GetComponentInChildren<InventoryItem>();
            InventoryData inventoryData;
            if (inventoryItem == null)
            {
                inventoryData = new(GetInventoryItems(), new ItemData(0, 0));
            }
            else
            {
                inventoryData = new(GetInventoryItems(), new ItemData(inventoryItem.Item.ItemID, inventoryItem.Count));
            }
            m_InventoryJsonSaveManager.SaveToFile(inventoryData);
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
            List<int> list = new();

            for (int i = 0; i < gridSize; i++)
            {
                ItemDataSO requiredMaterial = currentRecipe.Materials[i];
                InventoryItem item = craftingMaterialSlots[i].InventoryItem;
                m_ItemCount = 1;

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
                    if (item.Count > 0)
                    {
                        list.Add(item.Count);
                    }
                }

                m_ItemCount = list.Min();
            }

            Logging.Log($"{currentRecipe.Result} {m_ItemCount}개");
            return true;
        }

        public void TryMake(Empty empty)
        {
            if(CanMake(m_Recipe))
            {
                for (int i = 0; i < m_Recipe.Materials.Length; i++)
                {
                    if (craftingMaterialSlots[i].InventoryItem == null) continue;
                    craftingMaterialSlots[i].InventoryItem.RemoveStack(m_ItemCount);
                }
                Logging.Log("제작을 했습니다.");
            }
            else
            {
                Logging.LogWarning("제작을 할 수 없습니다.");
            }
        }

        private List<ItemData> GetInventoryItems()
        {
            List<ItemData> inventoryItems = new();
            foreach (var slot in slots)
            {
                if(slot.InventoryItem == null)
                {
                    inventoryItems.Add(new ItemData(0, 0));
                }
                else
                {                
                    inventoryItems.Add(new ItemData(slot.InventoryItem.Item.ItemID, slot.InventoryItem.Count));
                }
            }
            return inventoryItems;
        }
    }
}
