using PropertyChanged;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class ProjectViewModel : BaseViewModel{
        public string Title { get; set; }
        [DoNotNotify]
        public string TypeSection { get; set; }
        [DoNotNotify]
        public string ColumnSection { get; set; }
        public string Section { get; set; }
        public string Name { get; set; }

        public string Path { get; set; }
        public bool PathEnable { get; set; } = true;
        public int? IconIndex { get; set; }
        public string IconPath { get; set; }
        public string FontColor { get; set; }
        public string Arguments { get; set; }
        // 参数是否可调
        public bool ArgumentsEnable { get; set; } = true;
        public string RunStartPath { get; set; }
        public string HotKey { get; set; }

        public string Remark { get; set; }

        public string Kind { get; set; }
        // 项目信息,用于图标显示以及数据回传
        public Project Project { get; set; }
    }
}
