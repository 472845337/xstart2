using System.Collections.Generic;

namespace XStart2._0.Bean.Weather.Accu {
    public class AccuForecasts : AccuWeather {
        public const string AQI_NAME = "AirQuality";
        public const string ApiPath_1Day = "/forecasts/v1/daily/1day/";// 1日天气预报，用于获取空气质量
        public const string ApiPath_5Day = "/forecasts/v1/daily/5day/";// 5日天气预报
        public HeadlineBean Headline { get; set; }

        public List<DailyForecast> DailyForecasts { get; set; }
        public class HeadlineBean {
            public string EffectiveDate { get; set; }
            public long EffectiveEpochDate { get; set; }
            public int Severity { get; set; }
            public string Text { get; set; }
            public string Category { get; set; }
            public string EndDate { get; set; }
            public long? EndEpochDate { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }


        }

        public class DailyForecast {
            public string Date { get; set; }
            public long EpochDate { get; set; }
            public TemperatureRangeBeanNoUnit Temperature { get; set; }
            public List<AirAndPollenBean> AirAndPollen { get; set; }
            public DayBean Day { get; set; }
            public DayBean Night { get; set; }
        }

        public class AirAndPollenBean {
            public string Name { get; set; }
            public int Value { get; set; }
            public string Category { get; set; }
            public int CategoryValue { get; set; }
            public string Type { get; set; }
        }

        public class DayBean {
            public int Icon { get; set; }
            public string IconPhrase { get; set; }
            public LocalSourceBean LocalSource { get; set; }
            public bool HasPrecipitation { get; set; }
            public string PrecipitationType { get; set; }
            public string PrecipitationIntensity { get; set; }
            public string ShortPhrase { get; set; }
            public string LongPhrase { get; set; }
            public int PrecipitationProbability { get; set; }
            public int ThunderstormProbability { get; set; }
            public int RainProbability { get; set; }
            public int SnowProbability { get; set; }
            public int IceProbability { get; set; }
            public WindBeanNoUnit Wind { get; set; }
            public WindBeanNoUnit WindGust { get; set; }
            public ValueBean TotalLiquid { get; set; }
            public ValueBean Rain { get; set; }
            public ValueBean Snow { get; set; }
            public ValueBean Ice { get; set; }
            public float HoursOfPrecipitation { get; set; }
            public float HoursOfRain { get; set; }
            public int CloudCover { get; set; }
            public ValueBean Evapotranspiration { get; set; }
            public ValueBean SolarIrradiance { get; set; }
        }

        public class LocalSourceBean {
            public int Id { get; set; }
            public string Name { get; set; }
            public string WeatherCode { get; set; }
        }
    }
}
