using Code.Core.GlobalSO;
using Code.EntityScripts.Enums;
using Member.KJW.Code.CombatSystem.DamageSystem;
using UnityEngine;

namespace Code.EntityScripts.BaseClass {
    public class BadEntityBase : GraphEntity, IDamageable {
        [SerializeField] private BlackBoardGUIDSO stateGUID;
        [SerializeField] private BlackBoardGUIDSO badGuy;
        [SerializeField] private TagHandleSO playerTag;
        [SerializeField] private float atk;
        
        protected override void Awake() {
            base.Awake();
            Health.OnDead += HandleDead;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag(playerTag)) {
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null) {
                    DamageInfo damageInfo = new DamageInfo {
                        Damage = atk,
                        Source = gameObject
                    };
                    damageable.GetDamage(damageInfo);
                }
            }
        }

        private void OnDestroy() {
            Health.OnDead -= HandleDead;
        }

        public override void ResetEntity() {
            IsDead = false;
            Health.ResetHealth();
        }

        private void HandleDead() {
            IsDead = true;
            DropItem();
            GraphAgent.SetVariableValue(stateGUID, BadEntityStates.Dead);
        }

        public void GetDamage(DamageInfo damageInfo) {
            Health.ApplyDamage(damageInfo.Damage);
            if (!IsDead) {
                GraphAgent.SetVariableValue(stateGUID, BadEntityStates.Hurt);
                GraphAgent.SetVariableValue(badGuy, damageInfo.Source);
            }
        }
    }
}