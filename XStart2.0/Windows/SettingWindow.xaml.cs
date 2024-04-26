using System.Reflection;
using System.Windows;
using XStart2._0.Commands;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window {
        public int OpenTab { get; set; }
        public SettingViewModel settingVM = new SettingViewModel();
        public SettingWindow(MainViewModel mainVm) {
            InitializeComponent();
            settingVM.Audio = mainVm.Audio;
            settingVM.TopMost = mainVm.TopMost;
            settingVM.AutoRun = mainVm.AutoRun;
            settingVM.ExitWarn = mainVm.ExitWarn;
            settingVM.CloseBorderHide = mainVm.CloseBorderHide;
            settingVM.ClickType = mainVm.ClickType;
            settingVM.RdpModel = mainVm.RdpModel;
            settingVM.UrlOpen = mainVm.UrlOpen;
            settingVM.UrlOpenCustomBrowser = mainVm.UrlOpenCustomBrowser;
            settingVM.IconSize = mainVm.IconSize;

            settingVM.Orientation = mainVm.Orientation;
            settingVM.HideTitle = mainVm.HideTitle;
            settingVM.OneLineMulti = mainVm.OneLineMulti;
            settingVM.TopMost = true;
            settingVM.WeatherApiAppId = Configs.weatherApiAppId;
            settingVM.WeatherApiAppSecret = Configs.weatherApiAppSecret;
            settingVM.WeatherApiUrl = Configs.weatherApiUrl;
            settingVM.WeatherImgTheme = Configs.weatherImgTheme;
            Loaded += SettingWindow_Loaded;
            Closing += SettingWindow_Closing;
        }

        private void SettingWindow_Loaded(object sender, RoutedEventArgs e) {
            DataContext = settingVM;
            SettingTabControl.SelectedIndex = OpenTab;
        }

        private void SettingWindow_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e) {
            // 如果配置了自定义浏览器，则校验是否选中了浏览器地址
            if (Constants.URL_OPEN_CUSTOM.Equals(settingVM.UrlOpen) && string.IsNullOrEmpty(settingVM.UrlOpenCustomBrowser)) {
                MessageBox.Show("自定义浏览器未配置路径", Constants.MESSAGE_BOX_TITLE_ERROR);
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
            OpenNewWindowUtils.SetTopmost(this);
            BackUpCommand.ShowBackUpWindow();
            OpenNewWindowUtils.RecoverTopmost(this, settingVM);
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recover_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            ResumeCommand.ShowResumeWindow();
            OpenNewWindowUtils.RecoverTopmost(this, settingVM);
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
            NotifyUtils.ShowNotification("桌面快捷方式创建成功！");
        }

        /// <summary>
        /// 打开注册页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoRegesit_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            ProjectUtils.ExecuteApp(new Bean.Project() { Kind = Bean.Project.KIND_URL, Path = Constants.WEATHER_REGISTER_URL });
        }
    }
}
