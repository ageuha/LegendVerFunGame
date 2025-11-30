using Input;
using Member.YDW.Script.BuildingSystem.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingGhost : MonoBehaviour
    {
        [SerializeField] private InputReader  inputReader;
        [SerializeField] private BuildingGhostEventSO buildingGhostEventSO;
        private SpriteRenderer _spriteRenderer;
        private bool _isMouseTrack;
        private Vector3 _beforePos;
        private Vector3 _aim;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            buildingGhostEventSO.OnEvent += HandleBuildingGhost;
            gameObject.SetActive(false);
        }
          
        

        private void Update()
        {
            if (_isMouseTrack && _beforePos != _aim)
            {
                transform.position = Camera.main.ScreenToWorldPoint(inputReader.MousePos);;
                _beforePos = transform.position;
            }
        }
                

        private void HandleBuildingGhost(BuildingGhostEvent obj)
        {
            gameObject.SetActive(obj.OnOff);
            _spriteRenderer.sprite =  obj.Image;
            _isMouseTrack = obj.OnOff;
        }

        private void OnDestroy()
        {
            buildingGhostEventSO.OnEvent -= HandleBuildingGhost;
        }
    }
}