using UnityEngine;
using UnityEngine.EventSystems;

namespace YTH.Code.TempInventory
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
                inventoryItem.parentAfterDrag = transform;
            }
        }
    }
}
