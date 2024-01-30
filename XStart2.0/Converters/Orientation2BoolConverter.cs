using System;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    /// <summary>
    /// 排列转成布尔类型
    /// </summary>
    public class Orientation2BoolConverter : BaseValueConverter<Orientation2BoolConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool isCheck = false;
            if (null != parameter) {
                if (null == value && Constants.STYLE_DEFAULT.Equals(parameter)) {
                    isCheck = true;
                } else if (null != value && value.Equals(parameter)) {
                    isCheck = true;
                }
            } else {
                if (null == value) {
                    isCheck = true;
                }
                if (Constants.ORIENTATION_HORIZONTAL.Equals(value)) {
                    isCheck = true;
                }
            }
            return isCheck;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null != parameter) {
                if (Constants.STYLE_DEFAULT.Equals(parameter)) {
                    return null;
                } else {
                    return parameter;
                }
            } else {
                if (value is bool orientationCheck && !orientationCheck) {
                    return Constants.ORIENTATION_VERTICAL;
                } else {
                    return Constants.ORIENTATION_HORIZONTAL;
                }
            }

        }
    }
}
