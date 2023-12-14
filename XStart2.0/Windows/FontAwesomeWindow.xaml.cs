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
using XStart2._0.Bean;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// FontAwesomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FontAwesomeWindow : Window {
        FontAwesomeVM vm;
        public FontAwesomeWindow() {
            InitializeComponent();
            vm = new FontAwesomeVM();
            vm.SolidFontAwesomes = new List<FontAwesome> { };

        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }
    }
}
