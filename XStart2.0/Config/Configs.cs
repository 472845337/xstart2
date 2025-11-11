using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using XStart2._0.Bean;
using XStart2._0.Bean.Weather;
using XStart2._0.Const;
using XStart2._0.Utils;
using static XStart2._0.Bean.SystemProjectParam;

namespace XStart2._0.Config {
    /// <summary>
    /// 系统缓存参数
    /// </summary>
    class Configs {
        public static string AppStartPath = AppDomain.CurrentDomain.BaseDirectory;// 当前程序的启动目录
        public static IntPtr Handler = IntPtr.Zero;// 当前窗口句柄
        public static IntPtr MstscHandler = IntPtr.Zero;// 远程窗口句柄
        public static bool mstscRealClose = false;// 远程真实关闭
        public static IntPtr CalendarHandler = IntPtr.Zero;// 日历窗口句柄
        public static IntPtr WeatherHandler = IntPtr.Zero;// 天气窗口句柄
        public static IntPtr AboutHandler = IntPtr.Zero;// 关于窗口句柄
        public static IntPtr LockHandler = IntPtr.Zero;// 锁定窗口句柄

        public static bool inited = false;// 是否初始化
        public static bool mainHeadShow;// 主页面头是否显示
        public static bool? typeTabExpand;// 类别标签头是否展开
        public static Tuple<double, double> scale;// 缩放

        public static string timeFormat;
        public static string dateFormat;
        public static string yearFormat;
        public static string monthFormat;
        public static string dayFormat;
        public static string weekFormat;

        public static Admin admin;// 管理员信息，头像，昵称，口令

        public static string mainBackground;// 主窗口背景
        public static double mainOpacity;// 主窗口透明度
        public static string projectForeground;// 项目文本背景
        public static string fontFamily;// 主窗口字体
        public static int fontSize;// 主窗口字体大小
        public static double opacity;// 不透明度
        public static bool isMaximum;// 是否最大化
        public static double mainHeight;// 主窗口高
        public static double mainWidth;// 主窗口宽
        public static double mainTop;// 主窗口顶部位置
        public static double mainLeft;// 主窗口左边位置
        public static string themeName;// 主题
        public static string themeCustom;// 自定义主题
        public static bool topMost;// 是否置顶
        public static bool audio;// 是否开启音效
        public static bool autoRun;// 随系统启动
        public static bool runDirectly;// 自启动直接运行
        public static string openType = null;// 启动后打开的类别
        public static string clickType = Constants.CLICK_TYPE_SINGLE;// 默认单击
        public static string rdpModel;// 远程方式
        public static bool showInTaskbar;// 显示在任务栏
        public static bool closeBorderHide;// 靠边自动隐藏
        public static string textEditor;
        public static string urlOpen;
        public static string urlOpenCustomBrowser;
        public static int iconSize;
        public static string orientation;
        public static bool hideTitle;
        public static bool oneLineMulti;
        public static bool closeMiniWarn;// 关闭最小化提醒
        public static bool exitWarn;// 退出提醒
        public static bool exitButtonType;// 退出按钮类型，true:退出，false:最小化
        public static bool forceExit = false;// 直接退出
        public static int delCount;
        public static bool dpiChange = false;

        public static IntPtr taskbarHandler = IntPtr.Zero;

        public static bool taskbarIsShow = true;
        public static uint volume;
        public static bool waveMuted = false;

        public static int micVolume;
        public static int lineInVolume;
        public static int cdPlayerVolume;
        public static bool micMuted = false;
        public static bool lineInMuted = false;
        public static bool cdPlayerMuted = false;

        #region weather
        public static List<Province> Provinces = new List<Province>();
        public static Dictionary<string, Country> Countries = new Dictionary<string, Country>();
        public static string lastWeacherCountry;

        public static string lastCountries;
        public static string weatherApi;
        public static string weatherImgTheme;// 天气主题

        #region 易客云
        public static string weatherYkyApiUrl;
        public static string weatherYkyApiAppId;
        public static string weatherYkyApiAppSecret;
        #endregion
        #region 高德
        public static string weatherGaodeApiUrl;
        public static string weatherGaodeAppKey;
        #endregion
        #region 心知
        public static string weatherSeniverseApiUrl;
        public static string weatherSeniverseAppKey;
        #endregion
        #region 和风
        public static string weatherQApiUrl;
        public static string weatherQAppKey;
        #endregion
        #region OpenWeather
        public static string weatherOpenApiUrl;
        public static string weatherOpenAppKey;
        #endregion
        #region AccuWeather
        public static string weatherAccuApiUrl;
        public static string weatherAccuAppKey;
        #endregion
        #region Visual Crossing
        public static string weatherVcApiUrl;
        public static string weatherVcAppKey;
        #endregion
        #endregion

