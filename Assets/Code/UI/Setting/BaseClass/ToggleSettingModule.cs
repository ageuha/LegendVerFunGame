using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Setting.BaseClass {
    public abstract class ToggleSettingModule : SettingModule<bool> {
        [SerializeField] protected Toggle toggleButton;

        protected sealed override void Awake() {
            toggleButton.onValueChanged.AddListener(OnToggleButtonClicked);
            base.Awake();
        }

        protected virtual void OnToggleButtonClicked(bool isOn) {
        }
    }
}