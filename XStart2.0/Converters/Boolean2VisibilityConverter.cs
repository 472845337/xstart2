using System;
using System.Windows.Data;

namespace XStart2._0.Converters {
    public class Boolean2VisibilityConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (targetType != typeof(System.Windows.Visibility)) {
                throw new InvalidOperationException("The target must be a System.Windows.Visibility");
            }
            bool boolValue = (bool)value;

            return boolValue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
