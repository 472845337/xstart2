using System;
using System.Windows.Data;

namespace XStart2._0.Converters {
    public class ProjectKind2EnabledConverter : BaseValueConverter<ProjectKind2EnabledConverter> {
        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            bool enabled = false;
            if (Bean.Project.KIND_FILE.Equals(value) || Bean.Project.KIND_DIRECTORY.Equals(value)) {
                enabled = true;
            }
            return enabled;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }

        #endregion
    }
}
