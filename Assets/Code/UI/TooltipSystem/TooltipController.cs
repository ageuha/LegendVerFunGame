using System;
using Code.Core.Utility;
using UnityEngine;

namespace Code.UI.TooltipSystem {
    public class TooltipController : MonoBehaviour {
        [SerializeField] private TooltipChannel channel;
        [SerializeField] private SerializeHelper<IUIElement<TooltipContext>> tooltip;

        private void Reset() {
            tooltip ??= new SerializeHelper<IUIElement<TooltipContext>>();
            tooltip.SetEditorValue(GetComponentInChildren<IUIElement<TooltipContext>>());
        }

        private void Awake() {
            channel.OnEvent += ChannelEventHandler;
        }

        private void OnDestroy() {
            channel.OnEvent -= ChannelEventHandler;
        }

        private void ChannelEventHandler(TooltipContext context) {
            if (context.Active) tooltip.Value.EnableFor(context);
            else tooltip.Value.Disable();
        }
    }
}