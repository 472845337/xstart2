using System;
using System.Globalization;

namespace XStart2._0.Converters {
    public class WeatherAirConverter : BaseValueConverter<WeatherAirConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (null != value) {
                int air = System.Convert.ToInt32(value);
                if (air <= 50) {
                    return "优";
                } else if (air <= 100) {
                    return "良";
                } else if (air <= 150) {
                    return "轻度污染";
                } else if (air <= 200) {
                    return "中度污染";
                } else if (air <= 300) {
                    return "重度污染";
                } else if (air <= 500) {
                    return "严重污染";
                } else {
                    return "污染暴表";
                }
            } else {
                return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
