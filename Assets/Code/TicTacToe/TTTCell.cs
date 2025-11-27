using System.Collections.Generic;
using Code.Core.Extensions;
using Code.Core.GlobalEnums;
using Code.Core.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.TicTacToe {
    public class TTTCell : PoolableObject {
        public TTTEnum CellType { get; private set; } = TTTEnum.None;

        private readonly Dictionary<Direction, TTTCell> _neighbors = new();
        private TTTManager _manager;
        private int _index;

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image background;
        [SerializeField] private Color oColor = Color.green;
        [SerializeField] private Color xColor = Color.red;
        
        public void Initialize(TTTManager manager, int index) {
            _manager = manager;
            _index = index;
            ResetCell();
        }
        
        public void ResetCell() {
            CellType = TTTEnum.None;
            text.text = string.Empty;
            background.color = Color.white;
            // _neighbors.Clear();
        }

        public void Connect(Direction direction, TTTCell cell) {
            _neighbors.Add(direction, cell);
        }

        public void OnCellClicked() {
            // Debug.Log($"{_index} Cell Clicked");
            if (!_manager.IsGameActive) {
                return;
            }
            if (CellType != TTTEnum.None) return;
            CellType = _manager.Turn;

            text.text = CellType.ToString();
            background.color = CellType switch {
                TTTEnum.O => oColor,
                TTTEnum.X => xColor,
                _ => Color.white
            };

            int temp = 1;
            for (int i = 1; i < 9; ++i) {
                Direction direction = (Direction)i;
                CheckConnectedCells(direction, ref temp, OnComplete);
                temp = 1;
            }
            _manager.Selected(_index);
        }

        public bool CheckDidBlock() {
            if (CellType == TTTEnum.None) return false;
            CellType = CellType.GetOpposite();

            int temp = 1;
            for (int i = 1; i < 9; ++i) {
                Direction direction = (Direction)i;
                bool didBlock = false;
                CheckConnectedCells(direction, ref temp, () => didBlock = true);
                if (didBlock) {
                    CellType = CellType.GetOpposite();
                    return true;
                }

                temp = 1;
            }
            CellType = CellType.GetOpposite();
            return false;
        }

        private void CheckConnectedCells(Direction direction, ref int count, System.Action onComplete, bool checkOpposite = true) {
            if (_neighbors.TryGetValue(direction, out TTTCell neighbor)) {
                if (neighbor.CellType != CellType || CellType == TTTEnum.None) return;
                ++count;
                if (count >= _manager.GridSize) {
                    onComplete?.Invoke();
                    return;
                }
                else {
                    neighbor.CheckConnectedCells(direction, ref count, onComplete, false);
                }
            }
            if(!checkOpposite) return;
            if(_neighbors.TryGetValue(direction.GetOppositeDirection(), out TTTCell oppositeNeighbor)) {
                if (oppositeNeighbor.CellType != CellType || CellType == TTTEnum.None) return;
                ++count;
                if (count >= _manager.GridSize) {
                    onComplete?.Invoke();
                }
                else {
                    oppositeNeighbor.CheckConnectedCells(direction.GetOppositeDirection(), ref count, onComplete, false);
                }
            }
        }

        private void OnComplete() => _manager.EndGame(CellType);

        public void ConnectWith(Direction direction, TTTCell cell) {
            Connect(direction, cell);
            cell.Connect(direction.GetOppositeDirection(), this);
        }
    }
}