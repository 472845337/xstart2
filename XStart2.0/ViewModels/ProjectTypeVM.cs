using System.Collections.Generic;

namespace XStart2._0.ViewModels {
    public class ProjectTypeVM : BaseViewModel {
        // 窗口的标题
        public string Title { get; set; }
        // 类别的section
        public string Section { get; set; }
        // 类别名称
        public string Name { get; set; }
        // 类别所选FontAwesome Icon
        public string SelectedFa { get; set; }
        // 类别Icon颜色
        public string SelectedIconColor { get; set; } = "LightSeaGreen";
        // 类别所选FontAwesome字体
        public string SelectedFf { get; set; } = "{pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid}";

        public List<string> PopularFas { get; set; }

        public bool MoreButtonEnable { get; set; } = true;
    }
}
