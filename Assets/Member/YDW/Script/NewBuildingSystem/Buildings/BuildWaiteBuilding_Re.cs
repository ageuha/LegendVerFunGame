using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core.Pool;
using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class BuildWaiteBuilding_Re : GridBoundsObject  , IPoolable
    {
        [SerializeField] private BuildingEventSO BuildingEventSO; 
        [SerializeField] private CooldownBar _cooldownBar;
        
        private SpriteRenderer _spriteRenderer;
        public int InitialCapacity { get; } = 10;
        
        private Vector2Int _size;
        private bool _canceled = false;
        protected override Vector2Int Size => _size;

        private void Awake()
        {
            gameObject.SetActive(false);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public IEnumerator OnBuildingWaiteBuilding(Vector2Int size,BuildingDataSO buildingData) //본인 또한 세팅이 되어야 함.
        {
            _cooldownBar.SetFillAmount(buildingData.BuildTime);
            yield return new WaitUntil(() => !_cooldownBar.gameObject.activeSelf || _canceled);
            _cooldownBar.gameObject.SetActive(false); 
            gameObject.SetActive(false);
            GridManager.Instance.DeleteBuildingObject(WorldPos);
            if (_canceled)
            {
                //페이백 아이템 떨궈줌.
                _canceled = false;
                yield break;
            }
            BuildingEventSO.Raise(new BuildingEvent(WorldPos,transform.position,buildingData.Building)); //생성
            PoolManager.Instance.Factory<BuildWaiteBuilding_Re>().Push(this);
        }

        


        public void OnPopFromPool()
        {
            
        }

        public void OnReturnToPool()
        {
            
        }
    }
}