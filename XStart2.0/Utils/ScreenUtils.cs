using System.Windows;
using System.Windows.Forms;

namespace XStart2._0.Utils {
    internal class ScreenUtils {
        // 获取窗口所在的屏幕
        public static Screen GetMainWindowScreen(Window mainWindow) {
            // 将WPF坐标转换为屏幕坐标
            var windowCenter = new System.Drawing.Point(
                (int)(mainWindow.Left + mainWindow.Width / 2),
                (int)(mainWindow.Top + mainWindow.Height / 2)
            );

            return Screen.FromPoint(windowCenter);
        }

        // 设置窗口到指定屏幕
        public static void SetWindowToScreen(Window window, Screen screen) {
            var workingArea = screen.WorkingArea;

            window.WindowStartupLocation = WindowStartupLocation.Manual;

            // 在屏幕中央显示
            window.Left = workingArea.Left + (workingArea.Width - window.Width) / 2;
            window.Top = workingArea.Top + (workingArea.Height - window.Height) / 2;
        }
    }
}
