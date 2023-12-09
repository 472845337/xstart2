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
using XStart2._0.Bean;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using XStart2._0.Windows;

namespace XStart2._0 {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService appService = ProjectService.Instance;

        MainViewModel mainViewModel = new MainViewModel();
        public MainWindow() {
            InitializeComponent();
            DataContext = mainViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

            #region 加载数据
            // 加载类别配置
            List<XStart.Bean.Type> types = typeService.SelectList(new XStart.Bean.Type { OrderBy = "sort" });
            foreach (XStart.Bean.Type type in types) {
                if (string.IsNullOrEmpty(type.Name)) {
                    typeService.Delete(type.Section);
                    continue;
                }
                type.Locked = !string.IsNullOrEmpty(type.Password);
                XStartService.TypeDic.Add(type.Section, type);
            }
            // 加载栏目配置
            List<Column> columns = columnService.SelectList(new Column { OrderBy = "sort" });
            foreach (Column column in columns) {
                if (string.IsNullOrEmpty(column.Name)) {
                    columnService.Delete(column.Section);
                    continue;
                }
                column.Locked = !string.IsNullOrEmpty(column.Password);
                column.IsExpanded = false;
                XStartService.TypeDic[column.TypeSection].ColumnDic.Add(column.Section, column);
            }
            // 加载应用配置
            List<Project> projects = appService.SelectList(new Project { OrderBy = "sort" });

            foreach (Project project in projects) {
                XStartService.TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic.Add(project.Section, project);
            }
            #endregion
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
            mainViewModel.TypeTabToggleIcon = FaIcons.Outdent;
            mainViewModel.TypeWidth = 100;
            mainViewModel.Types = XStartService.TypeDic;
            MainTabControl.SelectedIndex = 0;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
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
            string columnSection = curExpander.GetValue(ElementParam.ColumnSectionProperty) as string;
            string typeSection = curExpander.GetValue(ElementParam.TypeSectionProperty) as string;
            XStart.Bean.Type type = XStartService.TypeDic[typeSection];

            foreach (Column c in type.ColumnDic.Values) {
                if (!columnSection.Equals(c.Section) && c.IsExpanded) {
                    c.IsExpanded = false;
                    c.ColumnHeight = 0;
                } else if (columnSection.Equals(c.Section)) {
                    c.ColumnHeight = (int)type.ExpandedColumnHeight;
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
            // 重新计算按钮宽度以及栏目高度
            CalculateWidthHeight();
        }
        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            AudioUtils.PlayWav(AudioUtils.CHANGE);
        }

        private void CalculateWidthHeight() {
            // 重新计算栏目高度
            foreach (KeyValuePair<string, XStart.Bean.Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                int headerHeight = 38;
                // 当高度为小于一定高度時，将高度置为-1
                double expandedHeight = MainTabControl.ActualHeight - headerHeight * type.Value.ColumnDic.Count - 6;
                if (expandedHeight < 100) {
                    expandedHeight = -1D;
                    // 滚动条应该出现
                    type.Value.VerticalScroll = ScrollBarVisibility.Visible;
                } else {
                    type.Value.VerticalScroll = ScrollBarVisibility.Hidden;
                }
                type.Value.ExpandedColumnHeight = expandedHeight;
                foreach (Column column in type.Value.ColumnDic.Values) {
                    if (column.IsExpanded) {
                        column.ColumnHeight = (int)expandedHeight;
                    }
                    // 栏目里项目的宽度 宽度根据ScrollView的ScrollChanged事件触发
                    //ComputedColumnProjectWidth(column);
                }
            }
        }

        private void ComputedColumnProjectWidth(Column column) {
            if(Visibility.Visible == column.VerticalScrollBar || ScrollBarVisibility.Visible == XStartService.TypeDic[column.TypeSection].VerticalScroll) {
                column.ProjectWidth = (int)MainTabControl.ActualWidth - mainViewModel.TypeWidth - 40;
            } else {
                column.ProjectWidth = (int)MainTabControl.ActualWidth - mainViewModel.TypeWidth-22;
            }
        }

        private void Open_Setting(object sender, RoutedEventArgs e) {
            SettingWindow settingWindow = new SettingWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen};
            settingWindow.ShowDialog();
        }
        /// <summary>
        /// 添加类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddType(object sender, RoutedEventArgs e) {
            ProjectTypeVM typeVm = new ProjectTypeVM();
            typeVm.Title = "添加类别";
            ProjectTypeWindow projectTypeWindow = new ProjectTypeWindow(typeVm) { WindowStartupLocation = WindowStartupLocation.CenterScreen};
            OperateType(projectTypeWindow);
        }

        private void UpdateType(object sender, RoutedEventArgs e) {
            Panel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as Panel;
            string typeSection = typePanel.Tag as string;
            ProjectTypeVM typeVm = new ProjectTypeVM() { Section = typeSection, Name = XStartService.TypeDic[typeSection].Name
                , SelectedFa = XStartService.TypeDic[typeSection].FaIcon};
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

        private void OperateType(ProjectTypeWindow projectTypeWindow) {
            if (true == projectTypeWindow.ShowDialog()) {
                // 保存数据到类别库中
                if (string.IsNullOrEmpty(projectTypeWindow.vm.Section)) {
                    XStart.Bean.Type projectType = new XStart.Bean.Type { Name = projectTypeWindow.vm.Name
                        , FaIcon = projectTypeWindow.vm.SelectedFa
                        , FaIconColor = projectTypeWindow.vm.SelectedIconColor
                        , FaIconFontFamily = projectTypeWindow.vm.SelectedFf.ToString()
                    };
                    projectType.Section = Guid.NewGuid().ToString();
                    projectType.Sort = XStartService.TypeDic[XStartService.TypeDic.Count - 1].Sort + 1;
                    typeService.Insert(projectType);
                    XStartService.TypeDic.Add(projectType.Section, projectType);
                } else {
                    XStart.Bean.Type projectType = XStartService.TypeDic[projectTypeWindow.vm.Section];
                    projectType.Name = projectTypeWindow.vm.Name; 
                    projectType.FaIcon = projectTypeWindow.vm.SelectedFa;
                    projectType.FaIconColor = projectTypeWindow.vm.SelectedIconColor;
                    projectType.FaIconFontFamily = projectTypeWindow.vm.SelectedFf.ToString();
                    typeService.Update(projectType);
                }
            }
            projectTypeWindow.Close();
        }

        private void DeleteType(object sender, RoutedEventArgs e) {
            StackPanel typePanel = ContextMenuService.GetPlacementTarget(LogicalTreeHelper.GetParent(sender as MenuItem)) as StackPanel;
            string section = typePanel.Tag as string;
            if (MessageBoxResult.OK == MessageBox.Show("确认删除该类别？", "提示", MessageBoxButton.OKCancel)) {
                if (XStartService.TypeDic[section].Locked) {
                    MessageBox.Show("当前类别已锁，不可删除！","错误");
                    return;
                }
                foreach (KeyValuePair<string, Column> k in XStartService.TypeDic[section].ColumnDic) {
                    if (k.Value.Locked) {
                        MessageBox.Show($"当前类别栏目[{k.Value.Name}]已锁，不可删除！","错误");
                        return;
                    }
                }
                RemoveType(section);
                DelCount(typeService);
                
                MessageBox.Show("类别删除成功");
            }
        }

        // 移除类别（左侧TreeMenu,TabControl的UIPage,TypeDic中数据，Ini文件）
        private void RemoveType(string section) {
            // 数据删除
            typeService.Delete(section);
            foreach (KeyValuePair<string, Column> column in XStartService.TypeDic[section].ColumnDic) {
                columnService.Delete(column.Value.Section);
                foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                    appService.Delete(project.Value.Section);
                }
            }
            XStartService.TypeDic.Remove(section);
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
                mainViewModel.TypeTabToggleIcon = FaIcons.Indent;
            } else {
                mainViewModel.TypeWidth =100;
                mainViewModel.TypeTabExpanded = true;
                mainViewModel.TypeTabToggleIcon = FaIcons.Outdent;
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
    }
}
