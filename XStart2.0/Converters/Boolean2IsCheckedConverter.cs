using System;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class Boolean2IsCheckedConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool isCheck = false;
            if (Constants.STYLE_DEFAULT.Equals(parameter)) { 
                if(null == value) {
                    isCheck = true;
                }
            } else {
                if (null != value && (bool)value == System.Convert.ToBoolean(parameter)) {
                    isCheck = true;
                }
            }
            return isCheck;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (Constants.STYLE_DEFAULT.Equals(parameter)) {
                return null;
            } else {
                return parameter;
            }
        }

        #endregion
    }
}
