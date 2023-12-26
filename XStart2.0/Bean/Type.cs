
namespace XStart2._0.Bean {
    /// <summary>
    /// 类别数据对象
    /// </summary>
    [Table("t_type")]
    public class Type : TableData {

        public const string KEY_PASSWORD = "Password";
        public const string KEY_OPEN_COLUMN = "OpenColumn";
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

        // 用于计算当前类别里的栏目高度
        public double ExpandedColumnHeight { get; set; }
        // 该类别Tab的内容框中是否展示滚动条
        private System.Windows.Controls.ScrollBarVisibility verticalScroll;
        public System.Windows.Controls.ScrollBarVisibility VerticalScroll { get => verticalScroll; set { verticalScroll = value; OnPropertyChanged("VerticalScroll"); } }
        // 类别的栏目集合
        private ObservableDictionary<string, Column> columnDic = new ObservableDictionary<string, Column>();
        public ObservableDictionary<string, Column> ColumnDic { get => columnDic; set { columnDic = value; OnPropertyChanged("ColumnDic"); } }
       
    }
}
