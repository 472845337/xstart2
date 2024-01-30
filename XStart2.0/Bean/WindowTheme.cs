using PropertyChanged;
using System.Windows.Media;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.Bean {

    [AddINotifyPropertyChangedInterface]
    public class WindowTheme {
        private WindowTheme() {
            if (string.IsNullOrEmpty(ThemeName)) {
                ThemeName = Constants.WINDOW_THEME_BLUE;
            }
        }
        public static WindowTheme Instance { private set; get; } = new WindowTheme();
        // 主题名称
        [OnChangedMethod(nameof(ChangeTheme))]
        public string ThemeName { get; set; }
        // 自定义主题颜色
        [OnChangedMethod(nameof(ChangeTheme))]
        public string ThemeCustom { get; set; }
        public SolidColorBrush ButtonConfirmBackGround { get; set; }
        public SolidColorBrush ButtonConfirmForeGround { get; set; }
        public SolidColorBrush ButtonConfirmIsMouseOverBackGround { get; set; }
        public SolidColorBrush ButtonConfirmIsMouseOverForeGround { get; set; }

        public SolidColorBrush ButtonCancelBackGround { get; set; }
        public SolidColorBrush ButtonCancelForeGround { get; set; }
        public SolidColorBrush ButtonCancelIsMouseOverBackGround { get; set; }
        public SolidColorBrush ButtonCancelIsMouseOverForeGround { get; set; }

        public SolidColorBrush ToggleButtonIsCheckedBackGround { get; set; }
        public SolidColorBrush ToggleButtonIsCheckedForeGround { get; set; }

        private void ChangeTheme() {
            if (Constants.WINDOW_THEME_BLUE.Equals(ThemeName)) {
                // 蓝色主题
                SetThemeColor(ColorUtils.GetBrush("#99CCFF"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#3399CC"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#99CCCC"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#0099CC"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#CCFFFF"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_GREEN.Equals(ThemeName)) {
                // 绿色主题
                SetThemeColor(ColorUtils.GetBrush("#339933"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#339966"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#669933"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#006633"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#99CC33"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_ORANGE.Equals(ThemeName)) {
                // 橙色主题
                SetThemeColor(ColorUtils.GetBrush("#FF9900"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#FF6600"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#CC9933"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#996600"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#FFCC00"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_RED.Equals(ThemeName)) {
                // 红色主题
                SetThemeColor(ColorUtils.GetBrush("#FF0033"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#CC0033"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#CC9999"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#990033"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#FF6666"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_GRAY.Equals(ThemeName)) {
                // 灰色主题
                SetThemeColor(ColorUtils.GetBrush("#CCCCCC"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#999999"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#DEDEDE"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#CBCBCB"), ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush("#DDDDDD"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_PURPLE.Equals(ThemeName)) {
                // 紫色主题
                SetThemeColor(ColorUtils.GetBrush("#9966CC"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#663399"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#996699"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#660066"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#FF99CC"), ColorUtils.GetBrush(Colors.Black));
            } else if (Constants.WINDOW_THEME_BLACK.Equals(ThemeName)) {
                // 黑色主题
                SetThemeColor(ColorUtils.GetBrush(Colors.Black), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#323232"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#565656"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#888888"), ColorUtils.GetBrush(Colors.White), ColorUtils.GetBrush("#454545"), ColorUtils.GetBrush(Colors.White));
            } else if (Constants.WINDOW_THEME_CUSTOM.Equals(ThemeName)) {
                if (!string.IsNullOrEmpty(ThemeCustom)) {
                    string[] colors = ThemeCustom.Split(Constants.SPLIT_CHAR);
                    SetThemeColor(ColorUtils.GetBrush(colors[0]), ColorUtils.GetBrush(colors[1]), ColorUtils.GetBrush(colors[2]), ColorUtils.GetBrush(colors[3]), ColorUtils.GetBrush(colors[4]), ColorUtils.GetBrush(colors[5]), ColorUtils.GetBrush(colors[6])
                        , ColorUtils.GetBrush(colors[7]), ColorUtils.GetBrush(colors[8]), ColorUtils.GetBrush(colors[9]));
                }
            }
        }

        private void SetThemeColor(SolidColorBrush brush1, SolidColorBrush brush2, SolidColorBrush brush3, SolidColorBrush brush4, SolidColorBrush brush5
            , SolidColorBrush brush6, SolidColorBrush brush7, SolidColorBrush brush8, SolidColorBrush brush9, SolidColorBrush brush10) {
            ButtonConfirmBackGround = brush1;
            ButtonConfirmForeGround = brush2;
            ButtonConfirmIsMouseOverBackGround = brush3;
            ButtonConfirmIsMouseOverForeGround = brush4;

            ButtonCancelBackGround = brush5;
            ButtonCancelForeGround = brush6;
            ButtonCancelIsMouseOverBackGround = brush7;
            ButtonCancelIsMouseOverForeGround = brush8;

            ToggleButtonIsCheckedBackGround = brush9;
            ToggleButtonIsCheckedForeGround = brush10;
        }

    }
}
