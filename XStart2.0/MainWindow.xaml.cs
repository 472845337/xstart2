using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Helper;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        // 时钟定时器
        private readonly System.Windows.Threading.DispatcherTimer AutoHideTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        private readonly System.Windows.Threading.DispatcherTimer currentTimer = new System.Windows.Threading.DispatcherTimer();
        private readonly System.Windows.Threading.DispatcherTimer currentDateTimer = new System.Windows.Threading.DispatcherTimer();
        // 数据服务
        public TypeService typeService = ServiceFactory.GetTypeService();
        public ColumnService columnService = ServiceFactory.GetColumnService();
        public ProjectService projectService = ServiceFactory.GetProjectService();
        // 模型
        readonly MainViewModel mainViewModel = new MainViewModel();
        private static bool IsAllShow = true;
        System.Windows.Forms.NotifyIcon notifyIcon = null;
        public MainWindow() {
            InitializeComponent();
            Configs.Handler = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            // Tick 超过计时器间隔时发生。
            currentTimer.Tick += new EventHandler(CurrentTimer_Tick);
            // Interval 获取或设置计时器刻度之间的时间段
            currentTimer.Interval = new TimeSpan(0, 0, 1);
            currentTimer.Start();
            // 日期定时任务
            currentDateTimer.Interval = new TimeSpan(0, 0, 1);
            currentDateTimer.Tick += new EventHandler(CurrentDate_Tick);
            currentDateTimer.Start();

            AutoHideTimer.Tick += new EventHandler(AutoHideTimer_Tick);
            AutoHideTimer.Start();
            // 将模型赋值上下文
            DataContext = mainViewModel;
        }

        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="param"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            #region 用户头像和昵称
            string avatarPath = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_USER, Constants.KEY_USER_AVATAR);
            string nickName = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_USER, Constants.KEY_USER_NICKNAME);
            if (!string.IsNullOrEmpty(avatarPath)) {
                if (!File.Exists(avatarPath)) {
                    avatarPath = Configs.AppStartPath + Constants.AVATAR_PATH_NOTEXIST;
                }
            } else {
                avatarPath = Configs.AppStartPath + Constants.AVATAR_PATH_DEFAULT;
            }
            mainViewModel.AvatarPath = avatarPath;
            mainViewModel.NickName = string.IsNullOrEmpty(nickName) ? Constants.NICKNAME_DEFAULT : nickName;
            #endregion

            #region 窗口相关加载，尺寸，位置，置顶
            string leftStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_LEFT);
            string topStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_TOP);
            string heightStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_HEIGHT);
            string widthStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_WIDTH);

            // 尺寸
            mainViewModel.MainHeight = string.IsNullOrEmpty(heightStr) ? Constants.MAIN_HEIGHT : Convert.ToDouble(heightStr);
            mainViewModel.MainWidth = string.IsNullOrEmpty(widthStr) ? Constants.MAIN_WIDTH : Convert.ToDouble(widthStr);
            // 位置
            mainViewModel.MainLeft = string.IsNullOrEmpty(leftStr) ? Constants.MAIN_LEFT : Convert.ToDouble(leftStr);
            mainViewModel.MainTop = string.IsNullOrEmpty(topStr) ? Constants.MAIN_TOP : Convert.ToDouble(topStr);

            Configs.mainHeight = mainViewModel.MainHeight;
            Configs.mainWidth = mainViewModel.MainWidth;
            Configs.mainLeft = mainViewModel.MainLeft;
            Configs.mainTop = mainViewModel.MainTop;
            #endregion
            #region 加载设置项
            string typeTabExpandStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_TYPE_TAB_EXPAND);
            string topMostStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_TOP_MOST);
            string openType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE);// 上次最后打开的类别
            string clickType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_CLICK_TYPE);// 点击方式，单击、双击
            string audio = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_AUDIO);
            string autoRun = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_AUTO_RUN);
            string exitWarn = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_EXIT_WARN);
            string closeBorderHide = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE);
            string urlOpen = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN);
            string urlOpenCustomBrowser = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN_CUSTOM_BROWSER);

            Configs.typeTabExpand = string.IsNullOrEmpty(typeTabExpandStr) || Convert.ToBoolean(typeTabExpandStr);
            Configs.topMost = !string.IsNullOrEmpty(topMostStr) && Convert.ToBoolean(topMostStr);
            Configs.openType = openType;
            Configs.clickType = string.IsNullOrEmpty(clickType) ? Constants.CLICK_TYPE_SINGLE : clickType;
            Configs.audio = !string.IsNullOrEmpty(audio) && Convert.ToBoolean(audio);
            Configs.autoRun = !string.IsNullOrEmpty(autoRun) && Convert.ToBoolean(autoRun);
            Configs.exitWarn = string.IsNullOrEmpty(exitWarn) || Convert.ToBoolean(exitWarn);
            Configs.closeBorderHide = string.IsNullOrEmpty(closeBorderHide) || Convert.ToBoolean(closeBorderHide);
            if (!string.IsNullOrEmpty(urlOpen)) { Configs.urlOpen = urlOpen; }
            Configs.urlOpenCustomBrowser = urlOpenCustomBrowser;

            mainViewModel.TypeTabExpanded = Configs.typeTabExpand;
            mainViewModel.ChangeTypeTab();// 初始化时要先触发一次
            mainViewModel.TopMost = Configs.topMost;
            mainViewModel.ClickType = Configs.clickType;
            mainViewModel.Audio = Configs.audio;
            mainViewModel.AutoRun = Configs.autoRun;
            mainViewModel.ExitWarn = Configs.exitWarn;
            mainViewModel.CloseBorderHide = Configs.closeBorderHide;
            mainViewModel.UrlOpen = Configs.urlOpen;
            mainViewModel.UrlOpenCustomBrowser = Configs.urlOpenCustomBrowser;
            // 系统其他配置
            string delCount = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_DEL_COUNT);// 数据库执行多少次删除后执行VACUUM
            Configs.delCount = string.IsNullOrEmpty(delCount) ? 0 : Convert.ToInt32(delCount);
            #endregion

            #region 系统功能图标和操作
            string systemProjectOpenPage = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_SYSTEM_PROJECT_OPEN_PAGE);
            Configs.systemAppOpenPage = string.IsNullOrEmpty(systemProjectOpenPage) ? 0 : Convert.ToInt32(systemProjectOpenPage);
            string addMulti = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_ADD_MULTI);
            Configs.systemAppAddMulti = !string.IsNullOrEmpty(addMulti) && Convert.ToBoolean(addMulti);
            Configs.InitIconDic();
            SystemProjectParam.InitOperate();
            Configs.taskbarHandler = DllUtils.FindWindow("Shell_TrayWnd", null);
            Configs.taskbarIsShow = (DllUtils.GetWindowLong(Configs.taskbarHandler, WinApi.GWL_STYLE) & WinApi.WS_VISIBLE) == WinApi.WS_VISIBLE;
            DllUtils.waveOutGetVolume(0, out uint volume);
            uint leftVolume = volume & 0x0000FFFF, rightVolume = volume >> 16;
            Configs.volume = (leftVolume + rightVolume) / 2;
            Configs.waveMuted = Configs.volume == 0x0000;
            Configs.micVolume = AudioUtils.GetDeviceVolume(Constants.DEVICE_NAME_MIC);// 音量
            Configs.lineInVolume = AudioUtils.GetDeviceVolume(Constants.DEVICE_NAME_LINE_IN);
            Configs.cdPlayerVolume = AudioUtils.GetDeviceVolume(Constants.DEVICE_NAME_CD_PLAYER, NAudio.CoreAudioApi.DataFlow.Render);
            Configs.micMuted = AudioUtils.GetDeviceMute(Constants.DEVICE_NAME_MIC);// 是否静音
            Configs.lineInMuted = AudioUtils.GetDeviceMute(Constants.DEVICE_NAME_LINE_IN);
            Configs.cdPlayerMuted = AudioUtils.GetDeviceMute(Constants.DEVICE_NAME_CD_PLAYER, NAudio.CoreAudioApi.DataFlow.Render);
            #endregion

            #region 加载数据
            // 加载类别配置
            List<Bean.Type> types = typeService.SelectList(new Bean.Type { OrderBy = "sort" });
            foreach (Bean.Type type in types) {
                if (string.IsNullOrEmpty(type.Name)) {
                    typeService.Delete(type.Section);
                    continue;
                }
                type.Locked = !string.IsNullOrEmpty(type.Password);
                type.HasPassword = !string.IsNullOrEmpty(type.Password);
                if (string.IsNullOrEmpty(type.FaIconFontFamily)) {
                    type.FaIconFontFamily = Constants.FONT_FAMILY_FA_SOLID;
                }
                XStartService.TypeDic.Add(type.Section, type);
            }
            // 加载栏目配置
            List<Column> columns = columnService.SelectList(new Column { OrderBy = "sort" });
            foreach (Column column in columns) {
                if (string.IsNullOrEmpty(column.Name) || string.IsNullOrEmpty(column.TypeSection) || !XStartService.TypeDic.TryGetValue(column.TypeSection, out _)) {
                    // 如果栏目不合规范，删除该栏目
                    columnService.Delete(column.Section);
                    continue;
                }
                column.Locked = !string.IsNullOrEmpty(column.Password);
                column.HasPassword = !string.IsNullOrEmpty(column.Password);
                column.IsExpanded = false;
                XStartService.TypeDic[column.TypeSection].ColumnDic.Add(column.Section, column);
            }
            // 加载项目配置
            List<Project> projects = projectService.SelectList(new Project { OrderBy = "sort" });

            foreach (Project project in projects) {
                // 初始化项目的图示
                project.InitIcon();
                if (string.IsNullOrEmpty(project.Name) || string.IsNullOrEmpty(project.TypeSection)
                    || string.IsNullOrEmpty(project.ColumnSection) || !XStartService.TypeDic.TryGetValue(project.TypeSection, out _)
                    || !XStartService.TypeDic[project.TypeSection].ColumnDic.TryGetValue(project.ColumnSection, out _)) {
                    // 项目不合规范，删除该项目
                    projectService.Delete(project.Section);
                } else {
                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic.Add(project.Section, project);
                }
            }
            #endregion
            #region 窗口相关数据初始化
            List<Project> autoRunProjects = new List<Project>();
            CalculateWidthHeight();
            // 面板初始化
            foreach (KeyValuePair<string, Bean.Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                for (int i = 0; i < type.Value.ColumnDic.Count; i++) {
                    // 打开哪个栏目
                    var column = type.Value.ColumnDic[i];
                    if ((string.IsNullOrEmpty(type.Value.OpenColumn) || !type.Value.ColumnDic.ContainsKey(type.Value.OpenColumn)) && i == 0) {
                        column.StartOpen = true;
                    } else {
                        if (column.Section.Equals(type.Value.OpenColumn)) {
                            column.StartOpen = true;
                        }
                    }
                    if (true == column.StartOpen) {
                        column.IsExpanded = true;
                        column.ColumnHeight = (int)type.Value.ExpandedColumnHeight;
                    } else {
                        column.IsExpanded = false;
                        column.ColumnHeight = 0;
                    }
                    // 加载应用
                    foreach (KeyValuePair<string, Project> project in column.ProjectDic) {
                        Project projectValue = project.Value;
                        // 自启动应用
                        if (projectValue.AutoRun != null && (bool)projectValue.AutoRun) {
                            autoRunProjects.Add(ProjectUtils.Copy(projectValue));
                        }
                    }
                }
            }
            mainViewModel.Types = XStartService.TypeDic;
            int openTypeIndex = mainViewModel.Types.IndexOf(openType);
            mainViewModel.SelectedIndex = openTypeIndex < 0 ? 0 : openTypeIndex;
            #endregion

            if (mainViewModel.Audio) {
                AudioUtils.PlayWav(AudioUtils.START);
            }
            // 自启动
            if (autoRunProjects.Count > 0) {
                // 新线程
                Dispatcher.Invoke(new Action(delegate {
                    AutoRunWindow autoRunWindow = new AutoRunWindow { AutoRunProjects = autoRunProjects, Topmost = true };
                    if (true == autoRunWindow.ShowDialog()) {
                        // 启动项目
                        foreach (Project project in autoRunWindow.Projects) {
                            // 判断是否启动过
                            List<Process> existList = ProcessUtils.GetProcessByName(ProcessUtils.GetProcessName(project.Path), project.Path);
                            if (null == existList || existList.Count == 0) {
                                try {
                                    ExecuteProject(project);
                                } catch (Exception ex) {
                                    MessageBox.Show(ex.Message);
                                }
                            } else {
                                NotifyUtils.ShowNotification($"项目【{project.Name}】已存在，取消自启动！");
                            }
                        }
                    }
                    autoRunWindow.Close();
                }));
            }
            // 任务栏图标
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Files/xstart2.ico")).Stream;

            notifyIcon.Icon = new System.Drawing.Icon(iconStream);
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += MainWindow_Show;

            System.Windows.Forms.MenuItem showWindowMenuItem = new System.Windows.Forms.MenuItem("显示窗口");
            showWindowMenuItem.Click += MainWindow_Show;
            System.Windows.Forms.MenuItem closeWindowMenuItem = new System.Windows.Forms.MenuItem("退出");
            closeWindowMenuItem.Click += WindowCloseMenu_Click;
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { showWindowMenuItem, closeWindowMenuItem });
            Configs.inited = true;
            IsAllShow = true;
        }

        private void MainWindow_Show(object sender, EventArgs e) {
            IsAllShow = true;
            // 将窗口置为当前并显示
            IntPtr handle = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            DllUtils.ShowWindow(handle, WinApi.SW_NORMAL);
            DllUtils.SwitchToThisWindow(handle, true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            /* 关闭后保存所有类别打开的栏目***************************************************************/
            if (!Configs.inited) {
                return;
            }
            // 保存打开类别
            string setOpenType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE);
            if (string.IsNullOrEmpty(Configs.openType) || !Configs.openType.Equals(setOpenType)) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE, Configs.openType);
            }
            // 保存打开栏目
            foreach (KeyValuePair<string, Bean.Type> type in mainViewModel.Types) {
                foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                    if (column.Value.IsExpanded) {
                        if (!column.Value.Section.Equals(XStartService.TypeDic[column.Value.TypeSection].OpenColumn)) {
                            typeService.Update(new Bean.Type { Section = column.Value.TypeSection, OpenColumn = column.Value.Section });
                        }
                        break;
                    }
                }
            }
            // 窗口位置修改
            if (WindowState.Minimized != WindowState) {
                SaveFormSize();
                SaveFormLocation();
            }
            // 自启动
            Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (mainViewModel.AutoRun) {
                registryKey.SetValue(Constants.APP_NAME, System.Reflection.Assembly.GetExecutingAssembly().Location, Microsoft.Win32.RegistryValueKind.String);
            } else {
                registryKey.DeleteValue(Constants.APP_NAME, false);
            }
            registryKey.Dispose();
            // 剪切板内容清除
            Clipboard.Clear();
            // 配置保存
            SaveSetting();
            if (!Configs.forceExit && Configs.exitWarn && MessageBoxResult.Cancel == MessageBox.Show("确认退出?", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                // 取消退出
                e.Cancel = true;
            } else {
                // 退出时回收相关资源
                Configs.Dispose();
                AudioUtils.Dispose();
                DataBase.SqLiteFactory.CloseAllSqLite();
                AutoHideTimer.Stop();
                currentDateTimer.Stop();
                currentTimer.Stop();
                notifyIcon.Dispose();
                e.Cancel = false;
            }
        }

        private void WindowCloseMenu_Click(object sender, EventArgs e) {
            if (!Configs.exitWarn || MessageBoxResult.OK == MessageBox.Show("确认退出?", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                Configs.forceExit = true;
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// 栏目面板展开事件，该栏目对应的类别中，其他展开的栏目收缩
        /// 1.收缩时，将IsExpanded置为fase,以及高度置为0
        /// 2.当前栏目高度置为展开的高度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Expander_Expanded_Handler(object sender, RoutedEventArgs e) {
            Expander curExpander = sender as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            ExpandColumn(typeSection, columnSection);
        }
        /// <summary>
        /// 展开栏目
        /// </summary>
        /// <param name="typeSection">类别Section</param>
        /// <param name="columnSection">栏目Section</param>
        private void ExpandColumn(string typeSection, string columnSection) {
            Bean.Type type = XStartService.TypeDic[typeSection];
            foreach (Column c in type.ColumnDic.Values) {
                if (!columnSection.Equals(c.Section) && c.IsExpanded) {
                    c.IsExpanded = false;
                    c.ColumnHeight = 0;
                } else if (columnSection.Equals(c.Section)) {
                    c.ColumnHeight = (int)type.ExpandedColumnHeight;
                    // 如果当前类别有口令，并且没有记住口令则展示为锁定
                    if (!c.Locked && !c.RememberSecurity && c.HasPassword) {
                        c.Locked = true;
                    }
                }
            }
            if (mainViewModel.Audio) {
                AudioUtils.PlayWav(AudioUtils.CHANGE);
            }
        }

        /// <summary>
        /// 点击的当前已经展开的栏目，事件不处理
        /// expander的收缩只能则其他的expander展开触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Expander_Header_PreviewMouseLeftButtonDown(object sender, RoutedEventArgs e) {
            ToggleButton expanderHeaderButton = sender as ToggleButton;
            if (true == expanderHeaderButton.IsChecked) {
                e.Handled = true;
            }
        }

        private void MainTabControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            mainViewModel.TabControlActualHeight = MainTabControl.ActualHeight;
            // 重新计算按钮宽度以及栏目高度
            CalculateWidthHeight();
        }
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (Configs.inited) {
                if (MainTabControl.SelectedIndex != -1) {
                    mainViewModel.SelectedIndex = MainTabControl.SelectedIndex;
                    KeyValuePair<string, Bean.Type> type = (KeyValuePair<string, Bean.Type>)MainTabControl.SelectedItem;
                    // 记录下当前打开的类别（关闭时判断如果和配置中打开不一样，则保存配置）
                    Configs.openType = type.Value.Section;
                    // 如果当前类别有口令，并且没有记住口令则展示为锁定
                    if (!type.Value.Locked && !type.Value.RememberSecurity && type.Value.HasPassword) {
                        type.Value.Locked = true;
                    }
                }
                if (mainViewModel.Audio) {
                    AudioUtils.PlayWav(AudioUtils.CHANGE);
                }
            }
        }

        /// <summary>
        /// 计算所有类别里的栏目高度
        /// </summary>
        private void CalculateWidthHeight() {
            // 重新计算栏目高度
            foreach (KeyValuePair<string, Bean.Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                CalculateWidthHeight(type.Value.Section);
            }
        }

        /// <summary>
        /// 计算指定的类别栏目高度
        /// </summary>
        /// <param name="typeSection">类别Section</param>
        private void CalculateWidthHeight(string typeSection) {
            Bean.Type type = XStartService.TypeDic[typeSection];
            // 计算栏目高度
            int headerHeight = 38;
            // 当高度为小于一定高度時，将高度置为-1
            double expandedHeight = MainTabControl.ActualHeight - headerHeight * type.ColumnDic.Count - 6;
            if (expandedHeight < 100) {
                expandedHeight = -1D;
                // 滚动条应该出现
                type.VerticalScroll = ScrollBarVisibility.Visible;
            } else {
                type.VerticalScroll = ScrollBarVisibility.Hidden;
            }
            type.ExpandedColumnHeight = expandedHeight;
            foreach (Column column in type.ColumnDic.Values) {
                if (column.IsExpanded) {
                    column.ColumnHeight = (int)expandedHeight;
                }
                // 栏目里项目的宽度 宽度根据ScrollView的ScrollChanged事件触发
                //ComputedColumnProjectWidth(column);
            }
        }

        private void ComputedColumnProjectWidth(Column column) {
            if (Visibility.Visible == column.VerticalScrollBar || ScrollBarVisibility.Visible == XStartService.TypeDic[column.TypeSection].VerticalScroll) {
                column.ProjectWidth = (int)(MainTabControl.ActualWidth - mainViewModel.TypeWidth - 46);
            } else {
                column.ProjectWidth = (int)(MainTabControl.ActualWidth - mainViewModel.TypeWidth - 28);
            }
        }

        private void Open_Setting(object sender, RoutedEventArgs e) {
            if (Topmost) {
                Topmost = false;
            }
            SettingWindow settingWindow = new SettingWindow(mainViewModel) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            if (true == settingWindow.ShowDialog()) {
                SettingViewModel settingVM = settingWindow.settingVM;
                // 将设置的值赋值，并写入配置
                mainViewModel.Audio = settingVM.Audio;
                mainViewModel.TopMost = settingVM.TopMost;
                mainViewModel.AutoRun = settingVM.AutoRun;
                mainViewModel.ExitWarn = settingVM.ExitWarn;
                mainViewModel.CloseBorderHide = settingVM.CloseBorderHide;
                mainViewModel.ClickType = settingVM.ClickType;
                mainViewModel.UrlOpen = settingVM.UrlOpen;
                mainViewModel.UrlOpenCustomBrowser = settingVM.UrlOpenCustomBrowser;
                SaveSetting();
            }
            settingWindow.Close();
            if (mainViewModel.TopMost) {
                Topmost = true;
            }
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddType_Click(object sender, RoutedEventArgs e) {
            ProjectTypeVM typeVm = new ProjectTypeVM {
                Title = "添加类别"
            };
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow(typeVm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateType(projectTypeWindow);
        }

        private void ClearType_Click(object sender, RoutedEventArgs e) {
            if (XStartService.TypeDic.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show("确认清空所有类别？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    foreach (KeyValuePair<string, Bean.Type> type in mainViewModel.Types) {
                        if (type.Value.Locked) {
                            MessageBox.Show($"[{type.Value.Name}]类别已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                            return;
                        }
                        foreach (KeyValuePair<string, Column> k in type.Value.ColumnDic) {
                            if (k.Value.Locked) {
                                MessageBox.Show($"类别[{type.Value.Name}]下栏目[{k.Value.Name}]已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                                return;
                            }
                        }
                        RemoveType(type.Value.Section);
                    }
                    NotifyUtils.ShowNotification("清空完成！");
                }
            } else {
                MessageBox.Show("当前无需清空！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
        }

        // 移除类别（左侧TreeMenu,TabControl的UIPage,TypeDic中数据，Ini文件）
        private void RemoveType(string section) {
            // 数据删除
            typeService.Delete(section);
            foreach (KeyValuePair<string, Column> column in XStartService.TypeDic[section].ColumnDic) {
                columnService.Delete(column.Value.Section);
                foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                    projectService.Delete(project.Value.Section);
                }
            }
            XStartService.TypeDic.Remove(section);
        }

        private void UpdateType_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string typeSection = typePanel.Tag as string;
            ProjectTypeVM typeVm = new ProjectTypeVM() {
                Section = typeSection, Name = XStartService.TypeDic[typeSection].Name
                , SelectedFa = XStartService.TypeDic[typeSection].FaIcon
            };
            if (!string.IsNullOrEmpty(XStartService.TypeDic[typeSection].FaIconColor)) {
                typeVm.SelectedIconColor = XStartService.TypeDic[typeSection].FaIconColor;
            }
            if (!string.IsNullOrEmpty(XStartService.TypeDic[typeSection].FaIconFontFamily)) {
                typeVm.SelectedFf = XStartService.TypeDic[typeSection].FaIconFontFamily;
            }
            typeVm.Title = "修改类别";
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow(typeVm) { WindowStartupLocation = WindowStartupLocation.CenterScreen, Topmost = true };
            OperateType(projectTypeWindow);
        }

        private void DeleteType_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            if (XStartService.TypeDic[section].Locked) {
                MessageBox.Show("当前类别已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("确认删除该类别？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {

                foreach (KeyValuePair<string, Column> k in XStartService.TypeDic[section].ColumnDic) {
                    if (k.Value.Locked) {
                        MessageBox.Show($"当前类别栏目[{k.Value.Name}]已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                        return;
                    }
                }
                RemoveTypeData(section);
                DelCount(typeService);
                NotifyUtils.ShowNotification("类别删除成功");
            }
        }

        private void OperateType(ProjectTypeWindow projectTypeWindow) {
            if (Topmost) {
                Topmost = false;
            }
            if (true == projectTypeWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(projectTypeWindow.VM.Section)) {
                    Bean.Type projectType = new Bean.Type {
                        Name = projectTypeWindow.VM.Name
                        , FaIcon = projectTypeWindow.VM.SelectedFa
                        , FaIconColor = projectTypeWindow.VM.SelectedIconColor
                        , FaIconFontFamily = projectTypeWindow.VM.SelectedFf.ToString()
                    };
                    projectType.Section = Guid.NewGuid().ToString();
                    projectType.Sort = XStartService.TypeDic[XStartService.TypeDic.Count - 1].Sort + 1;
                    typeService.Insert(projectType);
                    XStartService.TypeDic.Add(projectType.Section, projectType);
                    NotifyUtils.ShowNotification("新增类别成功！");
                } else {
                    Bean.Type projectType = XStartService.TypeDic[projectTypeWindow.VM.Section];
                    projectType.Name = projectTypeWindow.VM.Name;
                    projectType.FaIcon = projectTypeWindow.VM.SelectedFa;
                    projectType.FaIconColor = projectTypeWindow.VM.SelectedIconColor;
                    projectType.FaIconFontFamily = projectTypeWindow.VM.SelectedFf.ToString();
                    typeService.Update(projectType);
                    NotifyUtils.ShowNotification("修改类别成功！");
                }
            }
            projectTypeWindow.Close();
            if (mainViewModel.TopMost) {
                Topmost = true;
            }
        }

        private void AddTypeSecurity_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            AddSecurity(XStartService.TypeDic[section]);
            e.Handled = true;
        }
        private void UpdateTypeSecurity_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            Bean.Type type = XStartService.TypeDic[section];
            UpdateSecurity(type);
            e.Handled = true;
        }
        private void RemoveTypeSecurity_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            Bean.Type type = XStartService.TypeDic[section];
            RemoveSecurity(type);
            e.Handled = true;
        }
        // 类别锁定
        private void LockType_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            Bean.Type type = XStartService.TypeDic[section];
            type.Locked = true;
            e.Handled = true;
        }
        // 解锁类别
        private void UnlockType_Click(object sender, RoutedEventArgs e) {
            Button unlockButton = sender as Button;
            string typeSection = unlockButton.Tag as string;
            Bean.Type type = XStartService.TypeDic[typeSection];
            // 比较密码是否匹配
            if (type.Password.Equals(type.UnlockSecurity)) {
                // 解锁当前类别窗口
                type.Locked = false;
                type.UnlockSecurity = string.Empty;
            } else {
                MessageBox.Show("口令不匹配", Constants.MESSAGE_BOX_TITLE_ERROR);
                type.UnlockSecurity = string.Empty;
            }
            e.Handled = true;
        }

        // 清空类别下的所有栏目
        private void ClearColumn_Click(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string typeSection = typePanel.Tag as string;
            if (mainViewModel.Types[typeSection].ColumnDic.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show($"确认清空【{mainViewModel.Types[typeSection].Name}】类别下的所有栏目?", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    foreach (KeyValuePair<string, Column> column in mainViewModel.Types[typeSection].ColumnDic) {
                        if (column.Value.Locked) {
                            MessageBox.Show($"该类别下的栏目【{column.Value.Name}】已锁定不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                            e.Handled = true;
                            return;
                        }
                    }
                    RemoveTypeColumns(typeSection);
                }
            } else {
                MessageBox.Show("当前类别下没有栏目！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            e.Handled = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddColumn_Click(object sender, RoutedEventArgs e) {
            FrameworkElement element = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as FrameworkElement;
            // 需要判断是现有栏目发起的，还是类别空白发起的
            string typeSection = string.Empty;
            if (element is Expander) {
                typeSection = element.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            } else if (element is ScrollViewer) {
                typeSection = element.Tag as string;
            }
            if (string.Empty.Equals(typeSection)) {
                MessageBox.Show("无法获取当前类别", "错误");
                return;
            }
            ColumnVM vm = new ColumnVM {
                Title = "添加栏目",
                TypeSection = typeSection
            };
            ColumnWindow window = new ColumnWindow(vm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateColumn(window);
            CalculateWidthHeight(typeSection);
        }

        private void UpdateColumn_Click(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            ColumnVM vm = new ColumnVM {
                Title = "修改栏目",
                Name = XStartService.TypeDic[typeSection].ColumnDic[columnSection].Name,
                TypeSection = typeSection,
                Section = columnSection
            };
            ColumnWindow window = new ColumnWindow(vm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateColumn(window);
        }
        private void DeleteColumn_Click(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            if (MessageBoxResult.OK == MessageBox.Show("确认删除栏目:" + column.Name, Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                columnService.Delete(columnSection);
                XStartService.TypeDic[typeSection].ColumnDic.Remove(columnSection);
                NotifyUtils.ShowNotification("删除栏目成功！");
                CalculateWidthHeight(typeSection);
            }
        }

        private void OperateColumn(ColumnWindow columnWindow) {
            if (true == columnWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(columnWindow.vm.Section)) {
                    Column bean = new Column {
                        Name = columnWindow.vm.Name
                    };
                    bean.TypeSection = columnWindow.vm.TypeSection;
                    bean.Section = Guid.NewGuid().ToString();
                    bean.IsExpanded = true;
                    Column lastColumn = XStartService.TypeDic[columnWindow.vm.TypeSection].ColumnDic[XStartService.TypeDic[columnWindow.vm.TypeSection].ColumnDic.Count - 1];
                    if (null == lastColumn) {
                        bean.Sort = 0;
                    } else {
                        bean.Sort = lastColumn.Sort + 1;
                    }
                    columnService.Insert(bean);
                    XStartService.TypeDic[columnWindow.vm.TypeSection].ColumnDic.Add(bean.Section, bean);
                    // 展开新增的栏目
                    ExpandColumn(bean.TypeSection, bean.Section);
                    NotifyUtils.ShowNotification("新增栏目成功！");
                } else {
                    Column bean = XStartService.TypeDic[columnWindow.vm.TypeSection].ColumnDic[columnWindow.vm.Section];
                    bean.Name = columnWindow.vm.Name;
                    columnService.Update(bean);
                    NotifyUtils.ShowNotification("修改栏目成功！");
                }
            }
            columnWindow.Close();
        }

        private void AddColumnSecurity_Click(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            // 添加口令
            AddSecurity(column);
        }

        // 修改口令
        private void UpdateColumnSecurity_Click(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;

            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            UpdateSecurity(column);
        }
        private void RemoveColumnSecurity_Click(object sender, RoutedEventArgs e) {
            // 移除口令
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            RemoveSecurity(column);
        }
        // 栏目锁定
        private void LockColumn_Click(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            column.Locked = true;
        }
        // 解锁栏目
        private void UnlockColumn_Click(object sender, RoutedEventArgs e) {
            Button unlockButton = sender as Button;
            string columnSection = unlockButton.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = unlockButton.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            // 比较密码是否匹配
            if (column.Password.Equals(column.UnlockSecurity)) {
                // 解锁当前类别窗口
                column.Locked = false;
                column.UnlockSecurity = string.Empty;
            } else {
                MessageBox.Show("口令不匹配", Constants.MESSAGE_BOX_TITLE_ERROR);
                column.UnlockSecurity = string.Empty;
            }
        }

        private void AddSecurity<T>(T t) where T : TableData {
            // 添加口令
            AddSecurityWindow addSecurityWindow = new AddSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "添加口令", Section = t.Section, Kind = Constants.TYPE, Operate = Constants.OPERATE_CREATE }
            };
            if (true == addSecurityWindow.ShowDialog()) {
                // 保存口令
                T newT = Activator.CreateInstance<T>();
                newT.Section = t.Section;
                newT.Password = addSecurityWindow.VM.Security;
                new TableService<T>().Update(newT);
                // 更新数据
                t.Password = addSecurityWindow.VM.Security;
                t.HasPassword = true;
                t.Locked = true;
                t.RememberSecurity = false;
                NotifyUtils.ShowNotification("口令添加成功！");
            }
            addSecurityWindow.Close();
        }
        private void UpdateSecurity<T>(T t) where T : TableData {
            if (Topmost) {
                Topmost = false;
            }
            UpdateSecurityWindow updateSecurityWindow = new UpdateSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "修改口令", Section = t.Section, CurSecurity = t.Password, Kind = Constants.TYPE, Operate = Constants.OPERATE_UPDATE }
            };
            if (true == updateSecurityWindow.ShowDialog()) {
                T newT = Activator.CreateInstance<T>();
                newT.Section = t.Section;
                newT.Password = updateSecurityWindow.VM.Security;
                // 保存口令
                new TableService<T>().Update(newT);
                // 更新数据
                t.Password = updateSecurityWindow.VM.Security;
                t.HasPassword = true;
                t.Locked = true;
                NotifyUtils.ShowNotification("口令修改成功！");
            }
            updateSecurityWindow.Close();
            if (mainViewModel.TopMost) {
                Topmost = true;
            }
        }

        private void RemoveSecurity<T>(T t) where T : TableData {
            if (Topmost) {
                Topmost = false;
            }
            RemoveSecurityWindow removeSecurityWindow = new RemoveSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "移除口令", Section = t.Section, CurSecurity = t.Password, Kind = Constants.TYPE, Operate = Constants.OPERATE_REMOVE }
            };
            if (true == removeSecurityWindow.ShowDialog()) {
                T newT = Activator.CreateInstance<T>();
                newT.Section = t.Section;
                newT.Password = string.Empty;
                // 去除口令
                new TableService<T>().Update(newT);
                // 更新数据
                t.Password = string.Empty;
                t.HasPassword = false;
                t.Locked = false;
                NotifyUtils.ShowNotification("口令移除成功！");
            }
            removeSecurityWindow.Close();
            if (mainViewModel.TopMost) {
                Topmost = true;
            }
        }


        // 移除类别（左侧TreeMenu,TabControl的UIPage,TypeDic中数据，Ini文件）
        private void RemoveTypeData(string typeSection) {
            // 栏目数据删除
            RemoveTypeColumns(typeSection);
            // 类别删除
            typeService.Delete(typeSection);
            DelCount(typeService);
            // 页面数据删除
            mainViewModel.Types.Remove(typeSection);
        }

        private void RemoveTypeColumns(string typeSection) {
            foreach (KeyValuePair<string, Column> column in XStartService.TypeDic[typeSection].ColumnDic) {
                columnService.Delete(column.Value.Section);
                DelCount(columnService);
                RemoveColumnProjects(typeSection, column.Value.Section);
            }
            mainViewModel.Types[typeSection].ColumnDic.Clear();
        }

        private void RemoveColumnProjects(string typeSection, string columnSection) {
            foreach (KeyValuePair<string, Project> project in XStartService.TypeDic[typeSection].ColumnDic[columnSection].ProjectDic) {
                if (SystemProjectParam.MSTSC.Equals(project.Value.Path)) {
                    // 远程的rdp文件删除
                    RdpUtils.DeleteProfiles(Configs.AppStartPath + @$"rdp\{project.Value.Section}.rdp");
                }
                projectService.Delete(project.Value.Section);
                DelCount(projectService);

            }
            mainViewModel.Types[typeSection].ColumnDic[columnSection].ProjectDic.Clear();
        }

        private void ProjectButton_Click(object sender, RoutedEventArgs e) {
            if (Constants.CLICK_TYPE_SINGLE.Equals(mainViewModel.ClickType)) {
                // 运行项目
                ProjectButton_Open(sender);
            }
        }
        private void ProjectButton_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            if (Constants.CLICK_TYPE_DOUBLE.Equals(mainViewModel.ClickType)) {
                // 运行项目
                ProjectButton_Open(sender);
            }
        }
        // 打开项目
        private void ProjectButton_Open(object sender) {
            Project project = (Project)(sender as Button).Tag;
            ExecuteProject(project);
        }

        private void AddProject_Click(object sender, RoutedEventArgs e) {
            // 当前栏目
            FrameworkElement element = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as FrameworkElement;
            object tag = element.Tag;
            string typeSection = string.Empty;
            string columnSection = string.Empty;
            if (tag is Column column) {
                // 项目放置面板
                typeSection = column.TypeSection;
                columnSection = column.Section;
            } else if (tag is Project project) {
                typeSection = project.TypeSection;
                columnSection = project.ColumnSection;
            }
            ProjectWindow projectWindow = new ProjectWindow("添加项目", typeSection, columnSection);
            if (true == projectWindow.ShowDialog()) {
                if (string.IsNullOrEmpty(projectWindow.Project.Kind)) {
                    projectWindow.Project.Kind = XStartService.KindOfPath(projectWindow.Project.Path);
                }
                XStartService.AddNewApp(projectWindow.Project);
                NotifyUtils.ShowNotification($"添加[{projectWindow.Project.Name}]成功！");
            }
            projectWindow.Close();
        }

        private void ClearProject_Click(object sender, RoutedEventArgs e) {
            // 当前栏目
            FrameworkElement element = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as FrameworkElement;


            string typeSection = string.Empty;
            string columnSection = string.Empty;
            if (element is Expander columnExpander) {
                typeSection = columnExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
                columnSection = columnExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            } else {
                object tag = element.Tag;
                if (tag is Column column) {
                    // 项目放置面板
                    typeSection = column.TypeSection;
                    columnSection = column.Section;
                } else if (tag is Project project) {
                    typeSection = project.TypeSection;
                    columnSection = project.ColumnSection;
                }
            }

            if (mainViewModel.Types[typeSection].ColumnDic[columnSection].ProjectDic.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show("确认清空栏目所有项目？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    RemoveColumnProjects(typeSection, columnSection);
                    NotifyUtils.ShowNotification("项目清空成功！");
                }
            } else {
                MessageBox.Show("当前栏目无项目！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }


            e.Handled = true;
        }
        // 编辑项目
        private void EditProject_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            if (null != project) {
                ProjectWindow projectWindow = new ProjectWindow("修改项目", project.TypeSection, project.ColumnSection) { Project = project };
                if (true == projectWindow.ShowDialog()) {
                    projectWindow.Project.Icon = XStartService.GetIconImage(projectWindow.Project.Kind, projectWindow.Project.Path, projectWindow.Project.IconPath);
                    if (string.IsNullOrEmpty(projectWindow.Project.Kind)) {
                        projectWindow.Project.Kind = XStartService.KindOfPath(projectWindow.Project.Path);
                    }
                    projectService.Update(projectWindow.Project);
                    NotifyUtils.ShowNotification($"修改[{projectWindow.Project.Name}]成功！");
                }
                projectWindow.Close();
            } else {
                MessageBox.Show("系统错误！");
            }
        }
        // 删除项目
        private void DeleteProject_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            if (MessageBoxResult.OK == MessageBox.Show("确认删除该项目？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                if (null != project) {
                    int result = projectService.Delete(project.Section);
                    if (result > 0) {
                        if (SystemProjectParam.MSTSC.Equals(project.Path)) {
                            // 远程的rdp文件删除
                            RdpUtils.DeleteProfiles(Configs.AppStartPath + @$"rdp\{project.Section}.rdp");
                        }
                        DelCount(projectService);
                        XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic.Remove(project.Section);
                        NotifyUtils.ShowNotification($"{project.Name}项目删除成功！");
                    } else {
                        MessageBox.Show($"{project.Name}项目删除失败！");
                    }
                }
            }
        }

        private void ExecuteProject(Project project) {
            try {
                // 获取该类别的应用是否配置确认信息，有确认信息，则弹出确认窗口
                if (SystemProjectParam.OperateParam.TryGetValue(project.Path, out Bean.SystemProject appOperateParam)) {
                    if (null != appOperateParam && appOperateParam.Confirm) {
                        if (MessageBoxResult.Cancel == MessageBox.Show(appOperateParam.ConfirmMsg, Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                            return;
                        }
                    }
                }
                #region 应用是否需要生成相关文件
                if (SystemProjectParam.MSTSC.Equals(project.Path)) {
                    // 远程桌面
                    string rdpFilePath = Configs.AppStartPath + @$"rdp\{project.Section}.rdp";
                    if (!File.Exists(rdpFilePath)) {
                        RdpUtils.FreshRdp(project, Constants.OPERATE_CREATE);
                    }
                }
                #endregion
                // 执行该应用
                string result = ProjectUtils.ExecuteApp(project);
                if (!string.IsNullOrEmpty(result)) {
                    NotifyUtils.ShowNotification(result);
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, Constants.MESSAGE_BOX_TITLE_ERROR);
            }
        }

        // 计删除次数
        private void DelCount<T>(TableService<T> t) where T : TableData {
            if (Configs.delCount > Constants.DEL_COUNT_LIMIT) {
                Configs.delCount = 0;
                t.Vacuum();
            } else {
                Configs.delCount += 1;
            }
            XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_DEL_COUNT, Convert.ToString(Configs.delCount));

        }

        // 切换类别栏名称显示
        private void ToggleTabItem(object sender, RoutedEventArgs e) {
            mainViewModel.TypeTabExpanded = !mainViewModel.TypeTabExpanded;
        }

        /// <summary>
        /// 栏目里滚动条变动事件
        /// 获取当前滚动条是否显示，计算出项目的宽度（在一个项目一行的情况下）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollChanged(object sender, ScrollChangedEventArgs e) {
            ScrollViewer sv = sender as ScrollViewer;
            Column column = sv.Tag as Column;
            column.VerticalScrollBar = sv.ComputedVerticalScrollBarVisibility;
            ComputedColumnProjectWidth(column);
        }

        // 取消所有项目自启动
        private void CancelAllAutoRun_Click(object sender, RoutedEventArgs e) {
            List<Project> autoRunProjects = new List<Project>();
            foreach (KeyValuePair<string, Bean.Type> typeDic in mainViewModel.Types) {
                foreach (KeyValuePair<string, Column> columnDic in typeDic.Value.ColumnDic) {
                    foreach (KeyValuePair<string, Project> projectDic in columnDic.Value.ProjectDic) {
                        if (true == projectDic.Value.AutoRun) {
                            autoRunProjects.Add(projectDic.Value);
                        }
                    }
                }
            }
            if (autoRunProjects.Count > 0) {
                foreach (Project project in autoRunProjects) {
                    project.AutoRun = false;
                    projectService.Update(new Project() { Section = project.Section, AutoRun = false });
                }
                NotifyUtils.ShowNotification("已取消所有项目自启动！");
            } else {
                MessageBox.Show("当前无自启动项目！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            e.Handled = true;
        }

        // 恢复默认配置
        private void RestoreDefault_Click(object sender, RoutedEventArgs e) {
            if (MessageBoxResult.OK == MessageBox.Show("确认恢复默认配置？", Constants.MESSAGE_BOX_TITLE_WARN)) {
                mainViewModel.MainHeight = Constants.MAIN_HEIGHT;
                mainViewModel.MainWidth = Constants.MAIN_WIDTH;
                mainViewModel.MainLeft = Constants.MAIN_LEFT;
                mainViewModel.MainTop = Constants.MAIN_TOP;
                mainViewModel.TypeTabExpanded = true;
                mainViewModel.TopMost = false;
                mainViewModel.Audio = true;
                mainViewModel.ClickType = Constants.CLICK_TYPE_SINGLE;
                mainViewModel.CloseBorderHide = false;
                mainViewModel.AutoRun = true;
                mainViewModel.ExitWarn = true;
                mainViewModel.UrlOpen = Constants.URL_OPEN_DEFAULT;
            }
            e.Handled = true;
        }


        /// <summary>
        /// 显示关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAbout(object sender, RoutedEventArgs e) {
            System.Text.StringBuilder aboutSb = new System.Text.StringBuilder();
            //读取文件内容
            using (FileStream fs = File.OpenRead(Configs.AppStartPath + Constants.ABOUT_FILE)) {
                byte[] b = new byte[1024];
                System.Text.UTF8Encoding temp = new System.Text.UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0) {
                    aboutSb.Append(temp.GetString(b));
                }
            }
            NotifyUtils.ShowNotification(aboutSb.ToString(), Colors.LightBlue, "关于");
            e.Handled = true;
        }
        /// <summary>
        /// 保存当前窗口尺寸配置
        /// </summary>
        public void SaveFormSize() {
            SaveConfig(Constants.SECTION_SIZE, Constants.KEY_HEIGHT, ref Configs.mainHeight, mainViewModel.MainHeight);
            SaveConfig(Constants.SECTION_SIZE, Constants.KEY_WIDTH, ref Configs.mainWidth, mainViewModel.MainWidth);
        }

        /// <summary>
        /// 保存当前窗口位置
        /// </summary>
        public void SaveFormLocation() {
            // 保存当前位置
            SaveConfig(Constants.SECTION_LOCATION, Constants.KEY_LEFT, ref Configs.mainLeft, mainViewModel.MainLeft);
            SaveConfig(Constants.SECTION_LOCATION, Constants.KEY_TOP, ref Configs.mainTop, mainViewModel.MainTop);
        }
        //计时执行的程序
        private void CurrentTimer_Tick(object sender, EventArgs e) {
            mainViewModel.CurrentTime = DateTime.Now.ToString("T");
        }
        private void CurrentDate_Tick(object sender, EventArgs e) {
            // 下次运行时间
            TimeSpan timeToGo = GetTimeToNextMidnight(24);
            currentDateTimer.Interval = timeToGo;
            mainViewModel.CurrentDay = DateTime.Now.ToString("D");
            mainViewModel.CurrentWeekDay = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
        }

        private TimeSpan GetTimeToNextMidnight(int hour) {
            DateTime now = DateTime.Now;
            DateTime nextMidnight = now.Date.AddHours(hour);
            return nextMidnight - now;
        }

        private void ToggleAutoRun_Click(object sender, RoutedEventArgs e) {
            ToggleButton toggleButton = sender as ToggleButton;
            Project project = toggleButton.Tag as Project;
            // 切换项目自启动
            project.AutoRun = toggleButton.IsChecked;
            projectService.Update(new Project { Section = project.Section, AutoRun = project.AutoRun });
            e.Handled = true;
        }

        #region 保存配置项
        private void SaveSetting() {
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_TYPE_TAB_EXPAND, ref Configs.typeTabExpand, mainViewModel.TypeTabExpanded);// 类别标题是否展开
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_TOP_MOST, ref Configs.topMost, mainViewModel.TopMost);// 置顶
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_CLICK_TYPE, ref Configs.clickType, mainViewModel.ClickType);// 点击方式
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_AUDIO, ref Configs.audio, mainViewModel.Audio);// 音效开关
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_AUTO_RUN, ref Configs.autoRun, mainViewModel.AutoRun);// 自启动
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_EXIT_WARN, ref Configs.exitWarn, mainViewModel.ExitWarn);// 退出警告
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE, ref Configs.closeBorderHide, mainViewModel.CloseBorderHide);// 靠边隐藏
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN, ref Configs.urlOpen, mainViewModel.UrlOpen);// 浏览器打开链接
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN_CUSTOM_BROWSER, ref Configs.urlOpenCustomBrowser, mainViewModel.UrlOpenCustomBrowser);// 自定义浏览器
        }
        /// <summary>
        /// Config配置保存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="section">INI的section头</param>
        /// <param name="key">INI的关键字</param>
        /// <param name="from">原值</param>
        /// <param name="to">新值</param>
        private void SaveConfig<T>(string section, string key, ref T from, T to) {
            bool isChange = false;
            if (from is bool fromBool && to is bool toBool) {
                if (fromBool != toBool) {
                    isChange = true;
                }
            } else if (from is string fromStr && to is string toStr) {
                if (!fromStr.Equals(toStr)) {
                    isChange = true;
                }
            } else if (from is int fromInt && to is int toInt) {
                if (fromInt != toInt) {
                    isChange = true;
                }
            } else if (from is uint fromUint && to is uint toUint) {
                if (fromUint != toUint) {
                    isChange = true;
                }
            } else if (from is double fromDouble && to is double toDouble) {
                if (fromDouble != toDouble) {
                    isChange = true;
                }
            }
            if (isChange) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, section, key, Convert.ToString(to));
                from = to;
            }
        }
        #endregion

        #region 窗口靠边隐藏
        public void CancelAnchor() {
            AutoHideTimer.IsEnabled = false;
        }
        public void EnableAnchor() {
            AutoHideTimer.IsEnabled = true;
        }
        private void MainWindow_LocationChanged(object sender, EventArgs e) {
            StopAnchor();
        }

        internal System.Windows.Forms.AnchorStyles stopAnchor = System.Windows.Forms.AnchorStyles.None;
        /// <summary>
        /// 计时器控制函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AutoHideTimer_Tick(object sender, EventArgs e) {
            Point toPoint = new Point(Left, Top);
            double mouseDistance = 5;// 鼠标在边界距离多远范围
            double resumeSize = 5;// 隐藏后剩余出来的边界大小
            DllUtils.Point curPoint = new DllUtils.Point();
            DllUtils.GetCursorPos(ref curPoint); //获取鼠标相对桌面的位置
            bool isMouseEnter = curPoint.X >= Left - mouseDistance
                               && curPoint.X <= Left + Width + mouseDistance
                               && curPoint.Y >= Top - mouseDistance
                               && curPoint.Y <= Top + Height + mouseDistance;
            switch (stopAnchor) {
                case System.Windows.Forms.AnchorStyles.Top:
                    toPoint = IsAllShow || !Configs.closeBorderHide || isMouseEnter ? new Point(Left, 0) : new Point(Left, -(Height - resumeSize));
                    break;
                case System.Windows.Forms.AnchorStyles.Left:
                    toPoint = IsAllShow || !Configs.closeBorderHide || isMouseEnter ? new Point(0, Top) : new Point(-(Width - resumeSize), Top);
                    break;
                case System.Windows.Forms.AnchorStyles.Right:
                    toPoint = IsAllShow || !Configs.closeBorderHide || isMouseEnter ? new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Width, Top) : new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - resumeSize, Top);
                    break;
                case System.Windows.Forms.AnchorStyles.Bottom:
                    toPoint = IsAllShow || !Configs.closeBorderHide || isMouseEnter ? new Point(Left, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Height) : new Point(Left, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - resumeSize);
                    break;
            }
            if (isMouseEnter) {
                IsAllShow = false;
            }
            Animate2Location(this, toPoint);
        }
        private void StopAnchor() {
            if (Top <= 0 && Left <= 0) {
                stopAnchor = System.Windows.Forms.AnchorStyles.None;
            } else if (Top <= 0) {
                stopAnchor = System.Windows.Forms.AnchorStyles.Top;
            } else if (Left <= 0) {
                stopAnchor = System.Windows.Forms.AnchorStyles.Left;
            } else if (Left >= System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Width) {
                stopAnchor = System.Windows.Forms.AnchorStyles.Right;
            } else if (Top >= System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Height) {
                stopAnchor = System.Windows.Forms.AnchorStyles.Bottom;
            } else {
                stopAnchor = System.Windows.Forms.AnchorStyles.None;
            }
        }

        readonly int step = 1;
        private void Animate2Location(Window window, Point destination) {
            double curLeft = window.Left;
            double curTop = window.Top;
            double indexX = (destination.X - curLeft) / 200;
            double indexY = (destination.Y - curTop) / 200;
            if (indexX == 0) {
                indexX = destination.X > curLeft ? step : -step;
            }
            if (indexY == 0) {
                indexY = destination.Y > curTop ? step : -step;
            }
            int index = 1;
            bool xStop = false;
            bool yStop = false;
            double aniLocationX = curLeft;
            double aniLocationY = curTop;
            if (indexX == 0) {
                xStop = true;
            }
            if (indexY == 0) {
                yStop = true;
            }
            while (!xStop || !yStop) {
                if (!xStop) {
                    if (indexX < 0) {
                        if (aniLocationX + indexX <= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX += indexX;
                        }
                    } else {
                        if (aniLocationX + indexX >= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX += indexX;
                        }
                    }
                }
                if (!yStop) {
                    if (indexY < 0) {
                        if (aniLocationY + indexY < destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY += indexY;
                        }
                    } else {
                        if (aniLocationY + indexY > destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY += indexY;
                        }
                    }
                }
                window.Left = aniLocationX;
                window.Top = aniLocationY;
                index++;
            }
        }

        private void CloseBorderHide_Click(object sender, RoutedEventArgs e) {
            // 保存配置
            if (Configs.inited) {
                Configs.closeBorderHide = mainViewModel.CloseBorderHide;
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE, System.Convert.ToString(Configs.closeBorderHide));
            }
            if (!Configs.closeBorderHide) {
                // 不隐藏
                CancelAnchor();
            } else {
                EnableAnchor();
            }

        }
        #endregion

        private void FreshRdp_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            RdpUtils.FreshRdp(project, Constants.OPERATE_UPDATE);
            NotifyUtils.ShowNotification("RDP文件刷新成功！");
        }
        private void Property_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            if (null != project) {
                if (Project.KIND_FILE.Equals(project.Kind) || Project.KIND_DIRECTORY.Equals(project.Kind)) {
                    WinUtils.ShowFileProperties(project.Path);
                } else {
                    MessageBox.Show("该项目不可查看属性！", Constants.MESSAGE_BOX_TITLE_ERROR);
                }
            }
        }

        // 发送到桌面快捷方式
        private void Send2Desktop_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            if (null != project) {
                FileUtils.CreateShortCutOnDesktop(project.Name, project.Path);
                NotifyUtils.ShowNotification("已发送快捷方式到桌面！");
            } else {
                MessageBox.Show("该项目不可发送到桌面快捷方式！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
        }

        private void CutProject_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            project.Operate = Constants.OPERATE_CUT;// 粘贴完成后，原数据将删除
            WriteProject2Clipboard(project);
            // 缓存删除,但是数据不删除(在粘贴时判断)
            XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic.Remove(project.Section);
        }


        private void CopyProject_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            project.Operate = Constants.OPERATE_COPY;// 目前无逻辑处理
            WriteProject2Clipboard(project);
        }

        private void WriteProject2Clipboard(Project project) {
            Clipboard.SetData("XStartApp", new CopyProject {
                Name = project.Name, Section = project.Section, TypeSection = project.TypeSection, ColumnSection = project.ColumnSection
            , Path = project.Path, Kind = project.Kind, IconIndex = project.IconIndex, FontColor = project.FontColor, IconPath = project.IconPath
            , Arguments = project.Arguments, HotKey = project.HotKey, Remark = project.Remark, Operate = project.Operate
            });
        }

        private void PasteProject_Click(object sender, RoutedEventArgs e) {
            // 当前栏目
            FrameworkElement element = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as FrameworkElement;
            object tag = element.Tag;
            string typeSection = string.Empty;
            string columnSection = string.Empty;
            if (tag is Column column) {
                // 项目放置面板
                typeSection = column.TypeSection;
                columnSection = column.Section;
            } else if (tag is Project project) {
                // 选的已有项目粘贴
                typeSection = project.TypeSection;
                columnSection = project.ColumnSection;
            }
            if (Clipboard.ContainsData("XStartApp")) {
                CopyProject project = Clipboard.GetData("XStartApp") as CopyProject;
                string primarySection = project.Section;
                XStartService.AddNewApp(new Project {
                    Name = project.Name, TypeSection = typeSection, ColumnSection = columnSection
            , Path = project.Path, Kind = project.Kind, IconIndex = project.IconIndex, FontColor = project.FontColor, IconPath = project.IconPath
            , Arguments = project.Arguments, HotKey = project.HotKey, Remark = project.Remark
                });
                // 如果是剪切操作，需要将原数据删除
                if (Constants.OPERATE_CUT.Equals(project.Operate)) {
                    projectService.Delete(primarySection);
                }
            } else {
                MessageBox.Show("当前剪切板无应用数据！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            e.Handled = true;
        }

        private Project GetProjectByMenu(object sender) {
            FrameworkElement element = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as FrameworkElement;
            return element.Tag as Project;
        }

        #region 用户头像和昵称修改
        private void User_Click(object sender, RoutedEventArgs e) {
            UserWindow userWindow = new UserWindow(mainViewModel.AvatarPath, mainViewModel.NickName) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            if (true == userWindow.ShowDialog()) {
                // 保存配置项
                if (!mainViewModel.AvatarPath.Equals(userWindow.vm.AvatarPath)) {
                    XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_USER, Constants.KEY_USER_AVATAR, userWindow.vm.AvatarPath);
                    mainViewModel.AvatarPath = userWindow.vm.AvatarPath;
                }
                if (!mainViewModel.NickName.Equals(userWindow.vm.NickName)) {
                    XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_USER, Constants.KEY_USER_NICKNAME, userWindow.vm.NickName);
                    mainViewModel.NickName = userWindow.vm.NickName;
                }
            }
            userWindow.Close();
        }
        #endregion

        #region 项目拖拽
        // 拖拽到类别上自动打开该类别
        private void Type_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effects = DragDropEffects.All;
                DockPanel typePanel = sender as DockPanel;
                string typeSection = typePanel.Tag as string;
                int index = mainViewModel.Types.IndexOf(typeSection);
                mainViewModel.SelectedIndex = index;
            } else {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void Project_DragDrop(object sender, DragEventArgs e) {
            Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (array.Length > 5) {
                MessageBox.Show("最多拖拽5个项目！", Constants.MESSAGE_BOX_TITLE_ERROR);
                e.Effects = DragDropEffects.None;
                return;
            }
            FrameworkElement ele = sender as FrameworkElement;
            string typeSection, columnSection;

            if (ele is Expander columnExpander) {
                typeSection = columnExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
                columnSection = columnExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            } else if (ele is ScrollViewer projectScrollViewer) {
                Column column = projectScrollViewer.Tag as Column;
                typeSection = column.TypeSection;
                columnSection = column.Section;
            } else {
                MessageBox.Show("不支持的拖拽！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            int dragCount = 0;
            // 遍历拖进来的所有数据生成
            foreach (object o in array) {
                string path = o.ToString();
                Project project = new Project();
                // 如果是.lnk文件，读取里面的数据
                string ext = Path.GetExtension(path);
                if (ext.ToLower().Equals(".lnk")) {
                    // 快捷方式的文件处理
                    FileUtils.ShortCutUtil shortCut = FileUtils.ReadShortCut(path);
                    project.Kind = XStartService.KindOfPath(shortCut.TargetPath);
                    project.Path = shortCut.TargetPath;
                    string iconLocation = shortCut.IconLocation;
                    if (string.IsNullOrEmpty(iconLocation)) {
                        project.IconPath = shortCut.TargetPath;
                    } else {
                        string[] iconArray = iconLocation.Split(',');
                        project.IconPath = iconArray[0];
                        project.IconIndex = Convert.ToInt32(iconArray[1]);
                    }
                    project.Name = Path.GetFileName(shortCut.FullName).Replace(".lnk", string.Empty).Replace(".LNK", string.Empty);
                    project.RunStartPath = shortCut.WorkingDirectory;
                    project.Remark = shortCut.Description;
                    project.HotKey = shortCut.Hotkey;
                } else {
                    project.Kind = XStartService.KindOfPath(path);
                    project.Path = path;
                    project.IconPath = path;
                    project.IconIndex = 0;
                    project.Name = Path.GetFileNameWithoutExtension(path);
                    project.RunStartPath = Path.GetDirectoryName(path);
                }
                project.TypeSection = typeSection;
                project.ColumnSection = columnSection;
                XStartService.AddNewApp(project);
                dragCount++;
            }
            NotifyUtils.ShowNotification($"成功添加【{dragCount}】个项目！");
            e.Handled = true;
        }

        #endregion

        private void Calendar_Click(object sender, RoutedEventArgs e) {
            CalendarWindow cal = new CalendarWindow();
            cal.Show();
        }
    }
}
