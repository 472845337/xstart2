using XStart2._0.Bean;

namespace XStart2._0.ViewModel {
    public class CalendarViewModel : BaseViewModel {
        // 当前
        public MyDateTime MyDateTime { get; set; }
        // 当前农历年
        public string CurChineseDate { get; set; }
        // 当前干支年
        public string CurEraDate { get; set; }
        // 当前属相
        public string CurZodiac { get; set; }
        // 当前所在节气
        public string CurSolarTerm { get; set; }
        // 当前星期
        public string CurWeekDay { get; set; }
        // 当前节假日
        public string CurHoliday { get; set; }
        // 所选日期
        public string SelectedDate { get; set; } = "未选定日期";
        // 农历日期
        public string ChineseDate { get; set; }
        // 干支日期
        public string EraDate { get; set; }
        // 属相
        public string Zodiac { get; set; }
        // 节气
        public string SolarTerm { get; set; }
        // 周几
        public string WeekDay { get; set; }
        // 假日名
        public string Holiday { get; set; }
    }
}
