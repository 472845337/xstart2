using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// WeatherWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherWindow : Window {
        WeatherViewModel vm = new WeatherViewModel();
        public WeatherWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, EventArgs e) {
            // 初始化省市数据
            vm.Provinces.Add(new Bean.Province() { Id = 1, Name = "江西省", Cities = new ObservableCollection<Bean.City>() { new Bean.City() { Id = 1, Name = "九江" }, new Bean.City() { Id = 2, Name = "南昌" } } });
            vm.Provinces.Add(new Bean.Province() { Id = 2, Name = "河南省", Cities = new ObservableCollection<Bean.City>() { new Bean.City() { Id = 1, Name = "洛阳" }, new Bean.City() { Id = 2, Name = "郑州" } } });

            DataContext = vm;
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetWeather_Click(object sender, RoutedEventArgs e) {

        }
    }
}
