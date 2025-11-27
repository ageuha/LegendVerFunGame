using Code.AI.DQN;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.TicTacToe {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once IdentifierTypo
    public class TTTAIManager : MonoBehaviour {
        [SerializeField] private TTTManager manager;
        [SerializeField] private TMP_InputField fileNameInputField;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private int[] hiddenLayersSizes;
        [SerializeField] private float learningRate;
        [SerializeField] private TTTEnum myTurn = TTTEnum.O;
        

        private DQNAgent _agent;
        private float[] _previousState;
        private int _previousAction;

        private void Awake() {
            saveButton.onClick.AddListener(OnSaveButtonClicked);
            loadButton.onClick.AddListener(OnLoadButtonClicked);

            int inputCount = manager.GridSize * manager.GridSize;
            _agent = new DQNAgent(inputCount, inputCount, hiddenLayersSizes, learningRate);
        }

        public void OnCellSelected(int cellIndex) {
            if (manager.Turn != myTurn) return;

            float reward = 0f;
            bool success = false;
            var currentState = manager.CurrentGridState;

            if (!manager.IsGameActive) {
                if (manager.LastGameWinner == TTTEnum.X) {
                    reward -= 5f;
                }
                else if (manager.LastGameWinner == TTTEnum.O) {
                    reward += 5f;
                }
                else {
                    reward += .5f;
                }

                _agent.Remember(_previousState, _previousAction, reward, currentState, true);
                _agent.Replay();
                Debug.Log($"Reward: {reward}");
                return;
            }

            while (!success) {
                int action = _agent.SelectAction(currentState);
                success = manager.TrySelectCell(action);
                reward = success ? 0f : -0.2f;
                if (!manager.IsGameActive) {
                    if (manager.LastGameWinner == TTTEnum.X) {
                        reward -= 5f;
                    }
                    else if (manager.LastGameWinner == TTTEnum.O) {
                        reward += 5f;
                    }
                    else {
                        reward += 0.5f;
                    }
                }

                _previousState = manager.CurrentGridState;
                _previousAction = action;
                if (success && action == 4) {
                    reward += .5f; // Center control bonus
                    Debug.LogWarning("Center control bonus awarded.");
                }
                if (success && manager.CheckDidBlock(action)) {
                    reward += 1.5f;
                    Debug.LogWarning("Blocked opponent's winning move!");
                }

                _agent.Remember(currentState, action, reward, _previousState, !manager.IsGameActive);
                Debug.Log($"Action: {action}, Reward: {reward}");
            }

            if (!manager.IsGameActive)
                _agent.Replay();
        }

        private void OnLoadButtonClicked() {
            _agent.LoadModel(fileNameInputField.text);
        }

        private void OnSaveButtonClicked() {
            _agent.SaveModel(fileNameInputField.text);
        }
    }
}