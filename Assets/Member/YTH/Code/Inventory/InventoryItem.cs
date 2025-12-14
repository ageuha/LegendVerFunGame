using System;
using System.Runtime.InteropServices;
using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.UI.TooltipSystem;
using Member.KJW.Code.Input;
using Member.YTH.Code.Item;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace YTH.Code.Inventory
{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler, IPoolable, IPointerEnterHandler, IPointerExitHandler
    {
        [field:SerializeField] public ItemDataSO Item { get; private set; }
        [field:SerializeField] public int Count { get; private set; }
        public RectTransform RectTransform => m_RectTransform ??= transform as RectTransform;
        public int InitialCapacity => 60;

        public event Action CountChanged;
        
        [HideInInspector] public Transform parentAfterDrag;

        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private InventoryItemPickUpEventChannel inventoryItemPickUpEventChannel;
        [SerializeField] private InventoryItemPickDownEventChannel inventoryItemPickDownEventChannel;
        [SerializeField] private TooltipChannel tooltipEventChannel;
        [SerializeField] private TooltipContextDataSO tooltipContextData;
        [SerializeField] private bool m_IsHold;
        private InventoryManager m_InventoryManager;
        private RectTransform m_RectTransform;

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO)
        {
            this.m_InventoryManager = inventoryManager;
            SetItemData(itemDataSO); 
            m_IsHold = false;
            Count = 1;
            itemIcon.raycastTarget = true;   
            transform.localPosition = Vector2.zero; 
        }

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO, int count)
        {
            this.m_InventoryManager = inventoryManager;      
            SetItemData(itemDataSO, count); 
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
            if (countText != null)
            {
                if (Count == 1)
                {
                    countText.gameObject.SetActive(false);
                }
                else
                {
                    countText.gameObject.SetActive(true);  
                    countText.text = Count.ToString();
                }
            }
        }

        public int AddStack(int count = 1)
        {
            Count += count;

            if (Count > Item.MaxStack)
            {
                int remain = Count - Item.MaxStack;
                Count = Item.MaxStack;
                UpdateUI();
                CountChanged?.Invoke();
                return remain;
            }

            CountChanged?.Invoke();
            UpdateUI();
            return 0;
        }

        public void SetCount(int count = 1)
        {
            Count = count;
            CountChanged?.Invoke();
            UpdateUI();
        }

        public void RemoveStack(int count = 1)
        {
            Count -= count;
            if (Count <= 0)
            {
                if (this == m_InventoryManager.HoldItem) //여기 버그? 여기 한번 Inventory Slot 말고 Material Slot으로 바꿔서 해보거나 해
                {
                    inventoryItemPickDownEventChannel.Raise(new Empty());
                }
                CountChanged = null;
                PoolManager.Instance.Factory<InventoryItem>().Push(this);
            }
            else
            {
                UpdateUI();
            }
            CountChanged?.Invoke();
        }


        private void Update()
        {
            if (m_IsHold)
            {
                transform.position = Mouse.current.position.value;

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
            if (!m_InventoryManager.UIOpen) return;
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Item != null)
            {    
                TooltipContext tooltip = new();

                tooltip.Active = true;

                tooltip.BackgroundColor = tooltipContextData.BackgroundColor;
                tooltip.BorderColor = tooltipContextData.BorderColor;
                tooltip.DescriptionColor = tooltipContextData.DescriptionColor;
                tooltip.OutlineColor = tooltipContextData.OutlineColor;
                tooltip.TitleColor = tooltipContextData.TitleColor;

                tooltip.TitleText = Item.ToString();
                tooltip.DescriptionText = Item.GetDescription();
                tooltip.Position = (Vector2)RectTransform.position + tooltipContextData.Offset;

                tooltipEventChannel.Raise(tooltip);
            }
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (Item != null)
            {  
                TooltipContext tooltip = new();

                tooltip.Active = false;

                tooltipEventChannel.Raise(tooltip);
            }
        }
    }
}
