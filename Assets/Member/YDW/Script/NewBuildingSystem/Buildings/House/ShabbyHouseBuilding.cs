using System;
using Code.Core.Utility;
using Member.JJW.Code.TemperSystem;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem.Buildings.House
{
    public class ShabbyHouseBuilding : BoundsBuilding, IBuilding , IWaitable
    {
          private float _detectRange;
        private float _temperatureValue;
        public bool IsActive { get; private set; }
        public bool IsWaiting { get; private set; }
        
        public BuildingDataSO BuildingData { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        private LayerMask _playerMask;
        
        private TemperatureSystem _playerTemperatureSystem;

        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            SpriteRenderer ??= GetComponentInChildren<SpriteRenderer>();
            SpriteRenderer.sprite = BuildingData.Image;
            _detectRange = buildingData.InitValue.valueFloat;
            _playerMask = 1 << LayerMask.NameToLayer("Player");
        }


        private void Update()
        {
            if(!IsActive) return;
            #region TestInput

            if (Keyboard.current.fKey.wasPressedThisFrame)
                InPlayer();
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                OutPlayer();
            if (_playerTemperatureSystem != null)
            {
                if (_playerTemperatureSystem.CurrentTemperature < _temperatureValue)
                {
                    _playerTemperatureSystem.CurrentTemperature =  _temperatureValue;
                }
                 
                Logging.Log(_playerTemperatureSystem.CurrentTemperature);
                
            }
                

            #endregion
        }

        private void OutPlayer()
        {
            //기타 등등 필요한 처리.
            _playerTemperatureSystem = null;
        }

        private void InPlayer()
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position,_detectRange, _playerMask);
            Logging.Log(player == null);
            if (player != null && player.gameObject.TryGetComponent(out TemperatureSystem system) && _playerTemperatureSystem == null)
            {
                Logging.Log("시스템 세팅");
                player.transform.position = transform.position;
                //필요한거 처리.
                _playerTemperatureSystem = system;
            }

            if (player.gameObject.TryGetComponent(out TemperatureSystem systea))
            {
                Logging.Log("가져오기 성공");
            }
            else
                Logging.Log("가져오기 실패");
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
            }
            IsWaiting = waiting;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
        }
    }
}