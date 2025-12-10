using Code.Core.Utility;
using Member.YDW.Script.BuildingSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Member.YDW.Script.NewBuildingSystem.Buildings
{
    public class TurretBuilding : MonoBehaviour, IBuilding , IWaitable
    {
        [SerializeField] private AnimatorController _animatorController;
        public bool IsActive { get; private set; }
        public BuildingDataSO BuildingData { get; private set; }
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        public bool IsWaiting { get; private set; }
        public void Initialize(BuildingDataSO buildingData)
        {
            BuildingData = buildingData;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
            _animator.runtimeAnimatorController = _animatorController;
            _spriteRenderer.sprite = BuildingData.Image;
        }

        public void SetWaiting(bool waiting)
        {
            if (!waiting)
            {
                IsActive = true;
            }
            IsWaiting = waiting;
        }
        
        private void OnDestroy()
        {
            if(_animator != null)
                _animator.runtimeAnimatorController = null;
            if(_spriteRenderer != null)
                _spriteRenderer.sprite = null;
            Logging.Log("Oh.. Im Destroyed");
        }
    }
}