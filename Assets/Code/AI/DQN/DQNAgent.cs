using UnityEngine;

namespace Code.AI.DQN {
    public class DQNAgent {
        private NeuralNetwork _qNetwork;
        private NeuralNetwork _targetNetwork; // Target Network
        private ReplayBuffer _replayBuffer;
        private Trainer _trainer;

        // DQN 하이퍼파라미터
        public float epsilon = 1.0f; // 탐험 확률
        public float epsilonDecay = 0.997f; // 탐험 감소율
        public float epsilonMin = 0.01f; // 최소 탐험 확률
        public float gamma = 0.95f; // 할인 인자
        public int targetUpdateFreq = 100; // Target Network 업데이트 빈도

        private int _stepCount = 0;

        public DQNAgent(int stateSize, int actionSize, int[] hiddenLayers, float learningRate = 0.001f) {
            // Q-Network 생성
            _qNetwork = new NeuralNetwork(stateSize, hiddenLayers, actionSize);
            _qNetwork.InitializeWeights();

            // Target Network 생성 (구조 동일)
            _targetNetwork = new NeuralNetwork(stateSize, hiddenLayers, actionSize);
            _targetNetwork.InitializeWeights();

            _trainer = new Trainer(_qNetwork, learningRate);
            _replayBuffer = new ReplayBuffer(10000);

            // Target Network를 Q-Network로 초기화
            UpdateTargetNetwork();
        }

        public int SelectAction(float[] state) {
            if (Random.Range(0f, 1f) < epsilon) {
                return Random.Range(0, GetActionSize());
            }

            float[] qValues = _qNetwork.ForwardPropagation(state);
            return GetMaxValueIndex(qValues);
        }

        public void Remember(float[] state, int action, float reward, float[] nextState, bool done) {
            Experience experience = new Experience(state, action, reward, nextState, done);
            _replayBuffer.Add(experience);
        }

        public void Replay(int batchSize = 32) {
            if (!_replayBuffer.CanSample(batchSize)) return;

            Experience[] batch = _replayBuffer.SampleBatch(batchSize);

            foreach (Experience exp in batch) {
                float[] targetQValues = _qNetwork.ForwardPropagation(exp.state);

                if (exp.isDone) {
                    // 에피소드 종료시: Q(s,a) = reward
                    targetQValues[exp.action] = exp.reward;
                }
                else {
                    // Bellman 방정식: Q(s,a) = reward + γ * max(Q(s',a'))
                    float[] nextQValues = _targetNetwork.ForwardPropagation(exp.nextState);
                    float maxNextQ = GetMaxValue(nextQValues);
                    targetQValues[exp.action] = exp.reward + gamma * maxNextQ;
                }

                // 학습
                _trainer.Train(exp.state, targetQValues);
            }

            // Epsilon 감소
            if (epsilon > epsilonMin) {
                epsilon *= epsilonDecay;
            }

            _stepCount++;

            // Target Network 주기적 업데이트
            if (_stepCount % targetUpdateFreq == 0) {
                UpdateTargetNetwork();
            }
        }

        private void UpdateTargetNetwork() {
            _qNetwork.SaveToPersistentData("temp_qnetwork");
            _targetNetwork.LoadFromPersistentData("temp_qnetwork");
            Debug.Log("Target Network Updated!");
        }

        private int GetMaxValueIndex(float[] array) {
            int maxIndex = 0;
            for (int i = 1; i < array.Length; i++) {
                if (array[i] > array[maxIndex]) {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private float GetMaxValue(float[] array) {
            float max = array[0];
            for (int i = 1; i < array.Length; i++) {
                if (array[i] > max) {
                    max = array[i];
                }
            }

            return max;
        }

        private int GetActionSize() {
            // 출력층 크기 = 행동 수
            return _qNetwork.ForwardPropagation(new float[GetStateSize()]).Length;
        }

        private int GetStateSize() {
            // 이 부분은 생성자에서 받은 값을 저장해두어야 함
            // 임시로 하드코딩, 실제로는 멤버 변수로 저장
            return 9; // 틱택토 예시
        }

        // 모델 저장/로드
        public void SaveModel(string fileName) {
            _qNetwork.SaveToPersistentData(fileName);
            PlayerPrefs.SetFloat(fileName, epsilon);
            Debug.Log(epsilon);
        }

        public void LoadModel(string fileName) {
            _qNetwork.LoadFromPersistentData(fileName);
            UpdateTargetNetwork();
            epsilon = PlayerPrefs.GetFloat(fileName, 1.0f);
        }
    }
}