using Newtonsoft.Json;
using PropertyChanged;

namespace XStart2._0.Bean.Weather {
    /// <summary>
    /// "nums":226, //今日实时请求次数
    /// "cityid":"101120101", //城市ID
    /// "city":"济南",
    /// "date":"2022-05-05",
    /// "week":"星期四",
    /// "update_time":"22:38", //更新时间
    /// "wea":"多云", //天气情况
    /// "wea_img":"yun", //天气标识
    /// "tem":"25", //实况温度
    /// "tem_day":"30", //白天温度(高温)
    /// "tem_night":"23", //夜间温度(低温)
    /// "win":"南风", //风向
    /// "win_speed":"3级", //风力
    /// "win_meter":"19km\/h", //风速
    /// "air":"53", //空气质量
    /// "pressure":"987", //气压
    /// "humidity":"27%" //湿度
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    class CurWeather {
        [JsonIgnore]
        public const string ApiPath = "/free/day";
        [JsonProperty("nums")]
        public int Nums { get; set; }//今日实时请求次数
        [JsonProperty("cityid")]
        public string CityId { get; set; }//城市ID
        [JsonProperty("city")]
        public string City { get; set; }// 城市名
        [JsonProperty("date")]
        public string Date { get; set; }// 日期
        [JsonProperty("week")]
        public string Week { get; set; }// 星期
        [JsonProperty("update_time")]
        public string UpdateTime { get; set; }// 更新时间
        [JsonProperty("wea")]
        public string Wea { get; set; }// 天气
        [JsonProperty("wea_img")]
        public string WeaImg { get; set; }// 天气对应图
        [JsonProperty("tem")]
        public string Tem { get; set; }// 当前温度
        [JsonProperty("tem_day")]
        public string TemDay { get; set; }// 最高温度
        [JsonProperty("tem_night")]
        public string TemNight { get; set; }// 最低温度
        [JsonProperty("win")]
        public string Win { get; set; }// 风向
        [JsonProperty("win_speed")]
        public string WinSpeed { get; set; }// 风力
        [JsonProperty("win_meter")]
        public string WinMeter { get; set; }// 风速
        [JsonProperty("air")]
        public string Air { get; set; }// 空气质量
        [JsonProperty("pressure")]
        public string Pressure { get; set; }// 气压
        [JsonProperty("humidity")]
        public string Humidity { get; set; }// 湿度
    }
}
