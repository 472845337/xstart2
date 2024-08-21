using PropertyChanged;
using System.Windows.Media.Imaging;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Services;

namespace XStart2._0.ViewModel {
    public class ProjectViewModel : BaseViewModel {
        public string Title { get; set; }
        [DoNotNotify]
        public string TypeSection { get; set; }
        [DoNotNotify]
        public string ColumnSection { get; set; }
        public string Section { get; set; }
        public string Name { get; set; }
        [OnChangedMethod(nameof(InitIcon))]
        public string Path { get; set; }
        public bool PathReadonly { get; set; } = false;
        public int? IconIndex { get; set; }
        [OnChangedMethod(nameof(InitIcon))]
        public string IconPath { get; set; }
        public string FontColor { get; set; }
        [OnChangedMethod(nameof(InitShowArguments))]
        public string Arguments { get; set; }
        public string ShowArguments { get; set; }
        // 参数是否可调
        public bool ArgumentsReadonly { get; set; } = false;
        public string RunStartPath { get; set; }
        public string HotKey { get; set; }

        public string Remark { get; set; }
        [OnChangedMethod(nameof(InitIcon))]
        public string Kind { get; set; }
        public BitmapImage Icon { get; set; }
        // 项目信息,用于图标显示以及数据回传
        public Project Project { get; set; }

        public void InitIcon() {
            if (!string.IsNullOrEmpty(Path) || !string.IsNullOrEmpty(IconPath)) {
                Icon = XStartService.GetIconImage(Kind, Path, IconPath, Constants.ICON_SIZE_32);
            }
        }

        private void InitShowArguments() {
            if (!string.IsNullOrEmpty(Arguments)) {
                if (SystemProjectParam.MSTSC.Equals(Path)) {
                    string[] argumentArray = Arguments.Split(Constants.SPLIT_CHAR);
                    ShowArguments = $"{argumentArray[0]}{Constants.SPLIT_CHAR}{argumentArray[1]}{Constants.SPLIT_CHAR}{argumentArray[2]}{Constants.SPLIT_CHAR}******";
                } else {
                    ShowArguments = Arguments;
                }
            }
        }
    }
}
