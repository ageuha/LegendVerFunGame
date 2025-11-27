namespace Code.AI {
    public static class CostFunction {
        public static float MSE(float[] predicted, float[] actual) {
            if (predicted.Length != actual.Length) {
                throw new System.ArgumentException("Arrays must have the same length");
            }

            float sum = 0f;
            for (int i = 0; i < predicted.Length; i++) {
                float diff = predicted[i] - actual[i];
                sum += diff * diff;
            }
            return sum / predicted.Length;
        }

        public static float[] MSEDerivative(float[] predicted, float[] actual) {
            if (predicted.Length != actual.Length) {
                throw new System.ArgumentException("Arrays must have the same length");
            }

            float[] derivatives = new float[predicted.Length];
            for (int i = 0; i < predicted.Length; i++) {
                derivatives[i] = 2f * (predicted[i] - actual[i]) / predicted.Length;
            }
            return derivatives;
        }
    }
}