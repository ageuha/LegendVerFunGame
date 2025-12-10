using System;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.EventStruct;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class UnitBuilding : GridUnitObject , IPoolable
    {
        private HealthSystem  _healthSystem;
        private Component _currentBuildingComponent;
        private CooldownBar _cooldownBar;
        private BuildingTimer _timer;
        public int InitialCapacity { get; }
        
        public void Initialize(Component currentBuildingCompo,float maxHealth, float timerTime)
        {
            _healthSystem.Initialize(maxHealth);
            _healthSystem.ResetHealth();
            _currentBuildingComponent = currentBuildingCompo;
            _timer ??= new BuildingTimer();
            IWaitable obj = GetComponent<IWaitable>();
            _cooldownBar ??= GetComponentInChildren<CooldownBar>();
            _timer.StartTimer(obj,_cooldownBar,timerTime,this);
        }

        #region TestCode

        private void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                _healthSystem.ApplyDamage(1);
            }
        }

        #endregion
        private void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            GridManager.Instance.DeleteBuildingObject(WorldPos);
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            _healthSystem.OnDead -= HandleIDead;
        }
        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
            BuildingManager.Instance.SettingBuilding(new BuildingEvent(WorldPos, this));
        }
        public void SettingChildComponent(Component c)
        {
            _currentBuildingComponent = c;
        }

        public void OnPopFromPool()
        {
            _healthSystem ??= gameObject.GetComponentInChildren<HealthSystem>();
            _healthSystem.OnDead += HandleIDead;
            
        }

        public void OnReturnToPool()
        {
            if(_currentBuildingComponent != null)
                Destroy(_currentBuildingComponent);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
           
            Gizmos.DrawWireCube(new Vector3(WorldPos.x + 0.5f, WorldPos.y + 0.5f),Vector3.one); 
                
            
            
        }
    }
}