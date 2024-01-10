﻿using System;
using System.Windows.Data;
using XStart2._0.Const;

namespace XStart2._0.Converters {
    public class ProjectWidthConverter : IValueConverter {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            int width = -1;
            if(value is Bean.Project project) {
                if (Constants.ORIENTATION_VERTICAL.Equals(project.Orientation)) {
                    // 竖排的时候才会需要
                    width = project.IconSize + 20;
                }
            }
            return width;
        }


        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
        #endregion
    }
}