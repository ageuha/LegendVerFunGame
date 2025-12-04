/*using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem.Buildings;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using NUnit.Framework.Internal;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingBuilder : MonoBehaviour
    {
        [field: SerializeField] public BuildingEventSO BuildingEventSO { get; private set; }
     private bool IsDisable;
        private void Awake()
        {
            BuildingEventSO.OnEvent += BuildBuilding;
        }

        private void OnEnable()
        {
            IsDisable = false;
        }

        private void BuildBuilding(BuildingEvent obj)
        {
            if (CanBuild(obj))
            {
                OnCreate(obj);
            }
        }

        private void OnCreate(BuildingEvent obj)
        {
            Logging.Log("건물 생성");
            _ = CooldownBuild(obj);
        }

        private bool CanBuild(BuildingEvent obj)
        {
            if (obj.buildNode.cellPosition == new Vector3Int(12312399,12312399,12312399))
            {
                Logging.Log("정상적인 위치 노드 선택바람.");
                return false;
            }
            List<NodeData> nodeDatas = GetBuildingSizeNodes(obj.buildNode, obj.buildingData);

            foreach (NodeData nodeData in nodeDatas)
            {
                if (ValueProvider.Instance.NodeDataManager.HasObject(nodeData))
                {
                    Logging.Log("건물을 겹치게 설치할 수 없습니다.");
                    //재화 페이백 실행.
                    return false;
                }
                    
            }

            Collider2D obstacle = Physics2D.OverlapBox(new Vector3(obj.buildNode.worldPosition.x,obj.buildNode.worldPosition.y - obj.buildingData.BuildingSize.y/3f),
                new Vector3(obj.buildingData.BuildingSize.x,obj.buildingData.BuildingSize.y/3f,0), 0); //어떻게 될진 모르겠으나, 따로 장애물을 레이어로 나누던가 뭔가 필터링 조취를 취해야 할듯.
            if (obstacle != null)
            {
                Logging.LogWarning("장애물이 존재합니다."); 
                return false;
            }
            //이미 건물이 존재합니다.
            //재화 체크는 여기서 필요하면 해줌.
            //추후 최대 한번에 생성 가능한 건물 대기열 수 같은게 생기면 추가로 조건 넣어야 할듯.
            return true;
        }

        private async Task CooldownBuild(BuildingEvent obj) //이거 광클을 막아야 할 것 같은데, 아마 여기서가 아니라, 아이템 쪽에서 바로 지워주면 될거 같긴 함.
        {
            CreateWaiteBuilding(obj);
            float buildTime = obj.buildingData.BuildTime * 1000f;
            await Task.Delay((int)buildTime);
            if(IsDisable) // 비활성화 (게임 종료나 씬 넘어감 등) 되었을 때 실행되지 않도록.
                return; 
            //건설 코드.
            CreateBuilding(obj);
        }

        private void CreateBuilding(BuildingEvent obj)
        {
            if(obj.buildingData.BuildingWaitPrefab == null)
                Logging.Log("BuildingData In Building");
            List<NodeData> nodeDatas = GetBuildingSizeNodes(obj.buildNode, obj.buildingData);
            foreach (NodeData nodeData in nodeDatas)
            {
                Logging.Log($"최종 세팅 노드들 : {nodeData.worldPosition}");
            }
            //풀 메니저 사용 예정
            IBuilding building =
                Instantiate(obj.buildingData.BuildingPrefab, obj.buildNode.worldPosition, Quaternion.identity)
                    .GetComponent<IBuilding>();
            building.Initialize(obj.buildingData, nodeDatas);
            ValueProvider.Instance.NodeDataManager.eventSO.Raise(new NodeDataManagerEvent(NodeDataManagerEventType.Add,
                ObjectType.Building,building,nodeDatas));
        }

        private void CreateWaiteBuilding(BuildingEvent obj)
        {
            //풀 메니저 사용 예정
            CreateWaitBuilding building = Instantiate(obj.buildingData.BuildingWaitPrefab, obj.buildNode.worldPosition,
                Quaternion.identity).GetComponentInChildren<CreateWaitBuilding>();
            List<NodeData>  nodeDatas = GetBuildingSizeNodes(obj.buildNode, obj.buildingData);
            ValueProvider.Instance.NodeDataManager.eventSO.Raise(new NodeDataManagerEvent(NodeDataManagerEventType.Add,
                ObjectType.Building,building,nodeDatas));
            building.Initialize(obj.buildingData,nodeDatas);
           building.CooldownBar.Value.SetActiveBar(Time.unscaledTime, obj.buildingData.BuildTime);
           foreach (NodeData nodeData in nodeDatas)
           {
               Logging.Log($"최종 세팅 노드들 (건설현장) : {nodeData.worldPosition}");
           }
        }
        private List<NodeData> GetBuildingSizeNodes(NodeData buildingNode, BuildingDataSO buildingData)
        {
            List<NodeData> nodeDatas = new();
            nodeDatas.Add(buildingNode);
            for (float i = -buildingData.BuildingSize.y / 2f; i <= buildingData.BuildingSize.y; i+= 0.5f)
            {
                for (float j = -buildingData.BuildingSize.x / 2f; j <= buildingData.BuildingSize.x; j+= 0.5f)
                {
                    if (ValueProvider.Instance.BakedDataSO.TryGetNode(new Vector3(Mathf.Approximately(j, 1f) && buildingData.BuildingSize.x == 1 ? 0 : j
                            ,Mathf.Approximately(i, 1f) && buildingData.BuildingSize.y == 1 ? 0 : i), out NodeData nodeData))
                    {
                        Logging.Log($"가져온 노드 데이터 : {nodeData.worldPosition}");
                        if (!nodeDatas.Contains(nodeData))
                        {
                            nodeDatas.Add(nodeData);
                        }
                    }
                    
                }
            }
            return nodeDatas;
        }

        private void OnDisable()
        {
            IsDisable = true;
        }

        private void OnDestroy()
        {
            BuildingEventSO.OnEvent -= BuildBuilding;
        }
    }
}*/