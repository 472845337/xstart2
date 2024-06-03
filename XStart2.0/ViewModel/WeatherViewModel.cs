using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean.Weather;

namespace XStart2._0.ViewModel {
    class WeatherViewModel : BaseViewModel {
        public string WeatherApi { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // 城市信息
        public List<Province> Provinces { get; set; }

        // 实时天气信息
        public CurWeather CurWeather { get; set; }

        // 七日天气信息
        public DayWeather DayWeather { get; set; }

        public string QueryCountry { get; set; }

        public ObservableCollection<Country> QueryCountries { get; set; } = new ObservableCollection<Country>();

        public ObservableCollection<Country> LastCountries { get; set; } = new ObservableCollection<Country>();
    }
}
