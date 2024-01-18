using System;
using System.Globalization;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.Converters {
    public class WeaImgConverter : BaseValueConverter<WeaImgConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (null != value) {
                return ImageUtils.File2BitmapImage(Config.Configs.AppStartPath + "Files/Images/Weather/" + Config.Configs.weatherImgTheme + "/" + value + ".png");
            } else {
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
