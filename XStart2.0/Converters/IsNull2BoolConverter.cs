using System;
using System.Globalization;

namespace XStart2._0.Converters {
    internal class IsNull2BoolConverter : BaseValueConverter<IsNull2BoolConverter> {
        /// <summary>
        /// 判断value是否为空，字符串会有IsNullOrEmpty判断
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter">如果参数为negative，则是取反</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            bool isNull = false; ;
            if (null != value) {
                if (value is string) {
                    string str = value as string;
                    if (string.IsNullOrEmpty(str)) {
                        isNull = true;
                    }
                }
            } else {
                isNull = true;
            }
            if (string.Equals("negative", parameter as string, StringComparison.OrdinalIgnoreCase)) {
                isNull = !isNull;
            }
            return isNull;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
