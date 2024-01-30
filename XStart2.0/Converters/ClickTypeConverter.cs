using System;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class ClickTypeConverter : BaseValueConverter<ClickTypeConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string openType = (string)value;
            if (Constants.CLICK_TYPE_SINGLE.Equals(openType)) {
                return true;
            } else {
                return false;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value || !(value is bool boolean)) {
                return Constants.CLICK_TYPE_SINGLE;
            } else {
                return boolean ? Constants.CLICK_TYPE_SINGLE : Constants.CLICK_TYPE_DOUBLE;
            }
        }
    }
}
