using Code.Core.Pool;
using Code.Core.Utility;
using Code.EntityScripts;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using Member.YTH.Code.Item;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public abstract class BoundsBuilding : GridBoundsObject, IPoolable
    {
        [SerializeField] private ReturnItemStruct[] items;
        [SerializeField] private PathBakeEventSO pathBakeEventSO;
        [SerializeField] protected CooldownBar cooldownBar;
        protected override Vector2Int Size => _size;
        private Vector2Int _size;
        protected HealthSystem  _healthSystem;
        protected BuildingTimer timer;
        public int InitialCapacity { get; protected set; }

        protected void Initialize(Vector2Int size,float maxHealth)
        {
            _size = size;
            _healthSystem.Initialize(maxHealth);
            _healthSystem.ResetHealth();
           
            timer ??= new BuildingTimer();
        }
       
        protected virtual void HandleIDead()
        {
            //아마 추후 이곳에서 아이템 다시 드랍해줄 듯.
            Logging.Log("Im Dead");
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
            Logging.Log($"건물 세팅됨. 위치 : {worldPos}");
            transform.position = GridManager.Instance.GetCellToWorldPosition(worldPos);
            transform.position += new Vector3(0.5f,0, 0);
            pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Set,WorldPos,Size));
        }

        public override void DestroyFromGrid()
        {
            base.DestroyFromGrid();
            pathBakeEventSO.Raise(new RunTimeBakeEvent(RunTimeBakeEventType.Delete,WorldPos,Size));
        }
        
        public virtual void OnPopFromPool()
        {
            _healthSystem ??= gameObject.GetComponentInChildren<HealthSystem>();
            _healthSystem.OnDead += HandleIDead;
        }

        public virtual void OnReturnToPool()
        {
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