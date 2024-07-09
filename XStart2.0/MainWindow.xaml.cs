﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using XStart2._0.Bean;
using XStart2._0.Commands;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Helper;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModel;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        // 时钟定时器
        private readonly System.Windows.Threading.DispatcherTimer AutoHideTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        private readonly System.Windows.Threading.DispatcherTimer currentTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        private readonly System.Windows.Threading.DispatcherTimer currentDateTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        private readonly System.Windows.Threading.DispatcherTimer AutoGcTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMinutes(5) };
        private readonly System.Windows.Threading.DispatcherTimer OperateMessageTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };

        // 数据服务
        public TableService<Bean.Type> typeService = ServiceFactory.GetTypeService();
        public TableService<Column> columnService = ServiceFactory.GetColumnService();
        public TableService<Project> projectService = ServiceFactory.GetProjectService();
        // 模型
        readonly MainViewModel mainViewModel = new MainViewModel();
        private static bool IsAllShow = true;
        System.Windows.Forms.NotifyIcon notifyIcon = null;
        public MainWindow() {
            InitializeComponent();
            Configs.Handler = new WindowInteropHelper(this).Handle;
            // 时钟定时任务
            currentTimer.Tick += new EventHandler(CurrentTimer_Tick);
            currentTimer.Start();
            // 日期定时任务(配置的是1秒，但是执行后，计算下一天的时间差)
            currentDateTimer.Tick += new EventHandler(CurrentDate_Tick);
            currentDateTimer.Start();
            // 靠边隐藏定时器
            AutoHideTimer.Tick += new EventHandler(AutoHideTimer_Tick);
            AutoHideTimer.Start();
            // 自动GC
            AutoGcTimer.Tick += new EventHandler(AutoGcTimer_Tick);
            AutoGcTimer.Start();
            // 操作消息（在设置消息的时候启动，计时完成后停止）
            OperateMessageTimer.Tick += new EventHandler(OperateMessageTimer_Tick);
            // 将模型赋值上下文
            DataContext = mainViewModel;
        }

        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="param"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            #region 读取配置文件
            var iniData = IniParserUtils.GetIniData(Constants.SET_FILE);
            #endregion
            #region 用户头像和昵称
            string avatarPath = iniData[Constants.SECTION_USER][Constants.KEY_USER_AVATAR];
            string nickName = iniData[Constants.SECTION_USER][Constants.KEY_USER_NICKNAME];
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
            string leftStr = iniData[Constants.SECTION_LOCATION][Constants.KEY_LEFT];
            string topStr = iniData[Constants.SECTION_LOCATION][Constants.KEY_TOP];
            string heightStr = iniData[Constants.SECTION_SIZE][Constants.KEY_HEIGHT];
            string widthStr = iniData[Constants.SECTION_SIZE][Constants.KEY_WIDTH];
            string themeName = iniData[Constants.SECTION_THEME][Constants.KEY_THEME_NAME];
            string themeCustom = iniData[Constants.SECTION_THEME][Constants.KEY_THEME_CUSTOM];

            Configs.mainHeight = string.IsNullOrEmpty(heightStr) ? Constants.MAIN_HEIGHT : Convert.ToDouble(heightStr);
            Configs.mainWidth = string.IsNullOrEmpty(widthStr) ? Constants.MAIN_WIDTH : Convert.ToDouble(widthStr);
            Configs.mainLeft = string.IsNullOrEmpty(leftStr) ? Constants.MAIN_LEFT : Convert.ToDouble(leftStr);
            Configs.mainTop = string.IsNullOrEmpty(topStr) ? Constants.MAIN_TOP : Convert.ToDouble(topStr);
            Configs.themeName = string.IsNullOrEmpty(themeName) ? Constants.WINDOW_THEME_BLUE : themeName;
            Configs.themeCustom = themeCustom;

            // 尺寸
            mainViewModel.MainHeight = Configs.mainHeight;
            mainViewModel.MainWidth = Configs.mainWidth;
            // 位置
            mainViewModel.MainLeft = Configs.mainLeft;
            mainViewModel.MainTop = Configs.mainTop;
            // 主题
            WindowTheme.Instance.ThemeCustom = Configs.themeCustom;
            WindowTheme.Instance.ThemeName = Configs.themeName;
            #endregion
            #region 加载设置项
            string mainHeadShowStr = iniData[Constants.SECTION_CONFIG][Constants.KEY_MAIN_HEAD_SHOW];
            string typeTabExpandStr = iniData[Constants.SECTION_CONFIG][Constants.KEY_TYPE_TAB_EXPAND];
            string topMostStr = iniData[Constants.SECTION_CONFIG][Constants.KEY_TOP_MOST];
            string openType = iniData[Constants.SECTION_CONFIG][Constants.KEY_OPEN_TYPE];// 上次最后打开的类别
            string clickType = iniData[Constants.SECTION_CONFIG][Constants.KEY_CLICK_TYPE];// 点击方式，单击、双击
            string rdpModel = iniData[Constants.SECTION_CONFIG][Constants.KEY_RDP_MODEL];
            string audio = iniData[Constants.SECTION_CONFIG][Constants.KEY_AUDIO];
            string autoRun = iniData[Constants.SECTION_CONFIG][Constants.KEY_AUTO_RUN];
            string exitWarn = iniData[Constants.SECTION_CONFIG][Constants.KEY_EXIT_WARN];
            string closeBorderHide = iniData[Constants.SECTION_CONFIG][Constants.KEY_CLOSE_BORDER_HIDE];
            string textEditor = iniData[Constants.SECTION_CONFIG][Constants.KEY_TEXT_EDITOR];
            string urlOpen = iniData[Constants.SECTION_CONFIG][Constants.KEY_URL_OPEN];
            string urlOpenCustomBrowser = iniData[Constants.SECTION_CONFIG][Constants.KEY_URL_OPEN_CUSTOM_BROWSER];
            string iconSize = iniData[Constants.SECTION_CONFIG][Constants.KEY_ICON_SIZE];
            string orientation = iniData[Constants.SECTION_CONFIG][Constants.KEY_ORIENTATION];// 排列方式
            string hideTitle = iniData[Constants.SECTION_CONFIG][Constants.KEY_HIDE_TITLE];// 标题隐藏
            string oneLineMulti = iniData[Constants.SECTION_CONFIG][Constants.KEY_ONE_LINE_MULTI];// 一行多个

            Configs.mainHeadShow = string.IsNullOrEmpty(mainHeadShowStr) || Convert.ToBoolean(mainHeadShowStr);
            Configs.typeTabExpand = string.IsNullOrEmpty(typeTabExpandStr) || Convert.ToBoolean(typeTabExpandStr);
            Configs.topMost = !string.IsNullOrEmpty(topMostStr) && Convert.ToBoolean(topMostStr);
            Configs.openType = openType;
            Configs.clickType = string.IsNullOrEmpty(clickType) ? Constants.CLICK_TYPE_SINGLE : clickType;
            Configs.rdpModel = string.IsNullOrEmpty(rdpModel) ? Constants.RDP_MODEL_CUSTOM : rdpModel;
            Configs.audio = !string.IsNullOrEmpty(audio) && Convert.ToBoolean(audio);
            Configs.autoRun = !string.IsNullOrEmpty(autoRun) && Convert.ToBoolean(autoRun);
            Configs.exitWarn = string.IsNullOrEmpty(exitWarn) || Convert.ToBoolean(exitWarn);
            Configs.closeBorderHide = string.IsNullOrEmpty(closeBorderHide) || Convert.ToBoolean(closeBorderHide);
            Configs.textEditor = textEditor;
            Configs.urlOpen = string.IsNullOrEmpty(urlOpen) ? Constants.URL_OPEN_DEFAULT : urlOpen;
            Configs.urlOpenCustomBrowser = urlOpenCustomBrowser;
            Configs.iconSize = string.IsNullOrEmpty(iconSize) ? Constants.ICON_SIZE_32 : Convert.ToInt32(iconSize);
            Configs.orientation = string.IsNullOrEmpty(orientation) ? Constants.ORIENTATION_HORIZONTAL : orientation;
            Configs.hideTitle = !string.IsNullOrEmpty(hideTitle) && Convert.ToBoolean(hideTitle);
            Configs.oneLineMulti = !string.IsNullOrEmpty(oneLineMulti) && Convert.ToBoolean(oneLineMulti);

            mainViewModel.MainHeadShow = Configs.mainHeadShow;
            mainViewModel.TypeTabExpanded = Configs.typeTabExpand;
            mainViewModel.ChangeTypeTab();// 初始化时要先触发一次
            mainViewModel.TopMost = Configs.topMost;
            mainViewModel.OpenType = Configs.openType;
            mainViewModel.ClickType = Configs.clickType;
            mainViewModel.RdpModel = Configs.rdpModel;
            mainViewModel.Audio = Configs.audio;
            mainViewModel.AutoRun = Configs.autoRun;
            mainViewModel.ExitWarn = Configs.exitWarn;
            mainViewModel.CloseBorderHide = Configs.closeBorderHide;
            mainViewModel.TextEditor = Configs.textEditor;
            mainViewModel.UrlOpen = Configs.urlOpen;
            mainViewModel.UrlOpenCustomBrowser = Configs.urlOpenCustomBrowser;
            mainViewModel.IconSize = Configs.iconSize;
            mainViewModel.Orientation = Configs.orientation;
            mainViewModel.HideTitle = Configs.hideTitle;
            mainViewModel.OneLineMulti = Configs.oneLineMulti;
            // 系统其他配置
            string delCount = iniData[Constants.SECTION_CONFIG][Constants.KEY_DEL_COUNT];// 数据库执行多少次删除后执行VACUUM
            Configs.delCount = string.IsNullOrEmpty(delCount) ? 0 : Convert.ToInt32(delCount);
            #endregion

            #region 系统功能图标和操作
            string systemProjectOpenPage = iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_SYSTEM_PROJECT_OPEN_PAGE];// 系统功能最后一次打开的TabPage页
            string addMulti = iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_ADD_MULTI];// 是否添加多个

            Configs.systemAppOpenPage = string.IsNullOrEmpty(systemProjectOpenPage) ? 0 : Convert.ToInt32(systemProjectOpenPage);
            Configs.systemAppAddMulti = !string.IsNullOrEmpty(addMulti) && Convert.ToBoolean(addMulti);
            // 系统功能图标初始化
            Configs.InitIconDic();
            SystemProjectParam.InitOperate();
            // 任务栏状态获取
            Configs.taskbarHandler = DllUtils.FindWindow("Shell_TrayWnd", null);
            Configs.taskbarIsShow = (DllUtils.GetWindowLong(Configs.taskbarHandler, WinApi.GWL_STYLE) & WinApi.WS_VISIBLE) == WinApi.WS_VISIBLE;
            // 音量状态获取
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

            #region 天气参数配置
            string weatherApi = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_API];
            string weatherImgTheme = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_IMG_THEME];
            Configs.weatherApi = weatherApi ?? Constants.WEATHER_API_GAODE;
            Configs.weatherImgTheme = weatherImgTheme ?? Constants.WEATHER_IMG_THEME_DEFAULT;
            mainViewModel.WeatherApi = Configs.weatherApi;
            mainViewModel.WeatherImgTheme = Configs.weatherImgTheme;
            Configs.lastWeacherCountry = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_COUNTRY];
            Configs.lastCountries = iniData[Constants.SECTION_WEATHER][Constants.KEY_LAST_CITYS];
            #region 易客云
            string weatherYkyUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_YKY_URL];
            Configs.weatherYkyApiAppId = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_YKY_APP_ID];
            Configs.weatherYkyApiAppSecret = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_YKY_APP_SECRET];
            Configs.weatherYkyApiUrl = weatherYkyUrl ?? Constants.WEATHER_YKY_API_URL;
            mainViewModel.WeatherYkyApiAppId = Configs.weatherYkyApiAppId;
            mainViewModel.WeatherYkyApiAppSecret = Configs.weatherYkyApiAppSecret;
            mainViewModel.WeatherYkyApiUrl = Configs.weatherYkyApiUrl;
            #endregion
            #region 高德
            string gaodeApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_GAODE_URL];
            string gaodeAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_GAODE_APP_KEY];
            Configs.weatherGaodeApiUrl = gaodeApiUrl ?? Constants.WEATHER_GAODE_API_URL;
            Configs.weatherGaodeAppKey = gaodeAppKey;
            mainViewModel.WeatherGaodeApiUrl = Configs.weatherGaodeApiUrl;
            mainViewModel.WeatherGaodeAppKey = Configs.weatherGaodeAppKey;
            #endregion
            #region 心知
            string seniverseApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_SENIVERSE_URL];
            string seniverseAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_SENIVERSE_APP_KEY];
            Configs.weatherSeniverseApiUrl = seniverseApiUrl ?? Constants.WEATHER_SENIVERSE_API_URL;
            Configs.weatherSeniverseAppKey = seniverseAppKey;
            mainViewModel.WeatherSeniverseApiUrl = Configs.weatherSeniverseApiUrl;
            mainViewModel.WeatherSeniverseAppKey = Configs.weatherSeniverseAppKey;
            #endregion
            #region 和风
            string qApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_Q_URL];
            string qAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_Q_APP_KEY];
            Configs.weatherQApiUrl = qApiUrl ?? Constants.WEATHER_Q_API_URL;
            Configs.weatherQAppKey = qAppKey;
            mainViewModel.WeatherQApiUrl = Configs.weatherQApiUrl;
            mainViewModel.WeatherQAppKey = Configs.weatherQAppKey;
            #endregion
            #region OpenWeather
            string openApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_OPEN_URL];
            string openAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_OPEN_APP_KEY];
            Configs.weatherOpenApiUrl = openApiUrl ?? Constants.WEATHER_OPEN_API_URL;
            Configs.weatherOpenAppKey = openAppKey;
            mainViewModel.WeatherOpenApiUrl = Configs.weatherOpenApiUrl;
            mainViewModel.WeatherOpenAppKey = Configs.weatherOpenAppKey;
            #endregion
            #region AccuWeather
            string accuApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_ACCU_URL];
            string accuAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_ACCU_APP_KEY];
            Configs.weatherAccuApiUrl = accuApiUrl ?? Constants.WEATHER_ACCU_API_URL;
            Configs.weatherAccuAppKey = accuAppKey;
            mainViewModel.WeatherAccuApiUrl = Configs.weatherAccuApiUrl;
            mainViewModel.WeatherAccuAppKey = Configs.weatherAccuAppKey;
            #endregion
            #region Visual Crossing
            string vcApiUrl = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_VC_URL];
            string vcAppKey = iniData[Constants.SECTION_WEATHER][Constants.KEY_WEATHER_VC_APP_KEY];
            Configs.weatherVcApiUrl = vcApiUrl ?? Constants.WEATHER_VC_API_URL;
            Configs.weatherVcAppKey = vcAppKey;
            mainViewModel.WeatherVcApiUrl = Configs.weatherVcApiUrl;
            mainViewModel.WeatherVcAppKey = Configs.weatherVcAppKey;
            #endregion
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
                Column column = XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection];
                if (null == column || string.IsNullOrEmpty(project.Name) || string.IsNullOrEmpty(project.TypeSection)
                    || string.IsNullOrEmpty(project.ColumnSection) || !XStartService.TypeDic.TryGetValue(project.TypeSection, out _)
                    || !XStartService.TypeDic[project.TypeSection].ColumnDic.TryGetValue(project.ColumnSection, out _)) {
                    // 项目不合规范，删除该项目
                    projectService.Delete(project.Section);
                } else {
                    // 初始化项目的图示
                    project.IconSize = null != column.IconSize ? (int)column.IconSize : Configs.iconSize;
                    project.Orientation = string.IsNullOrEmpty(column.Orientation) ? Configs.orientation : column.Orientation;
                    project.HideTitle = null == column.HideTitle ? Configs.hideTitle : (bool)column.HideTitle;
                    project.TypeName = XStartService.TypeDic[project.TypeSection].Name;
                    project.ColumnName = XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].Name;
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
                        } else {
                            column.StartOpen = false;
                        }
                    }
                    if (column.StartOpen) {
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
                            Project autoProject = ProjectUtils.Copy(projectValue, false);
                            autoProject.IconSize = Constants.ICON_SIZE_32;// 自启动的应用默认使用32的图标
                            autoRunProjects.Add(autoProject);
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
                    AutoRunProjectWindow(autoRunProjects);
                }));
            }
            autoRunProjects.Clear();
            #region 任务栏图标
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            using Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/Files/xstart2.ico")).Stream;

            notifyIcon.Icon = new System.Drawing.Icon(iconStream);
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += MainWindow_Show;
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();

            notifyIcon.ContextMenu.MenuItems.Add("显示窗口", MainWindow_Show);
            notifyIcon.ContextMenu.MenuItems.Add("设置", Open_Setting);
            notifyIcon.ContextMenu.MenuItems.Add("-");
            notifyIcon.ContextMenu.MenuItems.Add("日历", Calendar_Click);
            notifyIcon.ContextMenu.MenuItems.Add("文本编辑器", OpenNotePad_Click);
            notifyIcon.ContextMenu.MenuItems.Add("天气", Weather_Click);
            notifyIcon.ContextMenu.MenuItems.Add("-");
            notifyIcon.ContextMenu.MenuItems.Add("退出", WindowCloseMenu_Click);
            #endregion

            Configs.inited = true;
            IsAllShow = true;
            SetOperateMsg(Colors.Green, "加载完成");
        }

        private void AutoRunProjectWindow(List<Project> autoRunProjects, bool isStart = true) {
            OpenNewWindowUtils.SetTopmost(this);
            AutoRunWindow autoRunWindow = new AutoRunWindow { AutoRunProjects = autoRunProjects, Topmost = true, IsStart = isStart };
            if (true == autoRunWindow.ShowDialog()) {
                int existProjectCount = 0;
                int runProjectCount = 0;
                // 启动项目
                foreach (Project project in autoRunWindow.Projects) {
                    // 判断是否启动过
                    List<Process> existList = ProcessUtils.GetProcessByName(ProcessUtils.GetProcessName(project.Path), project.Path);
                    if (null == existList || existList.Count == 0) {
                        try {
                            ProjectUtils.ExecuteProject(project, mainViewModel.RdpModel);
                            runProjectCount++;
                        } catch (Exception ex) {
                            MessageBox.Show(ex.Message);
                        }
                    } else {
                        existProjectCount++;
                    }
                }
                SetOperateMsg(Colors.Green, $"{existProjectCount}个正在运行，执行{runProjectCount}个");
            }
            if (autoRunWindow.IsExit) {
                Close();
            }
            autoRunWindow.Close();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }

        private void MainWindow_Show(object sender, EventArgs e) {
            IsAllShow = true;
            // 将窗口置为当前并显示
            IntPtr handle = new WindowInteropHelper(this).Handle;
            DllUtils.ShowWindow(handle, WinApi.SW_NORMAL);
            DllUtils.SwitchToThisWindow(handle, true);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            /* 关闭后保存所有类别打开的栏目***************************************************************/
            if (!Configs.inited) {
                return;
            }

            // 配置以及窗口数据保存
            SaveSetting();
            if (!Configs.forceExit && Configs.exitWarn && MessageBoxResult.Cancel == MessageBox.Show("确认退出?", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                // 取消退出
                e.Cancel = true;
            } else {
                #region 子窗口退出
                // 远程窗口关闭
                Configs.mstscRealClose = true;
                if (Configs.MstscHandler.ToInt32() > 0) {
                    DllUtils.SendMessage(Configs.MstscHandler, WinApi.WM_CLOSE, 0, 0);
                    // 如果远程窗口取消关闭，则主窗口取消关闭
                    if (Configs.MstscHandler.ToInt32() > 0) {
                        e.Cancel = true;
                        return;
                    }
                }
                // 天气窗口关闭
                if (Configs.WeatherHandler.ToInt32() > 0) {
                    DllUtils.SendMessage(Configs.WeatherHandler, WinApi.WM_CLOSE, 0, 0);
                }
                // 日历窗口关闭
                if (Configs.CalendarHandler.ToInt32() > 0) {
                    DllUtils.SendMessage(Configs.CalendarHandler, WinApi.WM_CLOSE, 0, 0);
                }
                #endregion
                // 保存数据的调整（当前打开的类别，类别中打开的栏目，排序）
                int typeIndex = 0;
                foreach (KeyValuePair<string, Bean.Type> type in mainViewModel.Types) {
                    if (typeIndex != type.Value.Sort) {
                        typeService.UpdateSort(type.Value.Section, typeIndex);
                    }
                    int columnIndex = 0;
                    foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                        if (columnIndex != column.Value.Sort) {
                            columnService.UpdateSort(column.Value.Section, columnIndex);
                        }
                        if (column.Value.IsExpanded) {
                            if (!column.Value.Section.Equals(XStartService.TypeDic[column.Value.TypeSection].OpenColumn)) {
                                typeService.Update(new Bean.Type { Section = column.Value.TypeSection, OpenColumn = column.Value.Section });
                            }
                        }
                        int projectIndex = 0;
                        foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                            if (projectIndex != project.Value.Sort) {
                                projectService.UpdateSort(project.Value.Section, projectIndex);
                            }
                            projectIndex++;
                        }
                        columnIndex++;
                    }
                    typeIndex++;
                }

                // 自启动
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (mainViewModel.AutoRun) {
                    string exeLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    if (!exeLocation.Equals(registryKey.GetValue(Constants.APP_NAME))) {
                        registryKey.SetValue(Constants.APP_NAME, System.Reflection.Assembly.GetExecutingAssembly().Location, Microsoft.Win32.RegistryValueKind.String);
                    }
                } else {
                    registryKey.DeleteValue(Constants.APP_NAME, false);
                }
                registryKey.Dispose();
                // 退出时回收相关资源
                Configs.Dispose();
                AudioUtils.Dispose();
                DataBase.SqLiteFactory.CloseAllSqLite();
                AutoHideTimer.Stop();
                currentDateTimer.Stop();
                currentTimer.Stop();
                AutoGcTimer.Stop();
                OperateMessageTimer.Stop();
                notifyIcon.Dispose();
                DataContext = null;
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
            foreach (Column column in type.ColumnDic.Values) {
                if (!columnSection.Equals(column.Section) && column.IsExpanded) {
                    column.IsExpanded = false;
                    column.ColumnHeight = 0;
                } else if (columnSection.Equals(column.Section)) {
                    column.ColumnHeight = (int)type.ExpandedColumnHeight;
                    // 如果当前类别有口令，并且没有记住口令则展示为锁定
                    ChangeColumnResumeLock(column);
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
                    mainViewModel.OpenType = type.Value.Section;
                    // 如果当前类别有口令，并且没有记住口令则展示为锁定
                    if (!type.Value.Locked && !type.Value.RememberSecurity && type.Value.HasPassword) {
                        type.Value.Locked = true;
                    }
                    // 切换的类别中有栏目未记住口令，恢复锁定状态
                    foreach (var column in type.Value.ColumnDic) {
                        ChangeColumnResumeLock(column.Value);
                    }
                }
                if (mainViewModel.Audio) {
                    AudioUtils.PlayWav(AudioUtils.CHANGE);
                }
            }
        }

        private void ChangeColumnResumeLock(Column column) {
            // 如果当前类别有口令，并且没有记住口令则展示为锁定
            if (!column.Locked && !column.RememberSecurity && column.HasPassword) {
                column.Locked = true;
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

        private void ComputedColumnProject(Column column) {
            #region 图标尺寸
            int iconSize = Configs.iconSize;
            if (null != column.IconSize) {
                iconSize = (int)column.IconSize;
            }
            foreach (KeyValuePair<string, Project> project in column.ProjectDic) {
                if (project.Value.IconSize != iconSize) {
                    project.Value.IconSize = iconSize;
                }
            }
            #endregion
            #region 计算一行是否多个项目
            bool oneLineMulti = null == column.OneLineMulti ? mainViewModel.OneLineMulti : (bool)column.OneLineMulti;
            if (oneLineMulti) {
                // 一行多个
                column.ProjectWidth = -1;
            } else {
                // 一行一个
                if (Visibility.Visible == column.VerticalScrollBar || ScrollBarVisibility.Visible == XStartService.TypeDic[column.TypeSection].VerticalScroll) {
                    column.ProjectWidth = (int)(MainTabControl.ActualWidth - mainViewModel.TypeWidth - 46);
                } else {
                    column.ProjectWidth = (int)(MainTabControl.ActualWidth - mainViewModel.TypeWidth - 28);
                }
            }
            #endregion
        }

        private void Open_Setting(object sender, EventArgs e) {
            OpenSettingWindow();
        }

        private void OpenSettingWindow(int openTab = 0) {
            OpenNewWindowUtils.SetTopmost(this);
            SettingWindow settingWindow = new SettingWindow(mainViewModel) { WindowStartupLocation = WindowStartupLocation.CenterScreen, OpenTab = openTab };
            bool changeOneLineMulti = false;
            if (true == settingWindow.ShowDialog()) {
                SettingViewModel settingVM = settingWindow.settingVM;
                // 将设置的值赋值，关闭前写入配置
                mainViewModel.MainHeadShow = settingVM.MainHeadShow;
                mainViewModel.Audio = settingVM.Audio;
                mainViewModel.TopMost = settingVM.MainTopMost;
                mainViewModel.AutoRun = settingVM.AutoRun;
                mainViewModel.ExitWarn = settingVM.ExitWarn;
                mainViewModel.CloseBorderHide = settingVM.CloseBorderHide;
                mainViewModel.ClickType = settingVM.ClickType;
                mainViewModel.RdpModel = settingVM.RdpModel;
                mainViewModel.TextEditor = settingVM.TextEditor;
                mainViewModel.UrlOpen = settingVM.UrlOpen;
                mainViewModel.UrlOpenCustomBrowser = settingVM.UrlOpenCustomBrowser;
                mainViewModel.IconSize = settingVM.IconSize;
                mainViewModel.HideTitle = settingVM.HideTitle;
                mainViewModel.Orientation = settingVM.Orientation;
                if (mainViewModel.OneLineMulti != settingVM.OneLineMulti) {
                    changeOneLineMulti = true;
                    mainViewModel.OneLineMulti = settingVM.OneLineMulti;
                }
                mainViewModel.WeatherApi = settingVM.WeatherApi;
                mainViewModel.WeatherImgTheme = settingVM.WeatherImgTheme;
                #region 易客云
                mainViewModel.WeatherYkyApiAppId = settingVM.WeatherYkyApiAppId;
                mainViewModel.WeatherYkyApiAppSecret = settingVM.WeatherYkyApiAppSecret;
                mainViewModel.WeatherYkyApiUrl = settingVM.WeatherYkyApiUrl;
                #endregion
                #region 高德
                mainViewModel.WeatherGaodeApiUrl = settingVM.WeatherGaodeApiUrl;
                mainViewModel.WeatherGaodeAppKey = settingVM.WeatherGaodeAppKey;
                #endregion
                #region 心知
                mainViewModel.WeatherSeniverseApiUrl = settingVM.WeatherSeniverseApiUrl;
                mainViewModel.WeatherSeniverseAppKey = settingVM.WeatherSeniverseAppKey;
                #endregion
                #region 和风
                mainViewModel.WeatherQApiUrl = settingVM.WeatherQApiUrl;
                mainViewModel.WeatherQAppKey = settingVM.WeatherQAppKey;
                #endregion
                #region OpenWeather
                mainViewModel.WeatherOpenApiUrl = settingVM.WeatherOpenApiUrl;
                mainViewModel.WeatherOpenAppKey = settingVM.WeatherOpenAppKey;
                #endregion
                #region AccuWeather
                mainViewModel.WeatherAccuApiUrl = settingVM.WeatherAccuApiUrl;
                mainViewModel.WeatherAccuAppKey = settingVM.WeatherAccuAppKey;
                #endregion
                #region Visual Crossig
                mainViewModel.WeatherVcApiUrl = settingVM.WeatherVcApiUrl;
                mainViewModel.WeatherVcAppKey = settingVM.WeatherVcAppKey;
                #endregion
                SaveSetting();
            }
            // 导入时的数据处理，在导入数据后，新添加的Type需要计算相关的尺寸信息
            foreach (var type in mainViewModel.Types) {
                if (changeOneLineMulti) {
                    foreach (var column in type.Value.ColumnDic) {
                        ComputedColumnProject(column.Value);
                    }
                }
                if (type.Value.NewAdd) {
                    CalculateWidthHeight(type.Value.Section);
                    // 如果类别中有栏目，打开第一个栏目
                    if (type.Value.ColumnDic.Count > 0) {
                        type.Value.ColumnDic[0].IsExpanded = true;
                        type.Value.ColumnDic[0].ColumnHeight = (int)type.Value.ExpandedColumnHeight;
                    }
                    type.Value.NewAdd = false;
                }
            }
            settingWindow.Close();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }
        /// <summary>
        /// 查找应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Query(object sender, EventArgs e) {
            QueryWindow queryWindow = new QueryWindow(mainViewModel.RdpModel);
            queryWindow.ShowDialog();
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
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow { VM = typeVm, WindowStartupLocation = WindowStartupLocation.CenterScreen };
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

        // 移除类别
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
            Bean.Type type = GetMenuOfType(sender);
            ProjectTypeVM typeVm = new ProjectTypeVM() {
                Title = "修改类别",
                Section = type.Section, Name = type.Name
                , SelectedFa = type.FaIcon, SelectedIconColor = type.FaIconColor,
                SelectedFf = type.FaIconFontFamily
            };
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow { VM = typeVm, WindowStartupLocation = WindowStartupLocation.CenterScreen, Topmost = true };
            OperateType(projectTypeWindow);
        }

        private void DeleteType_Click(object sender, RoutedEventArgs e) {
            Bean.Type type = GetMenuOfType(sender);
            if (type.Locked) {
                MessageBox.Show("当前类别已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            if (MessageBoxResult.OK == MessageBox.Show("确认删除该类别？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {

                foreach (KeyValuePair<string, Column> k in type.ColumnDic) {
                    if (k.Value.Locked) {
                        MessageBox.Show($"当前类别栏目[{k.Value.Name}]已锁，不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                        return;
                    }
                }
                RemoveTypeData(type);
                DelCount(typeService);
                NotifyUtils.ShowNotification("类别删除成功");
            }
        }

        private void OperateType(ProjectTypeWindow projectTypeWindow) {
            OpenNewWindowUtils.SetTopmost(this);
            if (true == projectTypeWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(projectTypeWindow.VM.Section)) {
                    Bean.Type projectType = new Bean.Type {
                        Name = projectTypeWindow.VM.Name
                        , FaIcon = projectTypeWindow.VM.SelectedFa
                        , FaIconColor = projectTypeWindow.VM.SelectedIconColor
                        , FaIconFontFamily = projectTypeWindow.VM.SelectedFf.ToString()
                    };
                    XStartService.AddNewData(projectType);
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
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }

        private void AddTypeSecurity_Click(object sender, RoutedEventArgs e) {
            AddSecurity(GetMenuOfType(sender));
            e.Handled = true;
        }
        private void UpdateTypeSecurity_Click(object sender, RoutedEventArgs e) {
            Bean.Type type = GetMenuOfType(sender);
            UpdateSecurity(type);
            e.Handled = true;
        }
        private void RemoveTypeSecurity_Click(object sender, RoutedEventArgs e) {
            Bean.Type type = GetMenuOfType(sender);
            RemoveSecurity(type);
            e.Handled = true;
        }
        // 类别锁定
        private void LockType_Click(object sender, RoutedEventArgs e) {
            Bean.Type type = GetMenuOfType(sender);
            type.Locked = true;
            e.Handled = true;
        }

        private Bean.Type GetMenuOfType(object sender) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            return XStartService.TypeDic[section];
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
            Bean.Type type = GetMenuOfType(sender);
            if (type.ColumnDic.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show($"确认清空【{type.Name}】类别下的所有栏目?", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    foreach (KeyValuePair<string, Column> column in type.ColumnDic) {
                        if (column.Value.Locked) {
                            MessageBox.Show($"该类别下的栏目【{column.Value.Name}】已锁定不可删除！", Constants.MESSAGE_BOX_TITLE_ERROR);
                            e.Handled = true;
                            return;
                        }
                    }
                    RemoveTypeColumns(type);
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
                MessageBox.Show("无法获取当前类别", Constants.MESSAGE_BOX_TITLE_ERROR);
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
            Column column = GetMenuOfColumn(sender);
            ColumnVM vm = new ColumnVM {
                Title = "修改栏目",
                Name = column.Name,
                PriTypeSection = column.TypeSection,
                TypeSection = column.TypeSection,
                Section = column.Section,
                IconSize = column.IconSize,
                Orientation = column.Orientation,
                HideTitle = column.HideTitle,
                OneLineMulti = column.OneLineMulti,
                TopMost = true
            };
            ColumnWindow window = new ColumnWindow(vm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateColumn(window);
            if (!vm.PriTypeSection.Equals(vm.TypeSection)) {
                CalculateWidthHeight(vm.PriTypeSection);
                CalculateWidthHeight(vm.TypeSection);
            }
        }
        private void DeleteColumn_Click(object sender, RoutedEventArgs e) {
            Column column = GetMenuOfColumn(sender);
            if (MessageBoxResult.OK == MessageBox.Show("确认删除栏目:" + column.Name, Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                columnService.Delete(column.Section);
                int deleteColumnIndex = XStartService.TypeDic[column.TypeSection].ColumnDic.IndexOf(column.Section);
                XStartService.TypeDic[column.TypeSection].ColumnDic.Remove(column.Section);
                // 如果当前栏目是展开的,展开上一个栏目
                if (column.IsExpanded && XStartService.TypeDic[column.TypeSection].ColumnDic.Count > 0) {
                    if (deleteColumnIndex > 0) {
                        XStartService.TypeDic[column.TypeSection].ColumnDic[deleteColumnIndex-1].IsExpanded = true;
                    } else {
                        XStartService.TypeDic[column.TypeSection].ColumnDic[0].IsExpanded = true;
                    }
                    
                }
                NotifyUtils.ShowNotification("删除栏目成功！");
                CalculateWidthHeight(column.TypeSection);
            }
        }

        private void OperateColumn(ColumnWindow columnWindow) {
            OpenNewWindowUtils.SetTopmost(this);
            if (true == columnWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(columnWindow.vm.Section)) {
                    Column column = new Column {
                        Name = columnWindow.vm.Name,
                        Orientation = columnWindow.vm.Orientation,
                        HideTitle = columnWindow.vm.HideTitle,
                        IconSize = columnWindow.vm.IconSize,
                        OneLineMulti = columnWindow.vm.OneLineMulti
                    };
                    column.TypeSection = columnWindow.vm.TypeSection;
                    column.IsExpanded = true;
                    XStartService.AddNewData(column);
                    // 展开新增的栏目
                    ExpandColumn(column.TypeSection, column.Section);
                    NotifyUtils.ShowNotification("新增栏目成功！");
                } else {
                    Column column = XStartService.TypeDic[columnWindow.vm.PriTypeSection].ColumnDic[columnWindow.vm.Section];
                    column.TypeSection = columnWindow.vm.TypeSection;
                    column.Name = columnWindow.vm.Name;
                    column.Orientation = columnWindow.vm.Orientation;
                    column.HideTitle = columnWindow.vm.HideTitle;
                    column.IconSize = columnWindow.vm.IconSize;
                    if (column.OneLineMulti != columnWindow.vm.OneLineMulti) {
                        column.OneLineMulti = columnWindow.vm.OneLineMulti;
                        ComputedColumnProject(column);
                    }
                    columnService.Update(column, true);
                    #region 变更了类别
                    if (!columnWindow.vm.PriTypeSection.Equals(columnWindow.vm.TypeSection)) {
                        // 变更项目中的类别Section
                        foreach(var projectKV in column.ProjectDic) {
                            var projectUpdateModel = new Project() { Section = projectKV.Value.Section, TypeSection = column.TypeSection };
                            projectService.Update(projectUpdateModel);
                        }
                        if (column.IsExpanded) {
                            // 原类别中栏目展开第一个
                            if (XStartService.TypeDic[columnWindow.vm.PriTypeSection].ColumnDic.Count > 0) {
                                XStartService.TypeDic[columnWindow.vm.PriTypeSection].ColumnDic[0].IsExpanded = true;
                            }
                        }
                        column.IsExpanded = false;
                        // 数据结构变更
                        XStartService.TypeDic[columnWindow.vm.PriTypeSection].ColumnDic.Remove(column.Section);
                        XStartService.TypeDic[columnWindow.vm.TypeSection].ColumnDic.Add(column.Section, column);
                    }
                    #endregion
                    NotifyUtils.ShowNotification("修改栏目成功！");
                }
            }
            columnWindow.Close();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }

        private void AddColumnSecurity_Click(object sender, RoutedEventArgs e) {
            Column column = GetMenuOfColumn(sender);
            // 添加口令
            AddSecurity(column);
        }

        // 修改口令
        private void UpdateColumnSecurity_Click(object sender, RoutedEventArgs e) {
            Column column = GetMenuOfColumn(sender);
            UpdateSecurity(column);
        }
        private void RemoveColumnSecurity_Click(object sender, RoutedEventArgs e) {
            // 移除口令
            Column column = GetMenuOfColumn(sender);
            RemoveSecurity(column);
        }
        // 栏目锁定
        private void LockColumn_Click(object sender, RoutedEventArgs e) {
            Column column = GetMenuOfColumn(sender);
            column.Locked = true;
        }

        private Column GetMenuOfColumn(object sender) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            return XStartService.TypeDic[typeSection].ColumnDic[columnSection];
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
            OpenNewWindowUtils.SetTopmost(this);
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
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }
        private void UpdateSecurity<T>(T t) where T : TableData {
            OpenNewWindowUtils.SetTopmost(this);
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
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }

        private void RemoveSecurity<T>(T t) where T : TableData {
            OpenNewWindowUtils.SetTopmost(this);
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
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }


        // 移除类别（左侧TreeMenu,TabControl的UIPage,TypeDic中数据，Ini文件）
        private void RemoveTypeData(Bean.Type type) {
            // 栏目数据删除
            RemoveTypeColumns(type);
            // 类别删除
            typeService.Delete(type.Section);
            DelCount(typeService);
            // 页面数据删除
            mainViewModel.Types.Remove(type.Section);
        }

        private void RemoveTypeColumns(Bean.Type type) {
            foreach (KeyValuePair<string, Column> column in type.ColumnDic) {
                columnService.Delete(column.Value.Section);
                DelCount(columnService);
                RemoveColumnProjects(column.Value);
            }
            type.ColumnDic.Clear();
        }

        private void RemoveColumnProjects(string typeSection, string columnSection) {
            RemoveColumnProjects(mainViewModel.Types[typeSection].ColumnDic[columnSection]);
        }

        private void RemoveColumnProjects(Column column) {
            foreach (KeyValuePair<string, Project> project in column.ProjectDic) {
                if (SystemProjectParam.MSTSC.Equals(project.Value.Path)) {
                    // 远程的rdp文件删除
                    RdpUtils.DeleteProfiles(Configs.AppStartPath + @$"rdp\{project.Value.Section}.rdp");
                }
                projectService.Delete(project.Value.Section);
                DelCount(projectService);

            }
            column.ProjectDic.Clear();
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
            ProjectUtils.ExecuteProject(project, mainViewModel.RdpModel);
        }

        private void AddProject_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
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
            ProjectWindow projectWindow = new ProjectWindow("添加项目", typeSection, columnSection) { Topmost = true };
            if (true == projectWindow.ShowDialog()) {
                if (string.IsNullOrEmpty(projectWindow.Project.Kind)) {
                    projectWindow.Project.Kind = XStartService.KindOfPath(projectWindow.Project.Path);
                }
                XStartService.AddNewData(projectWindow.Project);
                NotifyUtils.ShowNotification($"添加[{projectWindow.Project.Name}]成功！");
            }
            projectWindow.Close();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
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

        private void AppRun_Click(object sender, RoutedEventArgs e) {
            Project project = GetProjectByMenu(sender);
            ProjectUtils.ExecuteProject(project, mainViewModel.RdpModel);
        }

        // 编辑项目
        private void EditProject_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            Project project = GetProjectByMenu(sender);
            if (null != project) {
                ProjectWindow projectWindow = new ProjectWindow("修改项目", project.TypeSection, project.ColumnSection) { Project = project, Topmost = true };
                if (true == projectWindow.ShowDialog()) {
                    //projectWindow.Project.Icon = XStartService.GetIconImage(projectWindow.Project.Kind, projectWindow.Project.Path, projectWindow.Project.IconPath, projectWindow.Project.IconSize);
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
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
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

        // 计删除次数
        private void DelCount<T>(TableService<T> t) where T : TableData {
            if (Configs.delCount > Constants.DEL_COUNT_LIMIT) {
                Configs.delCount = 0;
                t.Vacuum();
            } else {
                Configs.delCount += 1;
            }
            IniParserUtils.SaveIniData(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_DEL_COUNT, Convert.ToString(Configs.delCount));

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
            ComputedColumnProject(column);
        }

        private void AllAutoRun_Click(object sender, RoutedEventArgs e) {
            // 启动所有自启动
            List<Project> autoRunProjects = new List<Project>();
            foreach (KeyValuePair<string, Bean.Type> type in XStartService.TypeDic) {
                foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                    // 加载应用
                    foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                        Project projectValue = project.Value;
                        // 自启动应用
                        if (projectValue.AutoRun != null && (bool)projectValue.AutoRun) {
                            Project autoProject = ProjectUtils.Copy(projectValue, false);
                            autoProject.IconSize = Constants.ICON_SIZE_32;// 自启动的应用默认使用32的图标
                            autoRunProjects.Add(autoProject);
                        }
                    }
                }
            }
            // 自启动
            if (autoRunProjects.Count > 0) {
                // 新线程
                Dispatcher.Invoke(new Action(delegate {
                    AutoRunProjectWindow(autoRunProjects, false);
                }));
            } else {
                MessageBox.Show("当前无自启动应用！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            autoRunProjects.Clear();
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

        /// <summary>
        /// 取消所有有自定义样式的栏目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelAllCustomStyle_Click(object sender, EventArgs e) {
            if (MessageBoxResult.OK == MessageBox.Show("确认取消所有栏目自定义样式？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                foreach (KeyValuePair<string, Bean.Type> typeDic in mainViewModel.Types) {
                    foreach (KeyValuePair<string, Column> columnDic in typeDic.Value.ColumnDic) {
                        if (null != columnDic.Value.HideTitle || !string.IsNullOrEmpty(columnDic.Value.Orientation) || null != columnDic.Value.IconSize || null != columnDic.Value.OneLineMulti) {
                            Column clearStyle = new Column {
                                Section = columnDic.Value.Section,
                                HideTitle = null,
                                Orientation = null,
                                IconSize = null,
                                OneLineMulti = null
                            };
                            columnService.Update(clearStyle, true);
                        }
                    }
                }
                NotifyUtils.ShowNotification("取消所有栏目自定义样式成功！");
            }

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
                mainViewModel.RdpModel = Constants.RDP_MODEL_CUSTOM;
                mainViewModel.CloseBorderHide = false;
                mainViewModel.AutoRun = true;
                mainViewModel.ExitWarn = true;
                mainViewModel.TextEditor = string.Empty;
                mainViewModel.UrlOpen = Constants.URL_OPEN_DEFAULT;
                mainViewModel.IconSize = Constants.ICON_SIZE_32;
                mainViewModel.HideTitle = false;
                mainViewModel.OneLineMulti = false;
                mainViewModel.Orientation = Constants.ORIENTATION_HORIZONTAL;
                mainViewModel.WeatherApi = Constants.WEATHER_API_GAODE;
                mainViewModel.WeatherImgTheme = Constants.WEATHER_IMG_THEME_DEFAULT;
                mainViewModel.WeatherYkyApiUrl = Constants.WEATHER_YKY_API_URL;
                mainViewModel.WeatherGaodeApiUrl = Constants.WEATHER_GAODE_API_URL;
                mainViewModel.WeatherSeniverseApiUrl = Constants.WEATHER_SENIVERSE_API_URL;
                mainViewModel.WeatherQApiUrl = Constants.WEATHER_Q_API_URL;
                mainViewModel.WeatherOpenApiUrl = Constants.WEATHER_OPEN_API_URL;
                mainViewModel.WeatherAccuApiUrl = Constants.WEATHER_ACCU_API_URL;
                mainViewModel.WeatherVcApiUrl = Constants.WEATHER_VC_API_URL;
                WindowTheme.Instance.ThemeName = Constants.WINDOW_THEME_BLUE;
            }
            e.Handled = true;
        }


        /// <summary>
        /// 显示关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAbout(object sender, RoutedEventArgs e) {
            StringBuilder aboutSb = new StringBuilder();
            //读取文件内容
            using (FileStream fs = File.OpenRead(Configs.AppStartPath + Constants.ABOUT_FILE)) {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0) {
                    aboutSb.Append(temp.GetString(b));
                }
            }
            NotifyUtils.ShowNotification(aboutSb.ToString(), Colors.LightBlue, "关于");
            e.Handled = true;
        }

        //计时执行的程序
        private void CurrentTimer_Tick(object sender, EventArgs e) {
            mainViewModel.MyDateTime.CurTime = DateTime.Now.ToString("T");
        }
        private void CurrentDate_Tick(object sender, EventArgs e) {
            // 下次运行时间
            TimeSpan timeToGo = TimeUtils.GetTimeToNext(TimeEnum.DAY);
            currentDateTimer.Interval = timeToGo;
            mainViewModel.MyDateTime.CurDate = DateTime.Now.ToString("D");
            mainViewModel.MyDateTime.CurWeekDay = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
        }

        private void AutoGcTimer_Tick(object sender, EventArgs e) {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                //  配置工作使用空间
                DllUtils.SetProcessWorkingSetSize(Configs.Handler, -1, -1);
            }
        }

        private void OperateMessageTimer_Tick(object sender, EventArgs e) {
            // 执行完成后,定时器停止，清除消息
            OperateMessageTimer.Stop();
            mainViewModel.InitOperateMsg(Colors.White, string.Empty);
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
            IniParser.Model.IniData iniData = new IniParser.Model.IniData();

            #region 窗口位置和尺寸保存
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_SIZE, Constants.KEY_HEIGHT, ref Configs.mainHeight, mainViewModel.MainHeight);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_SIZE, Constants.KEY_WIDTH, ref Configs.mainWidth, mainViewModel.MainWidth);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_LOCATION, Constants.KEY_LEFT, ref Configs.mainLeft, mainViewModel.MainLeft);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_LOCATION, Constants.KEY_TOP, ref Configs.mainTop, mainViewModel.MainTop);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_THEME, Constants.KEY_THEME_NAME, ref Configs.themeName, WindowTheme.Instance.ThemeName);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_THEME, Constants.KEY_THEME_CUSTOM, ref Configs.themeCustom, WindowTheme.Instance.ThemeCustom);
            #endregion

            #region 配置项
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_MAIN_HEAD_SHOW, ref Configs.mainHeadShow, mainViewModel.MainHeadShow);// 主页面头部显示
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE, ref Configs.openType, mainViewModel.OpenType);// 类别标题是否展开
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_TYPE_TAB_EXPAND, ref Configs.typeTabExpand, mainViewModel.TypeTabExpanded);// 类别标题是否展开
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_TOP_MOST, ref Configs.topMost, mainViewModel.TopMost);// 置顶
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_CLICK_TYPE, ref Configs.clickType, mainViewModel.ClickType);// 点击方式
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_RDP_MODEL, ref Configs.rdpModel, mainViewModel.RdpModel);// 远程方式
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_AUDIO, ref Configs.audio, mainViewModel.Audio);// 音效开关
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_AUTO_RUN, ref Configs.autoRun, mainViewModel.AutoRun);// 自启动
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_EXIT_WARN, ref Configs.exitWarn, mainViewModel.ExitWarn);// 退出警告
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE, ref Configs.closeBorderHide, mainViewModel.CloseBorderHide);// 靠边隐藏
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_TEXT_EDITOR, ref Configs.textEditor, mainViewModel.TextEditor);// 靠边隐藏
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN, ref Configs.urlOpen, mainViewModel.UrlOpen);// 浏览器打开链接
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN_CUSTOM_BROWSER, ref Configs.urlOpenCustomBrowser, mainViewModel.UrlOpenCustomBrowser);// 自定义浏览器
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_ICON_SIZE, ref Configs.iconSize, mainViewModel.IconSize);// 图标尺寸
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_ORIENTATION, ref Configs.orientation, mainViewModel.Orientation);// 排列方式
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_HIDE_TITLE, ref Configs.hideTitle, mainViewModel.HideTitle);// 隐藏标题
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_CONFIG, Constants.KEY_ONE_LINE_MULTI, ref Configs.oneLineMulti, mainViewModel.OneLineMulti);// 一行多个
            #endregion

            #region 天气配置
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_API, ref Configs.weatherApi, mainViewModel.WeatherApi);// API对接方
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_IMG_THEME, ref Configs.weatherImgTheme, mainViewModel.WeatherImgTheme);// 图片主题
            #region 易客云
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_YKY_APP_ID, ref Configs.weatherYkyApiAppId, mainViewModel.WeatherYkyApiAppId);// 接口Appid
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_YKY_APP_SECRET, ref Configs.weatherYkyApiAppSecret, mainViewModel.WeatherYkyApiAppSecret);// 接口AppSecret
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_YKY_URL, ref Configs.weatherYkyApiUrl, mainViewModel.WeatherYkyApiUrl);// 接口URL
            #endregion
            #region 高德
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_GAODE_URL, ref Configs.weatherGaodeApiUrl, mainViewModel.WeatherGaodeApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_GAODE_APP_KEY, ref Configs.weatherGaodeAppKey, mainViewModel.WeatherGaodeAppKey);
            #endregion
            #region 心知
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_SENIVERSE_URL, ref Configs.weatherSeniverseApiUrl, mainViewModel.WeatherSeniverseApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_SENIVERSE_APP_KEY, ref Configs.weatherSeniverseAppKey, mainViewModel.WeatherSeniverseAppKey);
            #endregion
            #region 和风
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_Q_URL, ref Configs.weatherQApiUrl, mainViewModel.WeatherQApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_Q_APP_KEY, ref Configs.weatherQAppKey, mainViewModel.WeatherQAppKey);
            #endregion
            #region OpenWeather
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_OPEN_URL, ref Configs.weatherOpenApiUrl, mainViewModel.WeatherOpenApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_OPEN_APP_KEY, ref Configs.weatherOpenAppKey, mainViewModel.WeatherOpenAppKey);
            #endregion
            #region AccuWeather
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_ACCU_URL, ref Configs.weatherAccuApiUrl, mainViewModel.WeatherAccuApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_ACCU_APP_KEY, ref Configs.weatherAccuAppKey, mainViewModel.WeatherAccuAppKey);
            #endregion
            #region Visual Crossing
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_VC_URL, ref Configs.weatherVcApiUrl, mainViewModel.WeatherVcApiUrl);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_VC_APP_KEY, ref Configs.weatherVcAppKey, mainViewModel.WeatherVcAppKey);
            #endregion
            #endregion

            IniParserUtils.SaveIniData(Constants.SET_FILE, iniData);
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
            double mouseDistance = 1;// 鼠标在边界距离多远范围
            double resumeSize = 1;// 隐藏后剩余出来的边界大小
            DllUtils.Point curPoint = new DllUtils.Point();
            DllUtils.GetCursorPos(ref curPoint); //获取鼠标相对桌面的位置
            bool isMouseEnter = curPoint.X >= Left - mouseDistance
                               && curPoint.X <= Left + Width + mouseDistance
                               && curPoint.Y >= Top - mouseDistance
                               && curPoint.Y <= Top + Height + mouseDistance;
            switch (stopAnchor) {
                case System.Windows.Forms.AnchorStyles.Top:
                    toPoint = IsAllShow || !mainViewModel.CloseBorderHide || isMouseEnter ? new Point(Left, 0) : new Point(Left, -(Height - resumeSize));
                    break;
                case System.Windows.Forms.AnchorStyles.Left:
                    toPoint = IsAllShow || !mainViewModel.CloseBorderHide || isMouseEnter ? new Point(0, Top) : new Point(-(Width - resumeSize), Top);
                    break;
                case System.Windows.Forms.AnchorStyles.Right:
                    toPoint = IsAllShow || !mainViewModel.CloseBorderHide || isMouseEnter ? new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Width, Top) : new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - resumeSize, Top);
                    break;
                case System.Windows.Forms.AnchorStyles.Bottom:
                    toPoint = IsAllShow || !mainViewModel.CloseBorderHide || isMouseEnter ? new Point(Left, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Height) : new Point(Left, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - resumeSize);
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
            if (!mainViewModel.CloseBorderHide) {
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

        private void OpenFolder_Click(object sender, EventArgs e) {
            Project project = GetProjectByMenu(sender);

            // 打开文件所在的目录
            if (Project.KIND_FILE.Equals(project.Kind) || Project.KIND_DIRECTORY.Equals(project.Kind)) {
                if (File.Exists(project.Path)) {
                    Process.Start("explorer.exe", $"/select,{project.Path}");
                } else {
                    MessageBox.Show($"文件不存在！", Constants.MESSAGE_BOX_TITLE_ERROR);
                }
            }
        }

        /// <summary>
        /// 备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUp_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            BackUpCommand.ShowBackUpWindow();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Recover_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            bool? resumeResult = ResumeCommand.ShowResumeWindow();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
            if (null != resumeResult && (bool)resumeResult) {
                CalculateWidthHeight();
            }
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
                XStartService.AddNewData(new Project {
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
                IniParser.Model.IniData iniData = new IniParser.Model.IniData();
                // 保存配置项
                if (!mainViewModel.AvatarPath.Equals(userWindow.vm.AvatarPath)) {
                    iniData[Constants.SECTION_USER][Constants.KEY_USER_AVATAR] = userWindow.vm.AvatarPath;
                    mainViewModel.AvatarPath = userWindow.vm.AvatarPath;
                }
                if (!mainViewModel.NickName.Equals(userWindow.vm.NickName)) {
                    iniData[Constants.SECTION_USER][Constants.KEY_USER_NICKNAME] = userWindow.vm.NickName;
                    mainViewModel.NickName = userWindow.vm.NickName;
                }
                IniParserUtils.SaveIniData(Constants.SET_FILE, iniData);
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
            if (null == array) {
                return;
            }
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
                XStartService.AddNewData(project);
                dragCount++;
            }
            NotifyUtils.ShowNotification($"成功添加【{dragCount}】个项目！");
            e.Handled = true;
        }

        #endregion

        /// <summary>
        /// 打开日历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_Click(object sender, EventArgs e) {
            if (Configs.CalendarHandler.ToInt32() > 0) {
                // 打开当前窗口
                DllUtils.SwitchToThisWindow(Configs.CalendarHandler, true);
                DllUtils.ShowWindow(Configs.CalendarHandler, WinApi.SW_NORMAL);
            } else {
                CalendarWindow cal = new CalendarWindow();
                cal.vm.MyDateTime = mainViewModel.MyDateTime;
                cal.Show();
            }

        }

        private void OpenNotePad_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainViewModel.TextEditor)) {
                Process.Start("notepad");
            } else {
                Process.Start(mainViewModel.TextEditor);
            }
        }

        /// <summary>
        /// 打开天气窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Weather_Click(object sender, EventArgs e) {
            bool openWeatherWindow = false;
            if ((string.IsNullOrEmpty(Configs.weatherYkyApiAppId) || string.IsNullOrEmpty(Configs.weatherYkyApiAppSecret))
                && string.IsNullOrEmpty(Configs.weatherGaodeAppKey)
                && string.IsNullOrEmpty(Configs.weatherSeniverseAppKey)
                && string.IsNullOrEmpty(Configs.weatherQAppKey)
                && string.IsNullOrEmpty(Configs.weatherOpenAppKey)
                && string.IsNullOrEmpty(Configs.weatherAccuAppKey)
                && string.IsNullOrEmpty(Configs.weatherVcAppKey)) {
                MessageBoxResult result = MessageBox.Show("天气API参数未配置，是否立即配置？", Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.YesNoCancel);
                if (MessageBoxResult.Yes == result) {
                    // 打开设置
                    OpenSettingWindow(2);
                } else if (MessageBoxResult.No == result) {
                    // 打开窗口
                    openWeatherWindow = true;
                }
            } else {
                openWeatherWindow = true;
            }
            if (openWeatherWindow) {
                if (Configs.WeatherHandler.ToInt32() > 0) {
                    // 打开当前窗口
                    DllUtils.SwitchToThisWindow(Configs.WeatherHandler, true);
                    DllUtils.ShowWindow(Configs.WeatherHandler, WinApi.SW_NORMAL);
                } else {
                    WeatherWindow weather = new WeatherWindow();
                    weather.Show();
                }
            }
        }

        private void SetOperateMsg(Color color, string msg) {
            OperateMessageTimer.Start();
            mainViewModel.InitOperateMsg(color, msg);
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e) {
            string themeName = (sender as MenuItem).Tag as string;
            if (!themeName.Equals(WindowTheme.Instance.ThemeName)) {
                if (Constants.WINDOW_THEME_CUSTOM.Equals(themeName) && string.IsNullOrEmpty(WindowTheme.Instance.ThemeCustom)) {
                    if (MessageBoxResult.OK == MessageBox.Show("未配置自定义主题，是否立即配置？", Constants.MESSAGE_BOX_TITLE_INFO, MessageBoxButton.OKCancel)) {
                        // 打开自定义主题页
                        OpenThemeWindow();
                    }
                } else {
                    WindowTheme.Instance.ThemeName = themeName;
                }
            }
        }
        private void CustomTheme_Click(object sender, RoutedEventArgs e) {
            // 打开自定义主题页
            OpenThemeWindow();
        }

        private void OpenThemeWindow() {
            OpenNewWindowUtils.SetTopmost(this);
            ThemeWindow themeWindow = new ThemeWindow();
            if (true == themeWindow.ShowDialog()) {
                StringBuilder themeCustomSb = new StringBuilder()
                    .Append(GetColorString(themeWindow.vm.ConfirmButtonBackGroundColor, 1)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.ConfirmButtonForeGroundColor, 0)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.ConfirmButtonMouseOverBackGroundColor, 1)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.ConfirmButtonMouseOverForeGroundColor, 0)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.CancelButtonBackGroundColor, 1)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.CancelButtonForeGroundColor, 0)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.CancelButtonMouseOverBackGroundColor, 1)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.CancelButtonMouseOverForeGroundColor, 0)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.ToggleButtonCheckedBackGroundColor, 1)).Append(Constants.SPLIT_CHAR)
                    .Append(GetColorString(themeWindow.vm.ToggleButtonCheckedForeGroundColor, 0));
                WindowTheme.Instance.ThemeCustom = themeCustomSb.ToString();
                WindowTheme.Instance.ThemeName = Constants.WINDOW_THEME_CUSTOM;
            }
            themeWindow.Close();
            OpenNewWindowUtils.RecoverTopmost(this, mainViewModel);
        }
        /// <summary>
        /// 获取画刷的颜色，如果brush为null,type==1表示背景色，type!=1 表示文字色
        /// </summary>
        /// <param name="brush"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetColorString(SolidColorBrush brush, int type) {
            if (null == brush) {
                return type == 1 ? "#FFFFFF" : "#000000";
            } else {
                return brush.Color.ToString();
            }
        }
    }
}
