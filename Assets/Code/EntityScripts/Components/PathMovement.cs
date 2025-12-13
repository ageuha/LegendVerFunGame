using System;
using Code.Core.Utility;
using Code.EntityScripts.BaseClass;
using Code.EntityScripts.Interface;
using Member.YDW.Script.PathFinder;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Code.EntityScripts.Components {
    [RequireComponent(typeof(PathAgent))]
    public class PathMovement : MonoBehaviour, IEntityModule {
        [SerializeField] private PathAgent pathAgent;
        [SerializeField] private int maxPathCount = 1000;

        private Vector3[] _path;
        private int _currentPathIndex;
        private Vector3 _nextPoint;
        private int _pathCount;
        private Entity _owner;
        private EntityMover _mover;
        private Vector2 _beforePosition;

        public bool IsArrived { get; private set; }
        public bool IsPathFailed { get; private set; }
        public bool IsPathPending { get; private set; }
        public bool IsStop { get; private set; }

        private void Reset() {
            pathAgent = GetComponent<PathAgent>();
        }

        public void Initialize(Entity agent) {
            _owner = agent;
            _path = new Vector3[maxPathCount];
            _mover = _owner.GetModule<EntityMover>();
            if (!_mover) {
                Logging.LogError("EntityMover module is missing!");
            }
        }

        public void SetDestination(Vector3 destination) {
            Vector3Int startCell = Vector3Int.FloorToInt(transform.position);
            Vector3Int endCell = Vector3Int.FloorToInt(destination);

            IsArrived = false;
            IsPathFailed = false;
            IsPathPending = true;
            _beforePosition = _owner.transform.position; // �� ó�� ��ġ

            _pathCount = pathAgent.GetPath(startCell, endCell, _path);


            if (_pathCount <= 0) {
                IsPathFailed = true;
            }
            else {
                _currentPathIndex = 1;
            }

            IsPathPending = false;
        }

        private void Update() {
            if (IsStop) return;
            if (_currentPathIndex >= _pathCount) return;

            if (CheckArrived() == false) {
                Vector2 direction = _path[_currentPathIndex] - _owner.transform.position;
                _mover.SetMovementInput(direction);
            }
            else {
                _mover.StopImmediately();
            }
        }

        private bool CheckArrived() {
            Vector2 destination = _path[_currentPathIndex];
            Vector2 currentPosition = _owner.transform.position;
            Vector2 beforeDirection = (destination - _beforePosition).normalized;
            Vector2 currentDirection = (destination - currentPosition).normalized;
            _beforePosition = currentPosition;

            if (Vector2.Dot(beforeDirection, currentDirection) <= 0
                || Vector2.Distance(destination, currentPosition) < 0.01f) {
                _currentPathIndex++;

                if (_currentPathIndex >= _pathCount)
                    IsArrived = true;
                return IsArrived;
            }

            return false;
        }


        private void OnDrawGizmos() {
            if (_pathCount <= 0) return;

            for (int i = 0; i < _pathCount - 1; i++) {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_path[i], 0.25f);
                Gizmos.DrawLine(_path[i], _path[i + 1]);
            }

            Gizmos.DrawSphere(_path[_pathCount - 1], 0.25f);
        }
    }
}