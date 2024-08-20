using PropertyChanged;
using System.Windows.Media;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Services;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class MainViewModel : BaseViewModel {

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
        // 管理员
        public string Admin { get; set; }
        #endregion

        #region 时间数据
        public MyDateTime MyDateTime { get; set; } = new MyDateTime();
        #endregion
        #region 应用数据
        public ObservableDictionary<string, Type> Types { get; set; }
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
        // 主窗口头部显示
        public bool MainHeadShow { get; set; }
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
        [DoNotNotify]
        public string OpenType { get; set; }
        public int SelectedIndex { get; set; }
        public double TabControlActualHeight { get; set; }
        // 音效开关
        public bool Audio { get; set; }
        public bool AutoRun { get; set; }
        public bool RunDirectly { get; set; }
        public bool ExitWarn { get; set; }
        public bool CloseBorderHide { get; set; }
        public string ClickType { get; set; }
        public string RdpModel { get; set; }
        public string TextEditor { get; set; }
        public string UrlOpen { get; set; }
        public string UrlOpenCustomBrowser { get; set; }
        [OnChangedMethod(nameof(ChangeIconSize))]
        public int IconSize { get; set; }
        [OnChangedMethod(nameof(ChangeColumnOrientation))]
        public string Orientation { get; set; }
        [OnChangedMethod(nameof(ChangeProjectHideTitle))]
        public bool HideTitle { get; set; }
        public bool OneLineMulti { get; set; }
        [DoNotNotify]
        public string WeatherApi { get; set; }
        [DoNotNotify]
        public string WeatherImgTheme { get; set; }

        [DoNotNotify]
        public string WeatherYkyApiAppId { get; set; }
        [DoNotNotify]
        public string WeatherYkyApiAppSecret { get; set; }
        [DoNotNotify]
        public string WeatherYkyApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherGaodeApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherGaodeAppKey { get; set; }
        [DoNotNotify]
        public string WeatherSeniverseApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherSeniverseAppKey { get; set; }
        [DoNotNotify]
        public string WeatherQApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherQAppKey { get; set; }
        [DoNotNotify]
        public string WeatherOpenApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherOpenAppKey { get; set; }
        [DoNotNotify]
        public string WeatherAccuApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherAccuAppKey { get; set; }
        [DoNotNotify]
        public string WeatherVcApiUrl { get; set; }
        [DoNotNotify]
        public string WeatherVcAppKey { get; set; }
        // 操作信息颜色
        public Brush OperateMsgColor { get; private set; }
        // 操作信息
        public string OperateMsg { get; private set; }

        private void ChangeIconSize() {
            ChangeProject(nameof(IconSize));
        }

        private void ChangeColumnOrientation() {
            ChangeProject(nameof(Orientation));
        }

        private void ChangeProjectHideTitle() {
            ChangeProject(nameof(HideTitle));
        }

        private void ChangeProject(string field) {
            if (Config.Configs.inited) {
                foreach (var type in XStartService.TypeDic) {
                    foreach (var column in type.Value.ColumnDic) {
                        bool isChange = false;
                        if ((nameof(IconSize).Equals(field) && null == column.Value.IconSize)
                            || (nameof(Orientation).Equals(field) && string.IsNullOrEmpty(column.Value.Orientation))
                            || (nameof(HideTitle).Equals(field) && null == column.Value.HideTitle)) {
                            // 非自定义的栏目才修改
                            isChange = true;
                        }
                        if (isChange) {
                            foreach (var project in column.Value.ProjectDic) {
                                if (nameof(IconSize).Equals(field)) {
                                    project.Value.IconSize = IconSize;
                                } else if (nameof(Orientation).Equals(field)) {
                                    project.Value.Orientation = Orientation;
                                } else if (nameof(HideTitle).Equals(field)) {
                                    project.Value.HideTitle = HideTitle;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void InitOperateMsg(Color color, string msg) {
            OperateMsgColor = new SolidColorBrush(color);
            OperateMsgColor.Freeze();
            OperateMsg = msg;
        }

        #endregion
    }
}
