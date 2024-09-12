using System;
using XStart2._0.Utils;

namespace XStart2._0.Converters {
    internal class GradientColorConverter :BaseValueConverter<GradientColorConverter> {

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return GradientColorUtils.GetBrush((string)value);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
