using System.Collections.Generic;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.SaveSystem;
using Member.KJW.Code.Input;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.PlayerLoop;
using YTH.Code.Inventorys;

namespace YTH.Code.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {
        [field:SerializeField] public InventoryItem HoldItem { get; private set; }
        public List<InventorySlot> inventorySlots;
        public InventoryTotemSlot totemSlot;
        [SerializeField] private GameObject mainInventory;
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private InventoryChangeEventChannel inventoryChangeEventChannel;
        [SerializeField] private InventorySelectedSlotChangeEventChannel inventorySelectedSlotChangeEventChannel;
        [SerializeField] private InputReader inputReader;

        private int m_SelectedSlot = 1;
        private bool m_Open = true;
        private JsonSaveManager<InventoryData> m_InventoryJsonSaveManager;
    

        private void Awake()
        {
            inventoryAddEventChannel.OnEvent += AddItem;
            inventoryItemPickUpEventChannel.OnEvent += PickUp;
            inventoryItemPickDownEventChannel.OnEvent += PickDown;
            inventoryChangeEventChannel.OnEvent += InventorySave;
            inputReader.OnNumKeyPressed += ChangeSelectedSlot;
            inputReader.OnInventory += MainInventory;
            inputReader.OnScrolled += ChangeSelectedSlotScroll;
        }

        private void Start()
        {
            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
            }
            
            totemSlot.Initialize(this);

            MainInventory();
            ChangeSelectedSlot(1);
        
            inventoryManagerEventChannel.Raise(this);   

            m_InventoryJsonSaveManager = new("Inventory.json");
            
            InventoryLoad();
        }

        private void OnDestroy()
        {
            inventoryAddEventChannel.OnEvent -= AddItem;
            inventoryItemPickUpEventChannel.OnEvent -= PickUp;
            inventoryItemPickDownEventChannel.OnEvent -= PickDown;
            inventoryChangeEventChannel.OnEvent -= InventorySave;
            inputReader.OnNumKeyPressed -= ChangeSelectedSlot;
            inputReader.OnInventory -= MainInventory;
            inputReader.OnScrolled -= ChangeSelectedSlotScroll;
        }

        private void MainInventory()
        {
            m_Open = !m_Open;
            mainInventory.SetActive(m_Open);
        }

        private void ChangeSelectedSlotScroll(float value)
        {
            if(value == 0) return;

            if(m_SelectedSlot + value <= 0)
            {
                m_SelectedSlot = 9;
            }
            else if (m_SelectedSlot + value >= 10)
            {
                m_SelectedSlot = 1;
            }
            else
            {
                m_SelectedSlot += (int)value;
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

        private void InventoryLoad()
        {
            InventoryData inventoryData = m_InventoryJsonSaveManager.LoadSaveData();

            if(inventoryData == null)
            {
                Logging.LogWarning("Null임");
                return;
            }
            
            for(int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventoryData.InventoryItems[i] != null)
                {
                    SpawnNewItem(inventoryData.InventoryItems[i].Item, inventorySlots[i], inventoryData.InventoryItems[i].Count);
                }
            }
        }

        private void InventorySave(Empty empty)
        {
            InventoryData inventoryData = new(GetInventoryItems(), totemSlot.GetComponentInChildren<InventoryItem>());
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

        public void AddItem(ItemDataSO item)
        {
            InventorySlot firstEmptySlot = null;

            for (int i = 0; i < inventorySlots.Count; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.InventoryItem;

                if (itemInSlot != null)
                {
                    if (itemInSlot.Item == item && itemInSlot.Count < item.MaxStack)
                    {
                        itemInSlot.AddStack();
                        return;
                    }
                }
                else if (firstEmptySlot == null)
                {
                    firstEmptySlot = slot;
                }
            }

            if (firstEmptySlot != null)
            {
                SpawnNewItem(item, firstEmptySlot);
                return;
            }

            inventoryChangeEventChannel.Raise(new Empty());
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

        private List<InventoryItem> GetInventoryItems()
        {
            List<InventoryItem> inventoryItems = new();
            foreach (var slot in inventorySlots)
            {
                inventoryItems.Add(slot.InventoryItem);
            }
            return inventoryItems;
        }
    }
}
