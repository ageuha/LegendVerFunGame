using System;
using Code.EntityScripts;
using Member.JJW.Code.Weather;
using Member.JJW.EventChannel;
using UnityEngine;

namespace Member.JJW.Code.TemperSystem
{
    public class TemperatureSystem : MonoBehaviour
    {
        [SerializeField] private FloatEventChannel floatEventChannel;
        private float _damagePercent;
        private HealthSystem _playerHealth;
        private float _currentTemperature;

        private void Awake()
        {
            _playerHealth = GetComponent<HealthSystem>();
            CurrentTemperature = 36.5f;
        }

        public float CurrentTemperature
        {
            get => _currentTemperature;
            set
            {
                _currentTemperature = value;
                floatEventChannel.Raise(_currentTemperature);
                CheckTemperature(_currentTemperature);
            }
        }
        private void Update()
        {
            if (WeatherManager.Instance.CurrentState == WeatherState.Rain)
            {
                CurrentTemperature -= Time.deltaTime * 0.3f;
            }
            else if(WeatherManager.Instance.CurrentState == WeatherState.HeavyRain)
            {
                CurrentTemperature -= Time.deltaTime * 0.5f;
            }
            else if(WeatherManager.Instance.CurrentState == WeatherState.Hot)
            {
                CurrentTemperature += Time.deltaTime * 0.4f;
            }

            if (_damagePercent > 0)
            {
                _playerHealth.ApplyDamage(Time.deltaTime * _damagePercent);
            }
        }
        private void CheckTemperature(float temperature)
        {
            if (temperature <= 32) //체력감소
            {
                _damagePercent = 0.5f;
            }

            else if (temperature <= 35) //이동속도 감소
            {
                
            }
            
            else if (temperature <= 37.5) //정상체온
            {
                Debug.Log("정상체온");
                _damagePercent = 0;
            }
            
            else if (temperature <= 40) //이동속도 감소
            {
                
            }
            
            else if (temperature <= 42) //체력감소
            {
                _damagePercent = 0.5f;
            }
            
            else //체력감소
            {
                _damagePercent = 0.7f;
            }
        }

    }
}