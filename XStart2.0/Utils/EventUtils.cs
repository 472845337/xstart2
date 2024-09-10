using System.Windows;
using System.Windows.Input;

namespace XStart2._0.Utils {
    internal class EventUtils {
        /// <summary>
        /// 判断是否按下某按键，为true时，焦点会置到下一个控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool InputKey(object sender, KeyEventArgs e, Key key) {
            bool result = false;
            if (key == e.Key) {
                // 文本框需要先失去焦点，才可以获取到文本内容
                FocusNext(sender);
                result = true;
            }
            return result;
        }

        public static void FocusNext(object sender) {
            FrameworkElement fe = sender as FrameworkElement;
            fe.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        public static void FocusPre(object sender) {
            FrameworkElement fe = sender as FrameworkElement;
            fe.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
        }
    }
}
