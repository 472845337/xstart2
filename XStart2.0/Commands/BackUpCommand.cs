using System.Windows;
using XStart2._0.Windows;

namespace XStart2._0.Commands {
    public class BackUpCommand {

        public static void ShowBackUpWindow(Window parentWindow) {
            BackUpWindow backUpWindow = new BackUpWindow() { Owner = parentWindow};
            backUpWindow.ShowDialog();
            backUpWindow.Close();
        }
    }
}
