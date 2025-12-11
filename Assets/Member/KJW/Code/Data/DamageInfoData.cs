using Member.KJW.Code.CombatSystem;
using UnityEngine;

namespace Member.KJW.Code.Data {
    [CreateAssetMenu(fileName = "DamageInfoData", menuName = "SO/DamageInfoData", order = 0)]
    public class DamageInfoData : ScriptableObject {
        public float damage;
        public float knockback;

        public DamageInfo ToStruct(GameObject source) {
            return new DamageInfo() { Damage = damage, Knockback = knockback, Source = source };
        }
    }
}