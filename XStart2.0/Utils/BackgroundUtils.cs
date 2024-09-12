using System.Windows.Media;

namespace XStart2._0.Utils {
    internal class BackgroundUtils {
        public static Brush GetBrush(string background, double opacity = 1D) {
            Brush backgrounBrush;
            if (string.IsNullOrEmpty(background)) {
                // 为空表示为透明
                backgrounBrush = ColorUtils.GetBrush(Colors.Transparent);
            } else if (background.StartsWith("#")) {
                string[] colorArray = background.Split(';');
                if (colorArray.Length > 1) {
                    // 渐变色 color1:point1;colo2:point2;angle，如果配置的渐变有问题，则对gradients里的颜色数进行判断
                    backgrounBrush = GradientColorUtils.GetBrush(background);
                } else {
                    // 单色
                    backgrounBrush = ColorUtils.GetBrush(background);
                }
            } else {
                // 图片背景
                backgrounBrush = new ImageBrush {
                    ImageSource = ImageUtils.File2BitmapImage(background)
                };
            }
            backgrounBrush.Opacity = opacity;
            backgrounBrush.Freeze();
            return backgrounBrush;
        }
    }
}
