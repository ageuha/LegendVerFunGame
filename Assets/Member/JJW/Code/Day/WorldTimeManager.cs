using System;
using Code.Core.Utility;
using UnityEngine;

namespace Member.JJW.Code.Day
{
    public class WorldTimeManager : MonoSingleton<WorldTimeManager>
    {
        public event Action<int> UpdateDay;
        public event Action<int> UpdateHour;
        
        private int _dayCount = 1;
        private int _hourCount = 12;
        private float _timer;

        private void Start()
        {
            UpdateDay?.Invoke(_dayCount);
            UpdateHour?.Invoke(_hourCount);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= 1) //만약 60분이 지났다면
            {
                _hourCount++;
                if (_hourCount >= 24) //24시간이 지났다면
                {
                    _hourCount = 0;
                    _dayCount++;
                    UpdateHour?.Invoke(_hourCount);
                    UpdateDay?.Invoke(_dayCount);
                    _timer = 0;
                    return;
                }
                UpdateHour?.Invoke(_hourCount);
                _timer = 0;
            }
        }
    }
}