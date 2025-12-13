using System.Collections.Generic;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using Code.UI.TooltipSystem;
using Member.KJW.Code.Input;
using Member.YTH.Code.Item;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {
        [field:SerializeField] public InventoryItem HoldItem { get; private set; }
        [field:SerializeField] public bool UIOpen { get; private set; }
        public List<InventorySlot> inventorySlots;
        public InventoryTotemSlot totemSlot;
        [SerializeField] private GameObject mainInventory;
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private CraftingCloseEventChannel craftingCloseEventChannel;
        [SerializeField] private TooltipChannel tooltipEventChannel;
        [SerializeField] private InventorySelectedSlotChangeEventChannel inventorySelectedSlotChangeEventChannel;
        [SerializeField] private InventorySaveEventChannel inventorySaveEventChannel;
        [SerializeField] private InputReader inputReader;
        

        private int m_SelectedSlot = 1;
        private bool m_Open = true;
        private JsonSaveManager<InventoryData> m_InventoryJsonSaveManager;
    

        private void Awake()
        {
            inventoryAddEventChannel.OnEvent += AddItem;
            inventoryItemPickUpEventChannel.OnEvent += PickUp;
            inventoryItemPickDownEventChannel.OnEvent += PickDown;
            inventorySaveEventChannel.OnEvent += InventorySave;
            craftingCloseEventChannel.OnEvent += InventoryLoad;
            inputReader.OnNumKeyPressed += ChangeSelectedSlot;
            inputReader.OnInventory += MainInventory;
            inputReader.OnScrolled += ChangeSelectedSlotScroll;
        }

        private void Start()
        {
            m_InventoryJsonSaveManager = new("Inventory.json");
            inventoryManagerEventChannel.Raise(this);   
            
            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
            }
            
            totemSlot.Initialize(this);

            MainInventory();
            ChangeSelectedSlot(1);
        

            
            InventoryLoad(new Empty());
        }

        private void OnDestroy()
        {
            inventoryAddEventChannel.OnEvent -= AddItem;
            inventoryItemPickUpEventChannel.OnEvent -= PickUp;
            inventoryItemPickDownEventChannel.OnEvent -= PickDown;
            inventorySaveEventChannel.OnEvent -= InventorySave;
            craftingCloseEventChannel.OnEvent -= InventoryLoad;
            inputReader.OnNumKeyPressed -= ChangeSelectedSlot;
            inputReader.OnInventory -= MainInventory;
            inputReader.OnScrolled -= ChangeSelectedSlotScroll;
        }

        public void Open()
        {
            UIOpen = true;
        }

        public void Close()
        {
            UIOpen = false;
        }

        private void MainInventory()
        {
            if (HoldItem != null) return;
            
            m_Open = !m_Open;
            mainInventory.SetActive(m_Open);
            
            if (mainInventory.activeSelf)
            {
                Open();
            }
            else
            {
                Close();
            }

            TooltipContext tooltip = new();
            tooltip.Active = false;

            tooltipEventChannel.Raise(tooltip);
        }

        public void MainInventoryClose()
        {
            m_Open = false;
            mainInventory.SetActive(false);
            Close();

            TooltipContext tooltip = new();
            tooltip.Active = false;

            tooltipEventChannel.Raise(tooltip);
        }

        private void ChangeSelectedSlotScroll(float value)
        {
            if(value == 0) return;

            if(m_SelectedSlot - value <= 0)
            {
                m_SelectedSlot = 9;
            }
            else if (m_SelectedSlot - value >= 10)
            {
                m_SelectedSlot = 1;
            }
            else
            {
                m_SelectedSlot -= (int)value;
            }
            ChangeSelectedSlot(m_SelectedSlot);
        }

        private void ChangeSelectedSlot(int value)
        {
            for (int i = 0; i < 9; i++)
            {
                inventorySlots[i].UnSelect();
            }

            inventorySlots[value - 1].Select();
            m_SelectedSlot = value;
            inventorySelectedSlotChangeEventChannel.Raise(new Empty());
        }

        private void InventoryLoad(Empty empty)
        {
            InventoryData inventoryData = m_InventoryJsonSaveManager.LoadSaveData();

            if(inventoryData == null)
            {
                Logging.LogWarning("인벤토리 데이터 없음");
                return;
            }
            
            for(int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].InventoryItem != null)
                {
                    Logging.Log("인벤토리 로드할 때 아이템 삭제함");
                    PoolManager.Instance.Factory<InventoryItem>().Push(inventorySlots[i].InventoryItem);
                }

                Logging.Log($"{i}번 : {inventoryData.InventoryItems[i]}");
                if (inventoryData.InventoryItems[i].ItemID != 0)
                {
                    Logging.Log("인벤토리 로드할 때 아이템 추가함");
                    ItemDataSO item = GetItemData.Instance.ItemDataListSO[inventoryData.InventoryItems[i].ItemID];
                    SpawnNewItem(item, inventorySlots[i], inventoryData.InventoryItems[i].Count);
                }
            }

            if (totemSlot.InventoryItem != null)
            {
                PoolManager.Instance.Factory<InventoryItem>().Push(totemSlot.InventoryItem);
            }

            if (inventoryData.TotemItem.ItemID != 0)
            {
                Logging.Log("인벤토리 로드할 때 아이템 추가함");
                ItemDataSO item = GetItemData.Instance.ItemDataListSO[inventoryData.TotemItem.ItemID];
                SpawnNewItem(item, totemSlot, inventoryData.TotemItem.Count);
            }

            Logging.Log("인벤토리 로드");
        }

        private void InventorySave(Empty empty)
        {
            InventoryItem inventoryItem = totemSlot.GetComponentInChildren<InventoryItem>();
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

        private void PickUp(InventoryItem item) 
        {
            HoldItem = item;
        }

        private void PickDown(Empty empty)
        {
            HoldItem = null;
        }

        public void AddItem(ItemData item)
        {
            int remain = item.Count;

            for (int i = 0; i < inventorySlots.Count && remain > 0; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.InventoryItem;
                ItemDataSO itemDataSO = GetItemData.Instance.ItemDataListSO[item.ItemID];

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

                ItemDataSO itemDataSO = GetItemData.Instance.ItemDataListSO[item.ItemID];
                Logging.Log(itemDataSO);
                int add = Mathf.Min(itemDataSO.MaxStack, remain);
                SpawnNewItem(itemDataSO, emptySlot, add);
                remain -= add;
            }
        }

        private void SpawnNewItem(ItemDataSO item, InventorySlot slot, int count = 1)
        {
            InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(slot.transform);
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;
            newItem.Initialize(this, item, count);
        }

        public ItemDataSO GetSelectedItem()
        {
            InventorySlot slot = inventorySlots[m_SelectedSlot-1];
            InventoryItem itemInSlot = slot.InventoryItem;
            if (itemInSlot != null)
            {
                return itemInSlot.Item;
            }

            return null;
        }

        public bool UseSelectedItem()
        {
            InventorySlot slot = inventorySlots[m_SelectedSlot-1];
            InventoryItem itemInSlot = slot.InventoryItem;
            if (itemInSlot != null)
            {
                itemInSlot.RemoveStack();
                return true;
            }
            return false;
        }

        private List<ItemData> GetInventoryItems()
        {
            List<ItemData> inventoryItems = new();
            foreach (var slot in inventorySlots)
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

        private InventorySlot FindFirstEmptySlot()
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].InventoryItem == null)
                    return inventorySlots[i];
            }
            return null;
        }

    }
}
