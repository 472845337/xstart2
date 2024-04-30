using System;

namespace XStart2._0.Utils {
    public class DateUtil {
        public static DateTime GetTime(string time) {
            DateTime dateTime = DateTime.Parse(time);
            return dateTime;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">时间戳</param>
        /// <returns></returns>
        public static DateTime GetTime(long time) {
            DateTime dateTime = new DateTime(time);
            return dateTime;
        }

        public static string Format(DateTime dateTime, string format) {
            return dateTime.ToString(format);
        }
    }
}
