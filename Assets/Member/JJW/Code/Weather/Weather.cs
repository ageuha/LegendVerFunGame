using UnityEngine;

namespace Member.JJW.Code.Weather
{
    public abstract class Weather : MonoBehaviour
    {
        public abstract void OnStart();
        public abstract void OnStop();
    }
}