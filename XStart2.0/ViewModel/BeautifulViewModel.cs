using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Media;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    internal class BeautifulViewModel : BaseViewModel {
        public ObservableCollection<string> SystemFonts { get; set; }
        [OnChangedMethod(nameof(ConvertBrush))]
        public string Background { get; set; }
        public Brush BackgroundBrush { get; set; }
        public string Foreground { get; set; }
        public int TextFontSize { get; set; }
        public string TextFontFamily { get; set; }
        // 滚动条值范围是0-10，所以不透明度要除10
        [OnChangedMethod(nameof(SetShowValue))]
        public double Opacity { get; set; }
        // 显示的值
        public double ShowOpacity { get; set; }

        private void SetShowValue() {
            // 滚动条值是0-10
            ShowOpacity = Opacity / 10.0D;
        }

        public void ConvertBrush() {
            Brush backgrounBrush = null;
            if (string.IsNullOrEmpty(Background)) {
                backgrounBrush = new SolidColorBrush(Colors.White);
            } else if (Background.StartsWith("#")) {
                backgrounBrush = ColorUtils.GetBrush(Background);
            } else {
                // 读取图片文件
                backgrounBrush = new ImageBrush {
                    ImageSource = ImageUtils.File2BitmapImage(Background)
                };
            }
            backgrounBrush.Freeze();
            BackgroundBrush = backgrounBrush;
        }
    }
}
