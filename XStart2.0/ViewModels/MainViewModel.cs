using PropertyChanged;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Services;
using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    public class MainViewModel : BaseViewModel {

        public MainViewModel() {
            MainHeight = 800;
            MainWidth = 450;
        }

        #region 用户数据
        [DoNotNotify]
        private string avatarPath;
        public string AvatarPath { get => avatarPath; set { avatarPath = value; SetAvatar(); } }
        private void SetAvatar() {
            Avatar = ImageUtils.File2BitmapImage(avatarPath);
        }
        // 头像
        public System.Windows.Media.Imaging.BitmapImage Avatar { get; set; }
        // 昵称
        public string NickName { get; set; }
        #endregion

        #region 时间数据
        public string CurrentDay { get; set; }
        public string CurrentTime { get; set; }
        public string CurrentWeekDay { get; set; }
        #endregion
        #region 应用数据
        public ObservableDictionary<string, Bean.Type> Types { get; set; }
        #endregion

        #region 窗口相关
        // 主窗口高度
        public double MainHeight { get; set; }
        // 主窗口宽度
        public double MainWidth { get; set; }
        // 主窗口左边位置
        public double MainLeft { get; set; }
        // 主窗口顶部位置
        public double MainTop { get; set; }
        // 类别名称是否展开
        [OnChangedMethod(nameof(ChangeTypeTab))]
        public bool TypeTabExpanded { get; set; }
        // 类别宽度（TabControl的TabItem的宽度）
        public double TypeWidth { get; set; }
        // 类别名称开关图标
        public string TypeTabToggleIcon { get; set; }

        public void ChangeTypeTab() {
            if (TypeTabExpanded) {
                TypeWidth = Constants.TYPE_EXPAND_WIDTH;
                TypeTabToggleIcon = FontAwesome6.Outdent;
            } else {
                TypeWidth = Constants.TYPE_COLLAPSE_WIDTH;
                TypeTabToggleIcon = FontAwesome6.Indent;
            }
        }
        #endregion

        #region 窗口相关设置
        public int SelectedIndex { get; set; }
        public double TabControlActualHeight { get; set; }
        public bool TopMost { get; set; }
        // 音效开关
        public bool Audio { get; set; }
        public bool AutoRun { get; set; }
        public bool ExitWarn { get; set; }
        public bool CloseBorderHide { get; set; }
        public string ClickType { get; set; }
        public string UrlOpen { get; set; }
        public string UrlOpenCustomBrowser { get; set; }
        [OnChangedMethod(nameof(ChangeIconSize))]
        public int IconSize { get; set; }
        [OnChangedMethod(nameof(ChangeColumnOrientation))]
        public string Orientation { get; set; }
        [OnChangedMethod(nameof(ChangeProjectHideTitle))]
        public bool HideTitle { get; set; }
        public bool OneLineMulti { get; set; }

        private void ChangeIconSize() {
            if (Config.Configs.inited) {
                foreach(var type in XStartService.TypeDic) {
                    foreach(var column in type.Value.ColumnDic) {
                        if(null == column.Value.IconSize) {
                            // 非自定义的栏目才修改图标大小
                            foreach(var project in column.Value.ProjectDic) {
                                project.Value.IconSize = IconSize;
                                project.Value.Icon = XStartService.GetIconImage(project.Value.Kind, project.Value.Path, project.Value.IconPath, IconSize);
                            }
                        }
                    }
                }
            }
        }

        private void ChangeColumnOrientation() {
            if (Config.Configs.inited) {
                foreach (var type in XStartService.TypeDic) {
                    foreach (var column in type.Value.ColumnDic) {
                        if (string.IsNullOrEmpty(column.Value.Orientation)) {
                            // 非自定义的栏目才修改图标大小
                            foreach (var project in column.Value.ProjectDic) {
                                project.Value.Orientation = Orientation;
                            }
                        }
                    }
                }
            }
        }

        private void ChangeProjectHideTitle() {
            if (Config.Configs.inited) {
                foreach (var type in XStartService.TypeDic) {
                    foreach (var column in type.Value.ColumnDic) {
                        if (null == column.Value.HideTitle) {
                            // 非自定义的栏目才重置是否显示标题
                            foreach (var project in column.Value.ProjectDic) {
                                project.Value.HideTitle = HideTitle;
                            }
                        }
                    }
                }
            }
        }


        #endregion
    }
}
