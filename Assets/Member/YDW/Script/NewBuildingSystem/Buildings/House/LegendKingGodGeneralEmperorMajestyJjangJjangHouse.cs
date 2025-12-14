using Code.Core.Pool;
using Member.JJW.Code.TemperSystem;
using Member.YDW.Script.BuildingSystem;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem.Buildings.House
{
    public class LegendKingGodGeneralEmperorMajestyJjangJjangHouse : BoundsBuilding, IBuilding , IWaitable
    {
        [Header("SettingValues")]
        [SerializeField] private  float detectRange;
        [SerializeField] private float temperatureValue;
        [SerializeField] private LayerMask playerMask;
        

        //드롭 아이템 구조체 넣어줄 예정.
        public bool IsActive { get; private set; }
        public bool IsWaiting { get; private set; }
        

        public BuildingDataSO BuildingData { get; private set; }

        private TemperatureSystem _playerTemperatureSystem;

        public void InitializeBuilding(BuildingDataSO buildingData)
        { 
            BuildingData = buildingData;
            Initialize(buildingData.BuildingSize,buildingData.MaxHealth);
            timer.StartTimer(this,cooldownBar,buildingData.BuildTime,this,true);
            
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
                if (_playerTemperatureSystem.CurrentTemperature < temperatureValue)
                {
                    _playerTemperatureSystem.CurrentTemperature =  temperatureValue;
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
            Collider2D player = Physics2D.OverlapCircle(transform.position,detectRange, playerMask);
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
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
        
    }
}