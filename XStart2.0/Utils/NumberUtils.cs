using System;

namespace XStart2._0.Utils {
    class NumberUtils {
        public static double Accuracy(double d, int digit) {
            int digitNumber = GetDigit(digit);
            return Math.Round(d * digitNumber, digit) / (double)digitNumber;
        }

        public static double Accuracy(float f, int digit) {
            int digitNumber = GetDigit(digit);
            return Math.Round(f * digitNumber, digit) / (float)digitNumber;
        }

        private static int GetDigit(int digit) {
            int a = 1;
            for(int i = 0; i < digit; i++) {
                a *= 10;
            }
            return a;
        }
    }
}
