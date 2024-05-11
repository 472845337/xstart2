using System;
using System.Windows;
using System.Windows.Controls;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// CalendarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarWindow : Window {
        public readonly CalendarViewModel vm = new CalendarViewModel();

        private readonly System.Windows.Threading.DispatcherTimer currentMinuteTimer = new System.Windows.Threading.DispatcherTimer();
        public CalendarWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, EventArgs e) {
            SetLunarDateInfo(DateTime.Now);
            CurrentMinuteTime_Tick(sender, e);
            DataContext = vm;
            // Tick 分钟间隔时发生。
            currentMinuteTimer.Tick += new EventHandler(CurrentMinuteTime_Tick);
            // Interval 获取或设置计时器刻度之间的时间段
            currentMinuteTimer.Interval = TimeSpan.FromHours(1);
            currentMinuteTimer.Start();
            // 赋值句柄
            var formDependency = PresentationSource.FromDependencyObject(this);
            System.Windows.Interop.HwndSource winformWindow = formDependency as System.Windows.Interop.HwndSource;
            Config.Configs.CalendarHandler = winformWindow.Handle;
        }

        private void Window_Closing(object sender, EventArgs e) {
            Config.Configs.CalendarHandler = IntPtr.Zero;
            DataContext = null;
        }



        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            Calendar cal = sender as Calendar;
            if (cal.SelectedDate.HasValue) {
                DateTime dateTime = cal.SelectedDate.Value;
                SetLunarDateInfo(dateTime);
            }
        }

        private void SetLunarDateInfo(DateTime dateTime) {
            LunarCalendar lunarCalendar = new LunarCalendar(dateTime);
            vm.EraYear = lunarCalendar.GetEraYear();
            vm.Zodiac = lunarCalendar.ChineseZodiac;
            vm.EraMonth = lunarCalendar.GetEraMonth();
            vm.EraDay = lunarCalendar.GetEraDay();
            vm.WeekDay = lunarCalendar.ChineseWeek;
            vm.SolarTerm = string.IsNullOrEmpty(lunarCalendar.SolarTerm) ? lunarCalendar.SolarTermPrev : lunarCalendar.SolarTerm;
        }

        private void CurrentMinuteTime_Tick(object sender, EventArgs e) {
            // 下一分钟
            TimeSpan timeToGo = TimeUtils.GetTimeToNext(TimeEnum.MINUTE);
            currentMinuteTimer.Interval = timeToGo;
            LunarCalendar lunarCalendar = new LunarCalendar(DateTime.Now);
            vm.CurEraYear = lunarCalendar.GetEraYear();
            vm.CurZodiac = lunarCalendar.ChineseZodiac;
            vm.CurEraMonth = lunarCalendar.GetEraMonth();
            vm.CurEraDay = lunarCalendar.GetEraDay();
            vm.CurSolarTerm = string.IsNullOrEmpty(lunarCalendar.SolarTerm) ? lunarCalendar.SolarTermPrev : lunarCalendar.SolarTerm;
        }
    }
}
