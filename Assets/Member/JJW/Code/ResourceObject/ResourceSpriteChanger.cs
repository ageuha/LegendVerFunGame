using Code.Core.Utility;
using UnityEngine;
namespace Member.JJW.Code.ResourceObject
{
    public class ResourceSpriteChanger : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Resource resource;
        
        private SpriteRenderer _spriteRenderer;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            if (resource == null) return;
            if (resource.CurrentHp.Hp == null)
            {
                resource.CurrentHp.Initialize(resource.ResourceSO.MaxHp);
            }
            resource.CurrentHp.Hp.OnValueChanged += ChangeSprite;
            resource.OnInitialize += Init;
            Logging.Log("2");
        }

        private void Init()
        {
            _spriteRenderer.sprite = resource.ResourceSO.ResourceImage;
        }
        
        private void ChangeSprite(float before, float after)
        {
            if(sprites == null || sprites.Length == 0) return;
            if (before == after) return;
            if (after <= 0) return;
            float ratio = after / resource.ResourceSO.MaxHp;
            int idx = Mathf.RoundToInt(ratio * sprites.Length - 1);
            idx = sprites.Length - 1 - idx;
            _spriteRenderer.sprite = sprites[idx];
        }

        private void OnDisable()
        {
            resource.CurrentHp.Hp.OnValueChanged -= ChangeSprite;
            resource.OnInitialize -= Init;
        }
    }
}