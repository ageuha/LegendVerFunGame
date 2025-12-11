using System;
using System.Collections.Generic;
using Code.Core.Pool;
using Code.EntityScripts;
using Member.JJW.Code.SO;
using Unity.Cinemachine;
using UnityEngine;
using YTH.Code.Item;
using Random = UnityEngine.Random;

namespace Member.JJW.Code.ResourceObject
{
    public class Resource : MonoBehaviour,IPoolable
    {
        public event Action<Sprite> OnInit;
        public int InitialCapacity { get => initialCapacity; }
        [field:SerializeField] public ResourceSO ResourceSO {get; private set; }
        [field:SerializeField] public HealthSystem CurrentHp {get; private set; }
        [SerializeField] private int initialCapacity = 1;
        
        private SpriteRenderer _spriteRenderer;
        private bool _isCanSpawn = false;

        private void OnEnable()
        {
            CurrentHp.OnDead += SpawnItem;
            CurrentHp.OnDead += ChangeCondition;
        }
        public void Initialize(ResourceSO resourceSO) //다른 SO로 초기화 하고 싶을때 사용
        {
            if (resourceSO == null && !_isCanSpawn)
            {
                ResourceSO = resourceSO;
                CurrentHp.Initialize(ResourceSO.MaxHp);
                _isCanSpawn = false;
                OnInit?.Invoke(resourceSO.ResourceImage);
            }
        }

        public void GetDamage(ItemInfo itemInfo) //리소스에 줄 데미지를 계산후 줌
        {
            float finalDamage = 10;
            float onCorrectTypeDamage;
            if (itemInfo.ItemType == ItemType.Default)
            {
                onCorrectTypeDamage = 0;
            }
            else
            {
                if (itemInfo.ItemType == ResourceSO.correctType)
                {
                    onCorrectTypeDamage = 10;
                }
                else
                {
                    onCorrectTypeDamage = 1;
                }
            }
            
            var damagePlusValue = (int)itemInfo.Ingredient;
            finalDamage += damagePlusValue * onCorrectTypeDamage;
            CurrentHp.ApplyDamage(finalDamage);
        }

        private void SpawnItem() //리소스가 파괴되면 실행 리소스가 생성하는 아이템을 생성해줌 
        {
            if(_isCanSpawn == false) return;
            for (int i = 0; i < ResourceSO.SpawnItemAmount; i++)
            {
                Vector2 randomPos = (Vector2)transform.position + Random.insideUnitCircle;
                GameObject item = Instantiate(ResourceSO.ItemPrefab,randomPos,Quaternion.identity);
                if (item.TryGetComponent<ItemObject>(out ItemObject itemObject))
                {
                    itemObject.SetItemData(ResourceSO.ItemDataSO,ResourceSO.SpawnItemAmount);
                }
            }
            _isCanSpawn = false;
            PoolManager.Instance.Factory<Resource>().Push(this);
            Debug.Log("아이템 스폰");
        }

        private void ChangeCondition()
        {
            _isCanSpawn = true;
        }

        private void OnDisable()
        {
            CurrentHp.OnDead -= SpawnItem;
            CurrentHp.OnDead -= ChangeCondition;
        }

        private void OnDrawGizmos()
        {
            if (!ResourceSO) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, ResourceSO.ItemSpawnRadius);
        }
        
        public void OnPopFromPool()
        {
            
        }

        public void OnReturnToPool()
        {
            CurrentHp.ResetHealth();
        }
    }
}
