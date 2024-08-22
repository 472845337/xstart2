
using System.Windows;
using XStart2._0.Const;
using XStart2._0.ViewModel;


namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for CheckSecurityWindow.xaml
    /// </summary>
    public partial class CheckSecurityWindow : Window {
        SecurityVM vm;
        public CheckSecurityWindow(string title, string priSecurity, string ExitMsg = "") {
            InitializeComponent();
            vm = new SecurityVM {
                Title = title,
                PriSecurity = priSecurity,
                ExitMsg = ExitMsg
            };
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            // 口令校验窗口关闭前，退出提醒
            if ((null == DialogResult || false == DialogResult) && !string.IsNullOrEmpty(vm.ExitMsg)) {
                // 取消或退出时进行提醒
                if(MessageBoxResult.Cancel == MessageBox.Show(vm.ExitMsg, Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    e.Cancel = true;
                }
            }
        }

        private void CheckSecurity(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Security)) {
                MessageBox.Show("口令不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            } else if (!vm.Security.Equals(vm.PriSecurity)) {
                MessageBox.Show("口令不一致！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            DialogResult = true;
        }

        private void CancelSecurity(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }


}
