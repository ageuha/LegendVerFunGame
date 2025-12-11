using UnityEditor;
using UnityEngine;
using YTH.Code.Item;

namespace YTH.Code.Craft
{    
    [CreateAssetMenu(fileName = "RecipeSO", menuName = "SO/Recipe")]
    public class RecipeSO : ScriptableObject
    {
        [field:SerializeField] public ItemDataSO[] Materials { get; private set; }
        [field:SerializeField] public ItemDataSO Result { get; private set; }
        [field:SerializeField] public string RecipeID { get; private set; }


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            RecipeID = AssetDatabase.AssetPathToGUID(path);
        }
#endif
    }
}
