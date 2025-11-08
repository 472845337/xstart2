using PropertyChanged;
using System;
using System.Windows.Media;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Services;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class MainViewModel : BaseViewModel {

        public PathGeometry MaximumButtonPathData { get; set; }
        #region 用户数据
        [OnChangedMethod(nameof(SetAvatar))]
        public string AvatarPath { get; set; }
        private void SetAvatar() {
            // gif读取的流不能关闭（始终保持占用文件）,所以不可以使用ImageUtils的File2BitmapImage方法
            Avatar = ImageUtils.GetImageOutMs(AvatarPath);
        }
        // 头像
        public System.Windows.Media.Imaging.BitmapImage Avatar { get; set; }
        // gif动画速率
        public double GifSpeedRatio { get; set; }
        // 头像尺寸
        public int AvatarSize { get; set; }
        // 昵称
        public string NickName { get; set; }
        public string TimeFormat { get; set; }
        public string DateFormat { get; set; }
        public string YearFormat { get; set; }
        public string MonthFormat { get; set; }
        public string DayFormat { get; set; }
        public string WeekFormat { get; set; }
        // 管理员口令
        public string Security { get; set; }
        #endregion

        #region 时间数据
        public MyDateTime MyDateTime { get; set; } = new MyDateTime();
        #endregion
        #region 应用数据
        public ObservableDictionary<string, Bean.Type> Types { get; set; }
        #endregion

        #region 窗口相关
        // 主窗口背景，纯色，背景图
        [OnChangedMethod(nameof(ConvertBrush))]
        public string MainBackground { get; set; }
        // 背景不透明度
        [OnChangedMethod(nameof(ConvertBrush))]
        public double MainOpacity { get; set; }
        // 根据MainBackground和Opacity生成背景画刷
        public Brush BackgroundBrush { get; set; }
        // 是否最大化
        public bool IsMaximum { get; set; }
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
        // 类别名称是否展开,启动的时候就需要执行一次，所以类型为nullable
        [OnChangedMethod(nameof(ChangeTypeTab))]
        public bool? TypeTabExpanded { get; set; }
        // 类别宽度（TabControl的TabItem的宽度）
        public double TypeWidth { get; set; }
        // 类别名称开关图标
        public string TypeTabToggleIcon { get; set; }

        public void ChangeTypeTab() {
            if (null == TypeTabExpanded || (bool)TypeTabExpanded) {
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
        public bool ExitButtonType { get; set; }// 关闭按钮类型，true:表示退出，false:表示最小化
        public bool ShowInTaskbar { get; set; }
        [OnChangedMethod(nameof(AutoHideToggle))]
        public bool CloseBorderHide { get; set; }
        [DoNotNotify]
        public System.Windows.Threading.DispatcherTimer AutoHideTimer { get; set; }
        public bool CancelHide { get; set; } = false;
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

        public void AutoHideToggle() {
            if (CloseBorderHide && Configs.inited) {
                AutoHideTimer.Start();
            } else {
                if (!CloseBorderHide && Configs.inited) {
                    // 将取消隐藏置为真，定时器执行完成后，会将该状态复位，位置复位后，timer 停止
                    CancelHide = true;
                }
            }
        }

        public void ConvertBrush() {
            BackgroundBrush = BackgroundUtils.GetBrush(MainBackground, MainOpacity);
        }

        public void FreshDate() {
            string dateFormat = DateFormat;
            if (string.IsNullOrEmpty(dateFormat)) {
                MyDateTime.CurDate = DateTime.Now.ToString("D");
            } else {
                if (dateFormat.Contains("Y")) {
                    // 将年换成格式
                    dateFormat = dateFormat.Replace("Y", YearFormat);
                }
                if (dateFormat.Contains("M")) {
                    // 将月换成格式
                    dateFormat = dateFormat.Replace("M", MonthFormat);
                }
                if (dateFormat.Contains("D")) {
                    // 将日换成格式
                    dateFormat = dateFormat.Replace("D", DayFormat);
                }
                MyDateTime.CurDate = DateTime.Now.ToString(dateFormat);
            }
            string weekFormat = WeekFormat;
            if (string.IsNullOrEmpty(weekFormat) || "星期".Equals(weekFormat)) {
                MyDateTime.CurWeekDay = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
            } else {
                switch (weekFormat) {
                    case "周":
                        MyDateTime.CurWeekDay = DateTime.Now.ToString("ddd");
                        break;
                    case "曜日":
                        MyDateTime.CurWeekDay = System.Globalization.CultureInfo.CreateSpecificCulture("ja-JP").DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                        break;
                    case "Mon":
                        MyDateTime.CurWeekDay = DateTime.Now.ToString("ddd", System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                        break;
                    default:
                        MyDateTime.CurWeekDay = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);
                        break;
                }
            }
        }
    }
}
