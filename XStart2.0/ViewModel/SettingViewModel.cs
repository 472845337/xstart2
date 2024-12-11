using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using XStart2._0.Bean.Weather;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class SettingViewModel : BaseViewModel {
        public bool MainHeadShow { get; set; }
        public bool MainTopMost { get; set; }// 主窗口是否置顶
        public bool Audio { get; set; }
        public bool RunDirectly { get; set; }
        public bool AutoRun { get; set; }
        public bool ExitWarn { get; set; }
        public bool ExitButtonType { get; set; }// 关闭按钮类型，是表示退出，否表示最小化
        public bool ShowInTaskbar { get; set; }
        public bool CloseBorderHide { get; set; }
        public string ClickType { get; set; }
        public string RdpModel { get; set; }
        public string TextEditor { get; set; }
        public string UrlOpen { get; set; }
        public string UrlOpenCustomBrowser { get; set; }
        [OnChangedMethod(nameof(GetIcon))]
        public int IconSize { get; set; }
        public BitmapImage SettingIcon { get; set; }
        public string Orientation { get; set; }
        public bool HideTitle { get; set; }
        public bool OneLineMulti { get; set; }
        public string WeatherApi { get; set; }
        public string WeatherImgTheme { get; set; }
        public string WeatherYkyApiAppId { get; set; }
        public string WeatherYkyApiAppSecret { get; set; }
        public string WeatherYkyApiUrl { get; set; }
        public string WeatherGaodeApiUrl { get; set; }
        public string WeatherGaodeAppKey { get; set; }
        public string WeatherSeniverseApiUrl { get; set; }
        public string WeatherSeniverseAppKey { get; set; }
        public string WeatherQApiUrl { get; set; }
        public string WeatherQAppKey { get; set; }
        public string WeatherOpenApiUrl { get; set; }
        public string WeatherOpenAppKey { get; set; }
        public string WeatherAccuApiUrl { get; set; }
        public string WeatherAccuAppKey { get; set; }
        public string WeatherVcApiUrl { get; set; }
        public string WeatherVcAppKey { get; set; }
        [DependsOn("WeatherImgTheme")]
        public ObservableCollection<Theme> ThemePngs { get; set; } = new ObservableCollection<Theme> {
            new Theme("xue","雪"),new Theme("lei", "雷"),new Theme("shachen","沙尘"),
            new Theme("wu","雾"),new Theme("bingbao","冰雹"),new Theme("yun","云"),
            new Theme("yu","雨"),new Theme("yin","阴"),new Theme("qing", "晴") };

        public void GetIcon() {
            SettingIcon = IconUtils.GetBitmapImage(Configs.AppStartPath + Constants.APP_ICON, IconSize);
        }
    }
}
