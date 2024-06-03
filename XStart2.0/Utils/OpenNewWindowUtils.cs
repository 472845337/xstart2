using System.Windows;
using XStart2._0.ViewModel;

namespace XStart2._0.Utils {
    class OpenNewWindowUtils {

        public static void SetTopmost<T>(T t) where T : Window {
            if (t.Topmost) {
                t.Topmost = false;
            }
        }

        public static void RecoverTopmost<T, M>(T t, M m) where T : Window where M : BaseViewModel {
            if (m.TopMost) {
                t.Topmost = true;
            }
        }
    }
}
