using System;
using System.Windows;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class Orientation2CenterConverter : BaseValueConverter<Orientation2CenterConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return HorizontalAlignment.Left;
            }
            if (Constants.ORIENTATION_HORIZONTAL.Equals(value)) {
                return HorizontalAlignment.Left;
            } else {
                return HorizontalAlignment.Center;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value == null || !(value is HorizontalAlignment)) {
                return Constants.ORIENTATION_HORIZONTAL;
            } else {
                if (HorizontalAlignment.Left.Equals(value)) {
                    return Constants.ORIENTATION_HORIZONTAL;
                } else {
                    return Constants.ORIENTATION_VERTICAL;
                }
            }
        }
    }
}
