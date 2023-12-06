using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using XStart.Bean;
using XStart.Services;
using XStart2._0.Bean;
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
            mainViewModel.TypeWidth = 120;
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

        private void CalculateWidthHeight() {
            // 重新计算栏目高度
            foreach (KeyValuePair<string, XStart.Bean.Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                int headerHeight = 38;
                // 当高度为小于50时，将高度置为-1
                double expandedHeight = MainTabControl.ActualHeight - headerHeight * type.Value.ColumnDic.Count - 6;
                if (expandedHeight < 50) {
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
                }
            }
            // 重新计算栏目宽度
            mainViewModel.ProjectWidth = (int)MainTabControl.ActualWidth - mainViewModel.TypeWidth - 22;
        }
        private void Open_Setting(object sender, RoutedEventArgs e) {
            SettingWindow settingWindow = new SettingWindow() { WindowStartupLocation = WindowStartupLocation.CenterOwner};
            settingWindow.ShowDialog();
        }
    }
}
