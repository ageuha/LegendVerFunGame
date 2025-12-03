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
        private Dictionary<NodeData, IBuilding> _buildings = new();

        private void Awake()
        {
            buildingManagerEventSO.OnEvent += HandleBuildingManagerEvent;
        }

        private void HandleBuildingManagerEvent(BuildingManagerEvent obj)
        {
            
        }
    }
}