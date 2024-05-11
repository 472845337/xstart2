using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Q {
    public class QAir : QBaseWeather {
        public const string ApiPath = "/v7/air/now";
        [JsonProperty("now")]
        public Air Now { get; set; }
        [JsonProperty("station")]
        public List<StationBean> Station { get; set; }

        public class Air {
            [JsonProperty("pubTime")]
            public string PubTime { get; set; }
            [JsonProperty("aqi")]
            public string Aqi { get; set; }
            [JsonProperty("level")]
            public string Level { get; set; }
            [JsonProperty("category")]
            public string Category { get; set; }
            [JsonProperty("primary")]
            public string Primary { get; set; }
            [JsonProperty("pm10")]
            public string Pm10 { get; set; }
            [JsonProperty("pm2p5")]
            public string Pm2p5 { get; set; }
            [JsonProperty("no2")]
            public string No2 { get; set; }
            [JsonProperty("so2")]
            public string So2 { get; set; }
            [JsonProperty("co")]
            public string Co { get; set; }
            [JsonProperty("o3")]
            public string O3 { get; set; }
        }

        public class StationBean : Air {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("id")]
            public string Id { get; set; }

        }
    }
}
