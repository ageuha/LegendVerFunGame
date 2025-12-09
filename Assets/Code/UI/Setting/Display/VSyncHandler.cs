using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class VSyncHandler : MonoBehaviour, ISettingValueHandler<bool> {
        public void Initialize(SettingType settingType) {
        }

        public void OnValueChanged(bool prev, bool current) {
            QualitySettings.vSyncCount = current ? 1 : 0;
            SettingSaveManager.Instance.VSync = current;
        }
    }
}