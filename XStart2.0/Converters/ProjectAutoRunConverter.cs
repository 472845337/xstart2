using System;

namespace XStart2._0.Converters {
    public class ProjectAutoRunConverter : BaseValueConverter<ProjectAutoRunConverter> {
        #region IValueConverter Members

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
        //    XStart.Bean.Project project = (XStart.Bean.Project)value;
        //    if (null == project || null == project.AutoRun) {
        //        return false;
        //    } else {
        //        return (bool)project.AutoRun;
        //    }
        //}

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null == value) {
                return false;
            } else {
                return (bool)value;
            }
        }


        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return (bool)value;
        }
        #endregion
    }
}
