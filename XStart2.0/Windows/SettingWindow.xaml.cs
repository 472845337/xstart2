using System;
using System.Reflection;
using System.Windows;
using XStart.Const;
using XStart.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window {
        public SettingViewModel settingVM = new SettingViewModel();
        public SettingWindow(MainViewModel mainVm) {
            InitializeComponent();
            settingVM.Audio = mainVm.Audio;
            settingVM.TopMost = mainVm.TopMost;
            settingVM.AutoRun = mainVm.AutoRun;
            settingVM.ExitWarn = mainVm.ExitWarn;
            settingVM.CloseBorderHide = mainVm.CloseBorderHide;
            settingVM.ClickType = mainVm.ClickType;
            settingVM.UrlOpen = mainVm.UrlOpen;
            settingVM.UrlOpenCustomBrowser = mainVm.UrlOpenCustomBrowser;
            Loaded += SettingWindow_Loaded;
        }

        private void SettingWindow_Loaded(object sender, RoutedEventArgs e) {
            DataContext = settingVM;
        }
        
        private void SaveSetting_Click(object sender, RoutedEventArgs e) {
            // 如果配置了自定义浏览器，则校验是否选中了浏览器地址
            if (Constants.URL_OPEN_CUSTOM.Equals(settingVM.UrlOpen) && string.IsNullOrEmpty(settingVM.UrlOpenCustomBrowser)) {
                MessageBox.Show("自定义浏览器未配置路径", "错误");
                return;
            }
            DialogResult = true;
        }

        private void CancelSetting_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUp_Click(object sender, RoutedEventArgs e) {
            BackUpWindow backUpWindow = new BackUpWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen};
            backUpWindow.ShowDialog();
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recover_Click(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// 生成桌面快捷方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateDeskLink_Click(object sender, RoutedEventArgs e) {
            string name = Application.Current.MainWindow.GetType().Assembly.GetName().Name;
            string path = Assembly.GetExecutingAssembly().Location;
            FileUtils.CreateShortCutOnDesktop(name, path);
        }
    }
}
