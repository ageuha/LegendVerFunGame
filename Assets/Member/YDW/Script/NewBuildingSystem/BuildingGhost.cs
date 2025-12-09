using System;
using Code.Core.Pool;
using Code.Core.Utility;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.KJW.Code.Input;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.NewBuildingSystem.Buildings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BuildingGhost : GridBoundsObject
    {
        [SerializeField] private InputReader  inputReader;
        [SerializeField] private BuildingGhostEventSO buildingGhostEventSO;
        [SerializeField] private BuildingEventSO buildingEventSO;

        #region TestCode

        [SerializeField] private BuildingDataSO _buildingData;

        #endregion
        private SpriteRenderer _spriteRenderer;
        private bool _canBuild;
        private Vector2Int _beforePos;
        private Vector2 _aim;
        private Vector2Int _selectPos;
        private BuildingDataSO _currentBuildingData;
        private bool _eventFlag;
        private Vector2Int _size;
        protected override Vector2Int Size => _currentBuildingData.BuildingSize;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            buildingGhostEventSO.OnEvent += HandleBuildingGhost;
            //gameObject.SetActive(false);
        }
        
        
          
        

        private void Update()
        {
            if (inputReader != null && _aim != inputReader.MousePos)
                _aim = inputReader.MousePos;

            #region TestCode

            if(Keyboard.current.vKey.wasPressedThisFrame && _canBuild)
                CreateBuilding();
            if(Keyboard.current.escapeKey.wasPressedThisFrame && gameObject.activeSelf)
                OffBuildingGhostEvent();
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                HandleBuildingGhost(new  BuildingGhostEvent(_buildingData,true));
                _currentBuildingData = _buildingData;
            }
                

            #endregion
        }
                

        private void HandleBuildingGhost(BuildingGhostEvent obj)
        {
            if (obj.OnOff &&  !_eventFlag)
            {
                _eventFlag = true;
                inputReader.OnAttacked += OnBuildingGhostEvent;
                _spriteRenderer.sprite =  obj.buildingDataSO.Image;
                _currentBuildingData =  obj.buildingDataSO;
                _size = obj.buildingDataSO.BuildingSize;
            }
            else
            {
                _eventFlag = false;
                inputReader.OnAttacked -= OnBuildingGhostEvent;
                _spriteRenderer.sprite = null;
                _currentBuildingData = null;
                _size = Vector2Int.zero;
            }
                
           
        }

        private void OnBuildingGhostEvent() //미리보기 켜기 (특정 노드에 마우스 좌클릭 시.)
        {
            gameObject.SetActive(true);
            _selectPos = GridManager.Instance.GetWorldToCellPosition(Camera.main.ScreenToWorldPoint(_aim));
            
            Logging.Log($"Has Object : {CheckIntersect(_selectPos, GridManager.Instance.GridMap)}, SelectPos : {_selectPos}");
            // GridManager.Instance.LogTiles(WorldPos);
            if (!CheckIntersect(_selectPos, GridManager.Instance.GridMap) &&
                GridManager.Instance.HasGroundTile(_selectPos)) //만약 오브젝트가 없다면.
            {
                _canBuild = true;
                _spriteRenderer.color = Color.blue;
                GridManager.Instance.GridMap.TryDeleteCell(_beforePos);
                GridManager.Instance.GridMap.SetCellObject(_selectPos, this);
                Logging.Log("Delete Before Pos");
                _beforePos = _selectPos;
            }
            else
            {
                _canBuild = false;
                _spriteRenderer.color = Color.red;
            }
        }

        private void CreateBuilding() //추후 버튼이나 특정 키를 누를 시, 실행되도록.
        {
            if (_canBuild == false)
            {
                Logging.Log("건설할 수 없습니다.");
                return;
            }
            _canBuild = false;
            //gameObject.SetActive(false); 태스트 때문에 주석
            GridManager.Instance.DeleteBuildingObject(WorldPos);
            //여기서 waitBuilding을 가져오는게 아니라, 그냥 Building 자체를 설치 해버림.
            if (_currentBuildingData.Building is BoundsBuilding)
            {
                BoundsBuilding building = PoolManager.Instance.Factory<BoundsBuilding>().Pop(); //여기서 Pop 때려서 Building 생성시키고,

                if (typeof(MonoBehaviour).IsAssignableFrom(_currentBuildingData.RealType)) // 풀이 제네릭 타입 기반이라, addComponent시킴.
                {
                    Component compo = building.gameObject.AddComponent(_currentBuildingData.RealType);
                    if (compo is IBuilding buildCompo)
                    {
                        buildCompo.Initialize(_currentBuildingData);
                    }
                    building.SettingChildComponent(compo);
                }
                building.Initialize(_currentBuildingData.BuildingSize);
                GridManager.Instance.GridMap.SetCellObject(_selectPos, building);
                
            }
            else if (_currentBuildingData.Building is UnitBuilding unitBuilding)
            {
                UnitBuilding building = PoolManager.Instance.Factory<UnitBuilding>().Pop();
                Type t = building.GetType();

                if (typeof(MonoBehaviour).IsAssignableFrom(t)) // 풀이 타입 기반이라, addComponent시킴.
                {
                    Component compo = building.gameObject.AddComponent(t);
                    if (compo is IBuilding buildCompo)
                    {
                        buildCompo.Initialize(_currentBuildingData);
                    }
                    building.SettingChildComponent(compo);
                }
                GridManager.Instance.GridMap.SetCellObject(_selectPos, building);
            }
            else
            {
                Logging.Log("모든 캐스팅에 실패했습니다.");
            }
            
            OffBuildingGhostEvent();
        }

        protected override void OnSetCellObject(Vector2Int worldPos, GridMap map)
        {
            base.OnSetCellObject(worldPos, map);
            transform.position = GridManager.Instance.GetCellToWorldPosition(WorldPos);
            //transform.position += new Vector3(0.5f, 0.5f, 0);
        }


        private void OffBuildingGhostEvent() //미리보기 끄기 (esc누를 시.)
        {
            _eventFlag = false;
            inputReader.OnAttacked -= OnBuildingGhostEvent;
            _spriteRenderer.sprite = null;
            _currentBuildingData = null;
            _size = Vector2Int.zero;
            _selectPos = Vector2Int.zero;
            _canBuild = false;
           // gameObject.SetActive(false); 테스트 때문에 주석.
        }

        private void OnDestroy()
        {
            buildingGhostEventSO.OnEvent -= HandleBuildingGhost;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(_currentBuildingData != null)
                Gizmos.DrawCube(transform.position,new Vector3(Size.x,Size.y,0f));
        }

    }
}