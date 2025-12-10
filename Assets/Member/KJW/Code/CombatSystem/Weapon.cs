using System;
using UnityEngine;
using YTH.Code.Item;

namespace Member.KJW.Code.CombatSystem
{
    public class Weapon : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer => _spriteRenderer ??= GetComponent<SpriteRenderer>();

        private BoxCollider2D _collider;
        public BoxCollider2D Collider => _collider ??= GetComponent<BoxCollider2D>();

        private DamageInfo _damageInfo;
        
        public void Init(WeaponDataSO weapon)
        {
            SpriteRenderer.sprite = weapon.Icon;
            Collider.size = weapon.HitBoxSize;
            transform.localPosition = weapon.HitBoxOffset;
            _damageInfo = weapon.DamageInfoData.ToStruct();
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