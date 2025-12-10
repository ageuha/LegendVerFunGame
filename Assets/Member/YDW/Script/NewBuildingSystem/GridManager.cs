using System;
using Code.Core.Utility;
using Code.GridSystem.Map;
using Code.GridSystem.Objects;
using Member.YDW.Script.BuildingSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Member.YDW.Script.NewBuildingSystem
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private int gridSize = 10;
        public GridMap GridMap {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            GridMap = new GridMap(gridSize);
        }

        private void OnDrawGizmosSelected()
        {
            GridMap?.DrawChunkOutline();
        }

        public void LogTiles(Vector2Int position)
        {
            GridMap.LogChunk(position);
        }
        
        public void DeleteBuildingObject(Vector2Int position)
        {
            GridMap.DeleteCell(position);
        }

        public Vector2Int GetWorldToCellPosition(Vector2 position)
        {
            return (Vector2Int)groundTilemap.WorldToCell(position);
        }

        public Vector2 GetCellToWorldPosition(Vector2Int position)
        {
            return groundTilemap.CellToWorld((Vector3Int)position);
        }

        public bool HasGroundTile(Vector2Int position)
        {
            return groundTilemap.HasTile((Vector3Int)position);
        }

        public bool CheckHasNodeBound(Vector2Int position, Vector2Int size)
        {
            return GridMap.HasObjectInBounds(position, size);
        }
    }
}