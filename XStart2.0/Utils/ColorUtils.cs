using System.Windows.Media;

namespace XStart2._0.Utils {
    public class ColorUtils {
        public static Color GetColor(string htmlColor) {
            return (Color)ColorConverter.ConvertFromString(htmlColor);
        }

        public static string GetHtml(System.Drawing.Color color) {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        private static Color GetColor(System.Drawing.Color color) {
            return new Color() { R = color.R, G = color.G, B = color.B, A = color.A };
        }

        public static SolidColorBrush GetBrush(string htmlColor) {
            return GetBrush(GetColor(htmlColor));
        }

        public static SolidColorBrush GetBrush(System.Drawing.Color color) {
            return GetBrush(GetColor(color));
        }
        public static SolidColorBrush GetBrush(Color color) {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }

        public static SolidColorBrush GetBrush(int r, int g, int b) {
            return GetBrush(255, r, g, b);
        }

        public static SolidColorBrush GetBrush(int a, int r, int g, int b) {
            return GetBrush(Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b));
        }
    }
}
