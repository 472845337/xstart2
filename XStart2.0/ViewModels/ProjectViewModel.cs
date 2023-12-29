using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class ProjectViewModel : BaseViewModel{
        private string title;
        public string Title { get => title; set { title = value;OnPropertyChanged("Title"); } }
        public string TypeSection { get; set; }
        public string ColumnSection { get; set; }
        private string section;
        public string Section { get => section; set { section = value;OnPropertyChanged("Section"); } }
        private string name;
        public string Name { get => name; set { name = value;OnPropertyChanged("Name"); } }

        private string path;
        public string Path { get => path; set { path = value;OnPropertyChanged("Path"); } }
        private bool pathEnable = true;
        public bool PathEnable { get => pathEnable;set { pathEnable = value; OnPropertyChanged("PathEnable"); } }
        private int? iconIndex;
        public int? IconIndex { get => iconIndex; set { iconIndex = value;OnPropertyChanged("IconIndex"); } }
        private string iconPath;
        public string IconPath { get => iconPath; set { iconPath = value; OnPropertyChanged("IconPath"); } }
        private string fontColor;
        public string FontColor { get => fontColor; set { fontColor = value;OnPropertyChanged("FontColor"); } }
        private string arguments;
        public string Arguments { get => arguments; set { arguments = value; OnPropertyChanged("Arguments"); } }
        // 参数是否可调
        private bool argumentsEnable = true;
        public bool ArgumentsEnable { get => argumentsEnable; set { argumentsEnable = value;OnPropertyChanged("ArgumentsEnable"); } }
        private string runStartPath;
        public string RunStartPath { get => runStartPath; set { runStartPath = value; OnPropertyChanged("RunStartPath"); } }
        private string hotKey;
        public string HotKey { get => hotKey; set { hotKey = value; OnPropertyChanged("HotKey"); } }

        private string remark;
        public string Remark { get => remark; set { remark = value; OnPropertyChanged("Remark"); } }

        public string Kind { get; set; }
        // 项目信息,用于图标显示以及数据回传
        private Project project;
        public Project Project { get => project; set { project = value;OnPropertyChanged("Project"); } }
    }
}
