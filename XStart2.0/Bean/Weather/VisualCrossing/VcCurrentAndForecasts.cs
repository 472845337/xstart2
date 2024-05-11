using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.VisualCrossing {
    /// <summary>
    /// 获取当前的天气信息以及未来7天
    /// https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/
    /// 34.757975,113.665412/2024-05-08/2024-05-15?
    /// key={key}
    /// &unitGroup=metric
    /// &include=current
    /// &lang=zh
    /// </summary>
    public class VcCurrentAndForecasts : VcBase {
        public const string ApiPath = "/VisualCrossingWebServices/rest/services/timeline/";
        [JsonProperty("queryCost")]
        public long QueryCost { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("tzoffset")]
        public float TzOffset { get; set; }
        [JsonProperty("days")]

        public List<Day> Days { get; set; }
        [JsonProperty("currentConditions")]
        public CurrentConditionsBean CurrentConditions { get; set; }

        public class Day {
            [JsonProperty("datetime")]
            public string DateTime { get; set; }
            [JsonProperty("datetimeEpoch")]
            public long DateTimeEpoch { get; set; }
            [JsonProperty("tempmax")]
            public float TempMax { get; set; }
            [JsonProperty("tempmin")]
            public float TempMin { get; set; }
            [JsonProperty("temp")]
            public float Temp { get; set; }
            [JsonProperty("humidity")]
            public float Humidity { get; set; }
            [JsonProperty("windspeed")]
            public float WindSpeed { get; set; }
            [JsonProperty("winddir")]
            public float WindDir { get; set; }
            [JsonProperty("pressure")]
            public float Pressure { get; set; }
            [JsonProperty("conditions")]
            public string Conditions { get; set; }
            [JsonProperty("icon")]
            public string Icon { get; set; }
        }

        public class CurrentConditionsBean {
            [JsonProperty("datetime")]
            public string DateTime { get; set; }
            [JsonProperty("temp")]
            public float Temp { get; set; }
            [JsonProperty("humidity")]
            public float Humidity { get; set; }
            [JsonProperty("windgust")]
            public float WindGust { get; set; }
            [JsonProperty("windspeed")]
            public float WindSpeed { get; set; }
            [JsonProperty("winddir")]
            public float WindDir { get; set; }
            [JsonProperty("pressure")]
            public float Pressure { get; set; }
            [JsonProperty("visibility")]
            public float Visibility { get; set; }
            [JsonProperty("conditions")]
            public string Conditions { get; set; }
        }
    }
}
