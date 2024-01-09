using PropertyChanged;
using System.Windows;

namespace XStart2._0.Bean {
    /// <summary>
    /// 栏目数据
    /// </summary>
    [Table("t_column")]
    public class Column : TableData {

        public const string KEY_TYPE_SECTION = "TypeSection";
        public const string KEY_PASSWORD = "Password";
        [DoNotNotify]
        [TableParam("type_section", "VARCHAR")]
        public string TypeSection { get; set; }
        [DoNotNotify]
        [TableParam("start_open", "BIT")]
        public bool? StartOpen { get; set; }
        // 图标尺寸
        [TableParam("icon_size", "INT")]
        public double? IconSize { get; set; }
        // 是否横排
        [TableParam("Orientation", "VARCHAR")]
        public string Orientation { get; set; }
        // 隐藏标题
        [TableParam("Hide_Title", "INT")]
        public bool? HideTitle { get; set; }
        [DoNotNotify]
        public bool SaveSecurity { get; set; }
        // 垂直滚动条显示状态
        public Visibility VerticalScrollBar { get; set; }
        // 栏目高度
        public int ColumnHeight { get; set; }
        // 栏目是否展开
        public bool IsExpanded { get; set; }
        // 项目宽度
        public int ProjectWidth { get; set; }
        // 栏目中的项目字典
        public ObservableDictionary<string, Project> ProjectDic { get; set; } = new ObservableDictionary<string, Project>();
    }
}