        // 需要初始化，在窗口加载后
        public static Dictionary<string, BitmapImage> iconDic32 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic48 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic72 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic128 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic256 = new Dictionary<string, BitmapImage>();
        public static int systemAppOpenPage = 0;
        public static bool systemAppAddMulti = false;

        public static Dictionary<string, string> iconPathDic = new Dictionary<string, string>() {
            {APP, "Files/Icons/app.ico"},
            {URL, "Files/Icons/url.ico"},
            {MY_COMPUTER, "Files/Icons/System/MyComputer.ico"},
            {MY_DOCUMENT, "Files/Icons/System/MyDocument.ico"},
            {CONTROL,  "Files/Icons/System/Control.ico"},
            {RECYCLE_BIN,"Files/Icons/System/RecycleBin.ico"},
            {IE,"Files/Icons/System/IE.ico"},
            {INTERNET, "Files/Icons/System/Network.ico"},
            {TASKMGR, "Files/Icons/System/Taskmgr.ico"},
            {EXPLORER, "Files/Icons/System/WindowExplorer.ico"},
            {PRINT_FAX, "Files/Icons/System/PrintAndFax.ico"},
            {REGEDIT,"Files/Icons/System/Regedit.ico"},
            {CMD,"Files/Icons/System/Cmd.ico"},
            {FOLDER_OPTIONS,"Files/Icons/System/FolderOptions.ico"},
            {MSTSC,"Files/Icons/System/Mstsc.ico"},
            {CLOSE_PC,"Files/Icons/System/ClosePC.ico"},
            {RESTART_PC,"Files/Icons/System/RestartPC.ico"},
            {LOG_OUT,"Files/Icons/System/logout.ico"},
            {LOCK_PC,"Files/Icons/System/LockPC.ico"},
            {STANDBY_PC,"Files/Icons/System/StandbyPC.ico"},
            {SLEEP_PC,"Files/Icons/System/SleepPC.ico"},
            {NET_END,"Files/Icons/System/NetEnd.ico"},
            {SCREEN_SAVER, "Files/Icons/System/ScreenSaver.ico"},
            {OPEN_CD_ROM,"Files/Icons/System/CdRom.ico"},
            {CLOSE_CD_ROM,"Files/Icons/System/CdRom.ico"},
            {SHOW_HIDE_TASKBAR,"Files/Icons/System/TaskBar.ico"},
            {TURN_OFF_MONITOR,"Files/Icons/System/TurnoffMonitor.ico"},
            {CLEAR_RECYCLE_BIN, "Files/Icons/System/RecycleBin.ico"},
            {CLEAR_IE_ADDRESS, "Files/Icons/System/RecycleBin.ico"},
            {CLEAR_IE_HISTORY, "Files/Icons/System/RecycleBin.ico"},
            {CLEAR_IE_COOKIES, "Files/Icons/System/RecycleBin.ico"},
            {CLEAR_RENT, "Files/Icons/System/RecycleBin.ico"},
            {CLEAR_SOME_DIRECTORY, "Files/Icons/System/RecycleBin.ico"},
            {CONTROL_APP_MEMORY,"Files/Icons/System/Memory.ico"},
            {END_PROCESS,"Files/Icons/System/EndProcess.ico"},
            {VOLUME_ADD, "Files/Icons/System/VolumeAdd.ico"},
            {VOLUME_REDUCE, "Files/Icons/System/VolumeReduce.ico"},
            {VOLUME_SILENT_TOGGLE, "Files/Icons/System/VolumeSilent.ico"},
            {VOLUME_WAVE_ADD, "Files/Icons/System/VolumeAdd.ico"},
            {VOLUME_WAVE_REDUCE, "Files/Icons/System/VolumeReduce.ico"},
            {VOLUME_WAVE_SILENT_TOGGLE, "Files/Icons/System/VolumeSilent.ico"},
            {VOLUME_MIC_ADD, "Files/Icons/System/VolumeAdd.ico"},
            {VOLUME_MIC_REDUCE, "Files/Icons/System/VolumeReduce.ico"},
            {VOLUME_MIC_SILENT_TOGGLE, "Files/Icons/System/VolumeSilent.ico"},
            {VOLUME_LINE_IN_ADD, "Files/Icons/System/VolumeAdd.ico"},
            {VOLUME_LINE_IN_REDUCE, "Files/Icons/System/VolumeReduce.ico"},
            {VOLUME_LINE_IN_SILENT_TOGGLE, "Files/Icons/System/VolumeSilent.ico"},
            {VOLUME_CD_PLAYER_ADD, "Files/Icons/System/VolumeAdd.ico"},
            {VOLUME_CD_PLAYER_REDUCE, "Files/Icons/System/VolumeReduce.ico"},
            {VOLUME_CD_PLAYER_SILENT_TOGGLE, "Files/Icons/System/VolumeSilent.ico"},
            {ADD_OR_REMOVE_APP,"Files/Icons/System/AddOrRemoveApp.ico"},
            {INTERNET_OPTIONS,"Files/Icons/System/InternetOptions.ico"},
            {USER_ACCOUNT,"Files/Icons/System/UserAccount.ico"},
            {REGION_LANGUAGE_OPTIONS,"Files/Icons/System/RegionLanguageOptions.ico"},
            {PHONE_AND_MODEM_OPTIONS,"Files/Icons/System/PhoneModemOptions.ico"},
            {ACCESSIBILITY_OPTIONS,"Files/Icons/System/AccessibilityOptions.ico"},
            {POWER_OPTIONS,"Files/Icons/System/PowerOptions.ico"},
            {GAME_CONTROLLER,"Files/Icons/System/GameController.ico"},
            {NETWORK_CONNECT, "Files/Icons/System/Network.ico"},
            {SCREEN_SHOW, "Files/Icons/System/ScreenSaver.ico"},
            {SYSTEM_PROPERTIES,"Files/Icons/System/SystemProperties.ico"},
            {ADD_HARDWARE,"Files/Icons/System/AddHardware.ico"},
            {MOUSE,"Files/Icons/System/Mouse.ico"},
            {KEYBOARD,"Files/Icons/System/KeyBoard.ico"},
            {SOUND_AUDIO_EQUIPMENT,"Files/Icons/System/SoundAudioEquipment.ico"},
            {VOLUME_CONTROL,"Files/Icons/System/VolumeControl.ico"},
            {DATE_TIME,"Files/Icons/System/DateTime.ico"}
        };

