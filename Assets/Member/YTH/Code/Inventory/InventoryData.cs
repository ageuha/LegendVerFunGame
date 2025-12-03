using System.Collections.Generic;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{    
    public abstract class InventoryData : MonoBehaviour
    {
        [SerializeField] protected int inventorySize = 10;
        [SerializeField] protected List<InventoryItem> inventory;
        public IReadOnlyList<InventoryItem> Inventory => inventory;

        protected virtual void Awake()
        {
            inventory = new List<InventoryItem>(inventorySize);

            for (int i = 0; i < inventorySize; i++)
            {
                inventory.Add(null);
            }
        }

        public virtual InventoryItem GetItem(ItemDataSO itemData)
        {
            if (itemData == null || inventory == null) return null;

            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.itemData == itemData) return item;
            }

            return null;
        }

        public virtual IEnumerable<InventoryItem> GetItems(ItemDataSO itemData)
        {
            if (itemData == null || inventory == null) yield break;

            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                if (item == null) continue;
                if (item.itemData == itemData)yield return item;
            }
        }

        protected virtual InventoryItem GetEmptySlot()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == null) return inventory[i];
                if (inventory[i].IsEmpty) return inventory[i];
            }

            return null;
        }

        public abstract void AddItem(ItemDataSO itemData, int amount = 1);
        public abstract void RemoveItem(ItemDataSO itemData, int amount);
        public abstract bool CanAddItem(ItemDataSO itemData, int amount);
        public abstract int GetRemainSlotCount();
    }
}
