using System;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class RdpModelConverter : BaseValueConverter<RdpModelConverter> {
        // 单击为true,双击为false
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            string openType = (string)value;
            if (Constants.RDP_MODEL_CUSTOM.Equals(openType)) {
                return true;
            } else {
                return false;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value || !(value is bool boolean)) {
                return Constants.RDP_MODEL_SYSTEM;
            } else {
                return boolean ? Constants.RDP_MODEL_CUSTOM : Constants.RDP_MODEL_SYSTEM;
            }
        }
    }
}
