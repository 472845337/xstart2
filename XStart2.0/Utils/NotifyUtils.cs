using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using XStart2._0.ViewModels;
using XStart2._0.Windows;

namespace XStart2._0.Utils {
    public class NotifyUtils {
        public static List<NotificationWindow> _dialogs = new List<NotificationWindow>();
        public static void ShowNotification(string content, string title = "通知", int saveTime = 5) {
            ShowNotification(content, Colors.LightBlue, title, saveTime);
        }
        public static void ShowNotification(string content, Color background, string title = "通知", int saveTime = 5) {
            if (null == background) {
                background = Colors.LightBlue;
            }
            NotificationWindow notify = new NotificationWindow { SaveTime = saveTime };
            NotifyData notifyData = new NotifyData {
                Title = title,
                Background = background.ToString(),
                Content = content
            };
            notify.DataContext = notifyData;
            notify.Closed += (sender, e) => {
                NotificationWindow closedDialog = sender as NotificationWindow;
                _dialogs.Remove(closedDialog);
            };
            notify.TopFrom = GetTopFrom();
            _dialogs.Add(notify);
            notify.Show();
        }

        static double GetTopFrom() {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);

            while (isContinueFind) {
                topFrom -= 100;//此处是NotifyWindow的高
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }
            if (topFrom <= 0) {
                topFrom = SystemParameters.WorkArea.Bottom - 10;
            }
            return topFrom;
        }
    }
}
