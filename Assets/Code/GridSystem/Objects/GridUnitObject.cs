using Code.Core.Utility;
using Code.GridSystem.Map;
using UnityEngine;

namespace Code.GridSystem.Objects {
    public abstract class GridUnitObject : GridObject {
        internal override bool IsBoundsObject => false;
        protected override Vector2Int Size => default;

        internal sealed override void SetCellObject(Vector2Int worldPos, GridMap map) {
            Map = map;
            WorldPos = worldPos;
            map.SetCellObjectInternal(worldPos, this);
        }

        internal override bool TrySetCellObject(Vector2Int worldPos, GridMap map) {
            if (map.HasObjectAt(worldPos)) return false;
            Map = map;
            map.SetCellObjectInternal(worldPos, this);
            return true;
        }

        public override void DestroyFromGrid() {
            if (!IsSet) {
                Logging.LogWarning($"{name}: 설치되지 않은 오브젝트에 대한 Destroy 호출");
                return;
            }

            Map.DeleteCellInternal(WorldPos);
            Map = null;
        }
    }
}