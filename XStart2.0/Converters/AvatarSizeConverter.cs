using System;
using System.Globalization;

namespace XStart2._0.Converters {
    internal class AvatarSizeConverter : BaseValueConverter<AvatarSizeConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int avatarSize = Const.Constants.AVATAR_SIZE;
            if (null != value) {
                avatarSize = (int)value;
            }
            return new System.Windows.Rect(0, 0, avatarSize - 2, avatarSize - 2);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
