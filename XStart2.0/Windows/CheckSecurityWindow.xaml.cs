using System.Windows;
using System.Windows.Interop;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModel;


namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for CheckSecurityWindow.xaml
    /// </summary>
    public partial class CheckSecurityWindow : Window {
        private string title;
        private string priSecurity;
        private string exitMsg;
        private bool runTypeShow;
        SecurityVM vm = new SecurityVM();

        public string AutoRunType {  get; set; }
        public CheckSecurityWindow(string title, string priSecurity, string exitMsg = "", bool isInit = false, bool runDirectly = false) {
            InitializeComponent();
            this.title = title;
            this.priSecurity = priSecurity;
            this.exitMsg = exitMsg;
            this.runTypeShow = isInit && runDirectly;
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.Title = title;
            vm.PriSecurity = priSecurity;
            vm.ExitMsg = exitMsg;
            vm.RunTypeShow = runTypeShow;
            Configs.LockHandler = new WindowInteropHelper(this).Handle;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            // 口令校验窗口关闭前，退出提醒
            if (!Configs.forceExit && (null == DialogResult || false == DialogResult) && !string.IsNullOrEmpty(vm.ExitMsg)) {
                // 取消或退出时进行提醒
                if (MessageBoxResult.Cancel == MessageBox.Show(vm.ExitMsg, Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly)) {
                    e.Cancel = true;
                } else {
                    Configs.LockHandler = System.IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// 搜索框回车执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKey(object sender, System.Windows.Input.KeyEventArgs e) {
            if(EventUtils.InputKey(sender, e, System.Windows.Input.Key.Enter)) {
                CheckSecurity(sender, e);
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
            AutoRunType = vm.AutoRunType;
            DialogResult = true;
        }

        private void CancelSecurity(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }


}
