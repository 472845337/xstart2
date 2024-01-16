using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Bean {
    [AddINotifyPropertyChangedInterface]
    class Province {
        public int Id { get; set; }
        public string Name { get; set; }

        public ObservableCollection<City> Cities { get; set; }
    }
}
