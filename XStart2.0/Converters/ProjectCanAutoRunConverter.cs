using System;
using System.Windows.Data;

namespace XStart2._0.Converters {
    public class ProjectCanAutoRunConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Bean.Project project = (Bean.Project)value;
            return null == project ? false : (object)project.CanAutoRun;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }
}
