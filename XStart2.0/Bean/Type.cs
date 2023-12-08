using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        [TableParam("open_column", "VARCHAR")]
        public string OpenColumn { get; set; }

        private string faIcon;
        [TableParam("fa_icon", "VARCHAR")]
        public string FaIcon { get=> faIcon; set { faIcon = value;OnPropertyChanged("FaIcon"); } }

        private string faIconColor;
        [TableParam("fa_icon_color", "VARCHAR")]
        public string FaIconColor { get => faIconColor; set { faIconColor = value; OnPropertyChanged("FaIconColor"); } }

        private string faIconFontFamily;
        [TableParam("fa_icon_font_family", "VARCHAR")]
        public string FaIconFontFamily { get => faIconFontFamily; set { faIconFontFamily = value; OnPropertyChanged("FaIconFontFamily"); } }
        // 窗口用的属性，用于判断是否记住口令
        public bool SaveSecurity { get; set; }
        // 窗口用的属性，用于判定是否锁定状态
        public bool Locked { get; set; }
        // 窗口用的属性，用于计算当前类别里的Column高度
        public double ExpandedColumnHeight { get; set; }

        private System.Windows.Controls.ScrollBarVisibility verticalScroll;
        public System.Windows.Controls.ScrollBarVisibility VerticalScroll { get => verticalScroll; set { verticalScroll = value; OnPropertyChanged("VerticalScroll"); } }

        private LinkedHashMap<string, Column> columnDic = new LinkedHashMap<string, Column>();
        public LinkedHashMap<string, Column> ColumnDic { get =>columnDic; set { columnDic = value; OnPropertyChanged("ColumnDic"); }}
    }
}
