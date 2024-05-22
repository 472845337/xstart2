using System;

namespace XStart2._0.Converters {
    public class Boolean2MainHeadShowConverter : BaseValueConverter<Boolean2MainHeadShowConverter> {
        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool boolValue = (bool)value;
            return boolValue ? 68 : 0;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
