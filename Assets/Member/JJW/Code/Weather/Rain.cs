using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Member.JJW.Code.Weather
{
    public class Rain : Weather
    {
        [SerializeField] private ParticleSystem rainParticle;
        
        public override void OnStart()
        {
            rainParticle.Play();
            FadeToTargetColor(targetColor);
            ChangeVolumeWeight(0.4f);
        }

        public override void OnStop()
        {
            rainParticle.Stop();
            ChangeVolumeWeight(0);
        }
    }
}