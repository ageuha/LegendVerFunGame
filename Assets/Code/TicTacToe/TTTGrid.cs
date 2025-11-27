using System.Collections.Generic;
using Code.Core.Extensions;
using Code.Core.GlobalEnums;

namespace Code.TicTacToe {
    public class TTTGrid {
        private readonly TTTCell[,] _grid;
        private readonly TTTCell[] _gridAsOneDimensional;
        private readonly int _gridSize;

        public IReadOnlyList<TTTCell> GridAsOneDimensional => _gridAsOneDimensional;

        public bool IsFull {
            get {
                foreach (var cell in _gridAsOneDimensional) {
                    if (cell.CellType == TTTEnum.None) {
                        return false;
                    }
                }

                return true;
            }
        }

        public TTTGrid(int gridSize) {
            _grid = new TTTCell[gridSize, gridSize];
            _gridAsOneDimensional = new TTTCell[gridSize * gridSize];
            _gridSize = gridSize;
        }

        public TTTCell this[int x, int y] {
            get => _grid[x, y];
            set {
                _grid[x, y] = value;
                _gridAsOneDimensional[x * _gridSize + y] = value;
                for (int i = 1; i < 9; ++i) {
                    Direction direction = (Direction)i;
                    var vector = direction.ToVector2Int();
                    int neighborX = x + vector.x;
                    int neighborY = y + vector.y;
                    if (neighborX >= 0 && neighborX < _gridSize &&
                        neighborY >= 0 && neighborY < _gridSize) {
                        TTTCell neighborCell = _grid[neighborX, neighborY];
                        if (neighborCell) {
                            value.ConnectWith(direction, neighborCell);
                        }
                    }
                }
            }
        }
    }
}