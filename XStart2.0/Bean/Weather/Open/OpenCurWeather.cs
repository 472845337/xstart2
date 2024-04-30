using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Open {
    public class OpenCurWeather : OpenWeather {
        public const string ApiPath = "/data/2.5/weather";
        public CoordBean coord { get; set; }
        public List<WeatherBean> Weather { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("main")]
        public MainBean Main { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }// 能见度
        [JsonProperty("wind")]
        public WindBean Wind { get; set; }
        [JsonProperty("dt")]
        public long Dt { get; set; }
        public class MainBean {
            [JsonProperty("temp")]
            public float Temp { get; set; }// 温度
            [JsonProperty("feels_like")]
            public float FeelsLike { get; set; }// 体感温度
            [JsonProperty("temp_min")]
            public float TempMin { get; set; }// 最低温
            [JsonProperty("temp_max")]
            public float TempMax { get; set; }// 最高温
            [JsonProperty("pressure")]
            public int Pressure { get; set; }// 压强
            [JsonProperty("humidity")]
            public int Humidity { get; set; }// 温度
            [JsonProperty("SeaLevel")]
            public int SeaLevel { get; set; }// 海平面
            [JsonProperty("grnd_level")]
            public int GrndLevel { get; set; }// 地平面
        }
        public class WindBean {
            [JsonProperty("speed")]
            public float Speed { get; set; }
            [JsonProperty("deg")]
            public int Deg { get; set; }
            [JsonProperty("gust")]
            public float Gust { get; set; }
        }
    }
}
