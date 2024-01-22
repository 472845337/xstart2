using System;
using System.Globalization;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.Converters {
    public class WeaImgConverter : BaseMultiValueConverter<WeaImgConverter> {
        public override object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) {
            if (null != value && value.Length>0) {
                string themePath = Config.Configs.weatherImgTheme;
                if (value.Length>1) {
                    themePath = value[1] as string;
                }
                if (Constants.WEATHER_IMG_THEME_GIF.Equals(themePath)) {
                    return ImageUtils.File2BitmapImage($"{Config.Configs.AppStartPath}Files/Images/Weather/{themePath}/{value[0]}.gif");
                } else {
                    return ImageUtils.File2BitmapImage($"{Config.Configs.AppStartPath}Files/Images/Weather/{themePath}/{value[0]}.png");
                }
                
            } else {
                return null;
            }
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
