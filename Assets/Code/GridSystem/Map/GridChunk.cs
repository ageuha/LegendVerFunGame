using Code.Core.Utility;
using Code.GridSystem.Objects;
using UnityEngine;

namespace Code.GridSystem.Map {
    public class GridChunk {
        private readonly GridObject[,] _cells;
        private readonly int _chunkSize;
        private readonly GridMap _map;
        private readonly Vector2Int _chunkPos;

        public GridChunk(int chunkSize, GridMap map, Vector2Int chunkPos) {
            _cells = new GridObject[chunkSize, chunkSize];
            _chunkSize = chunkSize;
            _map = map;
            _chunkPos = chunkPos;
        }

        #region Get

        public GridObject GetObjectsAt(Vector2Int localCell)
        {
            return _cells[localCell.x, localCell.y];
        }

        public T GetObjectAt<T>(Vector2Int localCell) where T : GridObject {
            if (_cells[localCell.x, localCell.y] is T t) return t;
            return null;
        }

        #endregion

        #region TryGet

        public bool TryGetObjectsAt(Vector2Int localCell, out GridObject gridObject) {
            gridObject = _cells[localCell.x, localCell.y];
            if (!gridObject) return false;
            return true;
        }

        public bool TryGetObjectAt<T>(Vector2Int localCell, out T output) where T : GridObject {
            if (_cells[localCell.x, localCell.y] is T t) {
                output = t;
                return true;
            }

            output = null;
            return false;
        }

        #endregion

        #region Delete

        internal void DeleteCellInternal(Vector2Int localCell) {
            _cells[localCell.x, localCell.y] = null;
        }

        public void DeleteCell(Vector2Int localCell) {
            var obj = _cells[localCell.x, localCell.y];
            obj.DestroyFromGrid();
        }

        public bool TryDeleteCell(Vector2Int localCell) {
            if (!HasObjectAt(localCell)) {
                return false;
            }

            _cells[localCell.x, localCell.y].DestroyFromGrid();
            return true;
        }

        #endregion

        #region Check

        public bool HasObjectAt(Vector2Int localCell) =>  null != GetObjectsAt(localCell);

        #endregion

        #region Set

        public void SetCellObject(Vector2Int localCell, GridObject obj) {
            obj.SetCellObject(LocalToWorld(localCell), _map);
        }

        internal void SetCellObjectInternal(Vector2Int localCell, GridObject obj) {
            _cells[localCell.x, localCell.y] = obj;
        }

        #endregion

        #region TrySet

        public bool TrySetCellObject(Vector2Int localCell, GridObject obj) {
            if (obj.TrySetCellObject(LocalToWorld(localCell), _map)) {
                _cells[localCell.x, localCell.y] = obj;
                return true;
            }

            return false;
        }

        #endregion

        #region Utility

        internal Vector2Int LocalToWorld(Vector2Int localPos)
            => _chunkPos * _chunkSize + localPos;

        public void LogTiles()
        {
            for (int i = 0; i < _chunkSize; i++)
            {
                for (int j = 0; j < _chunkSize; j++)
                {
                    Logging.Log(_cells[i, j]);
                }
            }
        }

        #endregion
    }
}