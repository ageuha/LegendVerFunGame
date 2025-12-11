using System;
using System.Collections;
using Code.Core.Pool;
using Code.EntityScripts;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class Arrow : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed;
        [SerializeField] private float activeTime;
        [SerializeField] private float damage;
        private Rigidbody2D _rigidbody2D;
        private Vector2 dir;
        public int InitialCapacity { get; }

        private void Start()
        {
            _rigidbody2D ??= GetComponent<Rigidbody2D>();
        }
            

        public void Initialize(Vector2 startPos, Vector2 dir)
        {
            transform.position = startPos;
            this.dir = dir;
            StartCoroutine(Destroy());
        }

        private void FixedUpdate()
        {
            _rigidbody2D.linearVelocity = dir *  speed;
        }

        public IEnumerator Destroy()
        {
            yield return new WaitForSeconds(activeTime);
            PoolManager.Instance.Factory<Arrow>().Push(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            LayerMask layerMask = 1 << LayerMask.NameToLayer("Enemy");
            if ((layerMask & (1 << other.gameObject.layer)) != 0 && other.TryGetComponent<HealthSystem>(out  HealthSystem healthSystem))
            {
                healthSystem.ApplyDamage(damage);
                PoolManager.Instance.Factory<Arrow>().Push(this);
            }
        }

        public void OnPopFromPool()
        {
            
        }

        public void OnReturnToPool()
        {
            dir = Vector2.zero;
            _rigidbody2D.linearVelocity = Vector2.zero;
        }
    }
}