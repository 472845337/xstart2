using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean;
using XStart2._0.Commands;
using XStart2._0.Services;

namespace XStart2._0.ViewModel {
    /// <summary>
    /// ��ѯӦ��
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class QueryViewModel {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public QueryViewModel() {
            QueryCommand = new RelayCommand(() => {
                QueryProjects.Clear();
                // ��ѯ��Ŀ
                foreach (KeyValuePair<string, Type> type in XStartService.TypeDic) {
                    if (type.Value.Locked) {
                        continue;
                    }
                    foreach (KeyValuePair<string, Column> column in type.Value.ColumnDic) {
                        if (column.Value.Locked) {
                            continue;
                        }
                        foreach (KeyValuePair<string, Project> project in column.Value.ProjectDic) {
                            // ���ƣ�·������ע����
                            if ((!string.IsNullOrEmpty(project.Value.Name) && project.Value.Name.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.Path) && project.Value.Path.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.Remark) && project.Value.Remark.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.Arguments) && project.Value.Arguments.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)
                            || (!string.IsNullOrEmpty(project.Value.RunStartPath) && project.Value.RunStartPath.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) > -1)) {
                                QueryProjects.Add(project.Value);
                            }
                        }
                    }
                }
                QueryResult = "��ѯ��" + QueryProjects.Count + "���";
                if (QueryProjects.Count == 0) {
                    QueryProjects.Add(new Project() { Name = "�޲�ѯ���" });
                }
            }, CanExecute);
        }

        public string RdpModel { get; set; }

        private string query;
        public string Query { get { return query; } set { query = value; QueryCommand.RaiseCanExecuteChanged(); } }
        public ObservableCollection<Project> QueryProjects { get; set; } = new ObservableCollection<Project>();

        public string QueryResult { get; set; }
        public RelayCommand QueryCommand { get; set; }

        public bool CanExecute() {
            return !string.IsNullOrEmpty(query);
        }
    }
}