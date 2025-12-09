using Code.Core.GlobalSO;
using Code.UI.Setting.Enums;
using Code.UI.Setting.Interfaces;
using UnityEngine;

namespace Code.UI.Setting.Volume {
    public class VolumeHandler : MonoBehaviour, ISettingValueHandler<float> {
        [SerializeField] private AudioMixerSO mixerSO;

        private SettingType _settingType;

        public void Initialize(SettingType settingType) {
            _settingType = settingType;
        }

        public void OnValueChanged(float prev, float current) {
            mixerSO.SetNormalized(current);
            SettingSaveManager.Instance.SetFloat(_settingType, current);
        }
    }
}