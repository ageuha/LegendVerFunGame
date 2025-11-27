using UnityEngine;

namespace Code.AI {
    public class Trainer {
        private readonly NeuralNetwork _network;
        private readonly float _learningRate;

        public Trainer(NeuralNetwork network, float learningRate = 0.01f) {
            _network = network;
            _learningRate = learningRate;
        }

        public void Train(float[] inputs, float[] expectedOutputs) {
            float[] outputs = _network.ForwardPropagation(inputs);
            
            float cost = CostFunction.MSE(outputs, expectedOutputs);

            float[] outputErrors = CostFunction.MSEDerivative(outputs, expectedOutputs);
            
            _network.BackwardPropagation(outputErrors, _learningRate);
            
            Debug.Log($"Training Cost: {cost}");
        }
        
        public float[] TrainWithOutput(float[] inputs, float[] expectedOutputs) {
            float[] outputs = _network.ForwardPropagation(inputs);
            
            float cost = CostFunction.MSE(outputs, expectedOutputs);

            float[] outputErrors = CostFunction.MSEDerivative(outputs, expectedOutputs);
            
            _network.BackwardPropagation(outputErrors, _learningRate);
            
            Debug.Log($"Training Cost: {cost}");
            return outputs;
        }

        public void TrainBatch(float[][] inputsBatch, float[][] expectedOutputsBatch) {
            float totalCost = 0f;
            
            for (int i = 0; i < inputsBatch.Length; i++) {
                float[] outputs = _network.ForwardPropagation(inputsBatch[i]);
                totalCost += CostFunction.MSE(outputs, expectedOutputsBatch[i]);
                
                float[] outputErrors = CostFunction.MSEDerivative(outputs, expectedOutputsBatch[i]);
                _network.BackwardPropagation(outputErrors, _learningRate);
            }
            
            float averageCost = totalCost / inputsBatch.Length;
            Debug.Log($"Batch Training Average Cost: {averageCost}");
        }
    }
}