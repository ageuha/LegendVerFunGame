using System.Collections.Generic;
using Code.Core.Utility;
using Member.YTH.Code.Item;
using YTH.Code.Inventory;

namespace YTH.Code.Craft
{
    public class CraftingSystem
    {
        private const int gridSize = 9;
        private InventoryItem[] m_ItemArray;

        public CraftingSystem()
        {
            m_ItemArray = new InventoryItem[gridSize];
        }

        public bool IsEmpty(int index)
        {
            return m_ItemArray[index] == null;
        }
        
        public InventoryItem GetItem(int index)
        {
            return m_ItemArray[index];
        }

        public void SetItem(InventoryItem item, int index)
        {
            m_ItemArray[index] = item;
        }


        private void IncreaseItemStack(int index)
        {
            if (IsEmpty(index)) return;
            m_ItemArray[index].AddStack();
        }

        private void DecreaseItemStack(int index)
        {
            if (IsEmpty(index)) return;
            m_ItemArray[index].RemoveStack();
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
                if (item.Item == GetItem(index).Item)
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

        public bool TryRemove(InventoryItem item, int index)
        {
             if (IsEmpty(index))
            {
                SetItem(null, index);
                return false;
            }
            else
            {
                if (item.Item == GetItem(index).Item)
                {
                    DecreaseItemStack(index);
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
                InventoryItem currentItem = m_ItemArray[i];


                if (currentItem != null && currentItem.Item != null)
                {
                    if (currentItem.Item == requiredMaterial)
                    {
                        continue;
                    }
                    if (requiredMaterial == null)
                    {
                        return false;
                    }
                }

                if (currentItem == null)
                {
                    if (requiredMaterial != null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool TryMake(RecipeSO currentRecipe)
        {
            if(CanMake(currentRecipe))
            {
                for (int i = 0; i < currentRecipe.Materials.Length; i++)
                {
                    if(m_ItemArray[i] != null && currentRecipe.Materials[i] != null)
                    {    
                        if(!TryRemove(m_ItemArray[i], i))
                        {
                            Logging.LogWarning("제작을 할 수 없습니다.");
                            return false;
                        }
                    }
                }

                Logging.Log("제작을 했습니다.");
                return true;
            }
            else
            {
                Logging.LogWarning("제작을 할 수 없습니다.");
                return false;
            }
        }


    }
}
