using System.IO;
using System.Windows.Media.Imaging;

namespace XStart2._0.Utils {
    public class ImageUtils {
        /// <summary>
        /// 将拿到的图标Bitmap转换成可以绑定的BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap) {
            if (null == bitmap) {
                return null;
            }
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream()) {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            bitmap.Dispose();
            return bitmapImage;
        }

        public static BitmapImage File2BitmapImage(string filename) {
            if (File.Exists(filename)) {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new System.Uri(@filename);
                image.EndInit();
                image.Freeze();
                return image;
            } else {
                return null;
            }

        }
    }
}
