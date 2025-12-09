using System;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BoundsBuilding : GridBoundsObject, IPoolable
    {
        private Vector2Int _size;
        protected override Vector2Int Size => _size;
        private HealthSystem  _healthSystem;
        protected Component currentBuildingComponent; //자식에서 Init할때 세팅.
        public int InitialCapacity { get; protected set; }

        public void Initialize(Vector2Int size, float maxHealth)
        {
            _size = size;
            _healthSystem.Initialize(maxHealth);
            _healthSystem.ResetHealth();
        }
       
        protected virtual void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            GridManager.Instance.DeleteBuildingObject(WorldPos);
           
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,transform.position,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            _healthSystem.OnDead -= HandleIDead;
        }

        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            Logging.Log($"건물 세팅됨. 위치 : {worldPos}");
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
        }

        public void SettingChildComponent(Component c)
        {
            currentBuildingComponent = c;
        }
        public virtual void OnPopFromPool()
        {
            _healthSystem ??= gameObject.GetComponentInChildren<HealthSystem>();
            _healthSystem.OnDead += HandleIDead;
        }

        public virtual void OnReturnToPool()
        {
            Destroy(currentBuildingComponent);
            GridManager.Instance.DeleteBuildingObject(WorldPos);
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