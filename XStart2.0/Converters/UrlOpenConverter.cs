using System;

namespace XStart2._0.Converters {
    public class UrlOpenConverter : BaseValueConverter<UrlOpenConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value || string.IsNullOrEmpty(value as string) || parameter == null) {
                return false;
            }
            string checkValue = (string)value;
            string targetValue = (string)parameter;
            if (checkValue.Equals(targetValue)) {
                return true;
            } else {
                return false;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || parameter == null) {
                return null;
            }
            if ((bool)value) {
                return parameter.ToString();
            }
            return null;
        }
    }
}
