using Code.GridSystem.Map;
using UnityEngine;

namespace Code.GridSystem.Objects {
    public abstract class GridObject : MonoBehaviour {
        internal abstract bool IsBoundsObject { get; }
        protected abstract Vector2Int Size { get; }
        protected Vector2Int WorldPos;
        protected GridMap Map;
        protected bool IsSet => Map != null;

        internal bool TryGetSize(out Vector2Int size) {
            size = Size;
            return IsBoundsObject;
        }

        internal abstract void SetCellObject(Vector2Int worldPos, GridMap map);

        internal abstract bool TrySetCellObject(Vector2Int worldPos, GridMap map);

        public abstract void DestroyFromGrid();
    }
}