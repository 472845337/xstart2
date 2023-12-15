using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class FontAwesomeVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // 查询的字体名
        private string queryFontAwesomeName;
        public string QueryFontAwesomeName { get => queryFontAwesomeName; set { queryFontAwesomeName = value; OnPropertyChanged("QueryFontAwesomeName"); } }
        private string queryFontAwesomeResult;
        public string QueryFontAwesomeResult { get => queryFontAwesomeResult;set { queryFontAwesomeResult = value; OnPropertyChanged("QueryFontAwesomeResult"); } }
        // 选择的字体
        private string selectedFa;
        public string SelectedFa { get => selectedFa; set { selectedFa = value; OnPropertyChanged("SelectedFa"); } }
        // 类别Icon颜色
        private string selectedIconColor = "LightSeaGreen";
        public string SelectedIconColor { get => selectedIconColor; set { selectedIconColor = value; OnPropertyChanged("SelectedIconColor"); } }
        // 类别所选FontAwesome字体
        private string selectedFf = "{pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid}";
        public string SelectedFf { get => selectedFf; set { selectedFf = value; OnPropertyChanged("SelectedFf"); } }

        private List<FontAwesome> customFontAwesomes;

        public List<FontAwesome> CustomFontAwesomes { get => customFontAwesomes; set { customFontAwesomes = value; OnPropertyChanged("CustomFontAwesomes"); } }

        private List<FontAwesome> solidFontAwesomes;

        public List<FontAwesome> SolidFontAwesomes { get => solidFontAwesomes; set { solidFontAwesomes = value; OnPropertyChanged("SolidFontAwesomes"); } }

        private List<FontAwesome> brandsFontAwesomes;

        public List<FontAwesome> BrandsFontAwesomes { get => brandsFontAwesomes; set { brandsFontAwesomes = value; OnPropertyChanged("BrandsFontAwesomes"); } }

        private List<FontAwesome> regularFontAwesomes;

        public List<FontAwesome> RegularFontAwesomes { get => regularFontAwesomes; set { regularFontAwesomes = value; OnPropertyChanged("RegularFontAwesomes"); } }

        private List<FontAwesome> fontAwesomes4;
        public List<FontAwesome> FontAwesomes4 { get => fontAwesomes4; set { fontAwesomes4 = value; OnPropertyChanged("FontAwesomes4"); } }

        private ObservableCollection<FontAwesome> queryFontAwesomes;
        public ObservableCollection<FontAwesome> QueryFontAwesomes { get => queryFontAwesomes; set { queryFontAwesomes = value; OnPropertyChanged("QueryFontAwesomes"); } }

    }
}
