using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    class WeatherViewModel :BaseViewModel{
        // 城市信息
        public ObservableCollection<Province> Provinces { get; set; } = new ObservableCollection<Province>();

        // 实时天气信息

        // 七日天气信息

    }
}
