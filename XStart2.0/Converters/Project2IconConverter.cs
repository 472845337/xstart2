using System;
using System.Windows.Data;
using XStart2._0.Services;
using XStart2._0.Utils;

namespace XStart2._0.Converters {
    public class Project2IconConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(null != value) {
                if (value is Bean.BackData.BackProject backProject) {
                    return XStartService.GetIconImage(backProject.Kind, backProject.Path, backProject.IconPath);
                } else if (value is Bean.Project project) {
                    return XStartService.GetIconImage(project);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
