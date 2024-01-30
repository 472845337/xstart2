using Newtonsoft.Json;
using PropertyChanged;
using System.Collections.ObjectModel;

namespace XStart2._0.Bean.Weather {
    [AddINotifyPropertyChangedInterface]
    class DayWeather {
        public const string ApiPath = "/free/week";
        [JsonProperty("cityid")]
        public string CityId { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("update_time")]
        public string UpdateTime { get; set; }
        [JsonProperty("data")]
        public ObservableCollection<Data> Data { get; set; }
    }

    /// <summary>
    /// "date":"2020-04-21",
    ///       "wea":"晴",
    ///       "wea_img":"qing",
    ///        "tem_day":"17",
    ///        "tem_night":"4",
    ///        "win":"北风",
    ///        "win_speed":"3-4级"
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    class Data {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("wea")]
        public string Wea { get; set; }
        [JsonProperty("wea_img")]
        public string WeaImg { get; set; }
        [JsonProperty("tem_day")]
        public string TemDay { get; set; }
        [JsonProperty("tem_night")]
        public string TemNight { get; set; }
        [JsonProperty("win")]
        public string Win { get; set; }
        [JsonProperty("win_speed")]
        public string WinSpeed { get; set; }
    }
}
