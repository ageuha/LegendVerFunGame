using UnityEngine;

namespace Member.JJW.Code.Weather
{
    public class Foggy : Weather
    {
        [SerializeField] private ParticleSystem foggyParticles;
        public override void OnStart()
        {
            foggyParticles.Play();
            FadeToTargetColor(targetColor);
        }

        public override void OnStop()
        {
            foggyParticles.Stop();
        }
    }
}