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
        readonly CalendarVM vm = new CalendarVM();
        public CalendarWindow() {
            InitializeComponent();


        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            Calendar cal = sender as Calendar;
            if (cal.SelectedDate.HasValue) {
                DateTime dateTime = cal.SelectedDate.Value;
                LunarCalendar lunarCalendar = new LunarCalendar(dateTime);
                vm.LunarYear = lunarCalendar.GetEraYear();
                vm.LunarMonth = lunarCalendar.GetEraMonth();
                vm.LunarDay = lunarCalendar.GetEraDay();
            }
        }
    }
}
