using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using Utils;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    //public partial class App : Application {
    //    private static Mutex _mutex = null;
    //    bool createdNew;
    //    protected override void OnStartup(StartupEventArgs e) {
    //        _mutex = new Mutex(true, Constants.APP_NAME, out createdNew);
    //        if (!createdNew) {
    //            SendShow();
    //            //应用程序已经在运行！当前的执行退出。
    //            Current.Shutdown();
    //        }
    //        FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
    //        base.OnStartup(e);
    //        // 判断dpi change,dpi change不启动口令和自启动窗口
    //        var iniData = IniParserUtils.GetIniData(Configs.AppStartPath + Constants.SET_FILE);
    //        string dpiChangeStr = iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_DPI_CHANGE];
    //        bool dpiChange = !string.IsNullOrEmpty(dpiChangeStr) && Convert.ToBoolean(dpiChangeStr);
    //        // 定义主窗口对象（构造器中就加载配置和数据库）
    //        MainWindow mainWindow = new MainWindow();
    //        if (!dpiChange) {
    //            // 项目自启动类型
    //            string autoRunType = Configs.runDirectly ? Constants.RUN_TYPE_AUTO : Constants.RUN_TYPE_MANUAL;
    //            if (!string.IsNullOrEmpty(Configs.admin.Password)) {
    //                // 配置了管理员口令，则校验口令
    //                CheckSecurityWindow checkSecurityWindow = new CheckSecurityWindow("请输入管理员口令", Configs.admin.Password, "关闭该窗口将退出程序！", true, Configs.runDirectly, mainWindow.AutoRunProjectList.Count > 0) { Topmost = true };
    //                if (true == checkSecurityWindow.ShowDialog()) {
    //                    if (Configs.runDirectly) {
    //                        autoRunType = checkSecurityWindow.AutoRunType;
    //                    }
    //                } else {
    //                    // 直接退出
    //                    Environment.Exit(0);
    //                }
    //            }
    //            if (mainWindow.AutoRunProjectList.Count > 0) {
    //                // 有自启项目
    //                if (Constants.RUN_TYPE_AUTO.Equals(autoRunType)) {
    //                    // 自启动
    //                    mainWindow.RunProjects(mainWindow.AutoRunProjectList);
    //                } else if (Constants.RUN_TYPE_MANUAL.Equals(autoRunType)) {
    //                    // 手工启动类型，则弹出启动窗口
    //                    if (!mainWindow.AutoRunProjectWindow(mainWindow.AutoRunProjectList, null, true)) {
    //                        // 直接退出
    //                        Environment.Exit(0);
    //                    }
    //                }
    //            }
    //        } else {
    //            iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_DPI_CHANGE] = Convert.ToString(false);
    //            IniParserUtils.SaveIniData(Configs.AppStartPath + Constants.SET_FILE, iniData);
    //        }
    //        // 显示主窗口
    //        mainWindow.Show();
    //    }

    //    public static void Restart() {
    //        // 释放互斥锁
    //        _mutex.ReleaseMutex();
    //        _mutex.Dispose();
    //        // 重启应用程序
    //        System.Windows.Forms.Application.Restart();
    //        Current.Shutdown();
    //    }


    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        bool createdNew;
        private static Mutex _mutex = null;
        protected override void OnStartup(StartupEventArgs e) {
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            _mutex = new Mutex(true, Constants.APP_NAME + currentIdentity.User, out createdNew);
            if (!createdNew) {
                SendShow();
                //应用程序已经在运行！当前的执行退出。
                Current.Shutdown();
            }
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
            base.OnStartup(e);
            MainWindow mainWindow = new MainWindow();
            string autoRunType = Configs.runDirectly ? Constants.RUN_TYPE_AUTO : Constants.RUN_TYPE_MANUAL;
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
                if (Constants.RUN_TYPE_AUTO.Equals(autoRunType)) {
                    mainWindow.RunProjects(mainWindow.AutoRunProjectList);
                } else if (Constants.RUN_TYPE_MANUAL.Equals(autoRunType)) {
                    if (!mainWindow.AutoRunProjectWindow(mainWindow.AutoRunProjectList, null, true)) {
                        Shutdown();
                        return;
                    }
                }
            }
            mainWindow.Show();
        }

        private void SendShow() {
            IntPtr hWnd = DllUtils.FindWindow(null, Constants.APP_TITLE);
            // 发送显示消息
            string msg = Constants.APP_SHOW;
            DllUtils.COPYDATA_STRUCT data = new DllUtils.COPYDATA_STRUCT { dwData = IntPtr.Zero, cbData = Encoding.Default.GetBytes(msg).Length + 1, lpData = msg };
            int sizeOfData = Marshal.SizeOf(data);
            IntPtr lParam = Marshal.AllocHGlobal(sizeOfData);
            Marshal.StructureToPtr(data, lParam, false);
            try {
                // 发送显示消息
                DllUtils.SendMessage(hWnd, WinApi.WM_COPYDATA, IntPtr.Zero, lParam);
                // 该次启动程序直接退出
                Environment.Exit(0);
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            } finally {
                Marshal.FreeHGlobal(lParam);
            }
        }
    }
}
