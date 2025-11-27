using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemDataSO itemData;

        private void OnValidate()
        {
            if (itemData == null) return;
            if (spriteRenderer == null) return;
            
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetItemData(ItemDataSO newData, Vector2 velocity)
        {
            this.itemData = newData;
            rigidbody2D.linearVelocity = velocity;
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        public void PickUp()
        {
            Debug.Log($"Picked up {itemData.ItemName}");
            Destroy(gameObject);
        }
    }
}
