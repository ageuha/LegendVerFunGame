using System;
using Code.Core.Utility;
using Code.EntityScripts;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.CombatSystem.DamageSystem;
using UnityEngine;

namespace Member.KJW.Code.Test
{
    [RequireComponent(typeof(HealthSystem))]
    public class Dummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        
        private HealthSystem _healthSystem;

        private void Awake()
        {
            _healthSystem = GetComponent<HealthSystem>();
            _healthSystem.Initialize(maxHealth);
        }

        public void GetDamage(DamageInfo damageInfo)
        {
            _healthSystem?.ApplyDamage(damageInfo.Damage);
            Logging.Log(_healthSystem?.Hp.Value);
        }
    }
}