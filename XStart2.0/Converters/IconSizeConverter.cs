using System;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class IconSizeConverter : IValueConverter {
        // 图标尺寸转换 32-Small 48-Mid, 72-Large, 256-Huge
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool isCheck = false;
            if (null == value || parameter == null) {
                return isCheck;
            }
            double checkValue = (double)value;
            string targetValue = (string)parameter;
            if (checkValue == Constants.ICON_SIZE_32 && Constants.ICON_SIZE_STR_SMALL.Equals(targetValue)) {
                isCheck = true;
            } else if (checkValue == Constants.ICON_SIZE_48 && Constants.ICON_SIZE_STR_MID.Equals(targetValue)) {
                isCheck = true;
            } else if (checkValue == Constants.ICON_SIZE_72 && Constants.ICON_SIZE_STR_LARGE.Equals(targetValue)) {
                isCheck = true;
            } else if (checkValue == Constants.ICON_SIZE_128 && Constants.ICON_SIZE_STR_HUGE.Equals(targetValue)) {
                isCheck = true;
            } else if (checkValue == Constants.ICON_SIZE_256 && Constants.ICON_SIZE_STR_JUMBO.Equals(targetValue)) {
                isCheck = true;
            }
            return isCheck;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || parameter == null) {
                return 0;
            }
            if ((bool)value) {
                string targetValue = (string)parameter;
                if (Constants.ICON_SIZE_STR_SMALL.Equals(targetValue)) {
                    return Constants.ICON_SIZE_32;
                }else if (Constants.ICON_SIZE_STR_MID.Equals(targetValue)) {
                    return Constants.ICON_SIZE_48;
                } else if (Constants.ICON_SIZE_STR_LARGE.Equals(targetValue)) {
                    return Constants.ICON_SIZE_72;
                } else if (Constants.ICON_SIZE_STR_HUGE.Equals(targetValue)) {
                    return Constants.ICON_SIZE_128;
                } else if (Constants.ICON_SIZE_STR_JUMBO.Equals(targetValue)) {
                    return Constants.ICON_SIZE_256;
                }

            }
            return 0;
        }
    }
}
