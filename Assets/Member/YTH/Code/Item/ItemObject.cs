using Code.Core.GlobalSO;
using Code.Core.Utility;
using DG.Tweening;
using UnityEngine;
using YTH.Code.Interface;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemDataSO itemData;
        [SerializeField] private TweeningInfoSO tweeningInfo;

        private void OnValidate()
        {
            if (itemData == null) return;
            if (spriteRenderer == null) return;
            
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            transform.DOMove(collision.gameObject.transform.position, tweeningInfo.Duration).SetEase(tweeningInfo.EasingType);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            transform.DOKill();
        }

        public void SetItemData(ItemDataSO newData, Vector2 velocity)
        {
            this.itemData = newData;
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        public void PickUp()
        {
            Logging.Log($"Picked up item: {itemData.ItemName}");
            Destroy(gameObject);
        }
    }
}
