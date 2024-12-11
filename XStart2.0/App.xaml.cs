using System.Windows;
using XStart2._0.Config;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            string autoRunType = Configs.runDirectly ? Const.Constants.RUN_TYPE_AUTO : Const.Constants.RUN_TYPE_MANUAL;
            if (!string.IsNullOrEmpty(Configs.admin.Password)) {
                CheckSecurityWindow checkSecurityWindow = new CheckSecurityWindow("请输入管理员口令", Configs.admin.Password, "关闭该窗口将退出程序！", true, Configs.runDirectly, mainWindow.AutoRunProjectList.Count > 0) { Topmost = true };
                if (true == checkSecurityWindow.ShowDialog()) {
                    if (Configs.runDirectly) {
                        autoRunType = checkSecurityWindow.AutoRunType;
                    }
                } else {
                    Shutdown();
                    return;
                }
            }
            if (mainWindow.AutoRunProjectList.Count > 0) {
                if (Const.Constants.RUN_TYPE_AUTO.Equals(autoRunType)) {
                    mainWindow.RunProjects(mainWindow.AutoRunProjectList);
                } else if (Const.Constants.RUN_TYPE_MANUAL.Equals(autoRunType)) {
                    if (!mainWindow.AutoRunProjectWindow(mainWindow.AutoRunProjectList, null, true)) {
                        Shutdown();
                        return;
                    }
                }
            }
            mainWindow.Show();
        }
    }
}