        public static void InitIconDic() {
            // 小图标启动就初始化
            InitIconDic(Constants.ICON_SIZE_32);
            //InitIconDic(Constants.ICON_SIZE_48);
            //InitIconDic(Constants.ICON_SIZE_72);
            //InitIconDic(Constants.ICON_SIZE_128);
            //InitIconDic(Constants.ICON_SIZE_256);
        }


        private static Dictionary<string, BitmapImage> GetIconDicBySize(int size) {
            Dictionary<string, BitmapImage> iconDic = null;
            if (Constants.ICON_SIZE_32 == size) {
                iconDic = iconDic32;
            } else if (Constants.ICON_SIZE_48 == size) {
                iconDic = iconDic48;
            } else if (Constants.ICON_SIZE_72 == size) {
                iconDic = iconDic72;
            } else if (Constants.ICON_SIZE_128 == size) {
                iconDic = iconDic128;
            } else if (Constants.ICON_SIZE_256 == size) {
                iconDic = iconDic256;
            }
            return iconDic;
        }

        public static BitmapImage GetIcon(int size, string key) {
            Dictionary<string, BitmapImage> iconDic = GetIconDicBySize(size);
            // 加载
            if (iconPathDic.ContainsKey(key) && !iconDic.ContainsKey(key)) {
                iconDic.Add(key, IconUtils.GetBitmapImage(AppStartPath + iconPathDic[key], size));
            }
            return iconDic.TryGetValue(key, out BitmapImage image) ? image : null;
        }

        private static void InitIconDic(int size) {
            Dictionary<string, BitmapImage> iconDic = GetIconDicBySize(size);
            ClearIconDic(iconDic);
            foreach (var iconPath in iconPathDic) {
                if (!iconDic.ContainsKey(iconPath.Key)) {
                    iconDic.Add(iconPath.Key, IconUtils.GetBitmapImage(AppStartPath + iconPath.Value, size));
                }
            }
        }
        public static void Dispose() {
            ClearIconDic(iconDic32);
            ClearIconDic(iconDic48);
            ClearIconDic(iconDic72);
            ClearIconDic(iconDic128);
            ClearIconDic(iconDic256);
        }

        private static void ClearIconDic(Dictionary<string, BitmapImage> iconDic) {
            foreach (KeyValuePair<string, BitmapImage> icon in iconDic) {
                icon.Value.Freeze();
            }
            iconDic.Clear();
        }
    }
}
