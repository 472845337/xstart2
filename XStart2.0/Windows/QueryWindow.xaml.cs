using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XStart2._0.Bean;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// QueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QueryWindow : Window {
        readonly QueryViewModel vm = new QueryViewModel();
        public QueryWindow(string rdpModel) {
            InitializeComponent();
            vm.RdpModel = rdpModel;
            DataContext = vm;
        }

        private void QueryProject_PreviewKeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter || e.Key == Key.Return) {
                vm.QueryCommand.Execute(null);
            }
        }

        private void ExecuteProject_MouseUp(object sender, MouseButtonEventArgs e) {
            Grid projectGrid = (Grid)sender;
            Project project = projectGrid.Tag as Project;
            // 因为无查询结果也算是一个项目，所以需要排除
            if (!string.IsNullOrEmpty(project.Path)) {
                ProjectUtils.ExecuteProject(project, vm.RdpModel);
            }
        }
    }
}
