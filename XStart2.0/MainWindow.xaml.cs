using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using XStart.Bean;
using XStart.Config;
using XStart.Const;
using XStart.Services;
using XStart.Utils;
using XStart2._0.Helper;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        // 时钟定时器
        private readonly System.Windows.Threading.DispatcherTimer currentTimer = new System.Windows.Threading.DispatcherTimer();
        // 数据服务
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService projectService = ProjectService.Instance;
        // 模型
        readonly MainViewModel mainViewModel = new MainViewModel();
        public MainWindow() {
            InitializeComponent();
            Configs.Handler = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            // Tick 超过计时器间隔时发生。
            currentTimer.Tick += new EventHandler(CurrentTimer_Tick);
            // Interval 获取或设置计时器刻度之间的时间段
            currentTimer.Interval = new TimeSpan(0, 0, 1);
            currentTimer.Start();
            // 将模型赋值上下文
            DataContext = mainViewModel;
        }

        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="param"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            #region 窗口相关加载，尺寸，位置，置顶
            string leftStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_LEFT);
            string topStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_TOP);
            string heightStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_HEIGHT);
            string widthStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_WIDTH);

            // 尺寸
            if (!string.IsNullOrEmpty(heightStr)) {
                mainViewModel.MainHeight = Convert.ToDouble(heightStr);
            }
            if (!string.IsNullOrEmpty(widthStr)) {
                mainViewModel.MainWidth = Convert.ToDouble(widthStr);
            }
            // 位置
            if (!string.IsNullOrEmpty(leftStr)) {
                mainViewModel.MainLeft = Convert.ToDouble(leftStr);
            }
            if (!string.IsNullOrEmpty(topStr)) {
                mainViewModel.MainTop = Convert.ToDouble(topStr);
            }


            Configs.mainHeight = mainViewModel.MainHeight;
            Configs.mainWidth = mainViewModel.MainWidth;
            Configs.mainLeft = mainViewModel.MainLeft;
            Configs.mainTop = mainViewModel.MainTop;
            #endregion
            #region 加载设置项
            string topMostStr = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_TOP_MOST);
            string openType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE);// 上次最后打开的类别
            string clickType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_CLICK_TYPE);// 点击方式，单击、双击
            string audio = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_AUDIO);
            string autoRun = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_AUTO_RUN);
            string exitWarn = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_EXIT_WARN);
            string closeBorderHide = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE);
            string urlOpen = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN);
            string urlOpenCustomBrowser = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN_CUSTOM_BROWSER);

            // 置顶
            Configs.topMost = !string.IsNullOrEmpty(topMostStr) && Convert.ToBoolean(topMostStr);
            Configs.openType = openType;
            Configs.clickType = string.IsNullOrEmpty(clickType) ? Constants.CLICK_TYPE_SINGLE : clickType;
            Configs.audio = !string.IsNullOrEmpty(audio) && Convert.ToBoolean(audio);
            Configs.autoRun = !string.IsNullOrEmpty(autoRun) && Convert.ToBoolean(autoRun);
            Configs.exitWarn = string.IsNullOrEmpty(exitWarn) || Convert.ToBoolean(exitWarn);
            Configs.closeBorderHide = string.IsNullOrEmpty(closeBorderHide) || Convert.ToBoolean(closeBorderHide);
            if (!string.IsNullOrEmpty(urlOpen)) { Configs.urlOpen = urlOpen; }
            Configs.urlOpenCustomBrowser = urlOpenCustomBrowser;

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
            string systemAppPage = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_SYSTEM_APP_PAGE);
            Configs.systemAppPage = string.IsNullOrEmpty(systemAppPage) ? 0 : Convert.ToInt32(systemAppPage);
            string addMulti = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_ADD_MULTI);
            Configs.addMulti = !string.IsNullOrEmpty(addMulti) && Convert.ToBoolean(addMulti);
            Configs.InitIconDic();
            SystemAppParam.InitOperate();
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
            List<XStart.Bean.Type> types = typeService.SelectList(new XStart.Bean.Type { OrderBy = "sort" });
            foreach (XStart.Bean.Type type in types) {
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
            // 加载应用配置
            List<Project> projects = projectService.SelectList(new Project { OrderBy = "sort" });

            foreach (Project project in projects) {
                project.Icon = XStartService.BitmapToBitmapImage(XStartService.GetIconImage(project));
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
            LinkedHashMap<string, Project> autoRunApp = new LinkedHashMap<string, Project>();
            CalculateWidthHeight();
            // 面板初始化
            foreach (KeyValuePair<string, XStart.Bean.Type> type in XStartService.TypeDic) {
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
                            autoRunApp.Add(projectValue.Section, projectValue);
                        }
                    }
                }
            }
            mainViewModel.TypeTabExpanded = true;
            mainViewModel.TypeTabToggleIcon = FontAwesome6.Outdent;
            mainViewModel.TypeWidth = 100;
            mainViewModel.Types = XStartService.TypeDic;
            int openTypeIndex = mainViewModel.Types.IndexOf(openType);
            mainViewModel.SelectedIndex = openTypeIndex < 0 ? 0 : openTypeIndex;
            #endregion

            Configs.inited = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            /* 关闭后保存所有类别打开的栏目***************************************************************/
            // 保存打开类别
            string setOpenType = XStartIniUtils.IniReadValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE);
            if (string.IsNullOrEmpty(Configs.openType) || !Configs.openType.Equals(setOpenType)) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_OPEN_TYPE, Configs.openType);
            }
            // 保存打开栏目
            foreach (KeyValuePair<string, XStart.Bean.Type> type in mainViewModel.Types) {
                foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                    if (column.Value.IsExpanded) {
                        if (!column.Value.Section.Equals(XStartService.TypeDic[column.Value.TypeSection].OpenColumn)) {
                            typeService.Update(new XStart.Bean.Type { Section = column.Value.TypeSection, OpenColumn = column.Value.Section });
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
            // 配置保存
            SaveSetting();
            if (Configs.exitWarn && MessageBoxResult.Cancel == MessageBox.Show("确认退出?", "警告", MessageBoxButton.OKCancel)) {
                e.Cancel = true;
            } else {
                e.Cancel = false;
            }

        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e) {
            // 回收相关资源
            Configs.Dispose();
            AudioUtils.Dispose();
            XStart.DataBase.SqLiteFactory.CloseAllSqLite();
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
            XStart.Bean.Type type = XStartService.TypeDic[typeSection];
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
            AudioUtils.PlayWav(AudioUtils.CHANGE);
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
                    KeyValuePair<string, XStart.Bean.Type> type = (KeyValuePair<string, XStart.Bean.Type>)MainTabControl.SelectedItem;
                    // 记录下当前打开的类别（关闭时判断如果和配置中打开不一样，则保存配置）
                    Configs.openType = type.Value.Section;
                    // 如果当前类别有口令，并且没有记住口令则展示为锁定
                    if (!type.Value.Locked && !type.Value.RememberSecurity && type.Value.HasPassword) {
                        type.Value.Locked = true;
                    }
                }

                AudioUtils.PlayWav(AudioUtils.CHANGE);
            }
        }

        /// <summary>
        /// 计算所有类别里的栏目高度
        /// </summary>
        private void CalculateWidthHeight() {
            // 重新计算栏目高度
            foreach (KeyValuePair<string, XStart.Bean.Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                CalculateWidthHeight(type.Value.Section);
            }
        }

        /// <summary>
        /// 计算指定的类别栏目高度
        /// </summary>
        /// <param name="typeSection">类别Section</param>
        private void CalculateWidthHeight(string typeSection) {
            XStart.Bean.Type type = XStartService.TypeDic[typeSection];
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
                column.ProjectWidth = (int)MainTabControl.ActualWidth - mainViewModel.TypeWidth - 40;
            } else {
                column.ProjectWidth = (int)MainTabControl.ActualWidth - mainViewModel.TypeWidth - 22;
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
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddType(object sender, RoutedEventArgs e) {
            ProjectTypeVM typeVm = new ProjectTypeVM {
                Title = "添加类别"
            };
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow(typeVm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateType(projectTypeWindow);
        }

        private void ClearType(object sender, RoutedEventArgs e) {
            if (XStartService.TypeDic.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show("确认清空所有类别？", "警告", MessageBoxButton.OKCancel)) {
                    foreach (KeyValuePair<string, XStart.Bean.Type> type in mainViewModel.Types) {
                        if (type.Value.Locked) {
                            MessageBox.Show($"[{type.Value.Name}]类别已锁，不可删除！", "错误");
                            return;
                        }
                        foreach (KeyValuePair<string, Column> k in type.Value.ColumnDic) {
                            if (k.Value.Locked) {
                                MessageBox.Show($"类别[{type.Value.Name}]下栏目[{k.Value.Name}]已锁，不可删除！", "错误");
                                return;
                            }
                        }
                        RemoveType(type.Value.Section);
                    }
                    NotifyUtils.ShowNotification("清空完成！");
                }
            } else {
                MessageBox.Show("当前无需清空！", "提示");
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

        private void UpdateType(object sender, RoutedEventArgs e) {
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
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow(typeVm) { WindowStartupLocation = WindowStartupLocation.CenterScreen };
            OperateType(projectTypeWindow);
        }

        private void DeleteType(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            if (MessageBoxResult.OK == MessageBox.Show("确认删除该类别？", "提示", MessageBoxButton.OKCancel)) {
                if (XStartService.TypeDic[section].Locked) {
                    MessageBox.Show("当前类别已锁，不可删除！", "错误");
                    return;
                }
                foreach (KeyValuePair<string, Column> k in XStartService.TypeDic[section].ColumnDic) {
                    if (k.Value.Locked) {
                        MessageBox.Show($"当前类别栏目[{k.Value.Name}]已锁，不可删除！", "错误");
                        return;
                    }
                }
                RemoveTypeData(section);
                DelCount(typeService);
                NotifyUtils.ShowNotification("类别删除成功");
            }
        }

        private void OperateType(ProjectTypeWindow projectTypeWindow) {
            if (true == projectTypeWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(projectTypeWindow.vm.Section)) {
                    XStart.Bean.Type projectType = new XStart.Bean.Type {
                        Name = projectTypeWindow.vm.Name
                        , FaIcon = projectTypeWindow.vm.SelectedFa
                        , FaIconColor = projectTypeWindow.vm.SelectedIconColor
                        , FaIconFontFamily = projectTypeWindow.vm.SelectedFf.ToString()
                    };
                    projectType.Section = Guid.NewGuid().ToString();
                    projectType.Sort = XStartService.TypeDic[XStartService.TypeDic.Count - 1].Sort + 1;
                    typeService.Insert(projectType);
                    XStartService.TypeDic.Add(projectType.Section, projectType);
                    NotifyUtils.ShowNotification("新增类别成功！");
                } else {
                    XStart.Bean.Type projectType = XStartService.TypeDic[projectTypeWindow.vm.Section];
                    projectType.Name = projectTypeWindow.vm.Name;
                    projectType.FaIcon = projectTypeWindow.vm.SelectedFa;
                    projectType.FaIconColor = projectTypeWindow.vm.SelectedIconColor;
                    projectType.FaIconFontFamily = projectTypeWindow.vm.SelectedFf.ToString();
                    typeService.Update(projectType);
                    NotifyUtils.ShowNotification("修改类别成功！");
                }
            }
            projectTypeWindow.Close();
        }

        private void AddTypeSecurity(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            AddSecurity(XStartService.TypeDic[section]);
        }
        private void UpdateTypeSecurity(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            XStart.Bean.Type type = XStartService.TypeDic[section];
            UpdateSecurity(type);
        }
        private void RemoveTypeSecurity(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            XStart.Bean.Type type = XStartService.TypeDic[section];
            RemoveSecurity(type);
        }
        // 类别锁定
        private void LockType(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string section = typePanel.Tag as string;
            XStart.Bean.Type type = XStartService.TypeDic[section];
            type.Locked = true;
        }
        // 解锁类别
        private void UnlockType(object sender, RoutedEventArgs e) {
            Button unlockButton = sender as Button;
            string typeSection = unlockButton.Tag as string;
            XStart.Bean.Type type = XStartService.TypeDic[typeSection];
            // 比较密码是否匹配
            if (type.Password.Equals(type.UnlockSecurity)) {
                // 解锁当前类别窗口
                type.Locked = false;
                type.Unlocked = true;
                type.UnlockSecurity = string.Empty;
            } else {
                MessageBox.Show("口令不匹配", "错误");
                type.UnlockSecurity = string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddColumn(object sender, RoutedEventArgs e) {
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

        private void UpdateColumn(object sender, RoutedEventArgs e) {
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
        private void DeleteColumn(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            if (MessageBoxResult.OK == MessageBox.Show("确认删除栏目:" + column.Name, "警告", MessageBoxButton.OKCancel)) {
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

        private void AddColumnSecurity(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            // 添加口令
            AddSecurity(column);
        }

        // 修改口令
        private void UpdateColumnSecurity(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;

            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            UpdateSecurity(column);
        }
        private void RemoveColumnSecurity(object sender, RoutedEventArgs e) {
            // 移除口令
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            RemoveSecurity(column);
        }
        // 栏目锁定
        private void LockColumn(object sender, RoutedEventArgs e) {
            Expander curExpander = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Expander;
            string columnSection = curExpander.GetValue(ElementParamHelper.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParamHelper.TypeSectionProperty) as string;
            Column column = XStartService.TypeDic[typeSection].ColumnDic[columnSection];
            column.Locked = true;
        }
        // 解锁栏目
        private void UnlockColumn(object sender, RoutedEventArgs e) {
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
                MessageBox.Show("口令不匹配", "错误");
                column.UnlockSecurity = string.Empty;
            }
        }

        private void AddSecurity<T>(T t) where T : TableData {
            // 添加口令
            AddSecurityWindow window = new AddSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "添加口令", Section = t.Section, Kind = Constants.TYPE, Operate = Constants.OPERATE_CREATE }
            };
            if (true == window.ShowDialog()) {
                // 保存口令
                T newT = Activator.CreateInstance<T>();
                newT.Section = t.Section;
                newT.Password = window.VM.Security;
                new TableService<T>().Update(newT);
                // 更新数据
                t.Password = window.VM.Security;
                t.HasPassword = true;
                t.Locked = true;
                t.RememberSecurity = false;
                NotifyUtils.ShowNotification("口令添加成功！");
            }
        }
        private void UpdateSecurity<T>(T t) where T : TableData {
            UpdateSecurityWindow window = new UpdateSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "修改口令", Section = t.Section, CurSecurity = t.Password, Kind = Constants.TYPE, Operate = Constants.OPERATE_UPDATE }
            };
            if (true == window.ShowDialog()) {
                T newT = Activator.CreateInstance<T>();
                newT.Section = t.Section;
                newT.Password = window.VM.Security;
                // 保存口令
                new TableService<T>().Update(newT);
                // 更新数据
                t.Password = window.VM.Security;
                t.HasPassword = true;
                t.Locked = true;
                NotifyUtils.ShowNotification("口令修改成功！");
            }
        }

        private void RemoveSecurity<T>(T t) where T : TableData {
            RemoveSecurityWindow window = new RemoveSecurityWindow() {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                VM = new SecurityVM() { Title = "移除口令", Section = t.Section, CurSecurity = t.Password, Kind = Constants.TYPE, Operate = Constants.OPERATE_REMOVE }
            };
            if (true == window.ShowDialog()) {
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
        }


        // 移除类别（左侧TreeMenu,TabControl的UIPage,TypeDic中数据，Ini文件）
        private void RemoveTypeData(string section) {
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

        private void ProjectButton_Click(object sender, RoutedEventArgs e) {

        }

        private void DelCount<T>(TableService<T> t) where T : TableData {
            if (Configs.delCount > Constants.DEL_COUNT_LIMIT) {
                Configs.delCount = 0;
                t.Vacuum();
            } else {
                Configs.delCount += 1;
            }
            XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_CONFIG, Constants.KEY_DEL_COUNT, Convert.ToString(Configs.delCount));

        }

        private void ToggleTabItem(object sender, RoutedEventArgs e) {
            if (mainViewModel.TypeTabExpanded) {
                // 类别是展开的
                mainViewModel.TypeWidth = 28;
                mainViewModel.TypeTabExpanded = false;
                mainViewModel.TypeTabToggleIcon = FontAwesome6.Indent;
            } else {
                mainViewModel.TypeWidth = 100;
                mainViewModel.TypeTabExpanded = true;
                mainViewModel.TypeTabToggleIcon = FontAwesome6.Outdent;
            }
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

        /// <summary>
        /// 显示关于窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAbout(object sender, RoutedEventArgs e) {
            NotifyUtils.ShowNotification("X启动2.0版", Colors.Gray, "关于");
        }
        /// <summary>
        /// 保存当前窗口尺寸配置
        /// </summary>
        public void SaveFormSize() {
            double height = Height;
            double width = Width;
            XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_HEIGHT, Convert.ToString(height));
            XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_SIZE, Constants.KEY_WIDTH, Convert.ToString(width));
        }

        /// <summary>
        /// 保存当前窗口位置
        /// </summary>
        public void SaveFormLocation() {
            // 保存当前位置
            if (0 == Configs.mainLeft || mainViewModel.MainLeft != Configs.mainLeft) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_LEFT, Convert.ToString(mainViewModel.MainLeft));
            }
            if (0 == Configs.mainTop || mainViewModel.MainTop != Configs.mainTop) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_LOCATION, Constants.KEY_TOP, Convert.ToString(mainViewModel.MainTop));
            }
        }
        //计时执行的程序
        private void CurrentTimer_Tick(object sender, EventArgs e) {
            mainViewModel.CurrentTime = DateTime.Now.ToString("T");
        }

        private void SaveSetting() {
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_TOP_MOST, ref Configs.topMost, mainViewModel.TopMost);// 置顶
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_CLICK_TYPE, ref Configs.clickType, mainViewModel.ClickType);// 点击方式
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_AUDIO, ref Configs.audio, mainViewModel.Audio);// 音效开关
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_AUTO_RUN, ref Configs.autoRun, mainViewModel.AutoRun);// 自启动
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_EXIT_WARN, ref Configs.exitWarn, mainViewModel.ExitWarn);// 退出警告
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_CLOSE_BORDER_HIDE, ref Configs.closeBorderHide, mainViewModel.CloseBorderHide);// 靠边隐藏
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN, ref Configs.urlOpen, mainViewModel.UrlOpen);// 浏览器打开链接
            SaveConfig(Constants.SECTION_CONFIG, Constants.KEY_URL_OPEN_CUSTOM_BROWSER, ref Configs.urlOpenCustomBrowser, mainViewModel.UrlOpenCustomBrowser);// 自定义浏览器
        }

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
            }else if(from is uint fromUint && to is uint toUint) {
                if (fromUint != toUint) {
                    isChange = true;
                }
            }
            if (isChange) {
                XStartIniUtils.IniWriteValue(Constants.SET_FILE, section, key, Convert.ToString(to));
                from = to;
            }
        }
    }
}
