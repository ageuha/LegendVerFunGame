using Code.Core.Pool;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class UnitBuilding : GridUnitObject , IPoolable
    {
        private HealthSystem  _healthSystem;
        private Component _currentBuildingComponent;
        public int InitialCapacity { get; }
        
        private void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            GridManager.Instance.DeleteBuildingObject(WorldPos);
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,transform.position,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            _healthSystem.OnDead -= HandleIDead;
        }
        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
        }
        public void SettingChildComponent(Component c)
        {
            _currentBuildingComponent = c;
        }

        public void OnPopFromPool()
        {
            _healthSystem ??= gameObject.GetComponentInChildren<HealthSystem>();
            _healthSystem.ResetHealth();
            
        }

        public void OnReturnToPool()
        {
            
        }
    }
}