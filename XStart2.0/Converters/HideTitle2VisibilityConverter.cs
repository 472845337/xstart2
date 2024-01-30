using System;

namespace XStart2._0.Converters {
    public class HideTitle2VisibilityConverter : BaseValueConverter<HideTitle2VisibilityConverter> {
        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (targetType != typeof(System.Windows.Visibility)) {
                throw new InvalidOperationException("The target must be a System.Windows.Visibility");
            }
            bool boolValue = (bool)value;
            // 隐藏标题为true则收缩，不隐藏则展示
            return boolValue ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
