using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Media;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    internal class BeautifulViewModel : BaseViewModel {
        public ObservableCollection<string> SystemFonts { get; set; }
        [OnChangedMethod(nameof(ConvertBrush))]
        public string Background { get; set; }// 窗口背景
        [OnChangedMethod(nameof(ConvertBrush))]
        public double MainOpacity { get; set; }// 窗口不透明度
        public Brush BackgroundBrush { get; set; }
        public string Foreground { get; set; }
        public int TextFontSize { get; set; }
        public string TextFontFamily { get; set; }
        public double Opacity { get; set; }// 按钮不透明度

        public void ConvertBrush() {
            BackgroundBrush = BackgroundUtils.GetBrush(Background, MainOpacity);
        }
    }
}
