using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace XStart2._0.Windows {
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window {
        public SettingWindow() {
            InitializeComponent();
        }

        private void ExitWarn_Checked(object sender, RoutedEventArgs e) {
            Console.WriteLine("当前退出提醒");
        }

        private void ExitWarn_Unchecked(object sender, RoutedEventArgs e) {
            Console.WriteLine("当前退出不提醒");
        }
    }
}
