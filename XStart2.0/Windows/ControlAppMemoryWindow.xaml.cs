using System.Windows;
using XStart2._0.Const;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// ControlAppMemory.xaml 的交互逻辑
    /// </summary>
    public partial class ControlAppMemoryWindow : Window {
        public ControlAppMemoryViewModel vm = new ControlAppMemoryViewModel();

        public ControlAppMemoryWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            string errMsg = null;
            if (string.IsNullOrEmpty(vm.AppName)) {
                errMsg = "应用程序名不能为空！";
            } else if (string.IsNullOrEmpty(vm.MinMemory)) {
                errMsg = "应用程序最小内存不能为空！";
            } else if (string.IsNullOrEmpty(vm.MaxMemory)) {
                errMsg = "应用程序最大内存不能为空！";
            } else {
                if (!int.TryParse(vm.MinMemory, out _)) {
                    errMsg = "应用程序最小内存非数字！";
                } else if (!int.TryParse(vm.MaxMemory, out _)) {
                    errMsg = "应用程序最大内存非数字！";
                }
            }
            if (string.IsNullOrEmpty(errMsg)) {
                DialogResult = true;
            } else {
                MessageBox.Show(errMsg, Constants.MESSAGE_BOX_TITLE_ERROR);
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
