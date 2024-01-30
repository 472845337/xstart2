using PropertyChanged;

namespace XStart2._0.Bean {
    [AddINotifyPropertyChangedInterface]
    public class MyDateTime {
        public string CurDate { get; set; }
        public string CurTime { get; set; }
        public string CurWeekDay { get; set; }
    }
}
