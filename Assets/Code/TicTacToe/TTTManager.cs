using Code.Core.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.TicTacToe {
    public class TTTManager : MonoBehaviour {
        [SerializeField] private TTTCell cellPrefab;
        [SerializeField] private GridLayoutGroup gridLayout;
        [SerializeField] private int gridSize = 3;
        [field: SerializeField] public UnityEvent<int> OnSelected { get; private set; }


        private TTTGrid _grid;
        private PoolFactory<TTTCell> _cellPoolFactory;

        public bool IsGameActive { get; private set; } = false;
        public TTTEnum LastGameWinner { get; private set; } = TTTEnum.None;

        public float[] CurrentGridState {
            get {
                float[] state = new float[gridSize * gridSize];
                for (int i = 0; i < _grid.GridAsOneDimensional.Count; i++) {
                    var cell = _grid.GridAsOneDimensional[i];
                    state[i] = cell.CellType switch {
                        TTTEnum.X => 1f,
                        TTTEnum.O => -1f,
                        _ => 0f
                    };
                }

                return state;
            }
        }

        public bool CheckDidBlock(int cellIndex) {
            return _grid.GridAsOneDimensional[cellIndex].CheckDidBlock();
        }

        public bool TrySelectCell(int cellIndex) {
            if (!IsGameActive) return false;
            if (cellIndex < 0 || cellIndex >= gridSize * gridSize) return false;
            var cell = _grid.GridAsOneDimensional[cellIndex];
            if (cell.CellType != TTTEnum.None) return false;
            cell.OnCellClicked();
            return true;
        }

        public int GridSize => gridSize;
        public TTTEnum Turn { get; private set; } = TTTEnum.X;

        private void Awake() {
            _cellPoolFactory = new PoolFactory<TTTCell>(cellPrefab, 100);
        }

        [ContextMenu("Start Game")]
        public void StartGame() {
            IsGameActive = true;
            _grid = new TTTGrid(gridSize);
            gridLayout.constraintCount = gridSize;

            for (int i = 0; i < gridSize; i++) {
                for (int j = 0; j < gridSize; j++) {
                    var cell = _cellPoolFactory.Pop();
                    cell.transform.SetParent(gridLayout.transform, false);
                    cell.Initialize(this, i * gridSize + j);
                    _grid[i, j] = cell;
                }
            }
        }

        [ContextMenu("Restart Game")]
        public void RestartGame() {
            foreach (var cell in _grid.GridAsOneDimensional) {
                cell.ResetCell();
            }

            Turn = TTTEnum.X;
            IsGameActive = true;
        }

        public void Selected(int cellIndex) {
            Turn = Turn == TTTEnum.X ? TTTEnum.O : TTTEnum.X;
            if (_grid.IsFull)
                EndGame(TTTEnum.None);
            OnSelected?.Invoke(cellIndex);
        }

        public void EndGame(TTTEnum winnerType) {
            if (IsGameActive)
                Debug.Log("Game Ended! Winner: " + winnerType);
            IsGameActive = false;
            LastGameWinner = winnerType;
        }
    }
}