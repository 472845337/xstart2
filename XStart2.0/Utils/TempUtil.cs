using System;

namespace XStart2._0.Utils {
    public class TempUtil {
        /// <summary>
        /// 华氏度转摄氏度
        /// </summary>
        /// <param name="F"></param>
        /// <returns></returns>
        public static double F2C(double F, int digits) {
            double c = (F - 32) * 5 / 9;
            return Math.Round(c, digits);
        }

        /// <summary>
        /// 开尔文转摄氏度
        /// </summary>
        /// <param name="F"></param>
        /// <returns></returns>
        public static double K2C(double K, int digits) {
            double c = K - 273.15;
            return Math.Round(c, digits);
        }
    }
}
