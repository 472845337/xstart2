using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Open {
    public class OpenAir : OpenWeather {
        public const string ApiPath = "/data/2.5/air_pollution";
        [JsonProperty("coord")]
        public CoordBean Coord { get; set; }
        [JsonProperty("list")]
        public List<Air> AirList { get; set; }

        public class Air {
            [JsonProperty("dt")]
            public long Dt { get; set; }
            [JsonProperty("main")]
            public MainBean Main { get; set; }
            [JsonProperty("components")]
            public ComponentsBean Components { get; set; }
        }

        public class MainBean {
            [JsonProperty("aqi")]
            public int Aqi { get; set; }
        }

        public class ComponentsBean {
            [JsonProperty("co")]
            public double Co { get; set; }
            [JsonProperty("no")]
            public double No { get; set; }
            [JsonProperty("no2")]
            public double No2 { get; set; }
            [JsonProperty("o3")]
            public double O3 { get; set; }
            [JsonProperty("so2")]
            public double So2 { get; set; }
            [JsonProperty("pm2_5")]
            public double Pm25 { get; set; }
            [JsonProperty("pm10")]
            public double Pm10 { get; set; }
            [JsonProperty("nh3")]
            public double Nh3 { get; set; }
        }
    }
}
