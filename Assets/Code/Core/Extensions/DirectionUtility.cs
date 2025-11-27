using Code.Core.GlobalEnums;
using UnityEngine;

namespace Code.Core.Extensions {
    public static class DirectionUtility {
        public static Direction GetOppositeDirection(this Direction direction) {
            return direction switch {
                Direction.Left => Direction.Right,
                Direction.Up => Direction.Down,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.LeftUp => Direction.RightDown,
                Direction.RightUp => Direction.LeftDown,
                Direction.RightDown => Direction.LeftUp,
                Direction.LeftDown => Direction.RightUp,
                _ => Direction.Center
            };
        }

        public static Vector2Int ToVector2Int(this Direction direction) {
            return direction switch {
                Direction.Left => new Vector2Int(-1, 0),
                Direction.Up => new Vector2Int(0, 1),
                Direction.Right => new Vector2Int(1, 0),
                Direction.Down => new Vector2Int(0, -1),
                Direction.LeftUp => new Vector2Int(-1, 1),
                Direction.RightUp => new Vector2Int(1, 1),
                Direction.RightDown => new Vector2Int(1, -1),
                Direction.LeftDown => new Vector2Int(-1, -1),
                _ => new Vector2Int(0, 0)
            };
        }
    }
}