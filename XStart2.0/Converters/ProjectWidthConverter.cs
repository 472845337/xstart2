using System;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class ProjectWidthConverter : BaseValueConverter<ProjectWidthConverter> {
        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            int width = -1;
            if (value is Bean.Project project) {
                if (Constants.ORIENTATION_VERTICAL.Equals(project.Orientation)) {
                    // 竖排的时候才会需要
                    width = project.IconSize + 20;
                }
            }
            return width;
        }


        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
}
