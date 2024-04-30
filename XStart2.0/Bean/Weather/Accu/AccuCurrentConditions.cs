
namespace XStart2._0.Bean.Weather.Accu {
    public class AccuCurrentConditions : AccuWeather {
        public const string ApiPath = "/currentconditions/v1/";
        public string LocalObservationDateTime { get; set; }
        public long EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public bool IsDayTime { get; set; }
        public LocalSourceBean LocalSource { get; set; }
        public TemperatureBean Temperature { get; set; }
        public class LocalSourceBean {
            public int Id { get; set; }
            public string Name { get; set; }
            public string WeatherCode { get; set; }
        }

        public class TemperatureBean {
            public UnitBean Metric { get; set; }
            public UnitBean Imperial { get; set; }
        }
    }
}
