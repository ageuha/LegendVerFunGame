namespace Code.Core.Utility {
    public static class ReLU {
        public static float Forward(float x) => x > 0 ? x : 0;
        public static double Forward(double x) => x > 0 ? x : 0;

        public static float Backward(float y) => y > 0 ? 1 : 0;
        public static double Backward(double y) => y > 0 ? 1 : 0;
    }
}