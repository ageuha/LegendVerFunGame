using System;
using System.Collections.Generic;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BoundsBuilding : GridBoundsObject, IPoolable
    {
        [SerializeField] private PathBakeEventSO _pathBakeEventSO;
        private Vector2Int _size;
        protected override Vector2Int Size => _size;
        private HealthSystem  _healthSystem;
        private Component _currentBuildingComponent; 
        private CooldownBar _cooldownBar;
        private BuildingTimer _timer;
        private BoxCollider2D _collider;
        public int InitialCapacity { get; protected set; }

        public void Initialize(Vector2Int size,Component currentBuilding,BuildingInitValue initValue ,float maxHealth, float timerTime)
        {
            _size = size;
            _healthSystem.Initialize(maxHealth);
            _healthSystem.ResetHealth();
            _currentBuildingComponent = currentBuilding;
            _timer ??= new BuildingTimer();
            IWaitable obj = GetComponentInChildren<IWaitable>();
            _cooldownBar ??= GetComponentInChildren<CooldownBar>();
            _collider ??= GetComponent<BoxCollider2D>();
            _collider.size = size;
            _timer.StartTimer(obj,_cooldownBar,timerTime,this,true);
            

        }
       
        protected virtual void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            GridManager.Instance.DeleteBuildingObject(WorldPos);
           
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            _healthSystem.OnDead -= HandleIDead;
        }

        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            Logging.Log($"건물 세팅됨. 위치 : {worldPos}");
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
            BuildingManager.Instance.SettingBuilding(new BuildingEvent(WorldPos, this));
            _pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Set,WorldPos,Size));
        }

        public override void DestroyFromGrid()
        {
            base.DestroyFromGrid();
            _pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Delete,WorldPos,Size));
        }
        
        
        #region TestCode

        private void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                _healthSystem.ApplyDamage(100);
            }
        }

        #endregion

        public void SettingChildComponent(Component c)
        {
            _currentBuildingComponent = c;
        }
        public virtual void OnPopFromPool()
        {
            _healthSystem ??= gameObject.GetComponentInChildren<HealthSystem>();
            _healthSystem.OnDead += HandleIDead;
        }

        public virtual void OnReturnToPool()
        {
            if(_currentBuildingComponent != null)
                Destroy(_currentBuildingComponent);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < _size.x; i++) 
            {
                for (int j = 0; j < _size.y; j++) 
                {
                    Gizmos.DrawWireCube(new Vector3(WorldPos.x + i + 0.5f, WorldPos.y + j + 0.5f),Vector3.one); 
                } 
            }
            
        }
    }
}