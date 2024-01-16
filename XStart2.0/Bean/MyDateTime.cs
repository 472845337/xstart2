using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.Bean {
    [AddINotifyPropertyChangedInterface]
    public class MyDateTime {
        public string CurDate { get; set; }
        public string CurTime { get; set; }
        public string CurWeekDay { get; set; }
    }
}
