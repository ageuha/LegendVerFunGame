using System;

namespace Code.AI {
    [Serializable]
    public class NetworkData {
        public LayerData[] layers;
        public int inputCount;
        public int[] hiddenLayerSizes;
        public int outputCount;

        public NetworkData(int inputCount, int[] hiddenLayerSizes, int outputCount) {
            this.inputCount = inputCount;
            this.hiddenLayerSizes = hiddenLayerSizes;
            this.outputCount = outputCount;
        }
    }

    [Serializable]
    public class LayerData {
        public NeuronData[] neurons;

        public LayerData(int neuronCount) {
            neurons = new NeuronData[neuronCount];
        }
    }

    [Serializable]
    public class NeuronData {
        public float[] weights;
        public float bias;

        public NeuronData(float[] weights, float bias) {
            this.weights = weights;
            this.bias = bias;
        }
    }

}