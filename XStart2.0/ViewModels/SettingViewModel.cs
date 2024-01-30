﻿using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using XStart2._0.Bean.Weather;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    public class SettingViewModel : BaseViewModel {
        public bool Audio { get; set; }
        public bool AutoRun { get; set; }
        public bool ExitWarn { get; set; }
        public bool CloseBorderHide { get; set; }
        public string ClickType { get; set; }
        public string UrlOpen { get; set; }
        public string UrlOpenCustomBrowser { get; set; }
        [OnChangedMethod(nameof(GetIcon))]
        public int IconSize { get; set; }
        public BitmapImage SettingIcon { get; set; }
        public string Orientation { get; set; }
        public bool HideTitle { get; set; }
        public bool OneLineMulti { get; set; }
        public string WeatherApiAppId { get; set; }
        public string WeatherApiAppSecret { get; set; }
        public string WeatherApiUrl { get; set; }
        public string WeatherImgTheme { get; set; }

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
