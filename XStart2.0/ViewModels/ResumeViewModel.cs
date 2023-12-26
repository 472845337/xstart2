using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;
using XStart2._0.View;

namespace XStart2._0.ViewModels {
    class ResumeViewModel : BaseViewModel {

        private string selectBackUpFilePath;
        public string SelectBackUpFilePath { get => selectBackUpFilePath; set { selectBackUpFilePath = value; OnPropertyChanged("SelectBackUpFilePath"); } }

        private bool isOverride = true;
        public bool IsOverride { get => isOverride; set { isOverride = value; OnPropertyChanged("IsOverride"); } }

        private ObservableCollection<CheckBoxTreeViewModel> items;

        public ObservableCollection<CheckBoxTreeViewModel> Items {
            get => items;
            set { items = value; OnPropertyChanged("Items"); }
        }

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
            if(null == Items) {
                Items = new ObservableCollection<CheckBoxTreeViewModel>();
            } else {
                Items.Clear();
            }
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
    }
}
