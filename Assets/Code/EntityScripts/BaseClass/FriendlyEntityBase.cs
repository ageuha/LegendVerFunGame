using Code.Core.GlobalSO;
using Code.EntityScripts.Enums;
using Member.KJW.Code.CombatSystem.DamageSystem;
using UnityEngine;

namespace Code.EntityScripts.BaseClass {
    public class FriendlyEntityBase : GraphEntity, IDamageable {
        [SerializeField] private BlackBoardGUIDSO stateGUID;
        [SerializeField] private BlackBoardGUIDSO badGuy;

        protected override void Awake() {
            base.Awake();
            Health.OnDead += HandleDead;
        }

        private void OnDestroy() {
            Health.OnDead -= HandleDead;
        }

        private void HandleDead() {
            IsDead = true;
            DropItem();
            GraphAgent.SetVariableValue(stateGUID, FriendlyEntityStates.Dead);
        }

        public void GetDamage(DamageInfo damageInfo) {
            Health.ApplyDamage(damageInfo.Damage);
            if (!IsDead) {
                GraphAgent.SetVariableValue(stateGUID, FriendlyEntityStates.Hurt);
                GraphAgent.SetVariableValue(badGuy, damageInfo.Source);
            }
        }
    }
}