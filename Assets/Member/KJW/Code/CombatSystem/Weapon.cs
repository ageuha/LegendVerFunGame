using System;
using UnityEngine;
using YTH.Code.Item;

namespace Member.KJW.Code.CombatSystem
{
    public class Weapon : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private DamageInfo _damageInfo;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>(true);
            _collider = GetComponentInChildren<BoxCollider2D>(true);
        }

        public void Init(WeaponDataSO weapon)
        {
            _spriteRenderer ??= GetComponentInChildren<SpriteRenderer>(true);
            _spriteRenderer.sprite = weapon.Icon;
            _collider.size = weapon.HitBoxSize;
            transform.localPosition = weapon.HitBoxOffset;
            _damageInfo = weapon.ThrowDataInfo.ToStruct();
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