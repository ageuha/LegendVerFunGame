using System;
using Code.EntityScripts;
using Member.JJW.Code.Interface;
using UnityEngine;
using YTH.Code.Interface;
using YTH.Code.Item;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.ResourceObject
{
    public class Resource : MonoBehaviour,IInteractable<float>
    {
        [field:SerializeField] public float MaxHp {get; private set; }
        [field:SerializeField] public HealthSystem CurrentHp {get; private set; }
        
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private ItemDataSO itemDataSO;
        [SerializeField] private int spawnItemAmount =1;
        [SerializeField] private float itemSpawnRadius = 1f;

        private void Awake()
        {
            CurrentHp.Initialize(MaxHp);
            CurrentHp.OnDead += SpawnItem;
        }

        

        private void SpawnItem()
        {
            for (int i = 0; i < spawnItemAmount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                GameObject item = Instantiate(itemPrefab,randomPos,Quaternion.identity);
                if (item.TryGetComponent<ItemObject>(out ItemObject itemObject))
                {
                    itemObject.SetItemData(itemDataSO,spawnItemAmount);
                }
            }
            Destroy(gameObject);
        }

        public void Interaction(float value)
        {
            CurrentHp.ApplyDamage(value);
        }

        private void OnDestroy()
        {
            CurrentHp.OnDead -= SpawnItem;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, itemSpawnRadius);
        }
    }
}