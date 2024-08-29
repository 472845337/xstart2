using System;
using System.Globalization;
using System.Windows.Media;
using XStart2._0.Utils;

namespace XStart2._0.Converters {
    internal class MainBackgroundConverter : BaseValueConverter<MainBackgroundConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Brush backgrounBrush = null;
            string background = value as string;
            if (string.IsNullOrEmpty(background)) {
                backgrounBrush = new SolidColorBrush(Colors.White);
            } else if (background.StartsWith("#")) {
                backgrounBrush = ColorUtils.GetBrush(background);
            } else {
                // 读取图片文件
                backgrounBrush = new ImageBrush {
                    ImageSource = ImageUtils.File2BitmapImage(background)
                };
            }
            backgrounBrush.Freeze();
            return backgrounBrush;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
