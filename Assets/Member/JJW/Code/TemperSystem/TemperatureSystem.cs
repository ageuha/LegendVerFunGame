using System;
using System.Collections;
using System.Collections.Generic;
using Code.EntityScripts;
using Member.JJW.Code.Weather;
using Member.JJW.EventChannel;
using Member.KJW.Code.CombatSystem;
using Member.KJW.Code.CombatSystem.DamageSystem;
using Member.KJW.Code.Player;
using UnityEngine;

namespace Member.JJW.Code.TemperSystem
{
    public class TemperatureSystem : MonoBehaviour
    {
        [SerializeField] private FloatEventChannel floatEventChannel;
        [SerializeField] private DamageInfo damageInfo;
        private AgentMovement _agentMovement;
        private float _damagePercent;
        private Player _playerHealth;
        private float _currentTemperature;
        private Coroutine _damageRoutine;
        private void Awake()
        {
            _playerHealth = GetComponent<Player>();
            _agentMovement = GetComponent<AgentMovement>();
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
                CurrentTemperature -= Time.deltaTime * 0.02f;
            }
            else if(WeatherManager.Instance.CurrentState == WeatherState.HeavyRain)
            {
                CurrentTemperature -= Time.deltaTime * 0.005f;
            }
            else if(WeatherManager.Instance.CurrentState == WeatherState.Hot)
            {
                CurrentTemperature += Time.deltaTime * 0.02f;
            }
        }
        private void CheckTemperature(float temperature)
        {
            if (temperature <= 32) //체력감소
            {
                StartDamage();
            }

            else if (temperature <= 35) //이동속도 감소
            {
                _agentMovement.SetMultiValue(0.7f);
            }
            
            else if (temperature <= 37.5) //정상체온
            {
                _damagePercent = 0;
                _agentMovement.SetMultiValue(1f);
                StopDamage();
            }
            
            else if (temperature <= 40) //이동속도 감소
            {
                _agentMovement.SetMultiValue(0.7f);
            }
            
            else if (temperature <= 42) //체력감소
            {
                StartDamage();
            }
            
            else //체력감소
            {
                StartDamage();
            }
        }
        private void StartDamage()
        {
            if (_damageRoutine == null)
                _damageRoutine = StartCoroutine(DamageOverTime());
        }

        private void StopDamage()
        {
            if (_damageRoutine != null)
            {
                StopCoroutine(_damageRoutine);
                _damageRoutine = null;
            }
        }
        private IEnumerator DamageOverTime()
        {
            while (true)
            {
                _playerHealth.GetDamage(damageInfo); // 실질적인 데미지
                yield return new WaitForSeconds(1f); // 1초마다 딜
            }
        }    }
}