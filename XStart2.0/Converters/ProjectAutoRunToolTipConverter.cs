using System;
using System.Windows.Data;

namespace XStart2._0.Converters {
    public class ProjectAutoRunToolTipConverter : IValueConverter {
        #region IValueConverter Members

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
        //    XStart.Bean.Project project = (XStart.Bean.Project)value;
        //    if (null == project || project.CanAutoRun) {
        //        return "切换自启动";
        //    } else {
        //        return "系统项目不可自启动";
        //    }
        //}
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value || (bool)value) {
                return "切换自启动";
            } else {
                return "系统项目不可自启动";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }
}
