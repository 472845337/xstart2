using XStart2._0.Bean;

namespace XStart.Bean {
    /// <summary>
    /// 类别数据对象
    /// </summary>
    [Table("t_type")]
    public class Type : TableData {

        public const string KEY_PASSWORD = "Password";
        public const string KEY_OPEN_COLUMN = "OpenColumn";
        // 加密
        [TableParam("password", "VARCHAR")]
        public string Password { get; set; }
        // 该类别初始展开的栏目
        [TableParam("open_column", "VARCHAR")]
        public string OpenColumn { get; set; }
        // 图标
        private string faIcon;
        [TableParam("fa_icon", "VARCHAR")]
        public string FaIcon { get => faIcon; set { faIcon = value; OnPropertyChanged("FaIcon"); } }
        // 图标颜色
        private string faIconColor;
        [TableParam("fa_icon_color", "VARCHAR")]
        public string FaIconColor { get => faIconColor; set { faIconColor = value; OnPropertyChanged("FaIconColor"); } }
        // 图标所属字体
        private string faIconFontFamily;
        [TableParam("fa_icon_font_family", "VARCHAR")]
        public string FaIconFontFamily { get => faIconFontFamily; set { faIconFontFamily = value; OnPropertyChanged("FaIconFontFamily"); } }


        #region 窗口中使用的属性
        // 是否有密码
        private bool hasPassword;
        public bool HasPassword { get => hasPassword; set { hasPassword = value; OnPropertyChanged("HasPassword"); SetCanLock(); } }
        // 用于判定是否锁定状态，展示锁定页面
        private bool locked;
        public bool Locked { get => locked; set { locked = value; OnPropertyChanged("Locked"); SetUnlocked(); SetCanLock(); } }
        // 是否非锁定状态，展示内容页面,则Locked控制
        private bool unlocked;
        public bool Unlocked { get => unlocked; set { unlocked = value; OnPropertyChanged("Unlocked"); } }
        // 是否可锁, HasPassword && !Locked
        private bool canLock;
        public bool CanLock { get => canLock; set { canLock = value; OnPropertyChanged("CanLock"); } }
        // 解锁口令
        private string unlockSecurity;
        public string UnlockSecurity { get => unlockSecurity; set { unlockSecurity = value; OnPropertyChanged("UnlockSecurity"); } }
        // 记住口令
        private bool rememberSecurity;
        public bool RememberSecurity { get => rememberSecurity; set { rememberSecurity = value; OnPropertyChanged("RememberSecurity"); } }
        #endregion

        // 用于计算当前类别里的栏目高度
        public double ExpandedColumnHeight { get; set; }
        // 该类别Tab的内容框中是否展示滚动条
        private System.Windows.Controls.ScrollBarVisibility verticalScroll;
        public System.Windows.Controls.ScrollBarVisibility VerticalScroll { get => verticalScroll; set { verticalScroll = value; OnPropertyChanged("VerticalScroll"); } }
        // 类别的栏目集合
        private ObservableDictionary<string, Column> columnDic = new ObservableDictionary<string, Column>();
        public ObservableDictionary<string, Column> ColumnDic { get => columnDic; set { columnDic = value; OnPropertyChanged("ColumnDic"); } }

        private void SetUnlocked() {
            Unlocked = !Locked;
        }

        private void SetCanLock() {
            CanLock = HasPassword && !Locked;
        }
    }
}
