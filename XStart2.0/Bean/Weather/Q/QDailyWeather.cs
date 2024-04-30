using Newtonsoft.Json;
using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Q {
    public class QDailyWeather : QBaseWeather {
        public const string ApiPath = "/v7/weather/7d";

        public List<DailyBean> Daily { get; set; }

        public class DailyBean {
            [JsonProperty("fxDate")]
            public string FxDate { get; set; }
            [JsonProperty("sunrise")]
            public string Sunrise { get; set; }
            [JsonProperty("sunset")]
            public string Sunset { get; set; }
            [JsonProperty("moonrise")]
            public string Moonrise { get; set; }
            [JsonProperty("moonset")]
            public string Moonset { get; set; }
            [JsonProperty("moonPhase")]
            public string MoonPhase { get; set; }
            [JsonProperty("moonPhaseIcon")]
            public string MoonPhaseIcon { get; set; }
            [JsonProperty("tempMax")]
            public string TempMax { get; set; }
            [JsonProperty("tempMin")]
            public string TempMin { get; set; }
            [JsonProperty("iconDay")]
            public string IconDay { get; set; }
            [JsonProperty("textDay")]
            public string TextDay { get; set; }
            [JsonProperty("iconNight")]
            public string IconNight { get; set; }
            [JsonProperty("textNight")]
            public string TextNight { get; set; }
            [JsonProperty("wind360Day")]
            public string Wind360Day { get; set; }
            [JsonProperty("windDirDay")]
            public string WindDirDay { get; set; }
            [JsonProperty("windScaleDay")]
            public string WindScaleDay { get; set; }
            [JsonProperty("windSpeedDay")]
            public string WindSpeedDay { get; set; }
            [JsonProperty("wind360Night")]
            public string Wind360Night { get; set; }
            [JsonProperty("windDirNight")]
            public string WindDirNight { get; set; }
            [JsonProperty("windScaleNight")]
            public string WindScaleNight { get; set; }
            [JsonProperty("windSpeedNight")]
            public string WindSpeedNight { get; set; }
            [JsonProperty("humidity")]
            public string Humidity { get; set; }
            [JsonProperty("precip")]
            public string Precip { get; set; }
            [JsonProperty("pressure")]
            public string Pressure { get; set; }
            [JsonProperty("vis")]
            public string Vis { get; set; }
            [JsonProperty("cloud")]
            public string Cloud { get; set; }
            [JsonProperty("uvIndex")]
            public string UvIndex { get; set; }
        }
    }
}
