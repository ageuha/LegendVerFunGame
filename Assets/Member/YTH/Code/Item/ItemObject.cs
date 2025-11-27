using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        private Rigidbody2D m_rigidbody2D;
        private SpriteRenderer m_spriteRenderer;
        [SerializeField] private ItemDataSO itemData;

        private void OnValidate()
        {
            if (itemData == null) return;
            if (m_spriteRenderer == null) return;
            
            m_spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        private void Awake()
        {
            m_rigidbody2D = GetComponent<Rigidbody2D>();
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetItemData(ItemDataSO newData, Vector2 velocity)
        {
            this.itemData = newData;
            m_rigidbody2D.linearVelocity = velocity;
            m_spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        public void PickUp()
        {
            Destroy(gameObject);
        }
    }
}
