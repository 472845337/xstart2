using System.Windows;
using XStart2._0.Const;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// MstscWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MstscWindow : Window {
        public MstscViewModel vm = new MstscViewModel();
        public MstscWindow() {
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
            if (string.IsNullOrEmpty(vm.Address)) {
                errMsg = "服务器地址不能为空！";
            } else if (!string.IsNullOrEmpty(vm.Port)) {
                if (!int.TryParse(vm.Port, out int port)) {
                    errMsg = "端口必须是数字！";
                    if (port <= 0 || port > 65535) {
                        errMsg = "端口值在0-65535之间";
                    }
                }
            }
            if (string.IsNullOrEmpty(errMsg)) {
                DialogResult = true;
            } else {
                MessageBox.Show(errMsg, Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            e.Handled = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
