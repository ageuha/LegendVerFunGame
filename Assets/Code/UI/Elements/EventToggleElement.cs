using System;
using Code.Core.GlobalInterfaces;
using Code.Core.Utility;

namespace Code.UI.Elements {
    public class EventToggleElement : ToggleElement, ISubscribable<bool> {
        public event Action<bool> OnValueChanged;

        protected override void AfterEnable(bool item) {
            base.AfterEnable(item);
            OnValueChanged?.Invoke(item);
        }

        public void Subscribe(Action<bool> action) {
            OnValueChanged += action;
        }

        public void Unsubscribe(Action<bool> action) {
            OnValueChanged -= action;
        }
    }
}