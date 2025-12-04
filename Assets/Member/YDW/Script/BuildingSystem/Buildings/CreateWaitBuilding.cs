using System;
using System.Collections.Generic;
using Code.Core.Utility;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem.Buildings
{
    public class CreateWaitBuilding : MonoBehaviour , IBuilding
    {
        [field:SerializeField] public SerializeHelper<ICooldownBar> CooldownBar { get; private set; }
        public List<NodeData> CurrentNodeData { get; private set; }
        [SerializeField] public BuildingDataSO BuildingData { get; private set; }
        public void Initialize(BuildingDataSO buildingData, List<NodeData> nodeData)
        {
            CurrentNodeData = nodeData;
            BuildingData = buildingData;
        }

        public void DestroyedBuilding()
        {
           
        }

        private void OnDestroy()
        {
            //지금은 삭제에서 해주지만, 추후 풀 메니저에서 수거할 때, 실행 할 예정이다.
            ValueProvider.Instance.NodeDataManager.eventSO.Raise(new NodeDataManagerEvent(NodeDataManagerEventType.Remove,
                ObjectType.Building,this,CurrentNodeData));
        }
    }
}