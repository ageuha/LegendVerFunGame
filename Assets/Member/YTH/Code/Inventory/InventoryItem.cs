using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Member.KJW.Code.Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YTH.Code.Item;

namespace YTH.Code.Inventory
{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler, IPoolable
    {
        public int InitialCapacity => 60;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private InputReader inputReader;
        [HideInInspector] public Transform parentAfterDrag;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [field:SerializeField] public ItemDataSO Item { get; private set; }
        [field:SerializeField] public int Count { get; private set; }

        private InventoryManager m_InventoryManager;
        [SerializeField] private bool m_IsHold;

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO)
        {
            this.m_InventoryManager = inventoryManager;
            SetItemData(itemDataSO); 
            m_IsHold = false;
            itemIcon.raycastTarget = true;   
            transform.localPosition = Vector2.zero;   
        }

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO, int count)
        {
            this.m_InventoryManager = inventoryManager;      
            SetItemData(itemDataSO,count); 
            m_IsHold = false;
            itemIcon.raycastTarget = true;   
            transform.localPosition = Vector2.zero;  
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
                if (this == m_InventoryManager.HoldItem)
                {
                    inventoryItemPickDownEventChannel.Raise(new Empty());
                }
                PoolManager.Instance.Factory<InventoryItem>().Push(this);
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

        public void PickUp()
        {
            m_IsHold = true;
            itemIcon.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            inventoryItemPickUpEventChannel.Raise(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_InventoryManager.HoldItem == null)
            {       
                if (!m_IsHold)
                {
                    if(eventData.button == PointerEventData.InputButton.Left)
                    {
                        PickUp();
                    }
                    else if(eventData.button == PointerEventData.InputButton.Right)
                    {
                        int splitCount = Split();
                        if (splitCount <= 0) return;
                        RemoveStack(splitCount);

                        InventoryItem newItem  = PoolManager.Instance.Factory<InventoryItem>().Pop(transform.root);
                        newItem.transform.localScale = Vector3.one;
                        newItem.transform.localPosition = Vector3.zero;
                        newItem.Initialize(m_InventoryManager, Item, splitCount);
                        newItem.PickUp();
                    }
                }
            }
            else
            {
                var HoldItem = m_InventoryManager.HoldItem;
                if (HoldItem.Item == Item)
                {
                    if(eventData.button == PointerEventData.InputButton.Left)
                    {
                        if (Count + HoldItem.Count <= Item.MaxStack)
                        {   
                            AddStack(HoldItem.Count);
                            HoldItem.RemoveStack(HoldItem.Count);
                        }
                        else
                        {
                            int remainCount = Item.MaxStack - Count;
                            AddStack(remainCount);
                            HoldItem.RemoveStack(remainCount);

                        }
                    }
                    else if(eventData.button == PointerEventData.InputButton.Right)
                    {
                        if (Count + 1 <= Item.MaxStack)
                        {    
                            AddStack(1);
                            HoldItem.RemoveStack(1);
                        }
                    }
                }
            }
        }

        private int Split()
        {
            int SplitCount = 0;

            if (Count > 1)
            {
                SplitCount = Count / 2;
            }

            return SplitCount;
        }

        public void OnPopFromPool()
        {
            
        }

        public void OnReturnToPool()
        {
            
        }
    }
}
