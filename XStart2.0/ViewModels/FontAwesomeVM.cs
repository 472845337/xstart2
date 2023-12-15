using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class FontAwesomeVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string selectedFa = string.Empty;
        public string SelectedFa { get => selectedFa; set { selectedFa = value; OnPropertyChanged("SelectedFa"); } }
        // 类别Icon颜色
        private string selectedIconColor = "LightSeaGreen";
        public string SelectedIconColor { get => selectedIconColor; set { selectedIconColor = value; OnPropertyChanged("SelectedIconColor"); } }
        // 类别所选FontAwesome字体
        private string selectedFf = "{pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid}";
        public string SelectedFf { get => selectedFf; set { selectedFf = value; OnPropertyChanged("SelectedFf"); } }

        private List<string> customFontAwesomes;

        public List<string> CustomFontAwesomes { get => customFontAwesomes; set { customFontAwesomes = value; OnPropertyChanged("CustomFontAwesomes"); } }

        private List<string> solidFontAwesomes;

        public List<string> SolidFontAwesomes { get => solidFontAwesomes; set { solidFontAwesomes = value;OnPropertyChanged("SolidFontAwesomes"); } }

        private List<string> brandsFontAwesomes;

        public List<string> BrandsFontAwesomes { get => brandsFontAwesomes; set { brandsFontAwesomes = value; OnPropertyChanged("BrandsFontAwesomes"); } }

        private List<string> regularFontAwesomes;

        public List<string> RegularFontAwesomes { get => regularFontAwesomes; set { regularFontAwesomes = value; OnPropertyChanged("RegularFontAwesomes"); } }
    }
}
