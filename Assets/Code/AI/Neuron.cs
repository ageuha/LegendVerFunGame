using System.Collections.Generic;
using Code.Core.Utility;
using UnityEngine;

namespace Code.AI {
    public class Neuron {
        private float[] _weights;
        private float _bias;
        private float _lastOutput;
        private float[] _lastInputs;

        public Neuron(int inputCount) {
            if (inputCount > 0)
                _weights = new float[inputCount];
            _bias = 0f;
        }

        public float Activate(float[] inputs) {
            _lastInputs = inputs;

            float sum = _bias;
            if (_weights != null)

                for (int i = 0; i < _weights.Length; i++) {
                    sum += _weights[i] * inputs[i];
                }

            _lastOutput = ReLU.Forward(sum);
            return _lastOutput;
        }

        public void UpdateWeights(float[] gradients, float learningRate) {
            for (int i = 0; i < _weights.Length; i++) {
                _weights[i] -= learningRate * gradients[i];
            }
        }

        public void UpdateBias(float gradient, float learningRate) {
            _bias -= learningRate * gradient;
        }

        public float GetLastOutput() => _lastOutput;
        public float[] GetLastInputs() => _lastInputs;

        public void SettingByRandom() {
            for (var i = 0; i < _weights.Length; i++) {
                _weights[i] = Random.Range(-1f, 1f);
            }

            _bias = Random.Range(-0.01f, 0.01f);
        }

        public IReadOnlyList<float> GetWeights() => _weights;
        public float GetBias() => _bias;

        public void SetWeights(float[] weights) {
            for (int i = 0; i < weights.Length; i++) {
                _weights[i] = weights[i];
            }
        }

        public void SetBias(float bias) => _bias = bias;
    }
}