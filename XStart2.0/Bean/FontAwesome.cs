using XStart2._0.Const;

namespace XStart2._0.Bean {
    public class FontAwesome {
        public string Name { get; set; }
        public string Value { get; set; }
        // 字体，查询的时候该字段有用 匹配Constants.Font_family_xxxx
        public string FfName { get; set; }

        public string FaName { get; set; } = Constants.FA_NAME_SOLID;
    }
}
