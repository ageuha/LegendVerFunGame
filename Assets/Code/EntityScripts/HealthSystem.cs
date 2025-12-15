using System;
using Code.Core.Utility;
using Code.EntityScripts.Interface;
using UnityEngine;

namespace Code.EntityScripts {
    public class HealthSystem : MonoBehaviour, IHealthSystem {
        public IReadOnlyNotifyValue<float> Hp => _hp;
        public event Action OnDead;
        public event Action<float> OnDamaged;
        public event Action<float> OnInitMaxHp;

        private NotifyValue<float> _hp;
        private bool _initialized;
        private bool _isDead;
        private float _maxHp;

        public void Initialize(float maxHp) {
            if (_initialized) return;
            if (maxHp <= 0) {
                Logging.LogError($"유효하지 않은 최대 체력입니다: {maxHp}");
            }

            _hp ??= new NotifyValue<float>();
            _initialized = true;

            _maxHp = maxHp;
            _hp.Value = _maxHp;
            OnInitMaxHp?.Invoke(maxHp);
        }

        public void ResetHealth(bool reInitialize = false) {
            if (reInitialize) {
                _initialized = false;
            }
            else {
                _hp.Value = _maxHp;
            }

            _isDead = false;
        }

        public void ApplyDamage(float damage) {
            if (!_initialized) {
                Logging.LogError("HealthSystem이 초기화되지 않았습니다.");
                return;
            }

            if (damage < 0) {
                Logging.LogWarning("대미지는 음수값이 될 수 없습니다.");
                damage = 0;
            }

            if (_isDead) return;

            float clampedHp = Mathf.Max(_hp.Value - damage, 0);

            _hp.Value = clampedHp;
            OnDamaged?.Invoke(_hp.Value / _maxHp);

            if (_hp.Value <= 0) {
                _isDead = true;
                OnDead?.Invoke();
            }
        }

        [ContextMenu("ad")]
        public void ApplyDamage()
        {
            ApplyDamage(1f);
        }

        public void ApplyHeal(float heal) {
            if (!_initialized) {
                Logging.LogError("HealthSystem이 초기화되지 않았습니다.");
                return;
            }

            if (heal < 0) {
                Logging.LogWarning("회복량은 음수값이 될 수 없습니다.");
                heal = 0;
            }

            if (_isDead) return;

            float clampedHp = Mathf.Min(_hp.Value + heal, _maxHp);

            _hp.Value = clampedHp;
        }

        // 수정 사항 있으면 말하렴 도원아
        // 나 도원인데, 역시 윤담이다. 완벽하네.
    }
}