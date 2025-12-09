using System;
using System.Collections.Generic;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BuildingManager : MonoSingleton<BuildingManager>
    {
        //[SerializeField] private BuildingEventSO buildingEvent;
        
        //private Dictionary<Vector2Int,GridObject>  _buildingData = new();
        
        protected override void Awake()
        {
            base.Awake();
            //buildingEvent.OnEvent += CreateBuilding;
        }

        /*private void HandleBuildBuildingEvent(BuildingEvent obj)
        {
            switch (obj.eventType)
            {
                case BuildingEventType.Create:
                    CreateBuilding(obj);
                    break;
                case BuildingEventType.Delete:
                    DestroyBuilding(obj);
                    break;
            }
                
           
        }*/
        public void DestroyBuilding(BuildingEvent obj) //삭제하려는 오브젝트를 받아서 삭제시킴.
        {
            GridManager.Instance.DeleteBuildingObject(obj.buildCellPosition);
            //_buildingData.Remove(obj.buildCellPosition);
        }

        /*public void CreateBuilding(BuildingEvent obj)
        {
            GridObject building = null;
           

            if (building != null)
            {
                GridManager.Instance.SetBuildingObject(obj.buildCellPosition,building);
                _buildingData[obj.buildCellPosition] = building;
            }
        }*/
        
        
    }
}