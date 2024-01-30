using System.Collections.Generic;

namespace XStart2._0.Bean.Weather {
    class Province {
        public string En { get; set; }
        public string Zh { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
    }
}
