using System.Collections.Generic;
using System.Collections.ObjectModel;
using XStart2._0.Bean.Weather;
using PropertyChanged;
using XStart2._0.Config;

namespace XStart2._0.ViewModels {
    class WeatherViewModel : BaseViewModel {

        public string Province { get; set; }
        public string City { get; set; }
        // 城市信息
        public List<Province> Provinces { get; set; }

        // 实时天气信息
        public CurWeather CurWeather { get; set; }

        // 七日天气信息
        public DayWeather DayWeather { get; set; }

        public string QueryCity { get; set; }

        public ObservableCollection<City> QueryCities { get; set; } = new ObservableCollection<City>();

        public ObservableCollection<City> LastCities { get; set; } = new ObservableCollection<City>();
    }
}
