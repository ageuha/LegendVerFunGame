using Code.Core.GlobalSO;
using UnityEngine;

namespace Code.UI.Setting.Volume {
    public class VolumeHandler : MonoBehaviour, ISettingValueHandler<float> {
        [SerializeField] private AudioMixerSO mixerSO;

        public void OnValueChanged(float prev, float current) {
            mixerSO.SetNormalized(current);
        }
    }
}