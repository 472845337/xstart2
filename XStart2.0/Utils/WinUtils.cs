using System;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace XStart2._0.Utils {
    public class WinUtils {
        public static bool ShowFileProperties(string Filename) {
            DllUtils.SHELLEXECUTEINFO info = new DllUtils.SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = WinApi.SW_SHOW;
            info.fMask = WinApi.SEE_MASK_INVOKEIDLIST;
            return DllUtils.ShellExecuteEx(ref info);
        }

        public static DriveInfo GetDriveInfoByName(string driveName) {
            DriveInfo[] driveInfoArray = DriveInfo.GetDrives();
            foreach (DriveInfo single in driveInfoArray) {
                if (single.Name.Equals(driveName)) {
                    return single;
                }
            }
            return null;
        }

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
