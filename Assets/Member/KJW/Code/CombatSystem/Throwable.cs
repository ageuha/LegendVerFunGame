using System;
using Code.Core.Pool;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Throwable : MonoBehaviour, IPoolable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        private DamageInfo _damageInfo;

        private void Awake()
        {
            _rb  = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        public Throwable Init(DamageInfo damageInfo)
        {
            _damageInfo = damageInfo;
            return this;
        }

        public void Throw(Vector2 dir, float speed)
        {
            _rb.linearVelocity = dir * speed;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable id))
            {
                id.GetDamage(_damageInfo);
            }
        }

        public GameObject GameObject => gameObject;
        public void OnPopFromPool()
        {
        }

        public void OnReturnToPool()
        {
        }
    }
}