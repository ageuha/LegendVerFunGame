using System;

namespace Code.Core.Extensions {
    public static class ExtendedRandom {
        public static double NextGaussianDouble(this Random r) {
            double num1;
            double d;
            do {
                num1 = 2.0 * r.NextDouble() - 1.0;
                double num2 = 2.0 * r.NextDouble() - 1.0;
                d = num1 * num1 + num2 * num2;
            } while (d >= 1.0);

            double num3 = Math.Sqrt(-2.0 * Math.Log(d) / d);
            return num1 * num3;
        }
    }
}