using System.Windows;
using XStart2._0.Const;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// AddSecurityWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RemoveSecurityWindow : Window {
        public SecurityVM VM { get; set; }
        public RemoveSecurityWindow() {
            InitializeComponent();
            Loaded += WindowLoaded;
            Closing += Window_Closing;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            DataContext = VM;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void SaveSecurity(object sender, RoutedEventArgs e) {
            if (Constants.OPERATE_UPDATE.Equals(VM.Operate) || Constants.OPERATE_REMOVE.Equals(VM.Operate)) {
                if (string.IsNullOrEmpty(VM.PriSecurity)) {
                    MessageBox.Show("原口令不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                    return;
                } else if (!VM.CurSecurity.Equals(VM.PriSecurity)) {
                    MessageBox.Show("原口令不一致！", Constants.MESSAGE_BOX_TITLE_ERROR);
                    return;
                }
            }
            if (Constants.OPERATE_CREATE.Equals(VM.Operate) || Constants.OPERATE_UPDATE.Equals(VM.Operate)) {
                if (string.IsNullOrEmpty(VM.Security)) {
                    MessageBox.Show("口令不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                    return;
                } else if (string.IsNullOrEmpty(VM.ConfirmSecurity)) {
                    MessageBox.Show("确认口令不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                    return;
                } else if (!VM.Security.Equals(VM.ConfirmSecurity)) {
                    MessageBox.Show("确认口令不一致！", Constants.MESSAGE_BOX_TITLE_ERROR);
                    return;
                }
            }
            DialogResult = true;
        }

        private void CancelSecurity(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
