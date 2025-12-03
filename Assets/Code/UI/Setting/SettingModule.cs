using Code.Core.Utility;
using Code.UI.Setting.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Setting {
    public abstract class SettingModule<T> : MonoBehaviour where T : struct {
        [SerializeField] protected Button leftButton;
        [SerializeField] protected Button rightButton;
        [SerializeField] protected SerializeHelper<ISettingValueHandler<T>> valueHandler;

        protected NotifyValue<T> SettingValue;
        public IReadOnlyNotifyValue<T> ExposedValue => SettingValue;

        private void Awake() {
            leftButton.onClick.AddListener(OnLeftButtonClicked);
            rightButton.onClick.AddListener(OnRightButtonClicked);
            SettingValue = new NotifyValue<T>();
            SettingValue.OnValueChanged += valueHandler.Value.OnValueChanged;
            AfterAwake();
        }

        private void OnDestroy() {
            SettingValue.OnValueChanged -= valueHandler.Value.OnValueChanged;
        }

        protected virtual void OnLeftButtonClicked() {
        }

        protected virtual void OnRightButtonClicked() {
        }

        protected virtual void AfterAwake() {
        }
        
        public abstract void SetSettingValue(T value);
    }
}