using System.Collections.Generic;
using YTH.Item;
using UnityEngine;

namespace YTH.Craft
{    
    public class Crafting : MonoBehaviour
    {
        [field:SerializeField] public RecipeListSO recipeListSO { get; private set; }
        [field:SerializeField] public ItemSO[] CraftingTime { get; private set; }
        
        [ContextMenu("tset")]
        public void Test()
        {
            foreach (var Recipe in recipeListSO.RecipeSOList)
            {
                if(Recipe.CheckCanCraft(CraftingTime))
                {
                    Debug.Log($"Can Craft: {Recipe.Result.itemSO.ItemName} {Recipe.Result.amount}개");
                }
                else
                {
                    Debug.Log($"Cannot Craft: {Recipe.Result.itemSO.ItemName}");
                }
            }
        }
    }
}
