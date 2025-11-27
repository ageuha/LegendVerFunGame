using System.Collections.Generic;
using UnityEngine;

namespace YTH.Craft
{    
    [CreateAssetMenu(fileName = "RecipeListSO", menuName = "SO/List/Recipe")]
    public class RecipeListSO : ScriptableObject
    {
        [field:SerializeField] public List<RecipeSO> RecipeSOList { get; private set; }
    }
}
