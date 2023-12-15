using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using XStart.Bean;
using XStart.Services;
using XStart2._0.Bean;
using XStart2._0.Commands;
using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    internal class MainViewModel : INotifyPropertyChanged {
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService projectService = ProjectService.Instance;
        public bool InitFinished { get; set; } = false;
        public MainViewModel() {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region 应用数据
        private ObservableDictionary<string, Type> types;
        public ObservableDictionary<string, Type> Types {
            get => types;
            set { types = value; OnPropertyChanged("Types"); }
        }
        #endregion

        #region 窗口相关
        // 类别宽度（TabControl的TabItem的宽度）
        private int typeWidth;
        public int TypeWidth { get => typeWidth; set { typeWidth = value; OnPropertyChanged("TypeWidth"); } }

        private bool typeTabExpanded;
        public bool TypeTabExpanded { get => typeTabExpanded; set { typeTabExpanded = value; OnPropertyChanged("ByteTabExpanded"); } }

        private string typeTabToggleIcon;
        public string TypeTabToggleIcon { get => typeTabToggleIcon; set { typeTabToggleIcon = value; OnPropertyChanged("TypeTabToggleIcon"); } }
        #endregion
        private int selectedIndex;
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged("SelectedIndex"); } }
        private double tabControlActualHeight;
        public double TabControlActualHeight { get => tabControlActualHeight; set { tabControlActualHeight = value; OnPropertyChanged("TabControlActualHeight"); } }

        public ICommand MainWindowLoadedCommand => new RelayCommand(WindowLoaded, RelayCommand.DefaultChangeFunc);

        /// <summary>
        /// 主窗口加载
        /// </summary>
        /// <param name="param"></param>
        private void WindowLoaded(object param) {
            #region 加载数据
            // 加载类别配置
            List<Type> types = typeService.SelectList(new Type { OrderBy = "sort" });
            foreach (Type type in types) {
                if (string.IsNullOrEmpty(type.Name)) {
                    typeService.Delete(type.Section);
                    continue;
                }
                type.Locked = !string.IsNullOrEmpty(type.Password);
                type.HasPassword = !string.IsNullOrEmpty(type.Password);
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
            LinkedHashMap<string, Project> autoRunApp = new LinkedHashMap<string, Project>();
            CalculateWidthHeight();
            // 面板初始化
            foreach (KeyValuePair<string, Type> type in XStartService.TypeDic) {
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
            TypeTabExpanded = true;
            TypeTabToggleIcon = FontAwesome6.Outdent;
            TypeWidth = 100;
            Types = XStartService.TypeDic;
            SelectedIndex = 0;
            InitFinished = true;
        }

        /// <summary>
        /// 计算所有类别里的栏目高度
        /// </summary>
        private void CalculateWidthHeight() {
            // 重新计算栏目高度
            foreach (KeyValuePair<string, Type> type in XStartService.TypeDic) {
                // 计算栏目高度
                CalculateWidthHeight(type.Value.Section);
            }
        }

        /// <summary>
        /// 计算指定的类别栏目高度
        /// </summary>
        /// <param name="typeSection">类别Section</param>
        private void CalculateWidthHeight(string typeSection) {
            Type type = XStartService.TypeDic[typeSection];
            // 计算栏目高度
            int headerHeight = 38;
            // 当高度为小于一定高度時，将高度置为-1
            double expandedHeight = TabControlActualHeight - headerHeight * type.ColumnDic.Count - 6;
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

    }
}
