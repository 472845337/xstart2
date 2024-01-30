using System.Windows;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// ColumnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnWindow : Window {
        public ColumnVM vm;
        public ColumnWindow(ColumnVM columnVm) {
            InitializeComponent();
            vm = columnVm;
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Save_Column(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Name)) {
                MessageBox.Show("名称不能为空！", "错误");
                return;
            } else {
                DialogResult = true;
            }
        }
    }
}
