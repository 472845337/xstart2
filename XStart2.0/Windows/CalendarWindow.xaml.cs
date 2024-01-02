using System.Windows;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// CalendarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarWindow : Window {
        CalendarVM vm = new CalendarVM();
        public CalendarWindow() {
            InitializeComponent();


        }
    }
}
