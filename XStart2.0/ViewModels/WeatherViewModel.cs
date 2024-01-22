﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;
using XStart2._0.Bean.Weather;

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
    }
}
