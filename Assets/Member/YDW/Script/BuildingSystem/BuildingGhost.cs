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
            inputReader.OnAimed += Aim;
            gameObject.SetActive(false);
        }

        private void Aim(Vector2 obj)
        {
            _aim = Camera.main.ScreenToWorldPoint(obj);
            _aim.z = 0;
        }

        private void Update()
        {
            if (_isMouseTrack && _beforePos != _aim)
            {
                transform.position = _aim;
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
            inputReader.OnAimed -= Aim;
        }
    }
}