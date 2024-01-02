using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    public class CalendarVM : BaseViewModel {
        public CalendarVM() {
            // 天干地支和星期
            LunarCalendar lc = LunarCalendar.Now;
            LunarYear = lc.GetEraYear();
            LunarMonth = lc.GetEraMonth();
            LunarDay = lc.GetEraDay();
            WeekDay = lc.ChineseWeek;
        }

        public string LunarYear { get; set; }
        public string LunarMonth { get; set; }
        public string LunarDay { get; set; }
        public string WeekDay { get; set; }
    }
}
