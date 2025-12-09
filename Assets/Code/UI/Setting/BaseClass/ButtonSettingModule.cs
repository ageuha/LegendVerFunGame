using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Setting.BaseClass {
    public abstract class ButtonSettingModule<T> : SettingModule<T> where T : struct {
        [SerializeField] protected Button leftButton;
        [SerializeField] protected Button rightButton;

        protected sealed override void Awake() {
            rightButton.onClick.AddListener(OnRightButtonClicked);
            leftButton.onClick.AddListener(OnLeftButtonClicked);
            base.Awake();
        }

        protected virtual void OnLeftButtonClicked() {
        }

        protected virtual void OnRightButtonClicked() {
        }
    }
}