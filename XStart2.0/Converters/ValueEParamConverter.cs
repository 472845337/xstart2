using System;
using System.Globalization;

namespace XStart2._0.Converters {
    /// <summary>
    /// 值和参数相等转换器
    /// </summary>
    class ValueEParamConverter : BaseValueConverter<ValueEParamConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (null != parameter && parameter.Equals(value)) {
                return true;
            } else {
                return false;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return parameter;
        }
    }
}
