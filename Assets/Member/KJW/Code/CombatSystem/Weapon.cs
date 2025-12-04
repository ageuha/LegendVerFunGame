using System;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Weapon : MonoBehaviour
    {
        private DamageInfo _damageInfo;
        
        public void Init(DamageInfo damageInfo)
        {
            _damageInfo = damageInfo;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable id))
            {
                id.GetDamage(_damageInfo);
            }
        }
    }
}