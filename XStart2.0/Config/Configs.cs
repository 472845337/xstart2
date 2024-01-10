using System.Collections.Generic;
using System.Reflection;
using System.Windows.Media.Imaging;
using XStart2._0.Const;
using XStart2._0.Utils;
using static XStart2._0.Bean.SystemProjectParam;

namespace XStart2._0.Config {
    /// <summary>
    /// 系统缓存参数
    /// </summary>
    class Configs {
        public static string AppStartPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static System.IntPtr Handler = System.IntPtr.Zero;
        public static bool inited = false;// 是否初始化

        public static bool typeTabExpand;// 类别标签头是否展开

        public static double mainHeight;
        public static double mainWidth;
        public static double mainTop;
        public static double mainLeft;
        public static bool topMost;
        public static bool audio;
        public static bool autoRun;// 随系统启动
        public static string openType = null;// 启动后打开的类别
        public static string clickType = Constants.CLICK_TYPE_SINGLE;// 默认单击
        public static bool closeBorderHide = true;// 靠边自动隐藏
        public static string urlOpen = Constants.URL_OPEN_DEFAULT;
        public static string urlOpenCustomBrowser;
        public static int iconSize;
        public static string orientation;
        public static bool hideTitle;
        public static bool oneLineMulti;
        public static bool exitWarn;// 退出提醒
        public static bool forceExit = false;// 直接退出
        public static int delCount;

        public static System.IntPtr taskbarHandler = System.IntPtr.Zero;

        public static bool taskbarIsShow = true;
        public static uint volume;
        public static bool waveMuted = false;

        public static int micVolume;
        public static int lineInVolume;
        public static int cdPlayerVolume;
        public static bool micMuted = false;
        public static bool lineInMuted = false;
        public static bool cdPlayerMuted = false;

        // 需要初始化，在窗口加载后
        public static Dictionary<string, BitmapImage> iconDic32 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic48 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic72 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic128 = new Dictionary<string, BitmapImage>();
        public static Dictionary<string, BitmapImage> iconDic256 = new Dictionary<string, BitmapImage>();
        public static int systemAppOpenPage = 0;
        public static bool systemAppAddMulti = false;

        public static void InitIconDic() {
            InitIconDic(Constants.ICON_SIZE_32);
            InitIconDic(Constants.ICON_SIZE_48);
            InitIconDic(Constants.ICON_SIZE_72);
            InitIconDic(Constants.ICON_SIZE_128);
            InitIconDic(Constants.ICON_SIZE_256);
        }

