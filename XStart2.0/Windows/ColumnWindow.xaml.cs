using System.Windows;
using System.Windows.Input;
using XStart2._0.Const;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// ColumnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnWindow : Window {
        public ColumnVM vm;
        public ColumnWindow(ColumnVM columnVm) {
            InitializeComponent();
            vm = columnVm;
            vm.Types = XStartService.TypeDic.Values;
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
        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // 确认按钮
                Save_Column(sender, e);
            }
        }
        private void Save_Column(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Name)) {
                MsgBoxUtils.ShowError("名称不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            } else {
                DialogResult = true;
            }
        }
    }
}
