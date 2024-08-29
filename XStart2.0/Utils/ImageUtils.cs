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
                image.CacheOption = BitmapCacheOption.OnLoad;
                using (Stream ms = new MemoryStream(File.ReadAllBytes(filename))) {
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze();
                };
                return image;
            } else {
                return null;
            }

        }

        /// <summary>
        /// 主要是提供给WpfGifAnimated插件用的
        /// 获取文件的图片，支持gif,gif中的流不能关闭，否则会报无法读取已关闭的流
        /// 所以gif中使用的图不能像方法File2BitmapImage那样关闭流
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static BitmapImage GetImageOutMs(string filename) {
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
