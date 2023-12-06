
using System;

namespace XStart.Bean {
    /// <summary>
    /// 快捷对象公共的属性
    /// </summary>
    /// 
    [Serializable]
    public abstract class TableData {

        public const string KEY_NAME = "Name";
        public const string KEY_SORT = "Sort";
        public const string KEY_ICON_INDEX = "IconIndex";
        // section
        [TableParam(true, "section", "VARCHAR")]
        public string Section { get; set; }
        // 名称
        [TableParam("name", "VARCHAR")]
        public string Name { get; set; }
        // 排序
        [TableParam("sort", "INT")]
        public int? Sort { get; set; }
        // 图标（symbol或exe 多图标的下标）
        [TableParam("icon_index", "INT")]
        public int? IconIndex { get; set; }
        public string OrderBy { get; set; }
    }
}
