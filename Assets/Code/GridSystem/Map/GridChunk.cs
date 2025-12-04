using System.Collections.Generic;
using Code.GridSystem.Objects;
using UnityEngine;

namespace Code.GridSystem.Map {
    public class GridChunk {
        private readonly GridObject[,] _cells;

        public GridChunk(int chunkSize) {
            _cells = new GridObject[chunkSize, chunkSize];
        }

        #region Get

        public GridObject GetObjectsAt(Vector2Int localCell)
            => _cells[localCell.x, localCell.y];

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

        public void DeleteCell(Vector2Int localCell) {
            _cells[localCell.x, localCell.y] = null;
        }

        public bool TryDeleteCell(Vector2Int localCell) {
            if (HasObjectAt(localCell)) {
                return false;
            }

            _cells[localCell.x, localCell.y] = null;
            return true;
        }

        #endregion

        #region Check

        public bool HasObjectAt(Vector2Int localCell) => !GetObjectsAt(localCell);

        #endregion

        #region Set

        public void SetCellObject(Vector2Int localCell, GridObject obj) {
            _cells[localCell.x, localCell.y] = obj;
        }

        #endregion

        #region TrySet

        public bool TrySetCellObject(Vector2Int localCell, GridObject obj) {
            if (HasObjectAt(localCell)) {
                return false;
            }

            _cells[localCell.x, localCell.y] = obj;
            return true;
        }

        #endregion
    }
}