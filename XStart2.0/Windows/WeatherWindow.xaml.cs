using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using XStart2._0.Bean.Weather;
using XStart2._0.Bean.Weather.Accu;
using XStart2._0.Bean.Weather.Gaode;
using XStart2._0.Bean.Weather.Open;
using XStart2._0.Bean.Weather.Q;
using XStart2._0.Bean.Weather.Seniverse;
using XStart2._0.Bean.Weather.VisualCrossing;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// WeatherWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherWindow : Window {
        readonly WeatherViewModel vm = new WeatherViewModel();
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
                    if (!cityDic.TryGetValue(cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn, out City _city)) {
                        City dataCity = new City { En = cityBean.LeaderEn, Zh = cityBean.LeaderZh };
                        cityDic.Add(cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn, dataCity);
                        province.Cities.Add(dataCity);
                    }
                    Country country = new Country {
                        Id = cityBean.Id, AreaCode = cityBean.AreaCode, En = cityBean.CityEn, Zh = cityBean.CityZh
                        , ProvinceEn = cityBean.ProvinceEn, ProvinceZh = cityBean.ProvinceZh
                    , LeaderEn = cityBean.LeaderEn, LeaderZh = cityBean.LeaderZh, Lat = cityBean.Lat, Lon = cityBean.Lon
                    };
                    cityDic[cityBean.ProvinceEn + Constants.SPLIT_CHAR + cityBean.LeaderEn].Countries.Add(country);
                    Configs.Countries.Add(cityBean.Id, country);
                }
                // 对省数据进行排序
                Configs.Provinces = provinceDic.Values.ToList().OrderBy(p => p.En).ToList();
                // 对城市数据进行排序
                foreach (Province province in provinceDic.Values) {
                    province.Cities = province.Cities.OrderBy(p => p.En).ToList();
                    // 对区县进行排序
                    foreach (City city in province.Cities) {
                        city.Countries = city.Countries.OrderBy(c => c.En).ToList();
                    }
                }
            }
            vm.Provinces = Configs.Provinces;
            if (!string.IsNullOrEmpty(Configs.lastWeacherCountry)) {
                vm.Province = Configs.Countries[Configs.lastWeacherCountry].ProvinceEn;
                vm.City = Configs.Countries[Configs.lastWeacherCountry].LeaderEn;
                vm.Country = Configs.lastWeacherCountry;
                //GetWeather(vm.City);
            }
            // 最近查询的城市信息
            if (!string.IsNullOrEmpty(Configs.lastCountries)) {
                string[] lastCityIdArray = Configs.lastCountries.Split(';');
                foreach (string lastCityId in lastCityIdArray) {
                    vm.LastCountries.Add(Configs.Countries[lastCityId]);
                }
            }
            vm.WeatherApi = Configs.weatherApi;
            DataContext = vm;
            Configs.WeatherHandler = new WindowInteropHelper(this).Handle;
        }

        private void Window_Closing(object sender, EventArgs e) {
            // 保存当前查询的城市
            IniParser.Model.IniData iniData = new IniParser.Model.IniData();
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
            Configs.WeatherHandler = IntPtr.Zero;
            DataContext = null;
        }

        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetWeather_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Country)) {
                MsgBoxUtils.ShowError("未选择区县");
            } else {
                GetWeather(vm.Country);
            }

        }

        private void GetWeather(string cityId) {
            bool checkResult = CheckWeatherApiConfig(vm.WeatherApi);
            if (checkResult) {
                vm.CurWeather = null;
                vm.DayWeather = null;
                // 添加到最近查询城市列表中
                RemoveLastCountry(cityId);
                Country country = Configs.Countries[cityId];
                vm.LastCountries.Insert(0, country);
                // 异步获取实时天气数据
                Task task = new Task(() => {
                    if (Constants.WEATHER_API_YKY.Equals(vm.WeatherApi)) {
                        // 易客云天气
                        GetYkyWeather(cityId);
                    } else if (Constants.WEATHER_API_GAODE.Equals(vm.WeatherApi)) {
                        string areaCode = country.AreaCode;
                        if (string.IsNullOrEmpty(areaCode)) {
                            MsgBoxUtils.ShowWarning("当前接口不支持查询该城市！");
                        } else {
                            // 高德天气
                            GetGaoDeWeather(areaCode);
                        }
                    } else if (Constants.WEATHER_API_SENIVERSE.Equals(vm.WeatherApi)) {
                        // 心知天气
                        GetSeniverseWeather(country.ProvinceEn, country.En);
                    } else if (Constants.WEATHER_API_Q.Equals(vm.WeatherApi)) {
                        // 和风天气
                        GetQWeather(country);
                    } else if (Constants.WEATHER_API_OPEN.Equals(vm.WeatherApi)) {
                        // OpenWeather天气
                        GetOpenWeather(country);
                    } else if (Constants.WEATHER_API_ACCU.Equals(vm.WeatherApi)) {
                        // AccuWeather天气
                        GetAccuWeather(country);
                    } else if (Constants.WEATHER_API_VC.Equals(vm.WeatherApi)) {
                        // Visual Crossing天气
                        GetVcWeather(country);
                    }
                });
                task.Start();
            }
        }

        /// <summary>
        /// 易客云天气
        /// </summary>
        /// <param name="cityId">城市ID</param>
        private void GetYkyWeather(string cityId) {
            string curUrl = $"{Configs.weatherYkyApiUrl}{CurWeather.ApiPath}? appid={Configs.weatherYkyApiAppId}&appsecret={Configs.weatherYkyApiAppSecret}&unescape=1&cityid={cityId}";
            string curWeatherJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
            CurWeather curWeather = JsonConvert.DeserializeObject<CurWeather>(curWeatherJson);
            if (null != curWeather.ErrCode) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{curWeather.ErrMsg}");
                return;
            }
            vm.CurWeather = curWeather;
            // 获取七日天气数据
            string weekUrl = $"{Configs.weatherYkyApiUrl}{DayWeather.ApiPath}?appid={Configs.weatherYkyApiAppId}&appsecret={Configs.weatherYkyApiAppSecret}&unescape=1&cityid={cityId}";
            string dayWeatherJson = HttpUtils.GetRequest(weekUrl, HttpUtils.ContentTypeJson);
            DayWeather dayWeather = JsonConvert.DeserializeObject<DayWeather>(dayWeatherJson);
            if (null != dayWeather.ErrCode) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{dayWeather.ErrMsg}");
                return;
            }
            vm.DayWeather = dayWeather;
        }

        /// <summary>
        /// 高德天气
        /// </summary>
        /// <param name="areaCode">地区编码</param>
        private void GetGaoDeWeather(string areaCode) {
            string curUrl = $"{Configs.weatherGaodeApiUrl}{GaodeWeather.ApiPath}?key={Configs.weatherGaodeAppKey}&extensions=base&city={areaCode}";
            string curWeatherJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
            GaodeWeather curWeather = JsonConvert.DeserializeObject<GaodeWeather>(curWeatherJson);
            if (GaodeWeather.STATUS_FAIL == curWeather.Status) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{curWeather.Info}");
                return;
            }
            vm.CurWeather = new CurWeather {
                City = curWeather.Lives[0].City, Date = curWeather.Lives[0].ReportTime, UpdateTime = curWeather.Lives[0].ReportTime,
                Tem = curWeather.Lives[0].Temperature,
                Wea = curWeather.Lives[0].Weather, WeaImg = WeatherUtil.GetWeatherImg(curWeather.Lives[0].Weather),
                Humidity = curWeather.Lives[0].Humidity, Win = curWeather.Lives[0].WindDirection, WinSpeed = curWeather.Lives[0].WindPower + "级"
            };
            // 获取预报天气数据
            string weekUrl = $"{Configs.weatherGaodeApiUrl}{GaodeWeather.ApiPath}?key={Configs.weatherGaodeAppKey}&extensions=all&city={areaCode}";
            string dayWeatherJson = HttpUtils.GetRequest(weekUrl, HttpUtils.ContentTypeJson);
            GaodeWeather dayWeather = JsonConvert.DeserializeObject<GaodeWeather>(dayWeatherJson);
            if (GaodeWeather.STATUS_FAIL == curWeather.Status) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{dayWeather.Info}");
                return;
            }
            DayWeather vmDayWeather = new DayWeather {
                City = dayWeather.Forecasts[0].City,
                UpdateTime = dayWeather.Forecasts[0].ReportTime
            };
            ObservableCollection<Data> datas = new ObservableCollection<Data>();
            foreach (GaodeWeather.Cast cast in dayWeather.Forecasts[0].Casts) {
                Data data = new Data {
                    Date = cast.Date, Wea = cast.DayWeather, WeaImg = WeatherUtil.GetWeatherImg(cast.DayWeather)
                , TemDay = cast.DayTemp, TemNight = cast.NightTemp, Win = cast.DayWind, WinSpeed = cast.DayPower + "级"
                };
                datas.Add(data);
            }
            vmDayWeather.Data = datas;
            vm.DayWeather = vmDayWeather;
        }

        /// <summary>
        /// 心知天气
        /// </summary>
        /// <param name="provinceEn">省拼音</param>
        /// <param name="countryEn">区县拼音</param>
        private void GetSeniverseWeather(string provinceEn, string countryEn) {
            string curUrl = $"{Configs.weatherSeniverseApiUrl}{NowWeather.ApiPath}?key={Configs.weatherSeniverseAppKey}&location={provinceEn} {countryEn}";
            try {
                string curWeatherJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
                NowWeather curWeather = JsonConvert.DeserializeObject<NowWeather>(curWeatherJson);
                vm.CurWeather = new CurWeather {
                    City = curWeather.Results[0].Location.Name, Date = curWeather.Results[0].LastUpdate, UpdateTime = curWeather.Results[0].LastUpdate,
                    Wea = curWeather.Results[0].Now.Text, WeaImg = WeatherUtil.GetWeatherImg(curWeather.Results[0].Now.Text),
                    Humidity = curWeather.Results[0].Now.Humidity, Tem = curWeather.Results[0].Now.Temperature, Win = curWeather.Results[0].Now.WindDirection,
                    WinSpeed = curWeather.Results[0].Now.WindScale + "级", Pressure = curWeather.Results[0].Now.Pressure
                };
            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
                return;
            }

            // 获取预报天气数据
            string weekUrl = $"{Configs.weatherSeniverseApiUrl}{DailyWeather.ApiPath}?key={Configs.weatherSeniverseAppKey}&location={provinceEn} {countryEn}&start=0&days=7";
            try {
                string dailyWeatherJson = HttpUtils.GetRequest(weekUrl, HttpUtils.ContentTypeJson);
                DailyWeather dailyWeather = JsonConvert.DeserializeObject<DailyWeather>(dailyWeatherJson);
                DayWeather vmDayWeather = new DayWeather {
                    City = dailyWeather.Results[0].Location.Name,
                    UpdateTime = dailyWeather.Results[0].LastUpdate
                };
                ObservableCollection<Data> datas = new ObservableCollection<Data>();
                foreach (DailyWeather.DailyBean daily in dailyWeather.Results[0].Daily) {
                    Data data = new Data {
                        Date = daily.Date, Wea = daily.TextDay, WeaImg = WeatherUtil.GetWeatherImg(daily.TextDay)
                    , TemDay = daily.High, TemNight = daily.Low, Win = daily.WindDirection, WinSpeed = daily.WindScale + "级"
                    };
                    datas.Add(data);
                }
                vmDayWeather.Data = datas;
                vm.DayWeather = vmDayWeather;
            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
                return;
            }
        }

        /// <summary>
        /// 和风天气
        /// </summary>
        /// <param name="country">城市对象</param>
        private void GetQWeather(Country country) {
            #region 获取当日天气和空气质量
            string curUrl = $"{Configs.weatherQApiUrl}{QNowWeather.ApiPath}?key={Configs.weatherQAppKey}&location={country.Id}";
            string curJson = HttpUtils.GetQRequest(curUrl, HttpUtils.ContentTypeJson);
            string airUrl = $"{Configs.weatherQApiUrl}{QAir.ApiPath}?key={Configs.weatherQAppKey}&location={country.Id}";
            string airJson = HttpUtils.GetQRequest(airUrl, HttpUtils.ContentTypeJson);

            QNowWeather curWeather = JsonConvert.DeserializeObject<QNowWeather>(curJson);
            if (!QBaseWeather.CODE_SUCCESS.Equals(curWeather.Code)) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{curWeather.Msg}");
                return;
            }
            QAir air = JsonConvert.DeserializeObject<QAir>(airJson);
            if (!QBaseWeather.CODE_SUCCESS.Equals(air.Code)) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{air.Msg}");
                return;
            }
            vm.CurWeather = new CurWeather {
                City = country.Zh, Date = curWeather.UpdateTime, UpdateTime = curWeather.UpdateTime,
                Tem = curWeather.Now.Temp,
                Wea = curWeather.Now.Text, WeaImg = WeatherUtil.GetWeatherImg(curWeather.Now.Text),
                Humidity = curWeather.Now.Humidity, Win = curWeather.Now.WindDir, WinSpeed = curWeather.Now.WindScale + "级",
                Pressure = curWeather.Now.Pressure, Air = air.Now.Aqi
            };
            #endregion
            #region 获取预报天气数据
            string weekUrl = $"{Configs.weatherQApiUrl}{QDailyWeather.ApiPath}?key={Configs.weatherQAppKey}&location={country.Id}";
            string daliyJson = HttpUtils.GetQRequest(weekUrl, HttpUtils.ContentTypeJson);
            // GZIP解压
            QDailyWeather daliyWeather = JsonConvert.DeserializeObject<QDailyWeather>(daliyJson);
            if (!QBaseWeather.CODE_SUCCESS.Equals(daliyWeather.Code)) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{daliyWeather.Msg}");
                return;
            }
            DayWeather vmDayWeather = new DayWeather {
                City = country.Zh,
                UpdateTime = daliyWeather.UpdateTime
            };
            ObservableCollection<Data> datas = new ObservableCollection<Data>();
            foreach (QDailyWeather.DailyBean daily in daliyWeather.Daily) {
                Data data = new Data {
                    Date = daily.FxDate, Wea = daily.TextDay, WeaImg = WeatherUtil.GetWeatherImg(daily.TextDay)
                , TemDay = daily.TempMax, TemNight = daily.TempMin, Win = daily.WindDirDay, WinSpeed = daily.WindScaleDay + "级"
                };
                datas.Add(data);
            }
            vmDayWeather.Data = datas;
            vm.DayWeather = vmDayWeather;
            #endregion
        }

        /// <summary>
        /// OpenWeather天气
        /// </summary>
        /// <param name="country">城市对象</param>
        private void GetOpenWeather(Country country) {
            #region 获取当日天气
            string curUrl = $"{Configs.weatherOpenApiUrl}{OpenCurWeather.ApiPath}?appid={Configs.weatherOpenAppKey}&lat={country.Lat}&lon={country.Lon}&lang=zh_cn";
            string airUrl = $"{Configs.weatherOpenApiUrl}{OpenAir.ApiPath}?appid={Configs.weatherOpenAppKey}&lat={country.Lat}&lon={country.Lon}&lang=zh_cn";
            try {
                string curJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
                string airJson = HttpUtils.GetRequest(airUrl, HttpUtils.ContentTypeJson);
                OpenCurWeather curWeather = JsonConvert.DeserializeObject<OpenCurWeather>(curJson);
                OpenAir air = JsonConvert.DeserializeObject<OpenAir>(airJson);
                vm.CurWeather = new CurWeather {
                    City = country.Zh, Date = DateUtil.Format(DateUtil.GetTime(curWeather.Dt), "yyyy-MM-dd"), UpdateTime = DateUtil.Format(DateUtil.GetTime(curWeather.Dt), "yyyy-MM-dd HH:mm:ss"),
                    Tem = TempUtil.K2C(curWeather.Main.Temp, 1).ToString(), TemDay = TempUtil.K2C(curWeather.Main.TempMax, 1).ToString(), TemNight = TempUtil.K2C(curWeather.Main.TempMin, 1).ToString(),
                    Wea = curWeather.Weather[0].Description, WeaImg = WeatherUtil.GetWeatherImg(curWeather.Weather[0].Description),
                    Humidity = curWeather.Main.Humidity.ToString(), Win = WindUtil.Deg2Direc(curWeather.Wind.Deg), WinSpeed = WindUtil.Speed2Scale(curWeather.Wind.Speed) + "级",
                    Pressure = curWeather.Main.Pressure.ToString(), Air = Convert.ToInt32(WeatherUtil.GetAirFromOpenAir(air)).ToString()
                };
            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
                return;
            }

            #endregion
            #region 获取预报天气数据 非免费接口
            string weekUrl = $"{Configs.weatherOpenApiUrl}{OpenForecastWeather.ApiPath}?appid={Configs.weatherOpenAppKey}&lat={country.Lat}&lon={country.Lon}&lang=zh_cn&cnt=7";
            DayWeather vmDayWeather = new DayWeather {
                City = country.Zh,
            };
            ObservableCollection<Data> datas = new ObservableCollection<Data>();
            try {
                string forecastJson = HttpUtils.GetRequest(weekUrl, HttpUtils.ContentTypeJson);
                OpenForecastWeather forecastWeather = JsonConvert.DeserializeObject<OpenForecastWeather>(forecastJson);
                if (OpenWeather.COD_SUCCESS != forecastWeather.Cod) {
                    MsgBoxUtils.ShowError($"获取天气数据异常：{forecastWeather.Msg}");
                    return;
                }
                foreach (OpenForecastWeather.Forecast forecast in forecastWeather.Casts) {
                    Data data = new Data {
                        Date = DateUtil.Format(DateUtil.GetTime(forecast.Dt), "yyyy-MM-dd")
                        , Wea = forecast.Weather[0].Description, WeaImg = WeatherUtil.GetWeatherImg(forecast.Weather[0].Description)
                    , TemDay = TempUtil.K2C(forecast.Temp.Day, 1).ToString(), TemNight = TempUtil.K2C(forecast.Temp.Night, 1).ToString()
                    , Win = WindUtil.Deg2Direc(forecast.Deg), WinSpeed = WindUtil.Speed2Scale(forecast.Speed) + "级"
                    };
                    datas.Add(data);
                }
            } catch (Exception e) {
                Data data = new Data {
                    Wea = e.Message
                };
                datas.Add(data);
            }
            vmDayWeather.Data = datas;
            vm.DayWeather = vmDayWeather;
            #endregion
        }

        /// <summary>
        /// AccuWeather天气
        /// </summary>
        /// <param name="country">城市对象</param>
        private void GetAccuWeather(Country country) {
            string key;
            #region 获取当日天气和空气质量
            try {
                string getKeyUrl = $"{Configs.weatherAccuApiUrl}{AccuGeoPositionSearch.ApiPath}?q={country.Lat},{country.Lon}&apikey={Configs.weatherAccuAppKey}&language=zh-cn";
                string getKeyJson = HttpUtils.GetRequest(getKeyUrl, HttpUtils.ContentTypeJson);
                AccuGeoPositionSearch geoPositionSearch = JsonConvert.DeserializeObject<AccuGeoPositionSearch>(getKeyJson);
                key = geoPositionSearch.Key;
            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
                return;
            }
            try {
                // 当前天气情况
                string curUrl = $"{Configs.weatherAccuApiUrl}{AccuCurrentConditions.ApiPath}{key}?apikey={Configs.weatherAccuAppKey}&language=zh-cn&details=true";
                // 当天气象预报（获取空气质量）
                string oneDayForecastUrl = $"{Configs.weatherAccuApiUrl}{AccuForecasts.ApiPath_1Day}{key}?apikey={Configs.weatherAccuAppKey}&language=zh-cn&details=true&metric=true";
                string curJson = HttpUtils.GetRequest(curUrl, HttpUtils.ContentTypeJson);
                string oneDayForecastJson = HttpUtils.GetRequest(oneDayForecastUrl, HttpUtils.ContentTypeJson);
                List<AccuCurrentConditions> curConditions = JsonConvert.DeserializeObject<List<AccuCurrentConditions>>(curJson);
                AccuForecasts oneDayForecasts = JsonConvert.DeserializeObject<AccuForecasts>(oneDayForecastJson);
                vm.CurWeather = new CurWeather {
                    City = country.Zh, Date = curConditions[0].LocalObservationDateTime, UpdateTime = curConditions[0].LocalObservationDateTime,
                    Tem = curConditions[0].Temperature.Metric.Value.ToString(), TemDay = curConditions[0].TemperatureSummary.Past24HourRange.Maximum.Metric.Value.ToString(),
                    TemNight = curConditions[0].TemperatureSummary.Past24HourRange.Minimum.Metric.Value.ToString(),
                    Pressure = curConditions[0].Pressure.Metric.Value.ToString(),
                    Win = WindUtil.Deg2Direc(curConditions[0].Wind.Direction.Degrees),
                    WinSpeed = WindUtil.Speed2Scale(WindUtil.SpeedConvert(curConditions[0].Wind.Speed.Metric.Value, 1)) + "级",
                    Humidity = curConditions[0].RelativeHumidity.Value.ToString(),
                    Wea = curConditions[0].WeatherText, WeaImg = WeatherUtil.GetWeatherImg(curConditions[0].WeatherText)
                };
                foreach (AccuForecasts.AirAndPollenBean airAndPollenBean in oneDayForecasts.DailyForecasts[0].AirAndPollen) {
                    if (AccuForecasts.AQI_NAME.Equals(airAndPollenBean.Name)) {
                        vm.CurWeather.Air = airAndPollenBean.Value.ToString();
                    }
                }
            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
                return;
            }

            #endregion
            #region 获取预报天气数据
            DayWeather vmDayWeather = new DayWeather {
                City = country.Zh,
            };
            ObservableCollection<Data> datas = new ObservableCollection<Data>();
            try {
                string fiveDayForecastUrl = $"{Configs.weatherAccuApiUrl}{AccuForecasts.ApiPath_5Day}{key}?apikey={Configs.weatherAccuAppKey}&language=zh-cn&details=true&metric=true";
                string forecastJson = HttpUtils.GetRequest(fiveDayForecastUrl, HttpUtils.ContentTypeJson);
                AccuForecasts forecasts = JsonConvert.DeserializeObject<AccuForecasts>(forecastJson);
                vmDayWeather.UpdateTime = forecasts.Headline.EffectiveDate;
                foreach (AccuForecasts.DailyForecast forecast in forecasts.DailyForecasts) {
                    Data data = new Data {
                        Date = DateUtil.Format(DateUtil.GetTime(forecast.Date), "yyyy-MM-dd"), Wea = forecast.Day.IconPhrase, WeaImg = WeatherUtil.GetWeatherImg(forecast.Day.IconPhrase)
                    , TemDay = forecast.Temperature.Maximum.Value.ToString(), TemNight = forecast.Temperature.Minimum.Value.ToString(),
                        Win = forecast.Day.Wind.Direction.Localized, WinSpeed = WindUtil.Speed2Scale(WindUtil.SpeedConvert(forecast.Day.Wind.Speed.Value, 1)) + "级"
                    };
                    datas.Add(data);
                }

            } catch (Exception e) {
                Data data = new Data {
                    Wea = e.Message
                };
                datas.Add(data);
            } finally {
                vmDayWeather.Data = datas;
                vm.DayWeather = vmDayWeather;
            }

            #endregion
        }

        /// <summary>
        /// Visual Crossing天气
        /// </summary>
        /// <param name="country">城市对象</param>
        private void GetVcWeather(Country country) {
            try {
                // 当前日期
                string now = DateUtil.Format(DateTime.Today, "yyyy-MM-dd");
                // 七天后日期
                string sevenDays = DateUtil.Format(DateUtil.OperateDateTime(DateTime.Today, 1, 3, 7), "yyyy-MM-dd");
                string vcUrl = $"{Configs.weatherVcApiUrl}{VcCurrentAndForecasts.ApiPath}/{country.Lat},{country.Lon}/{now}/{sevenDays}?key={Configs.weatherVcAppKey}&unitGroup=metric&include=current&lang=zh";
                string vcJson = HttpUtils.GetRequest(vcUrl, HttpUtils.ContentTypeJson);
                VcCurrentAndForecasts currentAndForecasts = JsonConvert.DeserializeObject<VcCurrentAndForecasts>(vcJson);
                vm.CurWeather = new CurWeather {
                    City = country.Zh, Date = currentAndForecasts.Days[0].DateTime,
                    Tem = currentAndForecasts.CurrentConditions.Temp.ToString(), TemDay = currentAndForecasts.Days[0].TempMax.ToString(),
                    TemNight = currentAndForecasts.Days[0].TempMin.ToString(),
                    Pressure = currentAndForecasts.CurrentConditions.Pressure.ToString(),
                    Win = WindUtil.Deg2Direc(Convert.ToInt32(currentAndForecasts.CurrentConditions.WindDir)),
                    WinSpeed = WindUtil.Speed2Scale(WindUtil.SpeedConvert(currentAndForecasts.CurrentConditions.WindSpeed, 1)) + "级",
                    Humidity = currentAndForecasts.CurrentConditions.Humidity.ToString(),
                    Wea = currentAndForecasts.CurrentConditions.Conditions, WeaImg = WeatherUtil.GetWeatherImg(currentAndForecasts.CurrentConditions.Conditions)
                };

                ObservableCollection<Data> datas = new ObservableCollection<Data>();
                DayWeather vmDayWeather = new DayWeather {
                    City = country.Zh,
                };
                foreach (VcCurrentAndForecasts.Day day in currentAndForecasts.Days) {
                    Data data = new Data {
                        Date = day.DateTime, Wea = day.Conditions, WeaImg = WeatherUtil.GetWeatherImg(day.Conditions)
                    , TemDay = day.TempMax.ToString(), TemNight = day.TempMin.ToString(),
                        Win = WindUtil.Deg2Direc(Convert.ToInt32(day.WindDir)), WinSpeed = WindUtil.Speed2Scale(WindUtil.SpeedConvert(day.WindSpeed, 1)) + "级"
                    };
                    datas.Add(data);
                }
                vmDayWeather.Data = datas;
                vm.DayWeather = vmDayWeather;

            } catch (Exception e) {
                MsgBoxUtils.ShowError($"获取天气数据异常：{e.Message}");
            }
        }

        private void OpenQuery_Click(object sender, RoutedEventArgs e) {
            QueryCity_Popup.IsOpen = false;
            QueryCity_Popup.IsOpen = true;
            e.Handled = true;
        }
        private void OpenLastCities_Click(object sender, RoutedEventArgs e) {
            LastCountries_Popup.IsOpen = false;
            LastCountries_Popup.IsOpen = true;
            e.Handled = true;
        }

        private void SelectCity(object sender, RoutedEventArgs e) {
            TextBlock lastCountriesListBox = sender as TextBlock;
            if (null != lastCountriesListBox.Tag) {
                Country selectedCountry = lastCountriesListBox.Tag as Country;
                vm.Province = selectedCountry.ProvinceEn;
                vm.City = selectedCountry.LeaderEn;
                vm.Country = selectedCountry.Id;
                LastCountries_Popup.IsOpen = false;
                QueryCity_Popup.IsOpen = false;
            }
            e.Handled = true;
        }

        private void RemoveAllLastCity_Click(object sender, RoutedEventArgs e) {
            if (vm.LastCountries.Count > 0) {
                if (MessageBoxResult.Yes == MsgBoxUtils.ShowAsk("确认清除所有历史记录？")) {
                    vm.LastCountries.Clear();
                }
            } else {
                MsgBoxUtils.ShowError("无历史记录！");
            }
            e.Handled = true;
        }

        private void RemoveLastCity_Click(object sender, RoutedEventArgs e) {
            TextBlock btn = sender as TextBlock;
            string countryId = btn.Tag as string;
            RemoveLastCountry(countryId);
            e.Handled = true;
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
            if (Key.Enter == e.Key) {
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            ComboBox weatherApiComboBox = (ComboBox)sender;
            string weatherApiSelected = weatherApiComboBox.SelectedValue as string;
            CheckWeatherApiConfig(weatherApiSelected);
        }

        private bool CheckWeatherApiConfig(string weatherApi) {
            bool result = true;
            string errMsg = string.Empty;
            if (Constants.WEATHER_API_YKY.Equals(weatherApi)) {
                // 易客云天气
                if (string.IsNullOrEmpty(Configs.weatherYkyApiAppId) || string.IsNullOrEmpty(Configs.weatherYkyApiAppSecret)) {
                    errMsg = "易客云未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_GAODE.Equals(weatherApi)) {
                if (string.IsNullOrEmpty(Configs.weatherGaodeAppKey)) {
                    errMsg = "高德未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_SENIVERSE.Equals(weatherApi)) {
                // 心知天气
                if (string.IsNullOrEmpty(Configs.weatherSeniverseAppKey)) {
                    errMsg = "心知未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_Q.Equals(weatherApi)) {
                // 和风天气
                if (string.IsNullOrEmpty(Configs.weatherQAppKey)) {
                    errMsg = "和风未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_OPEN.Equals(weatherApi)) {
                // OpenWeather天气
                if (string.IsNullOrEmpty(Configs.weatherOpenAppKey)) {
                    errMsg = "OpenWeather未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_ACCU.Equals(weatherApi)) {
                // AccuWeather天气
                if (string.IsNullOrEmpty(Configs.weatherAccuAppKey)) {
                    errMsg = "AccuWeather未配置参数！";
                    result = false;
                }
            } else if (Constants.WEATHER_API_VC.Equals(weatherApi)) {
                // Visual Crossing天气
                if (string.IsNullOrEmpty(Configs.weatherVcAppKey)) {
                    errMsg = "Visual Crossing未配置参数！";
                    result = false;
                }
            }
            if (!result) {
                MsgBoxUtils.ShowError(errMsg);
            }
            return result;
        }
    }
}
