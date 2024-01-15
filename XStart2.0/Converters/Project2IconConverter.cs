using System;
using System.Windows.Data;
using XStart2._0.Const;
using XStart2._0.Services;

namespace XStart2._0.Converters {
    public class Project2IconConverter : BaseValueConverter<Project2IconConverter> {
        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (null != value) {
                if (value is Bean.BackData.BackProject backProject) {
                    return XStartService.GetIconImage(backProject.Kind, backProject.Path, backProject.IconPath, Constants.ICON_SIZE_32);
                } else if (value is Bean.Project project) {
                    // 该块目前无用的，Project的图标直接读取Project中的Icon属性
                    return XStartService.GetIconImage(project);
                }
            }
            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
