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
    public class VcCurrentAndForecasts : VcBase{
        public const string ApiPath = "/VisualCrossingWebServices/rest/services/timeline/";

        public long queryCost { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public float tzoffset { get; set; }

        public List<Day> days { get; set; }

        public CurrentConditionsBean currentConditions { get; set; }

        public class Day {
            public string datetime { get; set; }
            public long datetimeEpoch { get; set; }
            public float tempmax { get; set; }
            public float tempmin { get; set; }
            public float temp { get; set; }
            public float humidity { get; set; }
            public float windspeed { get; set; }
            public float winddir { get; set; }
            public float pressure { get; set; }
            public string conditions { get; set; }
            public string icon { get; set; }
        }

        public class CurrentConditionsBean {
            public string datetime { get; set; }
            public float temp { get; set; }
            public float humidity { get; set; }
            public float windgust { get; set; }
            public float windspeed { get; set; }
            public float winddir { get; set; }
            public float pressure { get; set; }
            public float visibility { get; set; }
            public string conditions { get; set; }
        }
    }
}
