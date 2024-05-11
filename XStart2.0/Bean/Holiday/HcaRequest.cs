using Newtonsoft.Json;

namespace XStart2._0.Bean.Holiday {
    /// <summary>
    /// hca公共请求
    /// </summary>
    public abstract class HcaRequest {
        public const string ApiUrl = "http://hca-system.eicp.top/api/service/";
        [JsonProperty("loginname")]
        public string Loginname { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        public abstract string GetApiPath();
    }
}
