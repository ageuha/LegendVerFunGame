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
        protected HealthSystem  healthSystem;
        protected Component currentBuildingComponent; //자식에서 Init할때 세팅.
        public int InitialCapacity { get; protected set; }

        public void Initialize(Vector2Int size)
        {
            _size = size;
        }
       
        private void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            GridManager.Instance.DeleteBuildingObject(WorldPos);
           
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,transform.position,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            healthSystem.OnDead -= HandleIDead;
        }

        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            Logging.Log($"건물 세팅됨. 위치 : {worldPos}");
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            //transform.position += new Vector3(0.5f, 0.5f, 0);
        }

        public void SettingChildComponent(Component c)
        {
            currentBuildingComponent = c;
        }
        public virtual void OnPopFromPool()
        {
            
        }

        public virtual void OnReturnToPool()
        {
            Destroy(currentBuildingComponent);
            GridManager.Instance.DeleteBuildingObject(WorldPos);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(transform.position, new Vector3(_size.x,_size.y,0f));
            
        }
    }
}