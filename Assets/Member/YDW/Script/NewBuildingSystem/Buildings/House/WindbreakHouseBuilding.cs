using Member.JJW.Code.TemperSystem;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem.Buildings.House
{
    public class WindbreakHouseBuilding : BoundsBuilding, IBuilding, IWaitable
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
            if (player != null && player.TryGetComponent(out TemperatureSystem system) && _playerTemperatureSystem == null)
            {
                //필요한거 처리.
                _playerTemperatureSystem = system;
            }
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
            }
            IsWaiting = waiting;
        }
    }
}