using System.Collections.Generic;

namespace XStart2._0.Bean.Weather {
    // 省
    class Province {
        public string En { get; set; }
        public string Zh { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
    }

    // 市
    class City {
        public string En { get; set; }
        public string Zh { get; set; }
        public List<Country> Countries { get; set; } = new List<Country>();
    }

    // 区县
    class Country {
        public string Id { get; set; }
        public string En { get; set; }
        public string Zh { get; set; }
        public string ProvinceEn { get; set; }
        public string ProvinceZh { get; set; }
        public string LeaderEn { get; set; }
        public string LeaderZh { get; set; }

        public string FullName { get { return $"{ProvinceZh} {LeaderZh} {Zh}"; } }
    }
}
