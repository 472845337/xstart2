using PropertyChanged;
using System.Windows.Media;
using XStart2._0.Const;

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
        // 项目文本颜色
        public string ProjectForeground { get; set; }
        // 文本字体
        public string FontFamily { get; set; }
        // 字体大小
        public int FontSize { get; set; }
        // 控件不透明度
        public double Opacity { get; set; }
        // 自定义主题颜色
        [OnChangedMethod(nameof(ChangeTheme))]
        public string ThemeCustom { get; set; }
        public string ButtonConfirmBackGround { get; set; }
        public string ButtonConfirmForeGround { get; set; }
        public string ButtonConfirmIsMouseOverBackGround { get; set; }
        public string ButtonConfirmIsMouseOverForeGround { get; set; }

        public string ButtonCancelBackGround { get; set; }
        public string ButtonCancelForeGround { get; set; }
        public string ButtonCancelIsMouseOverBackGround { get; set; }
        public string ButtonCancelIsMouseOverForeGround { get; set; }

        public string ToggleButtonIsCheckedBackGround { get; set; }
        public string ToggleButtonIsCheckedForeGround { get; set; }

        private void ChangeTheme() {
            if (Constants.WINDOW_THEME_BLUE.Equals(ThemeName)) {
                // 蓝色主题
                SetThemeColor("#99CCFF", Colors.Black.ToString(), "#3399CC", Colors.White.ToString(), "#99CCCC", Colors.Black.ToString(), "#0099CC", Colors.White.ToString(), "#CCFFFF", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_GREEN.Equals(ThemeName)) {
                // 绿色主题
                SetThemeColor("#339933", Colors.White.ToString(), "#339966", Colors.White.ToString(), "#669933", Colors.White.ToString(), "#006633", Colors.White.ToString(), "#99CC33", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_ORANGE.Equals(ThemeName)) {
                // 橙色主题
                SetThemeColor("#FF9900", Colors.White.ToString(), "#FF6600", Colors.White.ToString(), "#CC9933", Colors.Black.ToString(), "#996600", Colors.White.ToString(), "#FFCC00", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_RED.Equals(ThemeName)) {
                // 红色主题
                SetThemeColor("#FF0033", Colors.White.ToString(), "#CC0033", Colors.White.ToString(), "#CC9999", Colors.Black.ToString(), "#990033", Colors.White.ToString(), "#FF6666", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_GRAY.Equals(ThemeName)) {
                // 灰色主题
                SetThemeColor("#CCCCCC", Colors.Black.ToString(), "#999999", Colors.White.ToString(), "#DEDEDE", Colors.Black.ToString(), "#CBCBCB", Colors.Black.ToString(), "#DDDDDD", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_PURPLE.Equals(ThemeName)) {
                // 紫色主题
                SetThemeColor("#9966CC", Colors.White.ToString(), "#663399", Colors.White.ToString(), "#996699", Colors.White.ToString(), "#660066", Colors.White.ToString(), "#FF99CC", Colors.Black.ToString());
            } else if (Constants.WINDOW_THEME_BLACK.Equals(ThemeName)) {
                // 黑色主题
                SetThemeColor(Colors.Black.ToString(), Colors.White.ToString(), "#323232", Colors.White.ToString(), "#565656", Colors.White.ToString(), "#888888", Colors.White.ToString(), "#454545", Colors.White.ToString());
            } else if (Constants.WINDOW_THEME_CUSTOM.Equals(ThemeName)) {
                if (!string.IsNullOrEmpty(ThemeCustom)) {
                    string[] colors = ThemeCustom.Split(Constants.SPLIT_CHAR);
                    SetThemeColor(colors[0], colors[1], colors[2], colors[3], colors[4], colors[5], colors[6], colors[7], colors[8], colors[9]);
                }
            }
        }

        private void SetThemeColor(string brush1, string brush2, string brush3, string brush4, string brush5
            , string brush6, string brush7, string brush8, string brush9, string brush10) {
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
