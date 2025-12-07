using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private InventoryRemoveEventChannel inventoryRemoveEventChannel;
        [SerializeField] private InventoryChangeEventChannel inventoryChangeEventChannel;
        private Dictionary<ItemDataSO,InventoryItem> m_itemDictionary;
        public InventoryItem[,] Inventory { get; private set; }

        private Vector2Int firstEmptyIndex = Vector2Int.zero;
        private Vector2Int m_size;
        
        private void Awake()
        {
            Inventory = new InventoryItem[4,9];
            m_size = new Vector2Int(4,9);
            m_itemDictionary = new Dictionary<ItemDataSO, InventoryItem>();
            inventoryAddEventChannel.OnEvent += Add;
            inventoryRemoveEventChannel.OnEvent += Remove;
        }

        public InventoryItem Get(ItemDataSO itemDataSO)
        {
            if (m_itemDictionary.TryGetValue(itemDataSO, out InventoryItem inventoryItem))
            {
                return inventoryItem;
            }

            return null;
        }

        public void Add(ItemDataSO itemDataSO)
        {
            if (m_itemDictionary.TryGetValue(itemDataSO, out InventoryItem inventoryItem))
            {
                inventoryItem.AddStack(1);
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemDataSO, 1, firstEmptyIndex);
                m_itemDictionary.Add(itemDataSO,newItem);
                Inventory[firstEmptyIndex.x, firstEmptyIndex.y] = newItem; 
            }
            UpdateEmptyIndex();
        }

        public void Remove(ItemDataSO itemDataSO)
        {
            if (m_itemDictionary.TryGetValue(itemDataSO, out InventoryItem inventoryItem))
            {
                inventoryItem.RemoveStack();

                if (inventoryItem.Count == 0)
                {
                    Inventory[inventoryItem.inventoryIndex.x, inventoryItem.inventoryIndex.y] = null;
                    m_itemDictionary.Remove(itemDataSO);
                }
            }
            UpdateEmptyIndex();
        }

        private void UpdateEmptyIndex()
        {
            inventoryChangeEventChannel.Raise(Inventory);
            
            for (int i = 0; i < m_size.x; i++)
            {
                for (int j = 0; j < m_size.y; j++)
                {
                    if(Inventory[i,j] == null)
                    {    
                        firstEmptyIndex.x = i;
                        firstEmptyIndex.y = j;
                        return;
                    }
                }
            }
        }

        [ContextMenu("Inventory")]
        private void Inventory1()
        {
            for (int i = 0; i < m_size.x; i++)
            {
                for (int j = 0; j < m_size.y; j++)
                {
                    if(Inventory[i,j] == null) continue;
                    Logging.Log($"[{i},{j}] {Inventory[i,j].itemData} {Inventory[i,j].Count}개");
                }
            }
        }



    }
}
