using UnityEngine;

namespace YTH.Item
{    
    [CreateAssetMenu(fileName = "ItemSO", menuName = "SO/Item")]
    public class ItemSO : ScriptableObject
    {
        [field:SerializeField] public int ItemID { get; private set; }
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public Sprite ItemIcon { get; private set; }
        [field:SerializeField,Range(1,99)] public int MaxStackAmount { get; private set; }

        private void OnValidate()
        {
            if (ItemID < 1)
            {
                ItemID = 1;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj is not ItemSO other) return false;

            return this.ItemID == other.ItemID;
        }

        public override int GetHashCode()
        {
            return ItemID;
        }

        public override string ToString()
        {
            return ItemName;
        }
    }
}
