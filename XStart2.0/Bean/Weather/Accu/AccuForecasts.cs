using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Bean.Weather.Accu {
    public class AccuForecasts : AccuWeather{
        public const string ApiPath = "/forecasts/v1/daily/5day/";
        public HeadlineBean Headline { get; set; }

        public List<DailyForecast> DailyForecasts { get; set; }
        public class HeadlineBean {
            public string EffectiveDate { get; set; }
            public long EffectiveEpochDate { get; set; }
            public int Severity { get; set; }
            public string Text { get; set; }
            public string Category { get; set; }
            public string EndDate { get; set; }
            public long EndEpochDate { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }


        }

        public class DailyForecast {
            public string Date { get; set; }
            public long EpochDate { get; set; }
            public TemperatureBean Temperature { get; set; }
            public List<AirAndPollenBean> AirAndPollen { get; set; }
            public DayBean Day { get; set; }
            public DayBean Night { get; set; }
        }

        public class TemperatureBean {
            public UnitBean Minimum { get; set; }
            public UnitBean Maximum { get; set; }
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
            public WindBean Wind { get; set; }
            public WindBean WindGust { get; set; }
            public UnitBean TotalLiquid { get; set; }
            public UnitBean Rain { get; set; }
            public UnitBean Snow { get; set; }
            public UnitBean Ice { get; set; }
            public float HoursOfPrecipitation { get; set; }
            public float HoursOfRain { get; set; }
            public int CloudCover { get; set; }
            public UnitBean Evapotranspiration { get; set; }
            public UnitBean SolarIrradiance { get; set; }
        }

        public class LocalSourceBean {
            public int Id { get; set; }
            public string Name { get; set; }
            public string WeatherCode { get; set; }
        }
        public class WindBean {
            public UnitBean Speed { get; set; }

            public DirectionBean Direction { get; set; }
        }

        public class DirectionBean {
            public double Degrees { get; set; }
            public string Localized { get; set; }
            public string English { get; set; }
        }
    }
}
