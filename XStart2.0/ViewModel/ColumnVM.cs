
using System.Collections.Generic;

namespace XStart2._0.ViewModel {
    public class ColumnVM : BaseViewModel {

        // 窗口的标题
        public string Title { get; set; }
        // section
        public string Section { get; set; }
        // 栏目类别
        public string TypeSection { get; set; }
        // 修改栏目时的原栏目类别
        public string PriTypeSection { get; set; }
        // 栏目名称
        public string Name { get; set; }
        // 栏目排列方式
        public string Orientation { get; set; }
        // 是否隐藏标题
        public bool? HideTitle { get; set; }
        // 图标尺寸
        public int? IconSize { get; set; }
        // 一行多个
        public bool? OneLineMulti { get; set; }
        // 所有类别集合
        public ICollection<Bean.Type> Types { get; set; }
    }
}
