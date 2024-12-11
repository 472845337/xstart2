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
            // 定义主窗口对象（构造器中就加载配置和数据库）
            MainWindow mainWindow = new MainWindow();
            // 项目自启动类型
            string autoRunType = Configs.runDirectly ? Const.Constants.RUN_TYPE_AUTO : Const.Constants.RUN_TYPE_MANUAL;
            if (!string.IsNullOrEmpty(Configs.admin.Password)) {
                // 配置了管理员口令，则校验口令
                CheckSecurityWindow checkSecurityWindow = new CheckSecurityWindow("请输入管理员口令", Configs.admin.Password, "关闭该窗口将退出程序！", true, Configs.runDirectly, mainWindow.AutoRunProjectList.Count > 0) { Topmost = true };
                if (true == checkSecurityWindow.ShowDialog()) {
                    if (Configs.runDirectly) {
                        autoRunType = checkSecurityWindow.AutoRunType;
                    }
                } else {
                    // 直接退出
                    Shutdown();
                    return;
                }
            }
            if (mainWindow.AutoRunProjectList.Count > 0) {
                // 有自启项目
                if (Const.Constants.RUN_TYPE_AUTO.Equals(autoRunType)) {
                    // 自启动
                    mainWindow.RunProjects(mainWindow.AutoRunProjectList);
                } else if (Const.Constants.RUN_TYPE_MANUAL.Equals(autoRunType)) {
                    // 手工启动类型，则弹出启动窗口
                    if (!mainWindow.AutoRunProjectWindow(mainWindow.AutoRunProjectList, null, true)) {
                        // 直接退出
                        Shutdown();
                        return;
                    }
                }
            }
            // 显示主窗口
            mainWindow.Show();
        }
    }
}
