using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    class AutoRunViewModel : BaseViewModel{

        private List<Project> autoRunProjects;
        public List<Project> AutoRunProjects { get => autoRunProjects; set { autoRunProjects = value;OnPropertyChanged("AutoRunProjects"); } }

    }
}
