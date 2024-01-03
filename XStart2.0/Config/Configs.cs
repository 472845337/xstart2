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
        public static bool exitWarn;// 退出提醒
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

        public static BitmapImage ICON_APP = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/app.ico", true);

        public static BitmapImage ICON_MYCOMPUTER = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/MyComputer.ico", true);
        public static BitmapImage ICON_MYDOCUMENT = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/MyDocument.ico", true);
        public static BitmapImage ICON_CONTROL = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Control.ico", true);
        public static BitmapImage ICON_RECYCLE_BIN = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RecycleBin.ico", true);
        public static BitmapImage ICON_IE = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/IE.ico", true);
        public static BitmapImage ICON_NETWORK = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Network.ico", true);
        public static BitmapImage ICON_WINDOW_EXPLORER = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/WindowExplorer.ico", true);
        public static BitmapImage ICON_PRINT_AND_FAX = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PrintAndFax.ico", true);
        public static BitmapImage ICON_REGEDIT = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Regedit.ico", true);
        public static BitmapImage ICON_CMD = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Cmd.ico", true);
        public static BitmapImage ICON_FOLDER_OPTIONS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/FolderOptions.ico", true);
        public static BitmapImage ICON_MSTSC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Mstsc.ico", true);

        public static BitmapImage ICON_CLOSE_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/ClosePC.ico", true);
        public static BitmapImage ICON_RESTART_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RestartPC.ico", true);
        public static BitmapImage ICON_LOGOUT_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/logout.ico", true);
        public static BitmapImage ICON_LOCK_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/LockPC.ico", true);
        public static BitmapImage ICON_STANDBY_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/StandbyPC.ico", true);
        public static BitmapImage ICON_SLEEP_PC = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SleepPC.ico", true);
        public static BitmapImage ICON_NET_END = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/NetEnd.ico", true);
        public static BitmapImage ICON_SCREEN_SAVER = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/ScreenSaver.ico", true);
        public static BitmapImage ICON_CD_ROM = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/CdRom.ico", true);
        public static BitmapImage ICON_DESKTOP_ICON = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/DeskTopIcon.ico", true);
        public static BitmapImage ICON_TASKBAR = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/TaskBar.ico", true);
        public static BitmapImage ICON_TURNOFF_MONITOR = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/TurnoffMonitor.ico", true);
        public static BitmapImage ICON_MEMORY = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Memory.ico", true);
        public static BitmapImage ICON_END_PROCESS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/EndProcess.ico", true);
        public static BitmapImage ICON_CUR_WINDOW = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/CurWindow.ico", true);

        public static BitmapImage ICON_VOLUME_ADD = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeAdd.ico", true);
        public static BitmapImage ICON_VOLUME_REDUCE = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeReduce.ico", true);
        public static BitmapImage ICON_SILENT_TOGGLE = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeSilent.ico", true);


        public static BitmapImage ICON_ADD_OR_REMOVE_APP = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AddOrRemoveApp.ico", true);
        public static BitmapImage ICON_INTERNET_OPTIONS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/InternetOptions.ico", true);
        public static BitmapImage ICON_USER_ACCOUNT = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/UserAccount.ico", true);
        public static BitmapImage ICON_REGION_LANGUAGE_OPTIONS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/RegionLanguageOptions.ico", true);
        public static BitmapImage ICON_PHONE_MODEM = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PhoneModemOptions.ico", true);
        public static BitmapImage ICON_ACCESSIBILITY_OPTIONS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AccessibilityOptions.ico", true);
        public static BitmapImage ICON_POWER_OPTIONS = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/PowerOptions.ico", true);
        public static BitmapImage ICON_GAME_CONTROLLER = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/GameController.ico", true);
        public static BitmapImage ICON_SYSTEM_PROPERTIES = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SystemProperties.ico", true);
        public static BitmapImage ICON_ADD_HARDWARE = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/AddHardware.ico", true);
        public static BitmapImage ICON_MOUSE = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/Mouse.ico", true);
        public static BitmapImage ICON_KEYBOARD = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/KeyBoard.ico", true);
        public static BitmapImage ICON_SOUND_AUDIO_EQUIPMENT = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/SoundAudioEquipment.ico", true);
        public static BitmapImage ICON_VOLUME_CONTROL = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/VolumeControl.ico", true);
        public static BitmapImage ICON_DATE_TIME = IconUtils.GetBitmapImage(AppStartPath + "Files/Icons/System/DateTime.ico", true);


        // 需要初始化，在窗口加载后
        public static Dictionary<string, BitmapImage> iconDic = new Dictionary<string, BitmapImage>();
        public static int systemAppOpenPage = 0;
        public static bool systemAppAddMulti = false;

        public static void InitIconDic() {
            iconDic.Clear();
            #region 系统链接
            iconDic.Add(MY_COMPUTER, ICON_MYCOMPUTER);
            iconDic.Add(MY_DOCUMENT, ICON_MYDOCUMENT);
            iconDic.Add(CONTROL, ICON_CONTROL);
            iconDic.Add(RECYCLE_BIN, ICON_RECYCLE_BIN);
            iconDic.Add(IE, ICON_IE);
            iconDic.Add(INTERNET, ICON_NETWORK);
            iconDic.Add(EXPLORER, ICON_WINDOW_EXPLORER);
            iconDic.Add(PRINT_FAX, ICON_PRINT_AND_FAX);
            iconDic.Add(REGEDIT, ICON_REGEDIT);
            iconDic.Add(CMD, ICON_CMD);
            iconDic.Add(FOLDER_OPTIONS, ICON_FOLDER_OPTIONS);
            iconDic.Add(MSTSC, ICON_MSTSC);
            #endregion
            #region 系统操作
            iconDic.Add(CLOSE_PC, ICON_CLOSE_PC);
            iconDic.Add(RESTART_PC, ICON_RESTART_PC);
            iconDic.Add(LOG_OUT, ICON_LOGOUT_PC);
            iconDic.Add(LOCK_PC, ICON_LOCK_PC);
            iconDic.Add(STANDBY_PC, ICON_STANDBY_PC);
            iconDic.Add(SLEEP_PC, ICON_SLEEP_PC);
            iconDic.Add(NET_END, ICON_NET_END);
            iconDic.Add(SCREEN_SAVER, ICON_SCREEN_SAVER);
            iconDic.Add(OPEN_CD_ROM, ICON_CD_ROM);
            iconDic.Add(CLOSE_CD_ROM, ICON_CD_ROM);
            iconDic.Add(SHOW_HIDE_TASKBAR, ICON_TASKBAR);
            iconDic.Add(TURN_OFF_MONITOR, ICON_TURNOFF_MONITOR);
            iconDic.Add(CLEAR_RECYCLE_BIN, ICON_RECYCLE_BIN);
            iconDic.Add(CLEAR_IE_ADDRESS, ICON_RECYCLE_BIN);
            iconDic.Add(CLEAR_IE_HISTORY, ICON_RECYCLE_BIN);
            iconDic.Add(CLEAR_IE_COOKIES, ICON_RECYCLE_BIN);
            iconDic.Add(CLEAR_RENT, ICON_RECYCLE_BIN);
            iconDic.Add(CLEAR_SOME_DIRECTORY, ICON_RECYCLE_BIN);
            iconDic.Add(CONTROL_APP_MEMORY, ICON_MEMORY);
            iconDic.Add(END_PROCESS, ICON_END_PROCESS);
            #endregion

            #region 音量控制
            iconDic.Add(VOLUME_ADD, ICON_VOLUME_ADD);
            iconDic.Add(VOLUME_REDUCE, ICON_VOLUME_REDUCE);
            iconDic.Add(VOLUME_SILENT_TOGGLE, ICON_SILENT_TOGGLE);
            iconDic.Add(VOLUME_WAVE_ADD, ICON_VOLUME_ADD);
            iconDic.Add(VOLUME_WAVE_REDUCE, ICON_VOLUME_REDUCE);
            iconDic.Add(VOLUME_WAVE_SILENT_TOGGLE, ICON_SILENT_TOGGLE);
            iconDic.Add(VOLUME_MIC_ADD, ICON_VOLUME_ADD);
            iconDic.Add(VOLUME_MIC_REDUCE, ICON_VOLUME_REDUCE);
            iconDic.Add(VOLUME_MIC_SILENT_TOGGLE, ICON_SILENT_TOGGLE);
            iconDic.Add(VOLUME_LINE_IN_ADD, ICON_VOLUME_ADD);
            iconDic.Add(VOLUME_LINE_IN_REDUCE, ICON_VOLUME_REDUCE);
            iconDic.Add(VOLUME_LINE_IN_SILENT_TOGGLE, ICON_SILENT_TOGGLE);
            iconDic.Add(VOLUME_CD_PLAYER_ADD, ICON_VOLUME_ADD);
            iconDic.Add(VOLUME_CD_PLAYER_REDUCE, ICON_VOLUME_REDUCE);
            iconDic.Add(VOLUME_CD_PLAYER_SILENT_TOGGLE, ICON_SILENT_TOGGLE);
            #endregion

            #region 控制面板
            iconDic.Add(ADD_OR_REMOVE_APP, ICON_ADD_OR_REMOVE_APP);
            iconDic.Add(INTERNET_OPTIONS, ICON_INTERNET_OPTIONS);
            iconDic.Add(USER_ACCOUNT, ICON_USER_ACCOUNT);
            iconDic.Add(REGION_LANGUAGE_OPTIONS, ICON_REGION_LANGUAGE_OPTIONS);
            iconDic.Add(PHONE_AND_MODEM_OPTIONS, ICON_PHONE_MODEM);
            iconDic.Add(ACCESSIBILITY_OPTIONS, ICON_ACCESSIBILITY_OPTIONS);
            iconDic.Add(POWER_OPTIONS, ICON_POWER_OPTIONS);
            iconDic.Add(GAME_CONTROLLER, ICON_GAME_CONTROLLER);
            iconDic.Add(NETWORK_CONNECT, ICON_NETWORK);
            iconDic.Add(SCREEN_SHOW, ICON_SCREEN_SAVER);
            iconDic.Add(SYSTEM_PROPERTIES, ICON_SYSTEM_PROPERTIES);
            iconDic.Add(ADD_HARDWARE, ICON_ADD_HARDWARE);
            iconDic.Add(MOUSE, ICON_MOUSE);
            iconDic.Add(KEYBOARD, ICON_KEYBOARD);
            iconDic.Add(SOUND_AUDIO_EQUIPMENT, ICON_SOUND_AUDIO_EQUIPMENT);
            iconDic.Add(VOLUME_CONTROL, ICON_VOLUME_CONTROL);
            iconDic.Add(DATE_TIME, ICON_DATE_TIME);
            #endregion
        }
        public static void Dispose() {
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public;
            System.Type configsType = typeof(Configs);
            var infos = configsType.GetFields(flag);
            foreach (var info in infos) {
                if ("Bitmap".Equals(info.FieldType.Name)) {
                    BitmapImage imageInfo = (BitmapImage)info.GetValue(new Configs());
                    if (null != imageInfo) {
                        imageInfo.Freeze();
                    }
                }
            }
        }
    }
}
