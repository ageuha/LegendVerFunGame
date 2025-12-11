using System;
using Code.Core.Pool;
using Code.Core.Utility;
using Member.KJW.Code.EventChannel;
using Member.KJW.Code.Input;
using Member.YDW.Script.BuildingSystem;
using Member.YDW.Script.EventStruct;
using UnityEngine;
using UnityEngine.InputSystem;
using YTH.Code.Inventory;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class BuildingGhost : MonoBehaviour
    {
        [SerializeField] private InputReader  inputReader;
        [SerializeField] private BuildingGhostEventSO buildingGhostEventSO;
        [SerializeField] private BuildingEventSO buildingEventSO;
        [SerializeField] private InventoryManagerEventChannel inventoryChannel;
        [SerializeField] private BuildingGhostFlagEventChannel buildingGhostFlagEventChannel;
        

        #region TestCode

        [SerializeField] private BuildingDataSO buildingData;

        #endregion
        private SpriteRenderer _spriteRenderer;
        private bool _canBuild;
        private Vector2 _aim;
        private Vector2Int _selectPos;
        private BuildingDataSO _currentBuildingData;
        private bool _eventFlag;
        private Vector2Int _size;
        private Vector2Int _worldPos;
        private InventoryManager _inventoryManager;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            buildingGhostEventSO.OnEvent += HandleBuildingGhost;
            inventoryChannel.OnEvent += InitInventory;
            //gameObject.SetActive(false);
        }
        
        private void InitInventory(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
        }

        private void OnEnable()
        {
            inputReader.OnPlaced += CreateBuilding;
        }

        private void OnDisable()
        {
            inputReader.OnPlaced -= CreateBuilding;
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
                HandleBuildingGhost(new  BuildingGhostEvent(buildingData,true));
                _currentBuildingData = buildingData;
            }
            
            if (_eventFlag)
                OnBuildingGhostEvent();

            #endregion
        }
                
        

        private void HandleBuildingGhost(BuildingGhostEvent obj)
        {
            if (obj.OnOff &&  !_eventFlag)
            {
                _eventFlag = true;
                // gameObject.SetActive(true);
                // inputReader.OnAttacked += OnBuildingGhostEvent;
                _currentBuildingData =  obj.buildingDataSO;
                _size = obj.buildingDataSO.BuildingSize;
            }
            else
            {
                _eventFlag = false;
                // gameObject.SetActive(false);
                // inputReader.OnAttacked -= OnBuildingGhostEvent;
                _spriteRenderer.sprite = null;
                _currentBuildingData = null;
                _size = Vector2Int.zero;
            }
            buildingGhostFlagEventChannel.Raise(_eventFlag);
        }

        private void OnBuildingGhostEvent() //미리보기 켜기 (특정 노드에 마우스 좌클릭 시.)
        {
            gameObject.SetActive(true);
            _spriteRenderer.sprite =  _currentBuildingData.Image;
            
            if (_selectPos == GridManager.Instance.GetWorldToCellPosition(Camera.main.ScreenToWorldPoint(_aim))) return;
            
            _selectPos = GridManager.Instance.GetWorldToCellPosition(Camera.main.ScreenToWorldPoint(_aim));
            
            //Logging.Log($"Has Object : {CheckIntersect(_selectPos, GridManager.Instance.GridMap)}, SelectPos : {_selectPos}");
            // GridManager.Instance.LogTiles(WorldPos);
            if (!GridManager.Instance.CheckHasNodeBound(_selectPos,_size) &&
                GridManager.Instance.HasGroundTile(_selectPos)) //만약 오브젝트가 없다면.
            {
                _canBuild = true;
                _spriteRenderer.color = Color.blue;
            }
            else
            {
                _canBuild = false;
                _spriteRenderer.color = Color.red;
            }

            _worldPos = _selectPos;
            transform.position = GridManager.Instance.GetCellToWorldPosition(_selectPos);
            transform.position += new Vector3(0.5f, 0.5f, 0);
        }

        private void CreateBuilding() //추후 버튼이나 특정 키를 누를 시, 실행되도록.
        {
            if (!_canBuild || !_currentBuildingData)
            {
                Logging.Log("건설할 수 없습니다.");
                return;
            }
            _canBuild = false;
            //gameObject.SetActive(false); 태스트 때문에 주석
            //GridManager.Instance.DeleteBuildingObject(WorldPos);
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
                    building.Initialize(_currentBuildingData.BuildingSize,compo,_currentBuildingData.InitValue,_currentBuildingData.MaxHealth,_currentBuildingData.BuildTime);
                }
                GridManager.Instance.GridMap.SetCellObject(_selectPos, building);
                HandleBuildingGhost(new  BuildingGhostEvent(null,false));
                _inventoryManager.UseSelectedItem();
            }
            else if (_currentBuildingData.Building is UnitBuilding)
            {
                UnitBuilding building = PoolManager.Instance.Factory<UnitBuilding>().Pop();

                if (typeof(MonoBehaviour).IsAssignableFrom(_currentBuildingData.RealType)) // 풀이 타입 기반이라, addComponent시킴.
                {
                    Component compo = building.gameObject.AddComponent(_currentBuildingData.RealType);
                    if (compo is IBuilding buildCompo)
                    {
                        buildCompo.Initialize(_currentBuildingData);
                    }
                    building.SettingChildComponent(compo);
                    building.Initialize(compo,_currentBuildingData.InitValue,_currentBuildingData.MaxHealth,_currentBuildingData.BuildTime);
                }
                //building.Initialize(_currentBuildingData.BuildingSize); 추후 필요하면 추가.
                GridManager.Instance.GridMap.SetCellObject(_selectPos, building);
                HandleBuildingGhost(new  BuildingGhostEvent(null,false));
                _inventoryManager.UseSelectedItem();
            }
            else
            {
                Logging.Log("모든 캐스팅에 실패했습니다.");
            }
            
            OffBuildingGhostEvent();
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
            inventoryChannel.OnEvent -= InitInventory;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if(_currentBuildingData != null)
                for (int i = 0; i < _size.x; i++)
                {
                    for (int j = 0; j < _size.y; j++)
                    {
                        
                       Gizmos.DrawWireCube(new Vector3(_worldPos.x + i + 0.5f, _worldPos.y + j + 0.5f),Vector3.one);
                    }
                }
        }

    }
}