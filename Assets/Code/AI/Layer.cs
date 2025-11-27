using System;
using Code.Core.Utility;
using UnityEngine;

namespace Code.AI {
    [Serializable]
    public class Layer {
        private readonly int _inputsPerNeuron;
        private Neuron[] _neurons;
        private Layer _nextLayer;
        public int NeuronCount { get; }

        public Layer(int neuronCount, int inputsPerNeuron) {
            _inputsPerNeuron = inputsPerNeuron;
            NeuronCount = neuronCount;
            _neurons = new Neuron[neuronCount];
            for (int i = 0; i < neuronCount; i++) {
                _neurons[i] = new Neuron(inputsPerNeuron);
            }
        }

        public void SetNextLayer(Layer nextLayer) => _nextLayer = nextLayer;

        public float[] ForwardPropagation(float[] inputs) {
            float[] outputs;
            if (_inputsPerNeuron == 0) {
                outputs = inputs;
            }
            else {
                outputs = new float[_neurons.Length];

                for (int i = 0; i < _neurons.Length; i++) {
                    outputs[i] = _neurons[i].Activate(inputs);
                }
            }

            if (_nextLayer != null) {
                return _nextLayer.ForwardPropagation(outputs);
            }

            return outputs;
        }

        public float[] BackwardPropagation(float[] errors, float learningRate) {
            float[] inputErrors = null;

            if (_neurons[0].GetWeights().Count > 0) {
                // 입력층이 아닌 경우
                inputErrors = new float[_neurons[0].GetWeights().Count];
            }

            for (int i = 0; i < _neurons.Length; i++) {
                float neuronError = errors[i];

                // ReLU 역전파
                float reluDerivative = ReLU.Backward(_neurons[i].GetLastOutput());
                float gradient = neuronError * reluDerivative;

                // 가중치 업데이트를 위한 그래디언트 계산
                var lastInputs = _neurons[i].GetLastInputs();
                if (lastInputs != null) {
                    float[] weightGradients = new float[lastInputs.Length];
                    for (int j = 0; j < lastInputs.Length; j++) {
                        weightGradients[j] = gradient * lastInputs[j];

                        // 이전 층으로 전달할 오차 계산
                        if (inputErrors != null) {
                            inputErrors[j] += neuronError * _neurons[i].GetWeights()[j];
                        }
                    }

                    // 가중치와 편향 업데이트
                    _neurons[i].UpdateWeights(weightGradients, learningRate);
                }

                _neurons[i].UpdateBias(gradient, learningRate);
            }

            return inputErrors;
        }

        public void InitializeWeights() {
            foreach (var neuron in _neurons) {
                neuron.SettingByRandom();
            }
        }

        public LayerData GetLayerData() {
            LayerData layerData = new LayerData(_neurons.Length);

            for (int i = 0; i < _neurons.Length; i++) {
                var weights = _neurons[i].GetWeights();
                float[] weightArray = new float[weights.Count];
                for (int j = 0; j < weights.Count; j++) {
                    weightArray[j] = weights[j];
                }

                layerData.neurons[i] = new NeuronData(weightArray, _neurons[i].GetBias());
            }

            return layerData;
        }

        public void LoadLayerData(LayerData layerData) {
            if (layerData.neurons.Length != _neurons.Length) {
                Debug.LogError("Neuron count mismatch!");
                return;
            }

            for (int i = 0; i < _neurons.Length; i++) {
                _neurons[i].SetWeights(layerData.neurons[i].weights);
                _neurons[i].SetBias(layerData.neurons[i].bias);
            }
        }
    }
}