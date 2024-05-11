using Newtonsoft.Json;

namespace XStart2._0.Bean.Holiday {
    class HcaResponse {
        public const string CODE_SUCCESS = "1";
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
