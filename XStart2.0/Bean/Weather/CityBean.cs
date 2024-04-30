using Newtonsoft.Json;

namespace XStart2._0.Bean.Weather {
    public class CityBean {
        // 数据ID(目前仅用于易客云)
        [JsonProperty("id")]
        public string Id { get; set; }
        // 区域编码
        [JsonProperty("areaCode")]
        public string AreaCode { get; set; }
        // 区县英文名
        [JsonProperty("cityEn")]
        public string CityEn { get; set; }
        // 区县中文名
        [JsonProperty("cityZh")]
        public string CityZh { get; set; }
        // 省英文名
        [JsonProperty("provinceEn")]
        public string ProvinceEn { get; set; }
        // 省中文名
        [JsonProperty("provinceZh")]
        public string ProvinceZh { get; set; }
        // 市英文名
        [JsonProperty("leaderEn")]
        public string LeaderEn { get; set; }
        // 市中文名
        [JsonProperty("leaderZh")]
        public string LeaderZh { get; set; }
        // 纬度
        [JsonProperty("lat")]
        public string Lat { get; set; }
        // 经度
        [JsonProperty("lon")]
        public string Lon { get; set; }
    }
}
