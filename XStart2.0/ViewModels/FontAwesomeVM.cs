using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    class FontAwesomeVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<FontAwesome> solidFontAwesomes;

        public List<FontAwesome> SolidFontAwesomes { get => solidFontAwesomes; set { solidFontAwesomes = value;OnPropertyChanged("SolidFontAwesomes"); } }

        private List<FontAwesome> brandFontAwesomes;

        public List<FontAwesome> BrandFontAwesomes { get => brandFontAwesomes; set { brandFontAwesomes = value; OnPropertyChanged("BrandFontAwesomes"); } }

        private List<FontAwesome> regularFontAwesomes;

        public List<FontAwesome> RegularFontAwesomes { get => regularFontAwesomes; set { regularFontAwesomes = value; OnPropertyChanged("RegularFontAwesomes"); } }
    }
}
