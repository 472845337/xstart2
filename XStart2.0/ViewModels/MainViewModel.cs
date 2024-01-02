using PropertyChanged;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Services;

namespace XStart2._0.ViewModels {
    public class MainViewModel : BaseViewModel {
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService projectService = ProjectService.Instance;

        public MainViewModel() {
            MainHeight = 800;
            MainWidth = 450;
            // 计算初始化信息
            Avatar = "/Files/Images/Avatar/DefaultUser.png";
            NickName = "昵称";
        }

        #region 用户数据
        // 头像
        public string Avatar { get; set; }
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
                TypeTabToggleIcon = Utils.FontAwesome6.Outdent;
            } else {
                TypeWidth = Constants.TYPE_COLLAPSE_WIDTH;
                TypeTabToggleIcon = Utils.FontAwesome6.Indent;
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
        #endregion
    }
}
