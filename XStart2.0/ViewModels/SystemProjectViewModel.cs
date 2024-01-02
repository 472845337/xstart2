using PropertyChanged;
using System.Collections.Generic;

using System.Windows.Media.Imaging;


namespace XStart2._0.ViewModels {
    public class SystemProjectViewModel : BaseViewModel {
        // 初始化时赋值
        [DoNotNotify]
        public string TypeSection { get; set; }
        // 初始化时赋值
        [DoNotNotify]
        public string ColumnSection { get; set; }

        public List<SystemProject> SystemLinks { get; set; }
        public List<SystemProject> SystemOperates { get; set; }
        public List<SystemProject> SystemAudioNormals { get; set; }
        public List<SystemProject> SystemAudioWaves { get; set; }
        public List<SystemProject> SystemAudioMics { get; set; }
        public List<SystemProject> SystemAudioLines { get; set; }
        public List<SystemProject> SystemAudioCdRoms { get; set; }
        public List<SystemProject> SystemControls { get; set; }
        public bool MultiAdd { get; set; }
        public int OpenPage { get; set; }

    }

    public class SystemProject {
        public string Name { get; set; }
        public string Content { get; set; }
        public BitmapImage Image { get; set; }
    }
}
