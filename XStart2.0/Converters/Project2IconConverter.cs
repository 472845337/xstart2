using System;
using System.Windows.Data;
using XStart.Services;

namespace XStart2._0.Converters {
    public class Project2IconConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            XStart.Bean.Project project = (XStart.Bean.Project)value;
            return XStartService.BitmapToBitmapImage(XStartService.GetIconImage(project));

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
