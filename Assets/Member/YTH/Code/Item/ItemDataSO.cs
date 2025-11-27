using System.Text;
using UnityEditor;
using UnityEngine;

namespace YTH.Code.Item
{    
    public class ItemDataSO : ScriptableObject
    {
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public string ItemID { get; private set; }
        [field:SerializeField] public int MaxStack { get; private set; }

        protected StringBuilder _stringBuilder = new StringBuilder();

        public virtual string GetDescription() => string.Empty;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            ItemID = AssetDatabase.AssetPathToGUID(path);
        }
#endif

    }
}
