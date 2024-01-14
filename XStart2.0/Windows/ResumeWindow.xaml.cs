using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;
using XStart2._0.Bean;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// BackUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ResumeWindow : Window {
        private readonly ResumeViewModel vm = new ResumeViewModel();
        // 数据服务
        public TableService<Bean.Type> typeService = ServiceFactory.GetTypeService();
        public TableService<Column> columnService = ServiceFactory.GetColumnService();
        public TableService<Project> projectService = ServiceFactory.GetProjectService();
        public ResumeWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }

        /// <summary>
        /// 选择备份文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBackupFile_Click(object sender, RoutedEventArgs e) {
            using System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog() { Filter = "xsb文件|*.xsb" };
            if (System.Windows.Forms.DialogResult.OK == openFileDialog.ShowDialog()) {
                string encryptBackupJson = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                string backupJson = AesUtils.DecryptContent(encryptBackupJson);
                if (string.IsNullOrEmpty(backupJson)) {
                    MessageBox.Show("不支持的备份文件！", "错误");
                } else {
                    BackData backData = JsonConvert.DeserializeObject<BackData>(backupJson);
                    if (null == backData.Types || backData.Types.Count == 0) {
                        MessageBox.Show("可导入数据为空！", "错误");
                    } else {
                        vm.SelectBackUpFilePath = openFileDialog.FileName;
                        vm.InitVmData(backData.Types);
                    }
                }
            }
        }

        /// <summary>
        /// 恢复数据按钮确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResumeConfirm_Click(object sender, RoutedEventArgs e) {
            bool isOverride = vm.IsOverride;
            if (null == vm.Items || vm.Items.Count == 0) {
                MessageBox.Show("请先选择备份文件!", "提示");
                return;
            }
            foreach (CheckBoxTreeViewModel backTypeVM in vm.Items) {
                BackData.BackType backType = backTypeVM.Data as BackData.BackType;

                foreach (CheckBoxTreeViewModel backColumnVM in backTypeVM.Children) {
                    BackData.BackColumn backColumn = backColumnVM.Data as BackData.BackColumn;

                    foreach (CheckBoxTreeViewModel backProjectVM in backColumnVM.Children) {
                        BackData.BackProject backProject = backProjectVM.Data as BackData.BackProject;

                        if (true == backProjectVM.IsChecked) {
                            Project project = GetByBackProject(backProject);
                            if (!XStartService.TypeDic.ContainsKey(backColumn.TypeSection)) {
                                // 先创建类别
                                Type type = GetByBackType(backType);
                                typeService.Insert(type);
                                XStartService.TypeDic.Add(type.Section, type);
                            }
                            if (!XStartService.TypeDic[backColumn.TypeSection].ColumnDic.ContainsKey(backColumn.Section)) {
                                // 先创建栏目
                                Column column = GetByBackColumn(backColumn);
                                columnService.Insert(column);
                                XStartService.TypeDic[column.TypeSection].ColumnDic.Add(column.Section, column);
                            }
                            if (XStartService.TypeDic[backColumn.TypeSection].ColumnDic[backColumn.Section].ProjectDic.ContainsKey(backProject.Section)) {
                                if (isOverride) {
                                    // 覆盖
                                    projectService.Update(project);
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Name = project.Name;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Sort = project.Sort;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].IconPath = project.IconPath;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].IconIndex = project.IconIndex;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Kind = project.Kind;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Path = project.Path;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].FontColor = project.FontColor;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Arguments = project.Arguments;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].RunStartPath = project.RunStartPath;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].HotKey = project.HotKey;
                                    XStartService.TypeDic[project.TypeSection].ColumnDic[project.Section].ProjectDic[project.Section].Remark = project.Remark;
                                }
                            } else {
                                // 新增
                                projectService.Insert(project);
                                XStartService.TypeDic[backColumn.TypeSection].ColumnDic[backColumn.Section].ProjectDic.Add(project.Section, project);
                            }
                        }
                    }
                    if (true == backColumnVM.IsChecked) {
                        Column column = GetByBackColumn(backColumn);
                        if (!XStartService.TypeDic.ContainsKey(backColumn.TypeSection)) {
                            // 先创建类别
                            Type type = GetByBackType(backType);
                            typeService.Insert(type);
                            XStartService.TypeDic.Add(type.Section, type);
                        }
                        if (XStartService.TypeDic[backColumn.TypeSection].ColumnDic.ContainsKey(backColumn.Section)) {
                            if (isOverride) {
                                // 覆盖
                                columnService.Update(column);
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].Name = column.Name;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].Sort = column.Sort;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].Password = column.Password;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].Orientation = column.Orientation;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].IconSize = column.IconSize;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].HideTitle = column.HideTitle;
                                XStartService.TypeDic[column.TypeSection].ColumnDic[column.Section].OneLineMulti = column.OneLineMulti;
                            }
                        } else {
                            // 新增
                            columnService.Insert(column);
                            XStartService.TypeDic[column.TypeSection].ColumnDic.Add(column.Section, column);
                        }
                    }
                }
                if (true == backTypeVM.IsChecked) {
                    Type type = GetByBackType(backType);
                    if ( XStartService.TypeDic.ContainsKey(backType.Section)) {
                        if (isOverride) {
                            // 覆盖
                            typeService.Update(type);
                            XStartService.TypeDic[type.Section].Name = type.Name;
                            XStartService.TypeDic[type.Section].Sort = type.Sort;
                            XStartService.TypeDic[type.Section].FaIcon = type.FaIcon;
                            XStartService.TypeDic[type.Section].FaIconColor = type.FaIconColor;
                            XStartService.TypeDic[type.Section].FaIconFontFamily = type.FaIconFontFamily;
                            XStartService.TypeDic[type.Section].Password = type.Password;
                        }
                    } else {
                        // 新增
                        typeService.Insert(type);
                        XStartService.TypeDic.Add(type.Section, type);
                    }
                }

            }
            NotifyUtils.ShowNotification("恢复文件写入成功");
            DialogResult = true;
        }

        private void ResumeCancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private Type GetByBackType(BackData.BackType backType) {
            return new Type { Section = backType.Section, Name = backType.Name, Sort = backType.Sort, FaIcon = backType.FaIcon, FaIconColor = backType.FaIconColor, FaIconFontFamily = backType.FaIconFontFamily, Password = backType.Password, NewAdd = true };
        }

        private Column GetByBackColumn(BackData.BackColumn backColumn) {
            return new Column {
                TypeSection = backColumn.TypeSection, Section = backColumn.Section, Name = backColumn.Name
                                    , Sort = backColumn.Sort, Password = backColumn.Password, Orientation = backColumn.Orientation, OneLineMulti = backColumn.OneLineMulti
                                    , IconSize = backColumn.IconSize, HideTitle = backColumn.HideTitle
            };
        }
        private Project GetByBackProject(BackData.BackProject backProject) {
            return new Project {
                TypeSection = backProject.TypeSection, ColumnSection = backProject.ColumnSection
                           , Section = backProject.Section, Name = backProject.Name, Sort = backProject.Sort, IconPath = backProject.IconPath
                           , IconIndex = backProject.IconIndex, Kind = backProject.Kind, Path = backProject.Path, FontColor = backProject.FontColor
                           , Arguments = backProject.Arguments, RunStartPath = backProject.RunStartPath, HotKey = backProject.HotKey, Remark = backProject.Remark
            };
        }
    }
}
