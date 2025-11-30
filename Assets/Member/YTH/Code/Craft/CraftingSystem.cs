using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Craft
{
    public class CraftingSystem
    {
        private const int gridSize = 9;
        private InventoryItem[] itemArray;

        public CraftingSystem()
        {
            itemArray = new InventoryItem[gridSize];
        }

        public bool IsEmpty(int index)
        {
            return itemArray[index] == null;
        }
        
        public InventoryItem GetItem(int index)
        {
            return itemArray[index];
        }

        public void SetItem(InventoryItem item, int index)
        {
            itemArray[index] = item;
        }


        private void IncreaseItemStack(int index)
        {
            if (IsEmpty(index)) return;
            itemArray[index].AddStack(1);
        }

        private void DecreaseItemStack(int index)
        {
            if (IsEmpty(index)) return;
            itemArray[index].RemoveStack(1);
        }

        public bool TryAddItem(InventoryItem item,int index)
        {
            if (IsEmpty(index))
            {
                SetItem(item, index);
                return true;
            }
            else
            {
                if (item.itemData == GetItem(index).itemData)
                {
                    IncreaseItemStack(index);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CanMake(RecipeSO currentRecipe)
        {
            if (currentRecipe == null) return false;

            for (int i = 0; i < gridSize; i++)
            {
                ItemDataSO requiredMaterial = currentRecipe.Materials[i];
                InventoryItem currentItem = itemArray[i];


                if (requiredMaterial == null)
                {
                    if (currentItem.itemData != null)
                    {
                        return false;
                    }
                }
                
                else
                {
                    if (currentItem == null || currentItem.itemData != requiredMaterial || currentItem.stackSize < 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


    }
}
