using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem.EventStruct;
using NUnit.Framework.Internal;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingBuilder : MonoBehaviour
    {
        [field: SerializeField] public BuildingEventSO BuildingEventSO { get; private set; }
        [field: SerializeField] public BuildingManagerEventSO BuildingManagerEventSO { get; private set; }
        private void Awake()
        {
            BuildingEventSO.OnEvent += BuildBuilding;
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

            Collider2D obstacle = Physics2D.OverlapBox(new Vector3(obj.buildNode.worldPosition.x,obj.buildNode.worldPosition.y - obj.buildingData.BuildingSize.y/3f),
                new Vector3(obj.buildingData.BuildingSize.x,obj.buildingData.BuildingSize.y/3f,0), 0); //어떻게 될진 모르겠으나, 따로 장애물을 레이어로 나누던가 뭔가 필터링 조취를 취해야 할듯.
            if (obstacle != null)
            {
                Logging.LogWarning("장애물이 존재합니다.");
                return false;
            }
            //재화 체크는 여기서 필요하면 해줌.
            //추후 최대 한번에 생성 가능한 건물 대기열 수 같은게 생기면 추가로 조건 넣어야 할듯.
            return true;
        }

        private async Task CooldownBuild(BuildingEvent obj) //이거 광클을 막아야 할 것 같은데, 아마 여기서가 아니라, 아이템 쪽에서 바로 지워주면 될거 같긴 함.
        {
            Debug.Log("건설 쿨타임 중");
            CreateWaiteBuilding(obj);
            float buildTime = obj.buildingData.BuildTime * 1000f;
            await Task.Delay((int)buildTime);
            //건설 코드.
            CreateBuilding(obj);
            Debug.Log("건설 성공!");
        }

        private void CreateBuilding(BuildingEvent obj)
        {
            //풀 메니저 사용 예정
            IBuilding building =
                Instantiate(obj.buildingData.BuildingPrefab, obj.buildNode.worldPosition, Quaternion.identity)
                    .GetComponent<IBuilding>();
            building.Initialize(obj.buildingData);
            BuildingManagerEventSO.Raise(new BuildingManagerEvent(BuildingManagerEventType.AddBuilding,obj.buildNode,obj.buildingData,building));
            
            
        }

        private void CreateWaiteBuilding(BuildingEvent obj)
        {
            //풀 메니저 사용 예정
            ICooldownBar bar = Instantiate(obj.buildingData.BuildingWaitPrefab,obj.buildNode.worldPosition,Quaternion.identity).GetComponentInChildren<ICooldownBar>();
            bar.SetActiveBar(Time.unscaledTime, obj.buildingData.BuildTime);
        }
    }
}