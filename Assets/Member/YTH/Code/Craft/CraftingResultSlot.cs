using Code.Core.GlobalStructs;
using Code.Core.Pool;
using Code.Core.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using YTH.Code.Interface;
using YTH.Code.Inventory;

namespace YTH.Code.Inventory
{
    public class CraftingResultSlot : InventorySlot
    {
        [SerializeField] private CraftEventChannel craftEventChannel;
        protected override void OnTransformChildrenChanged()
        {
            Logging.Log("OnTransformChildrenChanged");
            base.OnTransformChildrenChanged();
            //craftEventChannel.Raise(new Empty());
        }
        public override void OnPointerClick(PointerEventData eventData)
        {
            return;
        }
    }
}
