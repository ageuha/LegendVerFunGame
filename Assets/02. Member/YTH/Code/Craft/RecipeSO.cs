using _02._Member.YTH.Code.Item;
using UnityEngine;

namespace _02._Member.YTH.Code.Craft
{    
    [CreateAssetMenu(fileName = "RecipeSO", menuName = "SO/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        [field:SerializeField] public ItemInstance Result { get; private set; }
        [field:SerializeField] public ItemSO[] Ingredients { get; private set; }

        public bool CheckCanCraft(ItemSO[] itemSOs)
        {
            if(itemSOs.Length != Ingredients.Length) return false;

            for (int i = 0; i < Ingredients.Length; i++)
            {   
                if (Ingredients[i] == null && itemSOs[i] != null) return false;

                if (Ingredients[i] == null) continue;

                if (!Ingredients[i].Equals(itemSOs[i])) return false;

            }

            return true;
        }
    }
}
