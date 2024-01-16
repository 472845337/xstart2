using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Bean {
    [AddINotifyPropertyChangedInterface]
    class City {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
