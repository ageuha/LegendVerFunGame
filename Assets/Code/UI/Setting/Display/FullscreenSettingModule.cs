using Code.UI.Setting.BaseClass;

namespace Code.UI.Setting.Display {
    public class FullscreenSettingModule : ToggleSettingModule {
        public override void SetSettingValue(bool value) {
            SettingValue.Value = value;
        }

        protected override void AfterAwake() {
            base.AfterAwake();
            SetSettingValue(SettingSaveManager.Instance.IsFullscreen);
        }

        protected override void OnToggleButtonClicked(bool isOn) {
            base.OnToggleButtonClicked(isOn);
            SetSettingValue(isOn);
        }
    }
}