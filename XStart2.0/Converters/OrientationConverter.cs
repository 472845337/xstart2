using System;
using System.Windows.Controls;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class OrientationConverter : BaseValueConverter<OrientationConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return Orientation.Horizontal;
            }
            if (Constants.ORIENTATION_HORIZONTAL.Equals(value)) {
                return Orientation.Horizontal;
            }else {
                return Orientation.Vertical;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
