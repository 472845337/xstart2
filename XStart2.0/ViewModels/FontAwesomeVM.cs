using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class FontAwesomeVM : BaseViewModel {
        // 查询的字体名
        public string QueryFontAwesomeName { get; set; }
        public string QueryFontAwesomeResult { get; set; }
        // 选择的字体
        public string SelectedFa { get; set; }
        // 类别Icon颜色
        public string SelectedIconColor { get; set; } = "LightSeaGreen";
        // 类别所选FontAwesome字体
        public string SelectedFf { get; set; }
        public string SelectedFaName { get; set; }
        public string SelectedFaCode { get; set; }

        public List<FontAwesome> CustomFontAwesomes { get; set; }

        public IEnumerable<FontAwesome> SolidFontAwesomes { get; set; }

        public IEnumerable<FontAwesome> BrandsFontAwesomes { get; set; }

        public IEnumerable<FontAwesome> RegularFontAwesomes { get; set; }
        public IEnumerable<FontAwesome> FontAwesomes4 { get; set; }

        public ObservableCollection<FontAwesome> QueryFontAwesomes { get; set; }

    }
}
