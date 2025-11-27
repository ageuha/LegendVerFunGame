using Code.Core.Utility;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Craft
{    
    public class CraftingTable : MonoBehaviour
    {
        [SerializeField] private ItemDataSO[] defaultMaterials = new ItemDataSO[9];
        [SerializeField] private RecipeSO defaultRecipe;

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

            Logging.Log($"{craftingSystem.CanMake(defaultRecipe)}");
        }
    }
}
