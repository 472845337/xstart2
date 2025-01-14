using System;
using System.Windows;
using System.Windows.Media;
using Utils;

namespace XStart2._0.Utils {
    public class XStartWinUtils : WinUtils {

        private const int DESKTOPVERTRES = 117;
        private const int DESKTOPHORZRES = 118;
        // 获取真实屏幕宽高
        public static System.Drawing.Size GetScreenByDevice() {
            IntPtr hDc = DllUtils.GetDC(IntPtr.Zero);
            System.Drawing.Size size = new System.Drawing.Size() {
                Width = DllUtils.GetDeviceCaps(hDc, DESKTOPHORZRES),
                Height = DllUtils.GetDeviceCaps(hDc, DESKTOPVERTRES)
            };
            DllUtils.ReleaseDC(IntPtr.Zero, hDc);
            return size;
        }

        /// <summary>
        /// 获取窗口缩放比例
        /// </summary>
        /// <returns></returns>
        public static Tuple<double, double> GetScale(Window window) {
            PresentationSource presentationSource = PresentationSource.FromVisual(window);
            if (presentationSource != null) {
                Matrix m = presentationSource.CompositionTarget.TransformToDevice;
                double scaleX = m.M11;
                double scaleY = m.M22;
                return new Tuple<double, double>(scaleX, scaleY);
            } else {
                return new Tuple<double, double>(1D, 1D);
            }

        }
    }
}
