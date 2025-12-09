using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class ResolutionHandler : MonoBehaviour, ISettingValueHandler<Resolution> {
        public void Initialize(SettingType settingType) {
        }

        public void OnValueChanged(Resolution prev, Resolution current) {
            Screen.SetResolution(current.width, current.height, Screen.fullScreen);
            SettingSaveManager.Instance.Resolution = new Vector2Int(current.width, current.height);
        }
    }
}