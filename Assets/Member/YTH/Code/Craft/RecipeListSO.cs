using System.Collections.Generic;
using UnityEngine;

namespace _02._Member.YTH.Code.Craft
{    
    [CreateAssetMenu(fileName = "RecipeListSO", menuName = "SO/List/Recipe")]
    public class RecipeListSO : ScriptableObject
    {
        [field:SerializeField] public List<RecipeSO> RecipeSOList { get; private set; }
    }
}
