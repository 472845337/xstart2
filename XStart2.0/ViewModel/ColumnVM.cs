
namespace XStart2._0.ViewModel {
    public class ColumnVM : BaseViewModel {

        // 窗口的标题
        public string Title { get; set; }
        // section
        public string Section { get; set; }
        public string TypeSection { get; set; }
        public string Name { get; set; }
        public string Orientation { get; set; }
        public bool? HideTitle { get; set; }
        public int? IconSize { get; set; }
        public bool? OneLineMulti { get; set; }
    }
}
