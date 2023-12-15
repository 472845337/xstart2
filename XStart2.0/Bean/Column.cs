using System.ComponentModel;
using System.Windows;
using XStart2._0.Bean;

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
        // 是否有密码
        private bool hasPassword;
        public bool HasPassword { get => hasPassword; set { hasPassword = value; OnPropertyChanged("HasPassword"); } }
        // 垂直滚动条显示状态
        private Visibility verticalScrollBar;
        public Visibility VerticalScrollBar { get => verticalScrollBar; set { verticalScrollBar = value; OnPropertyChanged("VerticalScrollBar"); } }
        // 栏目高度
        private int columnHeight;
        public int ColumnHeight { get => columnHeight; set { columnHeight = value; OnPropertyChanged("ColumnHeight"); } }
        // 栏目是否锁定
        private bool locked;
        public bool Locked { get => locked; set { locked = value; OnPropertyChanged("Locked"); } }
        private bool unlocked;
        public bool Unlocked { get => unlocked; set { unlocked = value; OnPropertyChanged("Unlocked"); } }
        // 栏目是否展开
        private bool isExpanded;
        public bool IsExpanded { get => isExpanded; set { isExpanded = value; OnPropertyChanged("IsExpanded"); } }

        private int projectWidth;
        public int ProjectWidth { get => projectWidth; set { projectWidth = value; OnPropertyChanged("ProjectWidth"); } }


        private ObservableDictionary<string, Project> projectDic = new ObservableDictionary<string, Project>();
        public ObservableDictionary<string, Project> ProjectDic { get => projectDic; set { projectDic = value; OnPropertyChanged("ProjectDic"); } }
    }
}
