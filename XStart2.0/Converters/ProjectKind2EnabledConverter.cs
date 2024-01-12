using System;
using System.Windows.Data;

namespace XStart2._0.Converters {
    public class ProjectKind2EnabledConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool enabled = false;
            if (Bean.Project.KIND_FILE.Equals(value) || Bean.Project.KIND_DIRECTORY.Equals(value)) {
                enabled = true;
            }
            return enabled;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

        #endregion
    }
}
