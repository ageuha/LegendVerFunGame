using System.Security.Cryptography;
using Member.KJW.Code.Input;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private InputReader inputReader;
        [HideInInspector] public Transform parentAfterDrag;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [field:SerializeField] public ItemDataSO Item { get; private set; }
        [field:SerializeField] public int Count { get; private set; }

        private InventoryManager m_InventoryManager;
        private bool m_IsHold;

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO)
        {
            this.m_InventoryManager = inventoryManager;      
            SetItemData(itemDataSO); 
        }

        public void SetItemData(ItemDataSO itemDataSO)
        {
            this.Item = itemDataSO;
            UpdateUI();
        }

        public void SetItemData(ItemDataSO itemDataSO, int count)
        {
            this.Item = itemDataSO;
            this.Count = count;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (Item != null) itemIcon.sprite = Item.Icon;
            if (countText != null) countText.text = Count.ToString();
        }

        public void AddStack(int count = 1)
        {
            Count += count;
            UpdateUI();
        }

        public void RemoveStack(int count = 1)
        {
            Count -= count;
            if (Count <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                UpdateUI();
            }
        }


        private void Update()
        {
            if (m_IsHold)
            {
                transform.position = (Vector2)Camera.main.ScreenToWorldPoint(inputReader.MousePos);
            }
        }
        
        public void PickDown()
        {
            m_IsHold = false;
            itemIcon.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            transform.localPosition = Vector2.zero;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_InventoryManager.HoldItem == null)
            {       
                if (!m_IsHold)
                {
                    m_IsHold = true;
                    itemIcon.raycastTarget = false;
                    parentAfterDrag = transform.parent;
                    transform.SetParent(transform.root);
                    inventoryItemPickUpEventChannel.Raise(this);
                }
            }
        }
    }
}
