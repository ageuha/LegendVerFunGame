using Code.Events;
using UnityEngine;

namespace Code.UI.TooltipSystem {
    [CreateAssetMenu(fileName = "TooltipChannel", menuName = "EventChannel/Tooltip", order = 0)]
    public class TooltipChannel : EventChannel<TooltipContext> {
    }
}