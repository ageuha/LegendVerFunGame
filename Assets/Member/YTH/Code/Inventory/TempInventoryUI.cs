using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public class TempInventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryChangeEventChannel inventoryChangeEventChannel;
        [SerializeField] private TempInventorySlot prefab;
        private TempInventorySlot[,] m_tempInventorySlots;
        private Vector2Int m_size;

        private void Awake()
        {
            m_tempInventorySlots = new TempInventorySlot[4,9];
            m_size = new Vector2Int(4,9);
            inventoryChangeEventChannel.OnEvent += UpdateInventory; 

            for (int i = 0; i < m_size.x; i++)
            {
                for (int j = 0; j < m_size.y; j++)
                {
                    m_tempInventorySlots[i,j] = Instantiate(prefab, transform); // 나중에 풀링 알아서 하시고
                }

            }
        }


        private void UpdateInventory(InventoryItem[,] inventory) 
        {
            for (int i = 0; i < m_size.x; i++)
            {
                for (int j = 0; j < m_size.y; j++)
                {
                    if (inventory[i,j] == null) continue;
                    if (inventory[i,j].itemData == null ) continue;
                    
                    m_tempInventorySlots[i,j].SetItemData(inventory[i,j].itemData, inventory[i,j].Count);
                    m_tempInventorySlots[i,j].UpdateUI();
                }
            }
        }
    }
}
