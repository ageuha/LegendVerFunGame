using Code.Core.Utility;
using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.BaseClass {
    public abstract class SettingModule<T> : MonoBehaviour where T : struct {
        [SerializeField] protected SerializeHelper<ISettingValueHandler<T>> valueHandler;
        protected NotifyValue<T> SettingValue;
        [field: SerializeField] public SettingType SettingType { get; private set; }
        public IReadOnlyNotifyValue<T> ExposedValue => SettingValue;

        protected virtual void Awake() {
            SettingValue = new NotifyValue<T>();
            valueHandler.Value.Initialize(SettingType);
            SettingValue.OnValueChanged += valueHandler.Value.OnValueChanged;
            AfterAwake();
        }

        private void OnDestroy() {
            SettingValue.OnValueChanged -= valueHandler.Value.OnValueChanged;
        }

        protected virtual void AfterAwake() {
        }

        public abstract void SetSettingValue(T value);
    }
}