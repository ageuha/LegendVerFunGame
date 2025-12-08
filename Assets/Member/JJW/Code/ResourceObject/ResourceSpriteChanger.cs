using System;
using UnityEngine;

namespace Member.JJW.Code.ResourceObject
{
    public class ResourceSpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        private Resource _resource;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _resource = GetComponentInParent<Resource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            if( _resource.CurrentHp.Hp == null)
                _resource.CurrentHp.Initialize(_resource.MaxHp);
            
            _resource.CurrentHp.Hp.OnValueChanged += ChangeSprite;
        }

        private void ChangeSprite(float before, float after)
        {
            if(before == after) return;
            if(after <= 0) return;
            float ratio = after / _resource.MaxHp;
            int idx = Mathf.RoundToInt(ratio * sprites.Length - 1);
            idx = sprites.Length - 1 - idx;
            _spriteRenderer.sprite = sprites[idx];
        }

        private void OnDestroy()
        {
            _resource.CurrentHp.Hp.OnValueChanged -= ChangeSprite;
        }
    }
}