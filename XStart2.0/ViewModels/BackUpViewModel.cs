using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;
using XStart2._0.Services;
using XStart2._0.View;

namespace XStart2._0.ViewModels {
    class BackUpViewModel : BaseViewModel {

        public ObservableCollection<CheckBoxTreeViewModel> Items {
            get; set;
        }

        public void InitData() {
            Items = new ObservableCollection<CheckBoxTreeViewModel>();
            // 加载项目树
            foreach (KeyValuePair<string, Bean.Type> typeKV in XStartService.TypeDic) {
                CheckBoxTreeViewModel typeTreeNode = new CheckBoxTreeViewModel { Section = typeKV.Value.Section, Header = typeKV.Value.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true };
                foreach (KeyValuePair<string, Column> columnKV in typeKV.Value.ColumnDic) {
                    CheckBoxTreeViewModel columnNode = new CheckBoxTreeViewModel() { Section = columnKV.Value.Section, Header = columnKV.Value.Name, Children = new List<CheckBoxTreeViewModel>(), IsChecked = true };
                    typeTreeNode.Children.Add(columnNode);
                    foreach (KeyValuePair<string, Project> projectKV in columnKV.Value.ProjectDic) {
                        CheckBoxTreeViewModel appNode = new CheckBoxTreeViewModel { Section = projectKV.Value.Section, Header = projectKV.Value.Name, IsChecked = true, Data = projectKV.Value };
                        columnNode.Children.Add(appNode);
                    }
                }
                Items.Add(typeTreeNode);
            }
        }
    }
}
