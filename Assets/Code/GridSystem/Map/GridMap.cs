using System.Collections.Generic;
using Code.Core.Utility;
using Code.GridSystem.Objects;
using UnityEngine;

namespace Code.GridSystem.Map {
    public class GridMap {
        private readonly int _chunkSize;
        private readonly Dictionary<Vector2Int, GridChunk> _chunks;

        public GridMap(int chunkSize) {
            _chunkSize = chunkSize;
            _chunks = new Dictionary<Vector2Int, GridChunk>();
        }
        private static int FloorDiv(int value, int size)
        {
            return value >= 0
                ? value / size
                : (value - size + 1) / size;
        }

        private Vector2Int WorldToChunk(Vector2Int cellPos)
        {
            return new Vector2Int(
                FloorDiv(cellPos.x, _chunkSize),
                FloorDiv(cellPos.y, _chunkSize)
            );
        }

        private Vector2Int WorldToLocal(Vector2Int cellPos)
        {
            var x = cellPos.x % _chunkSize;
            var y = cellPos.y % _chunkSize;
            if (x < 0) x += _chunkSize;
            if (y < 0) y += _chunkSize;
            return new Vector2Int(x, y);
        }

        private GridChunk GetOrCreateChunk(Vector2Int chunkPos) {
            if (!_chunks.ContainsKey(chunkPos)) {
                _chunks[chunkPos] = new GridChunk(_chunkSize, this, chunkPos);
            }

            return _chunks[chunkPos];
        }

        #region Get

        public GridObject GetObjectsAt(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);
            
            Logging.Log(chunkPos);
            Logging.Log(localCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) return null;
            return chunk.GetObjectsAt(localCell);
        }

        public T GetObjectAt<T>(Vector2Int worldCell) where T : GridObject {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) return null;
            return chunk.GetObjectAt<T>(localCell);
        }

        #endregion

        #region TryGet

        public bool TryGetObjectsAt(Vector2Int worldCell, out GridObject gridObject) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) {
                gridObject = null;
                return false;
            }

            return chunk.TryGetObjectsAt(localCell, out gridObject);
        }

        public bool TryGetObjectAt<T>(Vector2Int worldCell, out T output) where T : GridObject {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) {
                output = null;
                return false;
            }

            return chunk.TryGetObjectAt(localCell, out output);
        }

        #endregion

        #region Delete

        internal void DeleteCellInternal(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            var chunk = GetOrCreateChunk(chunkPos);
            chunk.DeleteCellInternal(localCell);
        }

        public void DeleteCell(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) return;
            chunk.DeleteCell(localCell);
        }

        public bool TryDeleteCell(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) return true; // 없으면 삭제된 것으로 간주
            return chunk.TryDeleteCell(localCell);
        }

        #endregion

        #region Check

        public bool HasObjectAt(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            if (!_chunks.TryGetValue(chunkPos, out var chunk)) return false;
            return chunk.HasObjectAt(localCell);
        }

        public bool HasObjectInBounds(Vector2Int worldCell, Vector2Int size)
        {
            for (int i = 0; i < size.x; i++) {
                for (int j = 0; j < size.y; j++) {
                    Vector2Int cellPos = worldCell + new Vector2Int(i, j);
                    // Logging.Log($"CheckIntersect : {cellPos.x} , {cellPos.y}");
                    if (HasObjectAt(cellPos)) return true;
                }
            }

            return false;
        }

        #endregion

        #region Set

        public void SetCellObject(Vector2Int worldCell, GridObject obj) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            var chunk = GetOrCreateChunk(chunkPos);
            chunk.SetCellObject(localCell, obj);
        }

        internal void SetCellObjectInternal(Vector2Int worldCell, GridObject obj) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            var chunk = GetOrCreateChunk(chunkPos);
            chunk.SetCellObjectInternal(localCell, obj);
        }

        #endregion

        #region TrySet

        public bool TrySetCellObject(Vector2Int worldCell, GridObject obj) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);

            var chunk = GetOrCreateChunk(chunkPos);
            return chunk.TrySetCellObject(localCell, obj);
        }

        #endregion

        #region Utility

        public void LogChunk(Vector2Int worldCell)
        {
            var chunkPos = WorldToChunk(worldCell);
            
            var chunk = GetOrCreateChunk(chunkPos);
            chunk.LogTiles();
        }

        public void DrawChunkOutline()
        {
            Gizmos.color = Color.green;
            foreach (var chunk in _chunks.Values)
            {
                var bottomLeft = chunk.LocalToWorld(default);
                var middle = bottomLeft + new Vector2(_chunkSize / 2f, _chunkSize / 2f);
                
                Gizmos.DrawWireCube(middle, Vector3.one * _chunkSize);
            }
        }

        #endregion
    }
}