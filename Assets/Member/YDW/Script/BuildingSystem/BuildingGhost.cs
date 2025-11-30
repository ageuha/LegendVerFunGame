using Member.YDW.Script.BuildingSystem.EventStruct;
using UnityEngine;

namespace Member.YDW.Script.BuildingSystem
{
    public class BuildingGhost : MonoBehaviour
    {
        [SerializeField] private BuildingGhostEventSO buildingGhostEventSO;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            buildingGhostEventSO.OnEvent += HandleBuildingGhost;
            gameObject.SetActive(false);
        }

        private void HandleBuildingGhost(BuildingGhostEvent obj)
        {
            gameObject.SetActive(obj.OnOff);
            _spriteRenderer.sprite =  obj.Image;
            
        }
    }
}