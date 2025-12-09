using Code.UI.Setting.BaseClass;

namespace Code.UI.Setting.Display {
    public class VSyncSettingModule : ToggleSettingModule {
        public override void SetSettingValue(bool value) {
            SettingValue.Value = value;
        }

        protected override void AfterAwake() {
            base.AfterAwake();
            SetSettingValue(SettingSaveManager.Instance.VSync);
        }

        protected override void OnToggleButtonClicked(bool isOn) {
            base.OnToggleButtonClicked(isOn);
            SetSettingValue(isOn);
        }
    }
}