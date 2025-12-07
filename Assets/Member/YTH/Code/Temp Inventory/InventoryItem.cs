using Code.Core.Utility;
using Member.KJW.Code.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YTH.Code.TempInventory
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image itemIcon;
        [SerializeField] private InputReader inputReader;

        [HideInInspector] public Transform parentAfterDrag;

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
