using KJW.Code.Player;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace YTH.Code.Stat
{    
    public abstract class StatBaseSO : ScriptableObject
    {
        [field:SerializeField] public string StatID { get; private set; }
        public abstract void Effect(int amount, Player player);

        protected abstract string StatName { get; }

        public override string ToString()
        {
            return StatName;
        }


        public override bool Equals(object other)
        {
            if (ReferenceEquals(this, other)) return true;

            if (other is not StatBaseSO otherStat) return false;

            if (string.IsNullOrEmpty(StatID) && string.IsNullOrEmpty(otherStat.StatID)) return false;

            return StatID == otherStat.StatID;
        }

        public override int GetHashCode()
        {
            return string.IsNullOrEmpty(StatID) ? base.GetHashCode() : StatID.GetHashCode();
        }

        public static bool operator ==(StatBaseSO lhs, StatBaseSO rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;

            if (lhs is null || rhs is null) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(StatBaseSO lhs, StatBaseSO rhs) => !(lhs == rhs);


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            StatID = AssetDatabase.AssetPathToGUID(path);
        }
#endif
    }
}
