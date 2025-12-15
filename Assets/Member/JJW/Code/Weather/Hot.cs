using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.JJW.Code.Weather
{
    public class Hot : Weather
    {
        [SerializeField] private GameObject hotEffect;
        public override void OnStart()
        {
            FadeToTargetColor(targetColor);
            ChangeVolumeWeight(0.4f);
            hotEffect.SetActive(true);
        }

        public override void OnStop()
        {
            ChangeVolumeWeight(0);
            hotEffect.SetActive(false);
        }
    }
}