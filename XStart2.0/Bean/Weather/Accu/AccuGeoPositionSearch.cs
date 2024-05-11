using System;

namespace XStart2._0.Bean.Weather.Accu {
    /// <summary>
    /// 根据经纬度获取城市ID
    /// </summary>
    public class AccuGeoPositionSearch : AccuWeather {
        public const string ApiPath = "/locations/v1/cities/geoposition/search";
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public string PrimaryPostalCode { get; set; }

        public RegionBean Region { get; set; }

        public CountryBean Country { get; set; }

        public AdministrativeAreaBean AdministrativeArea { get; set; }
        public TimeZoneBean TimeZone { get; set; }
        public GeoPositionBean GeoPosition { get; set; }
        public bool IsAlias { get; set; }
        public class RegionBean {
            public string ID { get; set; }
            public string LocalizedName { get; set; }
            public string EnglishName { get; set; }
        }

        public class CountryBean {
            public string ID { get; set; }
            public string LocalizedName { get; set; }
            public string EnglishName { get; set; }
        }

        public class AdministrativeAreaBean {
            public string ID { get; set; }
            public string LocalizedName { get; set; }
            public string EnglishName { get; set; }
            public int Level { get; set; }
            public string LocalizedType { get; set; }
            public string EnglishType { get; set; }
            public string CountryID { get; set; }
        }
        public class TimeZoneBean {
            public string Code { get; set; }
            public string Name { get; set; }
            public float GmtOffset { get; set; }
            public bool IsDaylightSaving { get; set; }
            public DateTime? NextOffsetChange { get; set; }
        }

        public class GeoPositionBean {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public UnitBean Elevation { get; set; }
        }
    }
}
