using Code.Core.GlobalStructs;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YTH.Code.Inventory
{
    public class CraftingResultSlot : InventorySlot
    {
        [SerializeField] private CraftEventChannel craftEventChannel;
        protected override void OnTransformChildrenChanged()
        {
            Logging.Log("OnTransformChildrenChanged");
            base.OnTransformChildrenChanged();
            craftEventChannel.Raise(new Empty());
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            return;
        }
    }
}