        public static Dictionary<string, BitmapImage> GetIconDicBySize(int size) {
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

        private static void InitIconDic(int size) {
            Dictionary<string, BitmapImage> iconDic = GetIconDicBySize(size);
            ClearIconDic(iconDic);
            iconDic.Add(APP, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/app.ico", size));
            iconDic.Add(URL, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/url.ico", size));

            var recycleBin = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RecycleBin.ico", size);
            var network = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Network.ico", size);
            var screen = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/ScreenSaver.ico", size);
            var volumeAdd = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeAdd.ico", size);
            var volumeReduce = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeReduce.ico", size);
            var volumeSilent = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeSilent.ico", size);
            #region 系统链接
            iconDic.Add(MY_COMPUTER, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/MyComputer.ico", size));
            iconDic.Add(MY_DOCUMENT, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/MyDocument.ico", size));
            iconDic.Add(CONTROL, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Control.ico", size));
            iconDic.Add(RECYCLE_BIN, recycleBin);
            iconDic.Add(IE, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/IE.ico", size));
            iconDic.Add(INTERNET, network);
            iconDic.Add(EXPLORER, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/WindowExplorer.ico", size));
            iconDic.Add(PRINT_FAX, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PrintAndFax.ico", size));
            iconDic.Add(REGEDIT, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Regedit.ico", size));
            iconDic.Add(CMD, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Cmd.ico", size));
            iconDic.Add(FOLDER_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/FolderOptions.ico", size));
            iconDic.Add(MSTSC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Mstsc.ico", size));
            #endregion
            #region 系统操作
            iconDic.Add(CLOSE_PC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/ClosePC.ico", size));
            iconDic.Add(RESTART_PC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RestartPC.ico", size));
            iconDic.Add(LOG_OUT, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/logout.ico", size));
            iconDic.Add(LOCK_PC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/LockPC.ico", size));
            iconDic.Add(STANDBY_PC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/StandbyPC.ico", size));
            iconDic.Add(SLEEP_PC, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SleepPC.ico", size));
            iconDic.Add(NET_END, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/NetEnd.ico", size));
            iconDic.Add(SCREEN_SAVER, screen);
            iconDic.Add(OPEN_CD_ROM, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/CdRom.ico", size));
            iconDic.Add(CLOSE_CD_ROM, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/CdRom.ico", size));
            iconDic.Add(SHOW_HIDE_TASKBAR, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/TaskBar.ico", size));
            iconDic.Add(TURN_OFF_MONITOR, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/TurnoffMonitor.ico", size));

            iconDic.Add(CLEAR_RECYCLE_BIN, recycleBin);
            iconDic.Add(CLEAR_IE_ADDRESS, recycleBin);
            iconDic.Add(CLEAR_IE_HISTORY, recycleBin);
            iconDic.Add(CLEAR_IE_COOKIES, recycleBin);
            iconDic.Add(CLEAR_RENT, recycleBin);
            iconDic.Add(CLEAR_SOME_DIRECTORY, recycleBin);
            iconDic.Add(CONTROL_APP_MEMORY, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Memory.ico", size));
            iconDic.Add(END_PROCESS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/EndProcess.ico", size));
            #endregion

            #region 音量控制
            iconDic.Add(VOLUME_ADD, volumeAdd);
            iconDic.Add(VOLUME_REDUCE, volumeReduce);
            iconDic.Add(VOLUME_SILENT_TOGGLE, volumeSilent);
            iconDic.Add(VOLUME_WAVE_ADD, volumeAdd);
            iconDic.Add(VOLUME_WAVE_REDUCE, volumeReduce);
            iconDic.Add(VOLUME_WAVE_SILENT_TOGGLE, volumeSilent);
            iconDic.Add(VOLUME_MIC_ADD, volumeAdd);
            iconDic.Add(VOLUME_MIC_REDUCE, volumeReduce);
            iconDic.Add(VOLUME_MIC_SILENT_TOGGLE, volumeSilent);
            iconDic.Add(VOLUME_LINE_IN_ADD, volumeAdd);
            iconDic.Add(VOLUME_LINE_IN_REDUCE, volumeReduce);
            iconDic.Add(VOLUME_LINE_IN_SILENT_TOGGLE, volumeSilent);
            iconDic.Add(VOLUME_CD_PLAYER_ADD, volumeAdd);
            iconDic.Add(VOLUME_CD_PLAYER_REDUCE, volumeReduce);
            iconDic.Add(VOLUME_CD_PLAYER_SILENT_TOGGLE, volumeSilent);
            #endregion

            #region 控制面板
            iconDic.Add(ADD_OR_REMOVE_APP, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AddOrRemoveApp.ico", size));
            iconDic.Add(INTERNET_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/InternetOptions.ico", size));
            iconDic.Add(USER_ACCOUNT, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/UserAccount.ico", size));
            iconDic.Add(REGION_LANGUAGE_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RegionLanguageOptions.ico", size));
            iconDic.Add(PHONE_AND_MODEM_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PhoneModemOptions.ico", size));
            iconDic.Add(ACCESSIBILITY_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AccessibilityOptions.ico", size));
            iconDic.Add(POWER_OPTIONS, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PowerOptions.ico", size));
            iconDic.Add(GAME_CONTROLLER, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/GameController.ico", size));
            iconDic.Add(NETWORK_CONNECT, network);
            iconDic.Add(SCREEN_SHOW, screen);
            iconDic.Add(SYSTEM_PROPERTIES, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SystemProperties.ico", size));
            iconDic.Add(ADD_HARDWARE, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AddHardware.ico", size));
            iconDic.Add(MOUSE, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Mouse.ico", size));
            iconDic.Add(KEYBOARD, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/KeyBoard.ico", size));
            iconDic.Add(SOUND_AUDIO_EQUIPMENT, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SoundAudioEquipment.ico", size));
            iconDic.Add(VOLUME_CONTROL, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeControl.ico", size));
            iconDic.Add(DATE_TIME, IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/DateTime.ico", size));
            #endregion
        }
        public static void Dispose() {
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public;
            System.Type configsType = typeof(Configs);
            FieldInfo[] infos = configsType.GetFields(flag);
            foreach (FieldInfo info in infos) {
                if ("Bitmap".Equals(info.FieldType.Name)) {
                    BitmapImage imageInfo = (BitmapImage)info.GetValue(new Configs());
                    if (null != imageInfo) {
                        imageInfo.Freeze();
                    }
                }
            }
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
