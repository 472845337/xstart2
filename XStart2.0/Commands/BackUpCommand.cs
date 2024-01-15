using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Windows;

namespace XStart2._0.Commands {
    public class BackUpCommand {

        public static void ShowBackUpWindow() {
            BackUpWindow backUpWindow = new BackUpWindow();
            backUpWindow.ShowDialog();
            backUpWindow.Close();
        }
    }
}
