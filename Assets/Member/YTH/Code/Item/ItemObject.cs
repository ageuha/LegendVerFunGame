using Code.Core.Utility;
using Member.KJW.Code.Player;
using UnityEngine;
using YTH.Code.Interface;
using YTH.Code.Inventory;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ItemDataSO itemData;
        [SerializeField] private ItemObjectTrigger itemObjectTrigger;
        [SerializeField] private float speed = 5;

        private bool m_isLoockOn;
        private Player m_player;

        private void OnValidate()
        {
            if (itemData == null || spriteRenderer == null) return;

            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
            rb ??= GetComponentInChildren<Rigidbody2D>();
            spriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            itemObjectTrigger ??= GetComponentInChildren<ItemObjectTrigger>();
        }

        private void Reset()
        {
            rb ??= GetComponentInChildren<Rigidbody2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            itemObjectTrigger ??= GetComponentInChildren<ItemObjectTrigger>();
        }

        private void Awake()
        {
            itemObjectTrigger.Trigger += OnLockOn;
        }

        private void Update()
        {
            if(m_isLoockOn)
            {    
                Logging.Log("Move");
                Vector3 targetPos = m_player.transform.position;
                rb.AddForce((targetPos - transform.position).normalized * speed * Time.deltaTime, ForceMode2D.Impulse);

                float distance = Vector3.Distance(transform.position, targetPos);
                float scale = Mathf.Clamp01(distance);
                transform.localScale = new Vector3(scale, scale, 1);
            }
        }

        private void OnLockOn(Player player)
        {
            Logging.Log("LockOn");
            
            m_player = player;
            m_isLoockOn = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(m_isLoockOn)
            {       
                if (collision.TryGetComponent<Player>(out Player player))
                {
                    PickUp();
                }
            }
        }


        public void SetItemData(ItemDataSO newData)
        {
            itemData = newData;
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        public void PickUp()
        {
            Logging.Log($"Picked up item: {itemData.ItemName}");
            Destroy(gameObject);
            m_player.GetComponent<PlayerInventory>().AddItem(itemData);

        }
    }

}
