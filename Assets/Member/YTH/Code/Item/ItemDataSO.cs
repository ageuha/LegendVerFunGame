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

        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (other is not ItemDataSO otherItem) return false;

            if (string.IsNullOrEmpty(ItemID) && string.IsNullOrEmpty(otherItem.ItemID)) return false;

            return ItemID == otherItem.ItemID;
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(ItemID) ? base.GetHashCode() : ItemID.GetHashCode();
        }

        public static bool operator ==(ItemDataSO lhs, ItemDataSO rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;

            if (lhs is null || rhs is null) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(ItemDataSO lhs, ItemDataSO rhs) => !(lhs == rhs);


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            ItemID = AssetDatabase.AssetPathToGUID(path);
        }
#endif

    }
}
