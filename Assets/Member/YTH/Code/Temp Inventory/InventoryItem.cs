using Code.Core.Utility;
using Member.KJW.Code.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YTH.Code.Item;

namespace YTH.Code.TempInventory
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private InputReader inputReader;
        [HideInInspector] public Transform parentAfterDrag;
        private ItemDataSO m_itemDataSO;

        public void SetItemData(ItemDataSO itemDataSO)
        {
            this.m_itemDataSO = itemDataSO;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (m_itemDataSO != null) itemIcon.sprite = m_itemDataSO.Icon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            itemIcon.raycastTarget = false;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }


        public void OnDrag(PointerEventData eventData)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(inputReader.MousePos);
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            itemIcon.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
        }

    }
}
