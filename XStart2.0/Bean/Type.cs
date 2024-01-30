
using PropertyChanged;

namespace XStart2._0.Bean {
    /// <summary>
    /// 类别数据对象
    /// </summary>
    [Table("t_type")]
    public class Type : TableData {

        public const string KEY_PASSWORD = "Password";
        public const string KEY_OPEN_COLUMN = "OpenColumn";
        // 该类别初始展开的栏目
        [DoNotNotify]
        [TableParam("open_column", "VARCHAR")]
        public string OpenColumn { get; set; }
        // 图标
        [TableParam("fa_icon", "VARCHAR")]
        public string FaIcon { get; set; }
        // 图标颜色
        [TableParam("fa_icon_color", "VARCHAR")]
        public string FaIconColor { get; set; }
        // 图标所属字体
        [TableParam("fa_icon_font_family", "VARCHAR")]
        public string FaIconFontFamily { get; set; }

        // 用于计算当前类别里的栏目高度
        [DoNotNotify]
        public double ExpandedColumnHeight { get; set; }
        // 该类别Tab的内容框中是否展示滚动条
        public System.Windows.Controls.ScrollBarVisibility VerticalScroll { get; set; }
        // 类别的栏目集合
        public ObservableDictionary<string, Column> ColumnDic { get; set; } = new ObservableDictionary<string, Column>();
        // 是否新增，用于某些操作完成后，进行处理
        [DoNotNotify]
        public bool NewAdd { get; set; }

    }
}
