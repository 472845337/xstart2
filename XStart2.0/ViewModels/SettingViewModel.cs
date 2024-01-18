
namespace XStart2._0.ViewModels {
    public class SettingViewModel : BaseViewModel {
        public bool Audio { get; set; }
        public bool AutoRun { get; set; }
        public bool ExitWarn { get; set; }
        public bool CloseBorderHide { get; set; }
        public string ClickType { get; set; }
        public string UrlOpen { get; set; }
        public string UrlOpenCustomBrowser { get; set; }
        public int IconSize { get; set; }
        public string Orientation { get; set; }
        public bool HideTitle { get; set; }
        public bool OneLineMulti { get; set; }
        public string WeatherApiAppId { get; set; }
        public string WeatherApiAppSecret { get; set; }
        public string WeatherApiUrl { get; set; }
        public string WeatherImgTheme { get; set; }
    }
}
