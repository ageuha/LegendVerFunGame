using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Inventory;
using YTH.Code.Item;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> defaultMaterials = new List<InventorySlot>(gridSize);
        [SerializeField] private List<RecipeSO> recipeList;

        private const int gridSize = 9;



        private CraftingSystem craftingSystem = new();

        [ContextMenu("Info")]
        public void Info()
        {
            for (int i = 0; i < gridSize; i++)
            {
                craftingSystem.SetItem(defaultMaterials[i].GetInventoryItem(), i);
            }

            foreach (var r in recipeList)
            {
                if(craftingSystem.CanMake(r))
                {
                    Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                    return;
                }
            }   
        }

        [ContextMenu("Craft")]
        public void Craft()
        {
            for (int i = 0; i < gridSize; i++)
            {
                craftingSystem.SetItem(defaultMaterials[i].GetInventoryItem(), i);
            }

            foreach (var r in recipeList)
            {
                if(craftingSystem.TryMake(r))
                {
                    Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                    return;
                }
            }   
            
            Logging.LogWarning("만들 수 있는 제작법이 없습니다.");

        }   
    }
}
