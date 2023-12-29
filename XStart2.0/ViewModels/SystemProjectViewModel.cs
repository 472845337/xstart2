using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    public class SystemProjectViewModel : BaseViewModel{
        // 初始化时赋值
        public string TypeSection { get; set; }
        // 初始化时赋值
        public string ColumnSection { get; set; }

        private List<SystemProject> systemLinks;
        public List<SystemProject> SystemLinks { get => systemLinks;set { systemLinks = value; OnPropertyChanged("SystemLinks"); } }
        private List<SystemProject> systemOperates;
        public List<SystemProject> SystemOperates { get => systemOperates; set { systemOperates = value; OnPropertyChanged("SystemOperates"); } }
        private List<SystemProject> systemAudioNormals;
        public List<SystemProject> SystemAudioNormals { get => systemAudioNormals;set { systemAudioNormals = value; OnPropertyChanged("SystemAudioNormals"); } }
        private List<SystemProject> systemAudioWaves;
        public List<SystemProject> SystemAudioWaves { get => systemAudioWaves; set { systemAudioWaves = value; OnPropertyChanged("SystemAudioWaves"); } }
        private List<SystemProject> systemAudioMics;
        public List<SystemProject> SystemAudioMics { get => systemAudioMics; set { systemAudioMics = value; OnPropertyChanged("SystemAudioMics"); } }
        private List<SystemProject> systemAudioLines;
        public List<SystemProject> SystemAudioLines { get => systemAudioLines; set { systemAudioLines = value; OnPropertyChanged("SystemAudioLines"); } }
        private List<SystemProject> systemAudioCdRoms;
        public List<SystemProject> SystemAudioCdRoms { get => systemAudioCdRoms; set { systemAudioCdRoms = value; OnPropertyChanged("SystemAudioCdRoms"); } }
        private List<SystemProject> systemControls;
        public List<SystemProject> SystemControls { get => systemControls; set { systemControls = value; OnPropertyChanged("SystemControls"); } }
        private bool multiAdd;
        public bool MultiAdd { get => multiAdd; set { multiAdd = value;OnPropertyChanged("MultiAdd"); } }
        private int openPage;
        public int OpenPage { get => openPage; set { openPage = value; OnPropertyChanged("OpenPage");  } }

    }

    public class SystemProject {
        public string Name { get; set; }
        public string Content { get; set; }
        public BitmapImage Image { get; set; }
    }
}
