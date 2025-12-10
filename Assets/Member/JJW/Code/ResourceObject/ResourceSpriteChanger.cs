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
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _resource = GetComponentInParent<Resource>();
        }

        private void OnEnable()
        {
            _resource.OnInit += ChangeResourceSprite;
            _resource.CurrentHp.Hp.OnValueChanged += ChangeSprite;
            
            if (_resource.CurrentHp.Hp == null)
                _resource.CurrentHp.Initialize(_resource.ResourceSO.MaxHp);
        }

        private void ChangeResourceSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        private void ChangeSprite(float before, float after)
        {
            if(sprites == null || sprites.Length == 0) return;
            if (before == after) return;
            if (after <= 0) return;
            float ratio = after / _resource.ResourceSO.MaxHp;
            int idx = Mathf.RoundToInt(ratio * sprites.Length - 1);
            idx = sprites.Length - 1 - idx;
            _spriteRenderer.sprite = sprites[idx];
        }

        private void OnDisable()
        {
            _resource.CurrentHp.Hp.OnValueChanged -= ChangeSprite;
        }
    }
}