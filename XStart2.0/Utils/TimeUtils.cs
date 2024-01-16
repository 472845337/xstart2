using System;

namespace XStart2._0.Utils {
    public class TimeUtils {
        /// <summary>
        /// 计算到下一个节点所需时间
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static TimeSpan GetTimeToNext(TimeEnum timeEnum) {
            DateTime now = DateTime.Now;
            DateTime next;
            if (TimeEnum.DAY == timeEnum) {
                next = now.Date.AddHours(24);
            } else if (TimeEnum.HOUR == timeEnum) {
                DateTime nowHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
                next = nowHour.AddMinutes(60);
            } else if (TimeEnum.MINUTE == timeEnum) {
                DateTime nowMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                next = nowMinute.AddSeconds(60);
            } else if (TimeEnum.SECOND == timeEnum) {
                DateTime nowSecond = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                next = nowSecond.AddSeconds(1);
            } else {
                next = now.AddSeconds(1);
            }
            return next - now;
        }
    }

    public enum TimeEnum {
        DAY,
        HOUR,
        MINUTE,
        SECOND
    }
}
