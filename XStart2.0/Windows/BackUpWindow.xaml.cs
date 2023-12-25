﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using XStart.Bean;
using XStart.Services;
using XStart.Utils;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// BackUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BackUpWindow : Window {
        readonly BackUpViewModel vm = new BackUpViewModel();
        public BackUpWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.InitData();
        }

        private void BackUpConfirm_Click(object sender, RoutedEventArgs e) {
            BackData backData = new BackData();
            // 将选择备份的数据写入对象
            foreach (CheckBoxTreeViewModel typeNode in vm.Items) {
                string typeSection = typeNode.Section;
                XStart.Bean.Type dataType = XStartService.TypeDic[typeSection];
                if (typeNode.Children.Count > 0) {
                    foreach (CheckBoxTreeViewModel columnNode in typeNode.Children) {
                        string columnSection = columnNode.Section;
                        Column dataColumn = dataType.ColumnDic[columnSection];
                        if (columnNode.Children.Count > 0) {
                            foreach (CheckBoxTreeViewModel appNode in columnNode.Children) {
                                string projectSection = appNode.Section;
                                Project project = dataColumn.ProjectDic[projectSection];
                                if (true == appNode.IsChecked) {
                                    BackData.BackType type = this.GetType(typeSection, backData);
                                    if (null == type) {
                                        type = GetByTableType(dataType);
                                        if (null == backData.Types) {
                                            backData.Types = new List<BackData.BackType>();
                                        }
                                        backData.Types.Add(type);
                                    }
                                    BackData.BackColumn column = GetColumn(typeSection, columnSection, backData);
                                    if (null == column) {
                                        column = GetByTableColumn(dataColumn);
                                        if (null == type.Columns) {
                                            type.Columns = new List<BackData.BackColumn>();
                                        }
                                        type.Columns.Add(column);
                                    }
                                    if (null == column.Projects) {
                                        column.Projects = new List<BackData.BackProject>();
                                    }
                                    column.Projects.Add(GetByTableProject(project));
                                }
                            }
                        } else {
                            if (true == columnNode.IsChecked) {
                                BackData.BackType type = GetType(typeSection, backData);
                                if (null == type) {
                                    type = GetByTableType(dataType);
                                    if (null == backData.Types) {
                                        backData.Types = new List<BackData.BackType>();
                                    }
                                    backData.Types.Add(type);
                                }
                                BackData.BackColumn column = GetByTableColumn(dataColumn);
                                if (null == type.Columns) {
                                    type.Columns = new List<BackData.BackColumn>();
                                }
                                type.Columns.Add(column);
                            }
                        }
                    }
                } else {
                    BackData.BackType type = GetByTableType(dataType);
                    if (null == backData.Types) {
                        backData.Types = new List<BackData.BackType>();
                    }
                    backData.Types.Add(type);
                }
            }
            if (backData.Types.Count == 0) {
                System.Windows.MessageBox.Show("未选择任何备份数据！","错误");
            } else {
                // 将数据转成Json串
                
                string backupJson = JsonConvert.SerializeObject(backData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string encryptBackupJson = AesUtils.EntryptContent(backupJson);
                // 写入文件
                SaveFileDialog fileDialog = new SaveFileDialog() { Filter = "X启动备份文件|.xsb" };
                if (System.Windows.Forms.DialogResult.OK == fileDialog.ShowDialog()) {
                    File.WriteAllText(fileDialog.FileName, encryptBackupJson, Encoding.UTF8);
                    NotifyUtils.ShowNotification("备份成功！");
                    DialogResult = true;
                }
            }
        }

        private void BackUpCancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private BackData.BackType GetByTableType(Type type) {
            return new BackData.BackType {
                Section = type.Section, Name = type.Name, Sort = type.Sort, Password = type.Password
                , FaIcon = type.FaIcon, FaIconColor = type.FaIconColor, FaIconFontFamily = type.FaIconFontFamily
            };
        }
        private BackData.BackColumn GetByTableColumn(Column column) {
            return new BackData.BackColumn { Section = column.Section, Name = column.Name, Sort = column.Sort, Password = column.Password };
        }
        private BackData.BackProject GetByTableProject(Project project) {
            return new BackData.BackProject {
                Section = project.Section, TypeSection = project.TypeSection, ColumnSection = project.ColumnSection,
                Name = project.Name, Sort = project.Sort, IconIndex = project.IconIndex, Kind = project.Kind, Path = project.Path
                                        , IconPath = project.IconPath, FontColor = project.FontColor, Arguments = project.Arguments, RunStartPath = project.RunStartPath
                                        , HotKey = project.HotKey, Remark = project.Remark, AutoRun = project.AutoRun
            };
        }
        private BackData.BackType GetType(string typeSection, BackData backData) {
            BackData.BackType result = null;
            if (null != backData.Types) {
                foreach (BackData.BackType backType in backData.Types) {
                    if (typeSection.Equals(backType.Section)) {
                        result = backType;
                        break;
                    }
                }
            }
            return result;
        }

        

        private BackData.BackColumn GetColumn(string typeSection, string columnSection, BackData backData) {
            BackData.BackColumn result = null;
            if (null != backData.Types) {
                foreach (BackData.BackType backType in backData.Types) {
                    if (typeSection.Equals(backType.Section)) {
                        if (null != backType.Columns) {
                            foreach (BackData.BackColumn backColumn in backType.Columns) {
                                if (columnSection.Equals(backColumn.Section)) {
                                    result = backColumn;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}