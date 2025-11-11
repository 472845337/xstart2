using System.Windows.Controls;

namespace XStart2._0.Utils {
    internal class TextBoxUtils {

        public static void Move2Next(Control currentControl, Control[] controls) {
            for (int i = 0; i < controls.Length - 1; i++) {
                if (controls[i] == currentControl) {
                    // 找到下一个可见且可用的控件
                    for (int j = i + 1; j < controls.Length; j++) {
                        if (controls[j].IsVisible && controls[j].IsEnabled) {
                            // 密码框有两个，所以需要判断显示
                            controls[j].Focus();
                            if (controls[j] is TextBox textBox) {
                                textBox.SelectAll();
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
