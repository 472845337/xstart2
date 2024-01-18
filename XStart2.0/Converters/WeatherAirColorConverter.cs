using System;
using System.Globalization;
using System.Windows.Media;

namespace XStart2._0.Converters {
    public class WeatherAirColorConverter : BaseValueConverter<WeatherAirColorConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            SolidColorBrush brush = null;
            if (null != value) {
                int air = System.Convert.ToInt32(value);
                
                if (air <= 50) {
                    brush = new SolidColorBrush(Colors.Green);
                } else if (air <= 100) {
                    brush = new SolidColorBrush(Colors.GreenYellow);
                } else if (air <= 150) {
                    brush = new SolidColorBrush(Colors.Orange);
                } else if (air <= 200) {
                    brush = new SolidColorBrush(Colors.OrangeRed);
                } else if (air <= 300) {
                    brush = new SolidColorBrush(Colors.Red);
                } else if (air <= 500) {
                    brush = new SolidColorBrush(Colors.DarkRed);
                } else {
                    brush = new SolidColorBrush(Colors.Maroon);
                }
                brush.Freeze();
            }
            return brush;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
