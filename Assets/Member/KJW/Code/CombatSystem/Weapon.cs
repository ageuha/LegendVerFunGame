using System;
using Member.KJW.Code.CombatSystem.DamageSystem;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.KJW.Code.CombatSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private GameObject owner;
        
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        private BoxCollider2D _collider;
        public BoxCollider2D Collider => _collider ??= GetComponent<BoxCollider2D>();

        private DamageInfo _damageInfo;
        
        public void Init(WeaponDataSO weapon)
        {
            SpriteRenderer.sprite = weapon.Icon;
            Collider.size = weapon.WeaponHitBoxSize;
            transform.localPosition = weapon.HitBoxOffset;
            _damageInfo = weapon.DamageInfoData.ToStruct(owner ?? gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable id))
            {
                id.GetDamage(_damageInfo);
            }
        }
    }
}