using System;
using Code.Core.Pool;
using Code.Core.Utility;
using Member.KJW.Code.CombatSystem.DamageSystem;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Throwable : MonoBehaviour, IPoolable
    {
        private float _lifeTime;
        private Rigidbody2D _rb;
        public Rigidbody2D Rb => _rb ??= GetComponent<Rigidbody2D>();
        private SpriteRenderer _renderer;
        public SpriteRenderer Renderer => _renderer ??= GetComponent<SpriteRenderer>();
        private DamageInfo _damageInfo;
        private float _speed;
        private float _rotSpeed;
        private BoxCollider2D _collider;
        public BoxCollider2D Collider => _collider ??= GetComponent<BoxCollider2D>();
        private float _timer;

        public Throwable Init(ItemDataSO itemData, Vector2 pos)
        {
            _damageInfo = itemData.DamageInfoData.ToStruct(gameObject);
            Renderer.sprite = itemData.Icon;
            _speed = itemData.ThrowSpeed;
            Collider.size = itemData.HitBoxSize;
            _lifeTime = itemData.ThrowLifeTime;
            _rotSpeed = itemData.ThrowRotationSpeed;
            transform.position = pos;
            return this;
        }

        public void Throw(Vector2 dir)
        {
            Rb.linearVelocity = dir * _speed;
            Rb.AddTorque(1, ForceMode2D.Impulse);
            _timer = Time.time + _lifeTime;
        }

        private void Update()
        {
            if (Time.time > _timer)
                Push();
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