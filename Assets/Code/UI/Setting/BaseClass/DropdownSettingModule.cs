using TMPro;
using UnityEngine;

namespace Code.UI.Setting.BaseClass {
    public abstract class DropdownSettingModule<T> : SettingModule<T> where T : struct {
        [SerializeField] protected TMP_Dropdown dropdown;

        protected sealed override void Awake() {
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
            base.Awake();
        }

        protected virtual void OnDropdownValueChanged(int index) {
        }
    }
}