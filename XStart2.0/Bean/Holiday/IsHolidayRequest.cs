using Newtonsoft.Json;

namespace XStart2._0.Bean.Holiday {
    /// <summary>
    /// 是否节假日请求
    /// </summary>
    public class IsHolidayRequest : HcaRequest {
        public const string ApiPath = "is_holiday";
        /// <summary>
        /// 日期
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        public override string GetApiPath() {
            return ApiPath;
        }
    }
}
