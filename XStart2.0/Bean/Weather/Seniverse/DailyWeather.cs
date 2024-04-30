using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Seniverse {
    public class DailyWeather {
        [JsonIgnore]
        public const string ApiPath = "/v3/weather/daily.json";
        [JsonProperty("results")]
        public List<Result> Results { get; set; }


        public class Result {
            [JsonProperty("location")]
            public LocationBean Location { get; set; }
            [JsonProperty("daily")]
            public List<DailyBean> Daily { get; set; }
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
        ///  "date": "2015-09-20",              //日期（该城市的本地时间）
        /// "text_day": "多云",                //白天天气现象文字
        /// "code_day": "4",                  //白天天气现象代码
        /// "text_night": "晴",               //晚间天气现象文字
        /// "code_night": "0",                //晚间天气现象代码
        /// "high": "26",                     //当天最高温度
        /// "low": "17",                      //当天最低温度
        /// "precip": "0",                    //降水概率，范围0~1，单位百分比（目前仅支持国外城市）
        /// "wind_direction": "",             //风向文字
        /// "wind_direction_degree": "255",   //风向角度，范围0~360
        /// "wind_speed": "9.66",             //风速，单位km/h（当unit=c时）、mph（当unit=f时）
        /// "wind_scale": "",                 //风力等级
        /// "rainfall": "0.0",                //降水量，单位mm
        /// "humidity": "76"                  //相对湿度，0~100，单位为百分比
        /// </summary>
        public class DailyBean {
            [JsonProperty("date")]
            public string Date { get; set; }
            [JsonProperty("text_day")]
            public string TextDay { get; set; }
            [JsonProperty("code_day")]
            public string CodeDay { get; set; }
            [JsonProperty("text_night")]
            public string TextNight { get; set; }
            [JsonProperty("code_night")]
            public string CodeNight { get; set; }
            [JsonProperty("high")]
            public string High { get; set; }
            [JsonProperty("low")]
            public string Low { get; set; }
            [JsonProperty("precip")]
            public string Precip { get; set; }
            [JsonProperty("wind_direction")]
            public string WindDirection { get; set; }
            [JsonProperty("wind_direction_degree")]
            public string WindDirectionDegree { get; set; }
            [JsonProperty("wind_speed")]
            public string WindSpeed { get; set; }
            [JsonProperty("wind_scale")]
            public string WindScale { get; set; }
            [JsonProperty("rainfall")]
            public string RainFall { get; set; }
            [JsonProperty("humidity")]
            public string Humidity { get; set; }
        }
    }
}
