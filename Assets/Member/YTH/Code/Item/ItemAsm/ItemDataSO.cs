using Member.KJW.Code.CombatSystem.DamageSystem;
using UnityEngine;
using YTH.Code.Enum;

namespace Member.YTH.Code.Item
{
    public class ItemDataSO : ScriptableObject
    {
        [field:Header("Item Settings")]
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public string Description { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public int ItemID { get; private set; }
        [field:SerializeField] public int MaxStack { get; private set; }
        [field:SerializeField] public ItemType ItemType { get; private set; }

        [field: Header("Throw Settings")]
        [field: SerializeField]
        public float ThrowSpeed { get; private set; } = 5;

        [field: SerializeField] public float ThrowLifeTime { get; private set; } = 1;
        [field: SerializeField] public float ThrowRotationSpeed { get; private set; } = 1;
        [field: SerializeField] public Vector2 HitBoxSize { get; private set; } = Vector2.one;
        [field:SerializeField] public DamageInfoData DamageInfoData { get; private set; }


        public virtual string GetDescription() => Description;
        public override string ToString() => ItemName;
        public override int GetHashCode() => ItemID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not ItemDataSO other) return false;

            return ItemID == other.ItemID;
        }

        public static bool operator ==(ItemDataSO lhs, ItemDataSO rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            return lhs.ItemID == rhs.ItemID;
        }

        public static bool operator !=(ItemDataSO lhs, ItemDataSO rhs)
        {
            return !(lhs == rhs);
        }
    }
}
