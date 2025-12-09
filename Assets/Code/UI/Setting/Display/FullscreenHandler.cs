using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class FullscreenHandler : MonoBehaviour, ISettingValueHandler<bool> {
        public void Initialize(SettingType settingType) {
        }

        public void OnValueChanged(bool prev, bool current) {
            Screen.fullScreen = current;
            SettingSaveManager.Instance.IsFullscreen = current;
        }
    }
}