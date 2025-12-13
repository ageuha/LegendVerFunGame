using Code.Core.Pool;
using Member.JJW.Code.TemperSystem;
using UnityEngine;

namespace Member.JJW.Code.TemperItem
{
    public class Fan : MonoBehaviour
    {
        [SerializeField] private float downTemperatureValue;

        public void Use(TemperatureSystem temperatureSystem)
        {
            temperatureSystem.CurrentTemperature -= downTemperatureValue;
        }
    }
}