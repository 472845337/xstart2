using System;
using System.Globalization;

namespace XStart2._0.Converters {
    public class WeatherAirConverter : BaseValueConverter<WeatherAirConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string airStr = string.Empty;
            if (null != value) {
                int air = System.Convert.ToInt32(value);
                if (air <= 50) {
                    airStr = "优";
                } else if (air <= 100) {
                    airStr = "良";
                } else if (air <= 150) {
                    airStr = "轻度污染";
                } else if (air <= 200) {
                    airStr = "中度污染";
                } else if (air <= 300) {
                    airStr = "重度污染";
                } else if (air <= 500) {
                    airStr = "严重污染";
                } else {
                    airStr = "污染暴表";
                }
                airStr += " " + air;
            }
            return airStr;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
