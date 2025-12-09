using Code.Core.Utility;
using Code.GridSystem.Map;
using UnityEngine;

namespace Code.GridSystem.Objects {
    public abstract class GridBoundsObject : GridObject {
        internal override bool IsBoundsObject => true;

        internal sealed override void SetCellObject(Vector2Int worldPos, GridMap map) {
            Map = map;
            WorldPos = worldPos;
            for (int i = 0; i < Size.x; i++) {
                for (int j = 0; j < Size.y; j++) {
                    Vector2Int cellPos = worldPos + new Vector2Int(i, j);
                    map.SetCellObjectInternal(cellPos, this);
                    Logging.Log($"Setting pos :  {cellPos.x} , {cellPos.y}");
                }
            }
            OnSetCellObject(worldPos, map);
        }

        internal override bool TrySetCellObject(Vector2Int worldPos, GridMap map) {
            if(CheckIntersect(worldPos, map)) return false;

            SetCellObject(worldPos, map);
            OnSetCellObject(worldPos, map);
            return true;
        }

        public bool CheckIntersect(Vector2Int worldPos, GridMap map)
        {
            for (int i = 0; i < Size.x; i++) {
                for (int j = 0; j < Size.y; j++) {
                    Vector2Int cellPos = worldPos + new Vector2Int(i, j);
                    Logging.Log($"CheckIntersect : {cellPos.x} , {cellPos.y}");
                    if (map.HasObjectAt(cellPos)) return true;
                }
            }

            return false;
        }

        public override void DestroyFromGrid() {
            if (!IsSet) {
                Logging.LogWarning($"{name}: 설치되지 않은 오브젝트에 대한 Destroy 호출");
                return;
            }

            for (int i = 0; i < Size.x; i++) {
                for (int j = 0; j < Size.y; j++) {
                    Vector2Int cellPos = WorldPos + new Vector2Int(i, j);
                    if (Map.GetObjectsAt(cellPos) != this) continue;
                    Map.DeleteCellInternal(cellPos);
                }
            }
            
            OnDestroyedCellObject(WorldPos);
            Map = null;
        }
    }
}