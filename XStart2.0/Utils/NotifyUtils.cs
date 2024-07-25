using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using XStart2._0.Windows;

namespace XStart2._0.Utils {
    public class NotifyUtils {
        private static readonly List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        public static void ShowNotification(string content, int height = 100, string title = "通知", int saveTime = 5) {
            ShowNotification(content, Colors.LightBlue, height, title, saveTime);
        }
        public static void ShowNotification(string content, Color background, int height = 100, string title = "通知", int saveTime = 5) {
            if (null == background) {
                background = Colors.LightBlue;
            }

            NotificationWindow notify = new NotificationWindow(title, content, background, height, saveTime);

            notify.Closed += (sender, e) => {
                NotificationWindow closedDialog = sender as NotificationWindow;
                _dialogs.Remove(closedDialog);
            };
            notify.TopFrom = GetTopFrom(height);
            _dialogs.Add(notify);
            notify.Show();
        }

        static double GetTopFrom(int curHeight) {
            //屏幕的高度-底部TaskBar的高度。
            var lastNotify = _dialogs.Count > 0 ? _dialogs[_dialogs.Count - 1] : null;
            double topFrom = null != lastNotify ? (lastNotify.TopFrom - lastNotify.ActualHeight) : (SystemParameters.WorkArea.Bottom - 10);
            if (topFrom <= 0 || topFrom < curHeight) {
                topFrom = SystemParameters.WorkArea.Bottom - 10;
            }
            return topFrom;
        }
    }
}
