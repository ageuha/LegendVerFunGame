using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.JJW.Code.Weather
{
    public class HeavyRain : Weather
    {
        [SerializeField] private ParticleSystem heavyRainParticles;
        public override void OnStart()
        {
            ChangeVolumeWeight(0.6f);
            FadeToTargetColor(targetColor);
            heavyRainParticles.Play();
        }

        public override void OnStop()
        {
            heavyRainParticles.Stop();
            ChangeVolumeWeight(0);
        }
    }
}