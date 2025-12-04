using Member.KJW.Code.Input;
using Member.YDW.Script.EventStruct;
using Member.YDW.Script.PathFinder;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingGhost : MonoBehaviour
    {
        [SerializeField] private InputReader  inputReader;
        [SerializeField] private BuildingGhostEventSO buildingGhostEventSO;
        [SerializeField] private BuildingEventSO buildingEventSO;
        
        private SpriteRenderer _spriteRenderer;
        private bool _isMouseTrack;
        private Vector3 _beforePos;
        private Vector2 _aim;
        private NodeData _currentNode;
        private BuildingDataSO _currentBuildingData;
        private bool _eventFlag;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            buildingGhostEventSO.OnEvent += HandleBuildingGhost;
            gameObject.SetActive(false);
        }
          
        

        private void Update()
        {
            if (inputReader != null && _aim != inputReader.MousePos)
                _aim = inputReader.MousePos;
                
            
            
            if (_isMouseTrack && (Vector2)_beforePos != _aim)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(_aim);
                if (ValueProvider.Instance.BakedDataSO.TryGetNode(pos, out NodeData nodeData))
                {
                    _currentNode = nodeData;
                    transform.position = nodeData.worldPosition;
                }
                else
                {
                    _currentNode.cellPosition = new Vector3Int(12312399,12312399, 12312399);
                    transform.position = pos;
                }
                
                _beforePos = transform.position;
            }
        }
                

        private void HandleBuildingGhost(BuildingGhostEvent obj)
        {
            if (obj.OnOff &&  !_eventFlag)
            {
                _eventFlag = true;
                inputReader.OnAttacked += OnBuildingEvent;
                _spriteRenderer.sprite =  obj.buildingDataSO.Image;
                _currentBuildingData =  obj.buildingDataSO;
            }
            else
            {
                _eventFlag = false;
                inputReader.OnAttacked -= OnBuildingEvent;
                _spriteRenderer.sprite = null;
                _currentBuildingData = null;
            }
                
            gameObject.SetActive(obj.OnOff);
            _isMouseTrack = obj.OnOff;
        }

        private void OnBuildingEvent()
        {
            buildingEventSO.Raise(new BuildingEvent(_currentNode,_currentBuildingData));
        }

        private void OnDestroy()
        {
            buildingGhostEventSO.OnEvent -= HandleBuildingGhost;
        }

        private void OnDrawGizmos()
        {
            //해당 값은 추후 에셋 나오는거 보고, 임의로 수정 해야할 듯. 오버랩 사이즈는.
            if(_currentBuildingData!=null && _currentNode.cellPosition != new  Vector3Int(12312399,12312399,12312399))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(new Vector3(_currentNode.worldPosition.x,_currentNode.worldPosition.y - _currentBuildingData.BuildingSize.y/3f),new Vector3(_currentBuildingData.BuildingSize.x,_currentBuildingData.BuildingSize.y/3f,0));
            }
        }
    }
}