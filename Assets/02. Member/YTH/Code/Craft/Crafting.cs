using System.Collections.Generic;
using UnityEngine;
using YTH.Item;

namespace YTH_Craft
{    
    public class Crafting : MonoBehaviour
    {
        [field:SerializeField] public RecipeSO Recipe { get; private set; }
        [field:SerializeField] public ItemSO[] CraftingTime { get; private set; }
        
        [ContextMenu("tset")]
        public void Test()
        {
            Debug.Log(Recipe.CheckCanCraft(CraftingTime));
        }
    }
}
