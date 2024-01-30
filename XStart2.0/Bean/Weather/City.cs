using Newtonsoft.Json;

namespace XStart2._0.Bean.Weather {
    class City {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("cityEn")]
        public string CityEn { get; set; }
        [JsonProperty("cityZh")]
        public string CityZh { get; set; }
        [JsonProperty("provinceEn")]
        public string ProvinceEn { get; set; }
        [JsonProperty("provinceZh")]
        public string ProvinceZh { get; set; }
        [JsonProperty("leaderEn")]
        public string LeaderEn { get; set; }
        [JsonProperty("leaderZh")]
        public string LeaderZh { get; set; }
        [JsonProperty("lat")]
        public string Lat { get; set; }
        [JsonProperty("lon")]
        public string Lon { get; set; }

        public string FullName { get { return ProvinceZh + "  " + CityZh; } }
    }
}
