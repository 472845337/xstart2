using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private string lunarYear;
        public string LunarYear { get => lunarYear; set { lunarYear = value; OnPropertyChanged("LunarYear"); } }
        private string lunarMonth;
        public string LunarMonth { get => lunarMonth; set { lunarMonth = value; OnPropertyChanged("LunarMonth"); } }
        private string lunarDay;
        public string LunarDay { get => lunarDay; set { lunarDay = value; OnPropertyChanged("LunarDay"); } }
        private string weekDay;
        public string WeekDay { get => weekDay; set { weekDay = value; OnPropertyChanged("WeekDay"); } }
    }
}
