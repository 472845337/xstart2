using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XStart2._0.Config;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// MstscWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MstscWindow : Window {
        public MstscViewModel vm = new MstscViewModel();
        // 定义控件的跳转顺序
        Control[] controls;
        public MstscWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            controls = new Control[]{
                AddressTextBox,
                PortTextBox,
                AccountTextBox,
                PasswordPasswordBox,
                PasswordTextBox
            };
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DataContext = null;
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // 判断是否最后一个输入框
                var textBox = sender as Control;
                if (!vm.IsAdd || PasswordPasswordBox == textBox || PasswordTextBox == textBox) {
                    // 确认按钮
                    Confirm_Click(sender, e);
                } else if (null != controls && controls.Length > 0) {
                    TextBoxUtils.Move2Next(sender as Control, controls);
                }
            }
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
                MsgBoxUtils.ShowError(errMsg);
            }
            e.Handled = true;
        }

        private void SeePassowrd_Click(object sender, RoutedEventArgs e) {
            if (!vm.ShowPassword && !string.IsNullOrEmpty(Configs.admin.Password)) {
                CheckSecurityWindow checkSecurityWindow = new CheckSecurityWindow("请输入管理员口令", Configs.admin.Password) { Owner = this };
                Topmost = false;
                if (true != checkSecurityWindow.ShowDialog()) {
                    Topmost = true;
                    return;
                }
            }
            Topmost = true;
            vm.ShowPassword = !vm.ShowPassword;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
