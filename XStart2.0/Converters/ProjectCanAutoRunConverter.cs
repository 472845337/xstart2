using System;

namespace XStart2._0.Converters {
    public class ProjectCanAutoRunConverter : BaseValueConverter<ProjectCanAutoRunConverter> {
        #region IValueConverter Members
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Bean.Project project = (Bean.Project)value;
            return null == project ? false : (object)project.CanAutoRun;
        }


        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
        #endregion
    }
}
