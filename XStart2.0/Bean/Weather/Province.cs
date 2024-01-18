using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Bean.Weather {
    class Province {
        public string En { get; set; }
        public string Zh { get; set; }

        public List<City> Cities { get; set; } = new List<City>();
    }
}
