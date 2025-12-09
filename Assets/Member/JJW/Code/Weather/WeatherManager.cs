using System;
using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

// 제거: using Action = Unity.AppUI.Redux.Action;

namespace Member.JJW.Code.Weather
{
    public enum WeatherState
    {
        Sunny,
        Rain,
        HeavyRain,
        Foggy,
        Hot
    }

    public class WeatherManager : MonoSingleton<WeatherManager>
    {
        [SerializeField] private Rain rain; 
        [SerializeField] private HeavyRain heavyRain;
        [SerializeField] private Foggy foggy;
        [SerializeField] private Hot hot;
        [SerializeField] private Sunny sunny;
        
        private Dictionary<WeatherState, Weather> _weatherDictionary = new Dictionary<WeatherState, Weather>();
        private WeatherState _currentState;
        public WeatherState CurrentState
        {
            get => _currentState;                                       
            set
            {
                if (_currentState != null)
                {
                    _weatherDictionary[_currentState].OnStop();
                }
                _currentState = value;
                Debug.Log(_currentState + "로 날씨 바뀜");
                _weatherDictionary[value].OnStart();
                
            }
        }

        public WeatherState GetRandomWeather()
        {
            int rand = Random.Range(0, 12);
            if (rand <= 4)
            {
                return WeatherState.Sunny;
            }
            else if (rand <= 6)
            {
                return WeatherState.Rain;
            }
            else if(rand <= 7)
            {
                return WeatherState.HeavyRain;
            }
            else if (rand <= 9)
            {
                return WeatherState.Foggy;
            }
            return WeatherState.Hot;
        }
        private void Start()
        {
            _weatherDictionary.Add(WeatherState.Sunny,sunny);
            _weatherDictionary.Add(WeatherState.Rain,rain);
            _weatherDictionary.Add(WeatherState.HeavyRain,heavyRain);
            _weatherDictionary.Add(WeatherState.Foggy,foggy);
            _weatherDictionary.Add(WeatherState.Hot,hot);
        }
    }
}