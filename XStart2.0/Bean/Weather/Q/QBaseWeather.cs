using Newtonsoft.Json;

namespace XStart2._0.Bean.Weather.Q {
    public class QBaseWeather {
        public const string CODE_SUCCESS = "200";

        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }
        [JsonProperty("updateTime")]
        public string UpdateTime { get; set; }
        [JsonProperty("fxLink")]
        public string FxLink { get; set; }
    }
}
