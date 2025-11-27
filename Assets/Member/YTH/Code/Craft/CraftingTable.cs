using System.Collections.Generic;
using System.Linq;
using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private ItemDataSO[] defaultMaterials = new ItemDataSO[9];
        [SerializeField] private List<RecipeSO> recipeList;

        private CraftingSystem craftingSystem = new();

        [ContextMenu("Test")]
        public void Test()
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                if (defaultMaterials[i] != null)
                {
                    InventoryItem item = new InventoryItem(defaultMaterials[i], 1);
                    craftingSystem.TryAddItem(item, i);
                }
            }

            var craftableRecipes = recipeList.Where(r => craftingSystem.CanMake(r));

            foreach (var r in craftableRecipes)
            {
                Logging.Log($"만들 수 있는 레시피: {r.Result.ItemName}");
                return;
            }
            
            Logging.LogWarning("만들 수 있는 제작법이 없습니다.");

        }   
    }
}
