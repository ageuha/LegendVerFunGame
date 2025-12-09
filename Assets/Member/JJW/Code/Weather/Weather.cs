using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Member.JJW.Code.Weather
{
    public abstract class Weather : MonoBehaviour
    {
        public Color targetColor;
        public Volume volume;
        private ColorAdjustments _colorAdjustments;
        void Start()
        {
            volume.profile.TryGet(out _colorAdjustments);
        }
        public abstract void OnStart();
        public abstract void OnStop();

        protected void ChangeVolumeWeight(float targetVolumeWeight)
        {
            DOTween.To(() => volume.weight, x => volume.weight = x, targetVolumeWeight, 3);
        }
        protected void FadeToTargetColor(Color targetColor)
        {
            if (_colorAdjustments == null) return;
            DOTween.To(() => _colorAdjustments.colorFilter.value, x => _colorAdjustments.colorFilter.value = x, targetColor, 1);
        }
    }
}