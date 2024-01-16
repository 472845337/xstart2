using System.Collections.Generic;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {

    class AutoRunViewModel : BaseViewModel {
        // 是否启动时
        public bool IsStart { get; set; }
        public List<Project> AutoRunProjects { get; set; }

    }
}
