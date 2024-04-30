using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Open {
    public class OpenWeather {
        public const int COD_SUCCESS = 200;

        [JsonProperty("cod")]
        public int Cod { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }

        public class CoordBean {
            [JsonProperty("lon")]
            public float Lon { get; set; }
            [JsonProperty("lat")]
            public float Lat { get; set; }
        }
        public class FeelsLikeBean {
            [JsonProperty("day")]
            public float Day { get; set; }
            [JsonProperty("night")]
            public float Night { get; set; }
            [JsonProperty("eve")]
            public float Eve { get; set; }
            [JsonProperty("morn")]
            public float Morn { get; set; }
        }

        public class BaseWeather {
            [JsonProperty("dt")]
            public long Dt { get; set; }// 当前时间
            [JsonProperty("sunrise")]
            public long Sunrise { get; set; }// 日出时间
            [JsonProperty("sunset")]
            public long Sunset { get; set; }// 日落时间
            [JsonProperty("pressure")]
            public int Pressure { get; set; }// 压强
            [JsonProperty("humidity")]
            public int Humidity { get; set; }// 温度
            [JsonProperty("dew_point")]
            public float DewPoint { get; set; }// 冰点温度
            [JsonProperty("uvi")]
            public float Uvi { get; set; }
            [JsonProperty("clouds")]
            public int Clouds { get; set; }// 云量
            [JsonProperty("weather")]
            public List<WeatherBean> Weather { get; set; }
        }

        public class WeatherBean {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("main")]
            public string Main { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        public class TempBean {
            [JsonProperty("day")]
            public float Day { get; set; }
            [JsonProperty("min")]
            public float Min { get; set; }
            [JsonProperty("max")]
            public float Max { get; set; }
            [JsonProperty("night")]
            public float Night { get; set; }
            [JsonProperty("eve")]
            public float Eve { get; set; }
            [JsonProperty("morn")]
            public float Morn { get; set; }
        }
    }
}
