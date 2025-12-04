using System.Collections.Generic;
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

        private Vector2Int WorldToChunk(Vector2Int cellPos)
            => new(cellPos.x / _chunkSize, cellPos.y / _chunkSize);

        private Vector2Int WorldToLocal(Vector2Int cellPos)
            => new(cellPos.x % _chunkSize, cellPos.y % _chunkSize);

        private GridChunk GetOrCreateChunk(Vector2Int chunkPos) {
            if (!_chunks.ContainsKey(chunkPos)) {
                _chunks[chunkPos] = new GridChunk(_chunkSize);
            }
            return _chunks[chunkPos];
        }

        #region Get

        public GridObject GetObjectsAt(Vector2Int worldCell) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);
            
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

        #endregion

        #region Set

        public void SetCellObject(Vector2Int worldCell, GridObject obj) {
            var chunkPos = WorldToChunk(worldCell);
            var localCell = WorldToLocal(worldCell);
            
            var chunk = GetOrCreateChunk(chunkPos);
            chunk.SetCellObject(localCell, obj);
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
    }
}