using System;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class IconSizeConverter : BaseValueConverter<IconSizeConverter> {
        // 图标尺寸转换 32-Small 48-Mid, 72-Large, 256-Huge
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool isCheck = false;
            string targetValue = (string)parameter;
            if (null == value) {
                if (Constants.STYLE_DEFAULT.Equals(targetValue)) {
                    isCheck = true;
                }
            } else {
                int checkValue = (int)value;
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
            }

            return isCheck;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null || parameter == null) {
                return 0;
            }
            if ((bool)value) {
                string targetValue = (string)parameter;
                if (Constants.STYLE_DEFAULT.Equals(targetValue)) {
                    return null;
                } else if (Constants.ICON_SIZE_STR_SMALL.Equals(targetValue)) {
                    return Constants.ICON_SIZE_32;
                } else if (Constants.ICON_SIZE_STR_MID.Equals(targetValue)) {
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
