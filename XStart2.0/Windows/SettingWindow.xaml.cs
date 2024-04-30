using System.Reflection;
using System.Windows;
using XStart2._0.Commands;
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
            #region 天气配置
            settingVM.WeatherApi = mainVm.WeatherApi;
            settingVM.WeatherImgTheme = mainVm.WeatherImgTheme;
            #region 易客云
            settingVM.WeatherYkyApiAppId = mainVm.WeatherYkyApiAppId;
            settingVM.WeatherYkyApiAppSecret = mainVm.WeatherYkyApiAppSecret;
            settingVM.WeatherYkyApiUrl = mainVm.WeatherYkyApiUrl;
            #endregion
            #region 高德
            settingVM.WeatherGaodeApiUrl = mainVm.WeatherGaodeApiUrl;
            settingVM.WeatherGaodeAppKey = mainVm.WeatherGaodeAppKey;
            #endregion
            #region 心知
            settingVM.WeatherSeniverseApiUrl = mainVm.WeatherSeniverseApiUrl;
            settingVM.WeatherSeniverseAppKey = mainVm.WeatherSeniverseAppKey;
            #endregion
            #region 和风
            settingVM.WeatherQApiUrl = mainVm.WeatherQApiUrl;
            settingVM.WeatherQAppKey = mainVm.WeatherQAppKey;
            #endregion
            #region OpenWeather
            settingVM.WeatherOpenApiUrl = mainVm.WeatherOpenApiUrl;
            settingVM.WeatherOpenAppKey = mainVm.WeatherOpenAppKey;
            #endregion
            #region AccuWeather
            settingVM.WeatherAccuApiUrl = mainVm.WeatherAccuApiUrl;
            settingVM.WeatherAccuAppKey = mainVm.WeatherAccuAppKey;
            #endregion
            #endregion
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

        #region 打开注册页
        private void GoRegesitYky_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_YKY_REGISTER_URL);
        }
        private void GoRegesitGaode_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_GAODE_REGISTER_URL);
        }
        private void GoRegesitSeniverse_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_SENIVERSE_REGISTER_URL);
        }
        private void GoRegesitQ_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_Q_REGISTER_URL);
        }
        private void GoRegesitOpen_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_OPEN_REGISTER_URL);
        }
        private void GoRegesitAccu_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            OpenRegisterWeb(Constants.WEATHER_ACCU_REGISTER_URL);
        }
        private void OpenRegisterWeb(string registerUrl) {
            OpenNewWindowUtils.SetTopmost(this);
            ProjectUtils.ExecuteApp(new Bean.Project() { Kind = Bean.Project.KIND_URL, Path = registerUrl });
        }
        #endregion
    }
}
