using Member.KJW.Code.Player;
using UnityEngine;
using YTH.Code.Interface;
using YTH.Code.Inventory;

namespace YTH.Code.Item
{
    public class ItemObject : MonoBehaviour, IPickable
    {
        [SerializeField] private ItemObjectTrigger itemObjectTrigger;
        [SerializeField] private InventoryAddEventChannel inventoryAddEventChannel;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ItemDataSO itemData;
        [SerializeField] private float speed = 5;
        [SerializeField] private int count = 1;

        private bool m_isLoockOn;
        private Transform m_target;

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

        private void FixedUpdate()
        {
            if(m_isLoockOn)
            {    
                rb.AddForce((m_target.position - transform.position).normalized * speed * Time.deltaTime, ForceMode2D.Impulse);

                //float distance = Vector3.Distance(transform.position, m_target.position);
                //float scale = Mathf.Clamp01(distance);
                //transform.localScale = new Vector3(scale, scale, 1);
            }
        }

        private void OnLockOn(Player player)
        {
            m_isLoockOn = true;
            m_target = player.transform;
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


        public void SetItemData(ItemDataSO newData, int count = 1)
        {
            this.count = count;
            itemData = newData;
            spriteRenderer.sprite = itemData.Icon;
            gameObject.name = $"ItemObject_{itemData.ItemName}";
        }

        public void PickUp()
        {
            inventoryAddEventChannel.Raise(new ItemData(itemData, count));
            Destroy(gameObject);
        }
    }

}
