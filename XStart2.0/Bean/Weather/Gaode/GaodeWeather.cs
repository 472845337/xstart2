using Newtonsoft.Json;
using System.Collections.Generic;


namespace XStart2._0.Bean.Weather.Gaode {
    /// <summary>
    /// 高德天气对象
    /// </summary>
    public class GaodeWeather {
        public const int STATUS_SUCCESS = 1;
        public const int STATUS_FAIL = 0;
        [JsonIgnore]
        public const string ApiPath = "/v3/weather/weatherInfo";
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("info")]
        public string Info { get; set; }
        [JsonProperty("infocode")]
        public string InfoCode { get; set; }
        [JsonProperty("lives")]
        public List<Live> Lives { get; set; }
        [JsonProperty("forecasts")]
        public List<Forecast> Forecasts { get; set; }
        public class Live {
            [JsonProperty("province")]
            public string Province { get; set; }
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("adcode")]
            public string AdCode { get; set; }
            [JsonProperty("weather")]
            public string Weather { get; set; }
            [JsonProperty("temperature")]
            public string Temperature { get; set; }
            [JsonProperty("winddirection")]
            public string WindDirection { get; set; }
            [JsonProperty("windpower")]
            public string WindPower { get; set; }
            [JsonProperty("humidity")]
            public string Humidity { get; set; }
            [JsonProperty("reporttime")]
            public string ReportTime { get; set; }
        }

        public class Forecast {
            [JsonProperty("city")]
            public string City { get; set; }
            [JsonProperty("adcode")]
            public string AdCode { get; set; }
            [JsonProperty("province")]
            public string Province { get; set; }
            [JsonProperty("reporttime")]
            public string ReportTime { get; set; }
            [JsonProperty("casts")]
            public List<Cast> Casts { get; set; }
        }

        public class Cast {
            [JsonProperty("date")]
            public string Date { get; set; }
            [JsonProperty("week")]
            public string Week { get; set; }
            [JsonProperty("dayweather")]
            public string DayWeather { get; set; }
            [JsonProperty("nightweather")]
            public string NightWeather { get; set; }
            [JsonProperty("daytemp")]
            public string DayTemp { get; set; }
            [JsonProperty("nighttemp")]
            public string NightTemp { get; set; }
            [JsonProperty("daywind")]
            public string DayWind { get; set; }
            [JsonProperty("nightwind")]
            public string NightWind { get; set; }
            [JsonProperty("daypower")]
            public string DayPower { get; set; }
            [JsonProperty("nightpower")]
            public string NightPower { get; set; }
        }
    }
}
