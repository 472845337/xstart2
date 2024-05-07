
namespace XStart2._0.Bean.Weather.Accu {
    public class AccuCurrentConditions : AccuWeather {
        public const string ApiPath = "/currentconditions/v1/";
        public string LocalObservationDateTime { get; set; }
        public long EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool IsDayTime { get; set; }
        public LocalSourceBean LocalSource { get; set; }
        public UnitBean Temperature { get; set; }
        // 体感温度
        public UnitBean RealFeelTemperature { get; set; }
        public UnitBean RealFeelTemperatureShade { get; set; }
        public int? RelativeHumidity { get; set; }
        public int? IndoorRelativeHumidity { get; set; }
        // 露点
        public UnitBean DewPoint { get; set; }
        public WindBean Wind { get; set; }
        public WindBean WindGust { get; set; }
        public int? UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public UnitBean Visibility { get; set; }
        public string ObstructionsToVisibility { get; set; }
        public int? CloudCover { get; set; }
        public UnitBean Ceiling { get; set; }
        public UnitBean Pressure { get; set; }
        public PressureTendencyBean PressureTendency { get; set; }
        public UnitBean Past24HourTemperatureDeparture { get; set; }
        public UnitBean ApparentTemperature { get; set; }
        public UnitBean WindChillTemperature { get; set; }
        public UnitBean WetBulbTemperature { get; set; }
        public UnitBean WetBulbGlobeTemperature { get; set; }
        public UnitBean Precip1hr { get; set; }
        public PrecipitationSummaryBean PrecipitationSummary { get; set; }
        public TemperatureSummaryBean TemperatureSummary { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
        public class LocalSourceBean {
            public int Id { get; set; }
            public string Name { get; set; }
            public string WeatherCode { get; set; }
        }

        public class PressureTendencyBean {
            public string LocalizedText { get; set; }
            public string Code { get; set; }
        }

        public class PrecipitationSummaryBean {
            public UnitBean Precipitation { get; set; }
            public UnitBean PastHour { get; set; }
            public UnitBean Past3Hours { get; set; }
            public UnitBean Past6Hours { get; set; }
            public UnitBean Past9Hours { get; set; }
            public UnitBean Past12Hours { get; set; }
            public UnitBean Past18Hours { get; set; }
            public UnitBean Past24Hours { get; set; }
        }

        public class TemperatureSummaryBean {
            public TemperatureRangeBean Past6HourRange { get; set; }
            public TemperatureRangeBean Past12HourRange { get; set; }
            public TemperatureRangeBean Past24HourRange { get; set; }
        }
    }
}
