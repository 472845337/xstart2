using System;
using System.Globalization;
using System.Windows;

namespace XStart2._0.Converters {
    /// <summary>
    /// 参数和值相等转换显示隐藏
    /// </summary>
    internal class ValueEParam2CollapsedConverter : BaseValueConverter<ValueEParam2CollapsedConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || parameter == null)
                return Visibility.Visible;

            return parameter.Equals(value) ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return parameter;
        }
    }
}
