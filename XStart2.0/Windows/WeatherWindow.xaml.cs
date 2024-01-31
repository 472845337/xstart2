using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XStart2._0.Bean.Weather;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// WeatherWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherWindow : Window {
        WeatherViewModel vm = new WeatherViewModel();
        public WeatherWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, EventArgs e) {
            // 初始化省市数据
            if (Configs.Provinces.Count == 0) {
                string cityJson = File.ReadAllText(Configs.AppStartPath + Constants.CITY_JSON_FILE, Encoding.UTF8);
                List<CityBean> cityList = JsonConvert.DeserializeObject<List<CityBean>>(cityJson);
                Dictionary<string, Province> provinceDic = new Dictionary<string, Province>();
                Dictionary<string, City> cityDic = new Dictionary<string, City>();
                foreach (CityBean cityBean in cityList) {
                    if (!provinceDic.TryGetValue(cityBean.ProvinceEn, out Province province)) {
                        province = new Province() { En = cityBean.ProvinceEn, Zh = cityBean.ProvinceZh };
                        provinceDic.Add(cityBean.ProvinceEn, province);
                    }
                    if(!cityDic.TryGetValue(cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn, out City _city)) {
                        City dataCity = new City { En = cityBean.LeaderEn, Zh = cityBean.LeaderZh };
                        cityDic.Add(cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn, dataCity);
                        province.Cities.Add(dataCity);
                    }
                    Country country = new Country { Id = cityBean.Id, En = cityBean.CityEn, Zh = cityBean.CityZh
                        , ProvinceEn = cityBean.ProvinceEn, ProvinceZh = cityBean.ProvinceZh
                    , LeaderEn = cityBean.LeaderEn, LeaderZh = cityBean.LeaderZh};
                    cityDic[cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn].Countries.Add(country);
                    Configs.Countries.Add(cityBean.Id, country);
                }
                // 对省数据进行排序
                Configs.Provinces = provinceDic.Values.ToList().OrderBy(p => p.En).ToList();
                // 对城市数据进行排序
                foreach (Province province in provinceDic.Values) {
                    province.Cities = province.Cities.OrderBy(p => p.En).ToList();
                    // 对区县进行排序
                    foreach(City city in province.Cities) {
                        city.Countries = city.Countries.OrderBy(c => c.En).ToList();
                    }
                }
            }
            vm.Provinces = Configs.Provinces;
            if (!string.IsNullOrEmpty(Configs.lastWeacherCity)) {
                vm.Province = Configs.lastWeatherProvince;
                vm.City = Configs.lastWeacherCity;
                vm.Country = Configs.lastWeacherCountry;
                //GetWeather(vm.City);
            }
            // 最近查询的城市信息
            var iniData = IniParserUtils.GetIniData(Constants.SET_FILE);
            Configs.lastCountries = iniData[Constants.SECTION_WEATHER][Constants.KEY_LAST_CITYS];
            if (!string.IsNullOrEmpty(Configs.lastCountries)) {
                string[] lastCityIdArray = Configs.lastCountries.Split(';');
                foreach (string lastCityId in lastCityIdArray) {
                    vm.LastCountries.Add(Configs.Countries[lastCityId]);
                }
            }
            DataContext = vm;
        }

        private void Window_Closing(object sender, EventArgs e) {
            // 保存当前查询的城市
            IniParser.Model.IniData iniData = new IniParser.Model.IniData();
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_PROVINCE, ref Configs.lastWeatherProvince, vm.Province);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_CITY, ref Configs.lastWeacherCity, vm.City);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_COUNTRY, ref Configs.lastWeacherCountry, vm.Country);
            StringBuilder lastCountryStr = new StringBuilder();
            foreach (Country lastCountry in vm.LastCountries) {
                if (lastCountryStr.Length > 0) {
                    lastCountryStr.Append(";");
                }
                lastCountryStr.Append(lastCountry.Id);
            }
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_LAST_CITYS, ref Configs.lastCountries, lastCountryStr.ToString());
            IniParserUtils.SaveIniData(Constants.SET_FILE, iniData);
            DataContext = null;
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetWeather_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Country)) {
                MessageBox.Show("未选择区县", Constants.MESSAGE_BOX_TITLE_ERROR);
            } else {
                GetWeather(vm.Country);
            }

        }

        private void GetWeather(string cityId) {
            vm.CurWeather = null;
            vm.DayWeather = null;
            // 添加到最近查询城市列表中
            RemoveLastCountry(cityId);
            vm.LastCountries.Insert(0, Configs.Countries[cityId]);
            // 异步获取实时天气数据
            Task task = new Task(() => {
                string curUrl = $"{Configs.weatherApiUrl}{CurWeather.ApiPath}? appid={Configs.weatherApiAppId}&appsecret={Configs.weatherApiAppSecret}&unescape=1&cityid={cityId}";
                string curWeatherJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
                CurWeather curWeather = JsonConvert.DeserializeObject<CurWeather>(curWeatherJson);
                vm.CurWeather = curWeather;
                // 获取七日天气数据
                string weekUrl = $"{Configs.weatherApiUrl}{DayWeather.ApiPath}?appid={Configs.weatherApiAppId}&appsecret={Configs.weatherApiAppSecret}&unescape=1&cityid={cityId}";
                string dayWeatherJson = HttpUtils.GetRequest(weekUrl, HttpUtils.ContentTypeJson);
                DayWeather dayWeather = JsonConvert.DeserializeObject<DayWeather>(dayWeatherJson);
                vm.DayWeather = dayWeather;
            });
            task.Start();
        }

        private void OpenQuery_Click(object sender, RoutedEventArgs e) {
            QueryCity_Popup.IsOpen = false;
            QueryCity_Popup.IsOpen = true;
        }
        private void OpenLastCities_Click(object sender, RoutedEventArgs e) {
            LastCountries_Popup.IsOpen = false;
            LastCountries_Popup.IsOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBox lastCountriesListBox = sender as ListBox;
            if (null != lastCountriesListBox.SelectedItem) {
                Country selectedCountry = lastCountriesListBox.SelectedItem as Country;
                vm.Province = selectedCountry.ProvinceEn;
                vm.City = selectedCountry.LeaderEn;
                vm.Country = selectedCountry.Id;
                LastCountries_Popup.IsOpen = false;
                QueryCity_Popup.IsOpen = false;
            }
        }

        private void RemoveLastCity_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = sender as TextBlock;
            string countryId = btn.Tag as string;
            RemoveLastCountry(countryId);
        }

        private void RemoveLastCountry(string countryId) {
            int index = 0;
            bool isExist = false;
            for (int i = 0; i < vm.LastCountries.Count; i++) {
                Country country = vm.LastCountries[i];
                if (country.Id.Equals(countryId)) {
                    index = i;
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                vm.LastCountries.RemoveAt(index);
            }
        }

        private void QueryCountry_KeyDown(object sender, KeyEventArgs e) {
            if(Key.Enter == e.Key) {
                vm.QueryCountries.Clear();
                if (!string.IsNullOrWhiteSpace(vm.QueryCountry)) {
                    foreach (var country in Configs.Countries) {
                        if (country.Value.Zh.Contains(vm.QueryCountry)) {
                            vm.QueryCountries.Add(country.Value);
                        }
                    }
                }
            }
        }
    }
}
