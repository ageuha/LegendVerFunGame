using System;
using Code.Core.Pool;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Throwable : MonoBehaviour, IPoolable
    {
        [SerializeField] private float lifeTime;
        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        private DamageInfo _damageInfo;
        private float _speed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            Invoke(nameof(Push), lifeTime);
        }

        public Throwable Init(DamageInfo damageInfo)
        {
            _damageInfo = damageInfo;
            return this;
        }

        public void Throw(Vector2 dir)
        {
            _rb.linearVelocity = dir * _speed;
        }

        private void Push()
        {
            PoolManager.Instance.Factory<Throwable>().Push(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable id))
            {
                id.GetDamage(_damageInfo);
                PoolManager.Instance.Factory<Throwable>().Push(this);
            }
        }
        
        public int InitialCapacity => 5;

        public void OnPopFromPool()
        {
        }

        public void OnReturnToPool()
        {
        }
    }
}