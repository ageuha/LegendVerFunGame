using System;
using System.Collections.Generic;
using Member.YDW.Script.BuildingSystem.EventStruct;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingManagerEventSO buildingManagerEventSO;
        private Dictionary<IBuilding,List<NodeData>> _buildings = new(); //빌딩들의 노드 리스트.

        private void Awake()
        {
            buildingManagerEventSO.OnEvent += HandleBuildingManagerEvent;
        }

        private void HandleBuildingManagerEvent(BuildingManagerEvent obj)
        {
            
        }

        private void OnDestroy()
        {
            buildingManagerEventSO.OnEvent -= HandleBuildingManagerEvent;
        }
    }
}