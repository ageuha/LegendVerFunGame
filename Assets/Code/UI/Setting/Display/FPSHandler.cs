using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.Display {
    public class FPSHandler : MonoBehaviour, ISettingValueHandler<int> {
        public void Initialize(SettingType settingType) {
        }

        public void OnValueChanged(int prev, int current) {
            Application.targetFrameRate = current;
            SettingSaveManager.Instance.TargetFrame = current;
        }
    }
}