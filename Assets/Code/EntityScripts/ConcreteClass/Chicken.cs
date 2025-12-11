using Code.Core.GlobalSO;
using Code.Core.Utility;
using Code.EntityScripts.BaseClass;
using Code.EntityScripts.Enums;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.CombatSystem.DamageSystem;
using UnityEngine;

namespace Code.EntityScripts.ConcreteClass {
    public class Chicken : GraphEntity, IDamageable {
        [SerializeField] private BlackBoardGUIDSO stateGUID;
        [SerializeField] private BlackBoardGUIDSO badGuy;

        public override void DropItem() {
        }

        protected override void Awake() {
            base.Awake();
            Health.OnDead += HandleDead;
        }

        private void OnDestroy() {
            Health.OnDead -= HandleDead;
        }

        private void HandleDead() {
            IsDead = true;
            Logging.Log("Dead");
            GraphAgent.SetVariableValue(stateGUID, FriendlyEntityStates.Dead);
        }

        public void GetDamage(DamageInfo damageInfo) {
            Logging.LogWarning("동물학대");
            Health.ApplyDamage(damageInfo.Damage);
            if (!IsDead) {
                Logging.Log("Hurt");
                GraphAgent.SetVariableValue(stateGUID, FriendlyEntityStates.Hurt);
                GraphAgent.SetVariableValue(badGuy, damageInfo.Source);
            }
        }
    }
}