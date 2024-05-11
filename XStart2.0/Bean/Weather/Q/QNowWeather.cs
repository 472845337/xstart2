using Newtonsoft.Json;

namespace XStart2._0.Bean.Weather.Q {
    public class QNowWeather : QBaseWeather {
        public const string ApiPath = "/v7/weather/now";

        [JsonProperty("now")]
        public NowBean Now { get; set; }

        public class NowBean {
            [JsonProperty("obsTime")]
            public string ObsTime { get; set; }
            [JsonProperty("temp")]
            public string Temp { get; set; }
            [JsonProperty("feelsLike")]
            public string FeelsLike { get; set; }
            [JsonProperty("text")]
            public string Text { get; set; }
            [JsonProperty("wind360")]
            public string Wind360 { get; set; }
            [JsonProperty("windDir")]
            public string WindDir { get; set; }
            [JsonProperty("windScale")]
            public string WindScale { get; set; }
            [JsonProperty("windSpeed")]
            public string WindSpeed { get; set; }
            [JsonProperty("humidity")]
            public string Humidity { get; set; }
            [JsonProperty("precip")]
            public string Precip { get; set; }
            [JsonProperty("pressure")]
            public string Pressure { get; set; }
            [JsonProperty("cloud")]
            public string Cloud { get; set; }
            [JsonProperty("vis")]
            public string Vis { get; set; }
            [JsonProperty("dew")]
            public string Dew { get; set; }
        }
    }
}
