using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Member.JJW.Code.Weather
{
    public class Hot : Weather
    {
        public override void OnStart()
        {
            FadeToTargetColor(targetColor);
            ChangeVolumeWeight(0.4f);
        }

        public override void OnStop()
        {
            ChangeVolumeWeight(0);
        }
    }
}