using System.IO;
using UnityEngine;

namespace Code.AI {
    public class NeuralNetwork {
        private Layer _inputLayer;
        private Layer[] _hiddenLayers;
        private Layer _outputLayer;
        private readonly int _inputCount;

        public NeuralNetwork(int inputCount, int[] hiddenLayerSizes, int outputCount) {
            _inputLayer = new Layer(inputCount, 0);
            _inputCount = inputCount;

            _hiddenLayers = new Layer[hiddenLayerSizes.Length];
            for (int i = 0; i < hiddenLayerSizes.Length; i++) {
                int inputsPerNeuron = i == 0 ? inputCount : hiddenLayerSizes[i - 1];
                _hiddenLayers[i] = new Layer(hiddenLayerSizes[i], inputsPerNeuron);
                if (i > 0) {
                    _hiddenLayers[i - 1].SetNextLayer(_hiddenLayers[i]);
                }
                else {
                    _inputLayer.SetNextLayer(_hiddenLayers[i]);
                }
            }

            int lastHiddenLayerSize = hiddenLayerSizes.Length > 0 ? hiddenLayerSizes[^1] : inputCount;
            _outputLayer = new Layer(outputCount, lastHiddenLayerSize);
            if (hiddenLayerSizes.Length > 0) {
                _hiddenLayers[^1].SetNextLayer(_outputLayer);
            }
            else {
                _inputLayer.SetNextLayer(_outputLayer);
            }
        }

        public float[] ForwardPropagation(float[] inputs) {
            if (inputs.Length != _inputCount) {
                throw new System.ArgumentException("Input size does not match the network's input layer size.");
            }

            return _inputLayer.ForwardPropagation(inputs);
        }

        public void BackwardPropagation(float[] outputErrors, float learningRate) {
            // 출력층부터 역순으로 역전파
            float[] errors = outputErrors;

            // 출력층 역전파
            errors = _outputLayer.BackwardPropagation(errors, learningRate);

            // 히든층들 역전파 (역순)
            for (int i = _hiddenLayers.Length - 1; i >= 0; i--) {
                errors = _hiddenLayers[i].BackwardPropagation(errors, learningRate);
            }
        }

        // 모든 뉴런의 가중치를 랜덤으로 초기화
        public void InitializeWeights() {
            _outputLayer.InitializeWeights();
            foreach (var layer in _hiddenLayers) {
                layer.InitializeWeights();
            }
        }

        public void SaveToFile(string filePath) {
            int[] hiddenLayerSizes = new int[_hiddenLayers.Length];
            for (int i = 0; i < _hiddenLayers.Length; i++) {
                hiddenLayerSizes[i] = _hiddenLayers[i].NeuronCount;
            }

            NetworkData data = new NetworkData(_inputCount, hiddenLayerSizes, _outputLayer.NeuronCount);

            // 히든층 + 출력층 데이터 수집
            data.layers = new LayerData[_hiddenLayers.Length + 1];

            for (int i = 0; i < _hiddenLayers.Length; i++) {
                data.layers[i] = _hiddenLayers[i].GetLayerData();
            }

            data.layers[_hiddenLayers.Length] = _outputLayer.GetLayerData();

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);

            Debug.Log($"Network saved to: {filePath}");
        }

        // 네트워크 데이터 로드
        public void LoadFromFile(string filePath) {
            if (!File.Exists(filePath)) {
                Debug.LogError($"File not found: {filePath}");
                return;
            }

            string json = File.ReadAllText(filePath);
            NetworkData data = JsonUtility.FromJson<NetworkData>(json);

            // 네트워크 구조 확인
            if (data.inputCount != _inputCount || data.outputCount != _outputLayer.NeuronCount ||
                data.hiddenLayerSizes.Length != _hiddenLayers.Length) {
                Debug.LogError("Network structure mismatch!");
                return;
            }

            // 히든층 데이터 로드
            for (int i = 0; i < _hiddenLayers.Length; i++) {
                _hiddenLayers[i].LoadLayerData(data.layers[i]);
            }

            // 출력층 데이터 로드
            _outputLayer.LoadLayerData(data.layers[_hiddenLayers.Length]);

            Debug.Log($"Network loaded from: {filePath}");
        }

        // Unity의 Persistent Data Path에 저장 (더 안전)
        public void SaveToPersistentData(string fileName) {
            string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
            SaveToFile(filePath);
        }

        public void LoadFromPersistentData(string fileName) {
            string filePath = Path.Combine(Application.persistentDataPath, fileName + ".json");
            LoadFromFile(filePath);
        }
    }
}