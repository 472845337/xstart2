using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean;
using XStart2._0.Services;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    class BackUpViewModel : BaseViewModel {

        public ObservableCollection<CheckBoxTreeViewModel> Items { get; set; } = new ObservableCollection<CheckBoxTreeViewModel>();

        public void InitData() {
            // 加载项目树
            foreach (KeyValuePair<string, Type> typeKV in XStartService.TypeDic) {
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
            if(Items.Count == 0) {
                MsgBoxUtils.ShowWarning("当前无可导出项目！");
            }
        }
    }
}
