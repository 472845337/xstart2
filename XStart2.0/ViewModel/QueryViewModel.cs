using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean;
using XStart2._0.Services;

namespace XStart2._0.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class QueryViewModel : ViewModelBase {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public QueryViewModel() {
            QueryCommand = new RelayCommand(() => {
                QueryProjects.Clear();
                // 查询项目
                foreach (KeyValuePair<string, Type> type in XStartService.TypeDic) {
                    foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                        foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                            // 名称，路径，备注包含
                            if ((!string.IsNullOrEmpty(project.Value.Name) && project.Value.Name.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.Path) && project.Value.Path.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.Remark) && project.Value.Remark.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)) {
                                QueryProjects.Add(project.Value);
                            }
                        }
                    }
                }
                QueryResult = "查询到" + QueryProjects.Count + "结果";
                if (QueryProjects.Count == 0) {
                    QueryProjects.Add(new Project() { Name = "无查询结果" });
                }
            }, CanExecute);
        }

        public string RdpModel { get; set; }

        private string query;
        public string Query { get { return query; } set { query = value; RaisePropertyChanged("Query"); QueryCommand.RaiseCanExecuteChanged(); } }
        public ObservableCollection<Project> QueryProjects { get; set; } = new ObservableCollection<Project>();

        public string QueryResult { get; set; }
        public RelayCommand QueryCommand { get; set; }

        public bool CanExecute() {
            return !string.IsNullOrEmpty(query);
        }
    }
}