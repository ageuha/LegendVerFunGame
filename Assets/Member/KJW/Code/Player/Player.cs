using System;
using Code.Core.Utility;
using Code.EntityScripts;
using Member.BJH._01Script.Interact;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.Data;
using Member.KJW.Code.Input;
using UnityEngine;
using UnityEngine.Events;

namespace Member.KJW.Code.Player
{
    public class Player : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public RollingData RollingData { get; private set; }
        
        public AgentMovement MoveCompo { get; private set; }
        public HealthSystem HealthCompo { get; private set; }
        public Interactor Interactor { get; private set; }
        public Thrower Thrower { get; private set; }
        
        public bool IsRolling { get; private set; }
        private bool _isInvincible;
        
        public Vector2 StandDir { get; private set; } = Vector2.right;
        
        private float _coolTimer;
        private int _remainRoll;
        public int RemainRoll
        {
            get => _remainRoll;
            private set => _remainRoll = Mathf.Clamp(value, 0, RollingData.MaxRoll);
        }
        
        private void Awake()
        {
            MoveCompo = GetComponent<AgentMovement>();
            HealthCompo = GetComponent<HealthSystem>();
            Interactor = GetComponent<Interactor>();
            Thrower = GetComponent<Thrower>();

            RemainRoll = RollingData.MaxRoll;
        }

        private void OnEnable()
        {
            InputReader.OnInteracted += Interactor.Interact;
            InputReader.OnRolled += Roll;
            InputReader.OnMoved += UpdateStandDir;
        }

        private void Update()
        {
            if (RemainRoll == RollingData.MaxRoll) return;

            if (_coolTimer >= RollingData.StackCoolTime)
            {
                _coolTimer -= RollingData.StackCoolTime;
                ++RemainRoll;
            }

            _coolTimer += Time.deltaTime;
        }

        private void OnDisable()
        {
            InputReader.OnInteracted -= Interactor.Interact;
            InputReader.OnRolled -= Roll;
            InputReader.OnMoved -= UpdateStandDir;
        }

        public void HandleThrow()
        {
            
        }

        private void UpdateStandDir(Vector2 dir)
        {
            if (dir != Vector2.zero) StandDir = dir;
        }

        private void Roll()
        {
            if (RemainRoll == 0 || IsRolling || MoveCompo.IsStop) return;

            --RemainRoll;
            IsRolling = true;
            _isInvincible = true;
        }

        public void EndRoll()
        {
            IsRolling = false;
            _isInvincible = false;
            MoveCompo.RestartMove();
        }

        public void GetDamage(DamageInfo damageInfo)
        {
            if (_isInvincible) return;
            HealthCompo.ApplyDamage(damageInfo.Damage);
        }
    }
}