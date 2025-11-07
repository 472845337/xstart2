using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Utils;
using XStart2._0.Bean;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    class ResumeViewModel : BaseViewModel {

        [OnChangedMethod(nameof(InitBackupFile))]
        public string SelectBackUpFilePath { get; set; }

        public bool IsOverride { get; set; } = true;

        public ObservableCollection<CheckBoxTreeViewModel> Items { get; set; } = new ObservableCollection<CheckBoxTreeViewModel>();

        public void InitVmData(IEnumerable<Bean.Type> types) {
            Items = new ObservableCollection<CheckBoxTreeViewModel>();
            // 加载项目树
            foreach (Bean.Type type in types) {
                CheckBoxTreeViewModel typeTreeNode = new CheckBoxTreeViewModel { Section = type.Section, Header = type.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true, Data = type };
                foreach (KeyValuePair<string, Column> columnKV in type.ColumnDic) {
                    CheckBoxTreeViewModel columnNode = new CheckBoxTreeViewModel() { Section = columnKV.Value.Section, Header = columnKV.Value.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true, Data = columnKV.Value };
                    typeTreeNode.Children.Add(columnNode);
                    foreach (KeyValuePair<string, Project> projectKV in columnKV.Value.ProjectDic) {
                        CheckBoxTreeViewModel appNode = new CheckBoxTreeViewModel { Section = projectKV.Value.Section, Header = projectKV.Value.Name, IsChecked = true, Data = projectKV.Value };
                        columnNode.Children.Add(appNode);
                    }
                }
                Items.Add(typeTreeNode);
            }
        }

        public void InitVmData(List<BackData.BackType> types) {
            Items.Clear();
            // 加载项目树
            foreach (BackData.BackType type in types) {
                CheckBoxTreeViewModel typeTreeNode = new CheckBoxTreeViewModel { Section = type.Section, Header = type.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true, Data = type };
                foreach (BackData.BackColumn column in type.Columns) {
                    CheckBoxTreeViewModel columnNode = new CheckBoxTreeViewModel() { Section = column.Section, Header = column.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true, Data = column };
                    typeTreeNode.Children.Add(columnNode);
                    foreach (BackData.BackProject project in column.Projects) {
                        CheckBoxTreeViewModel appNode = new CheckBoxTreeViewModel { Section = project.Section, Header = project.Name, IsChecked = true, Data = project };
                        columnNode.Children.Add(appNode);
                    }
                }
                Items.Add(typeTreeNode);
            }
        }

        private void InitBackupFile() {
            try {
                if (string.IsNullOrEmpty(SelectBackUpFilePath) || !SelectBackUpFilePath.EndsWith(".xsb")) {
                    Items.Clear();
                } else {
                    string encryptBackupJson = File.ReadAllText(SelectBackUpFilePath, Encoding.UTF8);
                    string backupJson = AesUtils.DecryptContent(encryptBackupJson);
                    if (string.IsNullOrEmpty(backupJson)) {
                        MsgBoxUtils.ShowError("不支持的导入文件！");
                    } else {
                        BackData backData = JsonConvert.DeserializeObject<BackData>(backupJson);
                        if (null == backData.Types || backData.Types.Count == 0) {
                            MsgBoxUtils.ShowError("可导入数据为空！");
                        } else {
                            InitVmData(backData.Types);
                        }
                    }
                }
            } catch {

            }
        }
    }
}
