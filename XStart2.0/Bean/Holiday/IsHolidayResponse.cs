using Newtonsoft.Json;

namespace XStart2._0.Bean.Holiday {
    /// <summary>
    /// 是否节假日反馈
    /// </summary>
    class IsHolidayResponse : HcaResponse {
        /// <summary>
        /// true or false
        /// </summary>
        [JsonProperty("isHoliday")]
        public bool? IsHoliday { get; set; }
        /// <summary>
        /// holiday name 春节
        /// </summary>
        [JsonProperty("holidayName")]
        public string HolidayName { get; set; }
    }
}
