using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart.Bean {
    /// <summary>
    /// 栏目数据
    /// </summary>
    [Table("t_column")]
    public class Column : TableData, INotifyPropertyChanged {

        public const string KEY_TYPE_SECTION = "TypeSection";
        public const string KEY_PASSWORD = "Password";
        [TableParam("type_section", "VARCHAR")]
        public string TypeSection { get; set; }
        [TableParam("password", "VARCHAR")]
        public string Password { get; set; }
        [TableParam("start_open", "BIT")]
        public bool? StartOpen { get; set; }
        public bool SaveSecurity { get; set; }

        private int columnHeight;
        public int ColumnHeight { get => columnHeight; set { columnHeight = value;OnPropertyChanged("ColumnHeight"); } }

        private bool locked;
        public bool Locked { get => locked; set { locked = value; OnPropertyChanged("Locked"); } }

        private bool isExpanded;
        public bool IsExpanded { get => isExpanded; set { isExpanded = value; OnPropertyChanged("IsExpanded"); } }
        private LinkedHashMap<string, Project> projectDic = new LinkedHashMap<string, Project>();
        public LinkedHashMap<string, Project> ProjectDic { get => projectDic; set { projectDic = value; OnPropertyChanged("ProjectDic"); } }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
