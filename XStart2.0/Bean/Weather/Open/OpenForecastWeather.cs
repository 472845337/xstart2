using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Open {
    public class OpenForecastWeather : OpenWeather {
        public const string ApiPath = "/data/2.5/forecast/daily";
        [JsonProperty("city")]
        public CityBean City { get; set; }
        [JsonProperty("message")]
        public float Message { get; set; }
        [JsonProperty("cnt")]
        public int Cnt { get; set; }
        [JsonProperty("list")]
        public List<Forecast> Casts { get; set; }
        public class CityBean {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("city")]
            public CoordBean Coord { get; set; }
            public string country { get; set; }
            public long population { get; set; }
            public int timezone { get; set; }
        }

        public class Forecast {
            [JsonProperty("dt")]
            public long Dt { get; set; }// 当前时间
            [JsonProperty("sunrise")]
            public long Sunrise { get; set; }// 日出时间
            [JsonProperty("sunset")]
            public long Sunset { get; set; }// 日落时间
            [JsonProperty("temp")]
            public TempBean Temp { get; set; }
            [JsonProperty("feels_like")]
            public FeelsLikeBean FeelsLike { get; set; }
            [JsonProperty("pressure")]
            public int Pressure { get; set; }// 压强
            [JsonProperty("humidity")]
            public int Humidity { get; set; }// 温度
            [JsonProperty("weather")]
            public List<WeatherBean> Weather { get; set; }
            [JsonProperty("speed")]
            public float Speed { get; set; }// 风速 m/s
            [JsonProperty("deg")]
            public int Deg { get; set; }// 风向
            [JsonProperty("gust")]
            public float Gust { get; set; }// 阵风
            [JsonProperty("clouds")]
            public int Clouds { get; set; }// 云层
            [JsonProperty("pop")]
            public float Pop { get; set; }// 降水概率
            [JsonProperty("rain")]
            public float Rain { get; set; }// 降雨量
        }
    }
}
