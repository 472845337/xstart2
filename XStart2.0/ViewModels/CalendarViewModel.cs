using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class CalendarViewModel : BaseViewModel {
        // 当前
        public MyDateTime MyDateTime { get; set; }
        // 当前农历年
        public string CurEraYear { get; set; }
        // 当前属相
        public string CurZodiac { get; set; }
        // 当前农历月
        public string CurEraMonth { get; set; }
        // 当前农历日
        public string CurEraDay { get; set; }
        // 当前所在节气
        public string CurSolarTerm { get; set; }
        // 当前农历时
        public string CurEraHour { get; set; }
        // 当前农历分
        public string CurEraMinute { get; set; }
        // 当前节假日
        public string CurHoliday { get; set; }
        // 农历年
        public string EraYear { get; set; }
        // 属相
        public string Zodiac { get; set; }
        // 农历月
        public string EraMonth { get; set; }
        // 农历日
        public string EraDay { get; set; }
        // 节气
        public string SolarTerm { get; set; }
        // 周几
        public string WeekDay { get; set; }
        public string Holiday { get; set; }
    }
}
