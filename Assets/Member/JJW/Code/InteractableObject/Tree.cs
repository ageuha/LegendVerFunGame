using System;
using Code.EntityScripts;
using Member.JJW.Code.Interface;
using UnityEngine;
using UnityEngine.Serialization;
using YTH.Code.Interface;
using YTH.Code.Item;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.InteractableObject
{
    public class Tree :  ResourcesObject
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private ItemDataSO itemDataSO;
        [SerializeField] private float hp;
        [SerializeField] private int spawnItemCount =1;
        [SerializeField] private float itemSpawnRadius = 1f;
        
        private HealthSystem _hp;

        private void Awake()
        {
            _hp = GetComponent<HealthSystem>();
            _hp.Initialize(hp);
            _hp.OnDead += SpawnItem;
        }

        public override void SpawnItem()
        {
            for (int i = 0; i < spawnItemCount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                GameObject item = Instantiate(itemPrefab,transform.position,Quaternion.identity);
                if (item.TryGetComponent<IPickable>(out IPickable pickable))
                {
                    //초기화
                }
            }
            Destroy(gameObject);
        }

        public override void Interaction(float value)
        {
            _hp.ApplyDamage(value);
        }

        private void OnDestroy()
        {
            _hp.OnDead -= SpawnItem;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, itemSpawnRadius);
        }
    }
}