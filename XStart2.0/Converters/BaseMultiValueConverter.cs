using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace XStart2._0.Converters {
    /// <summary>
    /// 基础转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter
        where T:class, new (){

        private static T mConverter = null;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return mConverter ??= new T();
        }
        public abstract object Convert(object[] value, Type targetType, object parameter, CultureInfo culture);
        public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}
