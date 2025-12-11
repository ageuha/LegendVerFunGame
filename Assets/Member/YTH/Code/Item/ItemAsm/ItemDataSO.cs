<<<<<<< HEAD:Assets/Member/YTH/Code/Item/ItemDataSO.cs
using System.Text;
using Member.KJW.Code.Data;
=======
using Member.KJW.Code.CombatSystem.DamageSystem;
>>>>>>> LYD:Assets/Member/YTH/Code/Item/ItemAsm/ItemDataSO.cs
using UnityEngine;
using YTH.Code.Core.Enum;

namespace Member.YTH.Code.Item
{
    public class ItemDataSO : ScriptableObject
    {
        //Member.KJW.Code.Data 참조
        [field:Header("Item Settings")]
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField] public string Description { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public int ItemID { get; private set; }
        [field:SerializeField] public int MaxStack { get; private set; }
        [field:SerializeField] public ItemType ItemType { get; private set; }

        [field:Header("Throw Settings")]
        [field:SerializeField] public float ThrowSpeed { get; private set; }
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
