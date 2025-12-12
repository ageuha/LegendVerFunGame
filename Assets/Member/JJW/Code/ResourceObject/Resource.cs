using System;
using Code.Core.Pool;
using Code.EntityScripts;
using Code.GridSystem.Objects;
using Member.JJW.Code.SO;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.JJW.Code.ResourceObject
{
    public class Resource : GridBoundsObject,IPoolable,IHarvestable
    {
        public event Action OnInitialize;
        public int InitialCapacity { get => initialCapacity; }
        [field: SerializeField] public HealthSystem CurrentHp { get; set; }
        [field:SerializeField] public ResourceSO ResourceSO {get; set; }
        [SerializeField] private int initialCapacity = 1;
        
        protected override Vector2Int Size { get => _clickBoundSize; }//자신의 범위
        private Vector2Int _clickBoundSize;
        private BoxCollider2D _boxCollider;
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            Initialize(ResourceSO);
        }

        public void Initialize(ResourceSO resourceSO) //다른 SO로 초기화 하고 싶을때 사용
        {
            ResourceSO = resourceSO;
            CurrentHp.Initialize(ResourceSO.MaxHp);
            _boxCollider.size = resourceSO.DetectionRangeSize;
            _clickBoundSize = resourceSO.DetectionRangeSize;
            OnInitialize?.Invoke();
        }
        // Resource.cs 스크립트 내부
        private void OnValidate()
        {
            // 1. SpriteRenderer 컴포넌트를 가져옵니다. (OnValidate는 에디터에서 실행되므로 안전)
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }

            // 2. ResourceSO가 할당되어 있고, SpriteRenderer가 있을 때만 이미지 업데이트
            if (_spriteRenderer != null && ResourceSO != null && ResourceSO.ResourceImage != null)
            {
                // ⭐ SO에 정의된 이미지로 SpriteRenderer의 스프라이트를 즉시 변경합니다. ⭐
                _spriteRenderer.sprite = ResourceSO.ResourceImage;
            }
            // SO가 null이 되었거나 ResourceImage가 없으면 스프라이트를 지울 수도 있습니다.
            else if (_spriteRenderer != null && (ResourceSO == null || ResourceSO.ResourceImage == null))
            {
                _spriteRenderer.sprite = null;
            }
        }
        public void Harvest(ItemDataSO itemInfo)
        {
            CurrentHp.ApplyDamage(itemInfo.DamageInfoData.damage);  
        }
       
        private void OnDrawGizmosSelected()
        {
            if (!ResourceSO) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, ResourceSO.ItemSpawnRadius);
            Gizmos.color = Color.red;
            Vector3 vector2Int = new Vector3(Size.x, Size.y, 0);
            Gizmos.DrawWireCube(transform.position,vector2Int);
        }
        public void OnReturnToPool()
        {
            CurrentHp.ResetHealth();
        }
        public void OnPopFromPool()
        {
            CurrentHp.ResetHealth();
        }
    }
}
