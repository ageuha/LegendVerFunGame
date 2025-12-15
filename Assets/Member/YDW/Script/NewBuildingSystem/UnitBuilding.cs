using Code.Core.Pool;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using Member.YTH.Code.Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public abstract class UnitBuilding : GridUnitObject , IPoolable
    {
        [SerializeField] private ReturnItemStruct[] items;
        [SerializeField] private PathBakeEventSO pathBakeEventSO; 
        [SerializeField] protected CooldownBar cooldownBar;
        private HealthSystem  _healthSystem;
        private Component _currentBuildingComponent;
        protected BuildingTimer timer;
        private BuildingDataSO _dataSO;
        public int InitialCapacity { get; }
        
        public void Initialize(BuildingDataSO dataSO,float maxHealth)
        {
            _dataSO = dataSO;
            _healthSystem.Initialize(maxHealth);
            _healthSystem.ResetHealth();
            
            timer ??= new BuildingTimer();
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
        private void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            DropReturnItem();
            GridManager.Instance.DeleteBuildingObject(WorldPos);
            BuildingManager.Instance.DestroyBuilding(new BuildingEvent(WorldPos,this)); //자신의 셀 위치와 월드 위치, 그리고 객체를 넘김.
            _healthSystem.OnDead -= HandleIDead;
        }
        
        private void DropReturnItem()
        {
            for (int i = 0; i < items.Length; i++)
            { 
                ReturnItemStruct returnItem = items[i];
                ItemObject item = PoolManager.Instance.Factory<ItemObject>().Pop();
                item.SetItemData(returnItem.item,returnItem.amount);
            }
        }
        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
            pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Set,WorldPos,Size));
        }

        public override void DestroyFromGrid()
        {
            base.DestroyFromGrid();
            pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Delete,WorldPos,Size));
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