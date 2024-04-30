using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Seniverse {
    public class NowWeather {
        [JsonIgnore]
        public const string ApiPath = "/v3/weather/now.json";
        [JsonProperty("results")]
        public List<Result> Results { get; set; }


        public class Result {
            [JsonProperty("location")]
            public LocationBean Location { get; set; }
            [JsonProperty("now")]
            public NowBean Now { get; set; }
            [JsonProperty("last_update")]
            public string LastUpdate { get; set; }

        }

        /// <summary>
        /// "id": "C23NB62W20TF",
        ///    "name": "西雅图",
        ///    "country": "US",
        ///    "path": "西雅图,华盛顿州,美国",
        ///    "timezone": "America/Los_Angeles",
        ///    "timezone_offset": "-07:00"
        /// </summary>
        public class LocationBean {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("country")]
            public string Country { get; set; }
            [JsonProperty("path")]
            public string Path { get; set; }
            [JsonProperty("timezone")]
            public string Timezone { get; set; }
            [JsonProperty("timezone_offset")]
            public string TimezoneOffset { get; set; }
        }

        /// <summary>
        /// "text": "多云", //天气现象文字
        ///  "code": "4", //天气现象代码
        ///  "temperature": "14", //温度，单位为c摄氏度或f华氏度
        ///  "feels_like": "14", //体感温度，单位为c摄氏度或f华氏度
        ///  "pressure": "1018", //气压，单位为mb百帕或in英寸
        ///  "humidity": "76", //相对湿度，0~100，单位为百分比
        ///  "visibility": "16.09", //能见度，单位为km公里或mi英里
        ///  "wind_direction": "西北", //风向文字
        ///  "wind_direction_degree": "340", //风向角度，范围0~360，0为正北，90为正东，180为正南，270为正西
        ///    "wind_speed": "8.05", //风速，单位为km/h公里每小时或mph英里每小时
        ///    "wind_scale": "2", //风力等级，请参考：http://baike.baidu.com/view/465076.htm
        ///   "clouds": "90", //云量，单位%，范围0~100，天空被云覆盖的百分比 #目前不支持中国城市#
        ///   "dew_point": "-12" //露点温度，请参考：http://baike.baidu.com/view/118348.htm #目前不支持中国城市#
        /// </summary>
        public class NowBean {
            [JsonProperty("text")]
            public string Text { get; set; }
            [JsonProperty("code")]
            public string Code { get; set; }
            [JsonProperty("temperature")]
            public string Temperature { get; set; }
            [JsonProperty("feels_like")]
            public string FeelsLike { get; set; }
            [JsonProperty("pressure")]
            public string Pressure { get; set; }
            [JsonProperty("humidity")]
            public string Humidity { get; set; }
            [JsonProperty("visibility")]
            public string Visibility { get; set; }
            [JsonProperty("wind_direction")]
            public string WindDirection { get; set; }
            [JsonProperty("wind_direction_degree")]
            public string WindDirectionDegree { get; set; }
            [JsonProperty("wind_speed")]
            public string WindSpeed { get; set; }
            [JsonProperty("wind_scale")]
            public string WindScale { get; set; }
            [JsonProperty("clouds")]
            public string Clouds { get; set; }
            [JsonProperty("dew_point")]
            public string DewPoint { get; set; }
        }
    }
}
