using System;
using Code.Core.Pool;
using Code.GridSystem.Objects;
using Member.JJW.Code.TemperSystem;
using UnityEngine;

namespace Member.JJW.Code.TemperItem
{
    public class Bonfire : GridBoundsObject,IPoolable
    {
        public int InitialCapacity { get; }
        [SerializeField] private Vector2Int size;
        [SerializeField] private int initialCapacity;
        [SerializeField] private float raisingAmount;
        [SerializeField] private float useTime;

        protected override Vector2Int Size { get => size; }
        private float _usingStartTime;
        private TemperatureSystem _temperatureSystem;
        private bool _isUsing = false;


        public void Init(TemperatureSystem  temperatureSystem) //초기화 해주기
        {
            _temperatureSystem = temperatureSystem;
            PoolManager.Instance.Factory<Bonfire>().Pop();
            _usingStartTime = Time.time;
        }

        public void Use() //플레이어가 범위안에 들어왔을떄 사용
        {
            _isUsing = true;
        }

        private void Update()
        {
            if (Time.time - _usingStartTime >= useTime)
            {
                PoolManager.Instance.Factory<Bonfire>().Push(this);
            }

            else if (_isUsing)
            {
                _temperatureSystem.CurrentTemperature += raisingAmount * Time.deltaTime;
                _isUsing = false;
            }
        }

        public void OnPopFromPool()
        {
            
        }

        public void OnReturnToPool()
        {
            
        }
    }
}