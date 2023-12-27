using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.ViewModels {
    public class ProjectViewModel : BaseViewModel{
        private string title;
        public string Title { get => title; set { title = value;OnPropertyChanged("Title"); } }
    }
}
