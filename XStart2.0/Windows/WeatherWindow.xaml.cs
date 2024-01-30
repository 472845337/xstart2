using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
                List<City> cityList = JsonConvert.DeserializeObject<List<City>>(cityJson);
                Dictionary<string, Province> provinceDic = new Dictionary<string, Province>();
                foreach (City city in cityList) {
                    if (!provinceDic.TryGetValue(city.ProvinceEn, out Province province)) {
                        province = new Province() { En = city.ProvinceEn, Zh = city.ProvinceZh };
                        provinceDic.Add(city.ProvinceEn, province);
                    }
                    province.Cities.Add(city);
                    Configs.Cities.Add(city.Id, city);
                }
                // 对省数据进行排序
                Configs.Provinces = provinceDic.Values.ToList().OrderBy(p => p.En).ToList();
                // 对城市数据进行排序
                foreach (Province province in provinceDic.Values) {
                    province.Cities = province.Cities.OrderBy(p => p.CityEn).ToList();
                }
            }
            vm.Provinces = Configs.Provinces;
            if (!string.IsNullOrEmpty(Configs.lastWeacherCity)) {
                vm.Province = Configs.lastWeatherProvince;
                vm.City = Configs.lastWeacherCity;
                //GetWeather(vm.City);
            }
            // 最近查询的城市信息
            var iniData = IniParserUtils.GetIniData(Constants.SET_FILE);
            Configs.lastCitys = iniData[Constants.SECTION_WEATHER][Constants.KEY_LAST_CITYS];
            if (!string.IsNullOrEmpty(Configs.lastCitys)) {
                string[] lastCityIdArray = Configs.lastCitys.Split(';');
                foreach (string lastCityId in lastCityIdArray) {
                    vm.LastCities.Add(Configs.Cities[lastCityId]);
                }
            }
            DataContext = vm;
        }

        private void Window_Closing(object sender, EventArgs e) {
            // 保存当前查询的城市
            IniParser.Model.IniData iniData = new IniParser.Model.IniData();
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_PROVINCE, ref Configs.lastWeatherProvince, vm.Province);
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_WEATHER_CITY, ref Configs.lastWeacherCity, vm.City);
            StringBuilder lastCitysStr = new StringBuilder();
            foreach (City lastCity in vm.LastCities) {
                if (lastCitysStr.Length > 0) {
                    lastCitysStr.Append(";");
                }
                lastCitysStr.Append(lastCity.Id);
            }
            IniParserUtils.ConfigIniData(iniData, Constants.SECTION_WEATHER, Constants.KEY_LAST_CITYS, ref Configs.lastCitys, lastCitysStr.ToString());
            IniParserUtils.SaveIniData(Constants.SET_FILE, iniData);
            DataContext = null;
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetWeather_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.City)) {
                MessageBox.Show("未选择城市", Constants.MESSAGE_BOX_TITLE_ERROR);
            } else {
                GetWeather(vm.City);
            }

        }

        private void GetWeather(string cityId) {
            vm.CurWeather = null;
            vm.DayWeather = null;
            // 添加到最近查询城市列表中
            RemoveLastCity(cityId);
            vm.LastCities.Insert(0, Configs.Cities[cityId]);
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

        private void OpenLastCities_Click(object sender, RoutedEventArgs e) {
            LastCities_Popup.IsOpen = false;
            LastCities_Popup.IsOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ListBox lastCitysListBox = sender as ListBox;
            if (null != lastCitysListBox.SelectedItem) {
                City selectedCity = lastCitysListBox.SelectedItem as City;
                vm.Province = selectedCity.ProvinceEn;
                vm.City = selectedCity.Id;
                LastCities_Popup.IsOpen = false;
            }
        }

        private void RemoveLastCity_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = sender as TextBlock;
            string cityId = btn.Tag as string;
            RemoveLastCity(cityId);
        }

        private void RemoveLastCity(string cityId) {
            int index = 0;
            bool isExist = false;
            for (int i = 0; i < vm.LastCities.Count; i++) {
                City city = vm.LastCities[i];
                if (city.Id.Equals(cityId)) {
                    index = i;
                    isExist = true;
                    break;
                }
            }
            if (isExist) {
                vm.LastCities.RemoveAt(index);
            }
        }
    }
}
