using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using XStart.Const;
using XStart.Utils;
using static XStart.Bean.SystemAppParam;

namespace XStart.Config {
    /// <summary>
    /// 系统缓存参数
    /// </summary>
    class Configs {
        public static string AppStartPath = System.AppDomain.CurrentDomain.BaseDirectory;
        public static System.IntPtr Handler = System.IntPtr.Zero;
        public static bool inited = false;// 是否初始化
        public static string AppClickType = Constants.CLICK_TYPE_SINGLE;// 默认单击打开
        public static string UrlOpen = Constants.URL_OPEN_DEFAULT;
        public static string UrlOpenCustomBrowser;
        public static bool ExitWarn = true;// 退出提醒

        public static bool topMost = false;
        public static bool audio = false;
        public static bool autoRun = true;// 随系统启动
        public static string openType = null;// 启动后打开的类别
        public static string clickType = Constants.CLICK_TYPE_SINGLE;// 默认单击
        public static bool closeBorderHide = true;// 靠边自动隐藏
        public static int delCount = 0;

        public static System.IntPtr taskbarHandler = System.IntPtr.Zero;

        public static bool taskbarIsShow = true;
        public static uint volume = 0x0000;
        public static bool waveMuted = false;

        public static int micVolume = 0;
        public static int lineInVolume = 0;
        public static int cdPlayerVolume = 0;
        public static bool micMuted = false;
        public static bool lineInMuted = false;
        public static bool cdPlayerMuted = false;

        public static Image ICON_ABOUT = IconUtils.GetIcon(AppStartPath + "Files/Icons/about.ico", true)?.ToBitmap();
        public static Image ICON_ADD = IconUtils.GetIcon(AppStartPath + "Files/Icons/add.ico", true)?.ToBitmap();
        public static Image ICON_APP = IconUtils.GetIcon(AppStartPath + "Files/Icons/app.ico", true)?.ToBitmap();
        public static Image ICON_BUTTOM = IconUtils.GetIcon(AppStartPath + "Files/Icons/buttom.ico", true)?.ToBitmap();
        public static Image ICON_CLEAR = IconUtils.GetIcon(AppStartPath + "Files/Icons/clear.ico", true)?.ToBitmap();
        public static Image ICON_COPY = IconUtils.GetIcon(AppStartPath + "Files/Icons/copy.ico", true)?.ToBitmap();
        public static Image ICON_CUT = IconUtils.GetIcon(AppStartPath + "Files/Icons/cut.ico", true)?.ToBitmap();
        public static Image ICON_DEFAULT = IconUtils.GetIcon(AppStartPath + "Files/Icons/default.ico", true)?.ToBitmap();
        public static Image ICON_DESKTOP = IconUtils.GetIcon(AppStartPath + "Files/Icons/desktop.ico", true)?.ToBitmap();
        public static Image ICON_DOWN = IconUtils.GetIcon(AppStartPath + "Files/Icons/down.ico", true)?.ToBitmap();
        public static Image ICON_FOLDER = IconUtils.GetIcon(AppStartPath + "Files/Icons/folder.ico", true)?.ToBitmap();
        public static Image ICON_EXIT = IconUtils.GetIcon(AppStartPath + "Files/Icons/exit.ico", true)?.ToBitmap();
        public static Image ICON_LOCK = IconUtils.GetIcon(AppStartPath + "Files/Icons/lock.ico", true)?.ToBitmap();
        public static Image ICON_PASTE = IconUtils.GetIcon(AppStartPath + "Files/Icons/paste.ico", true)?.ToBitmap();
        public static Image ICON_PREFERENCE = IconUtils.GetIcon(AppStartPath + "Files/Icons/preference.ico", true)?.ToBitmap();
        public static Image ICON_PROPERTY = IconUtils.GetIcon(AppStartPath + "Files/Icons/property.ico", true)?.ToBitmap();
        public static Image ICON_REMOVE = IconUtils.GetIcon(AppStartPath + "Files/Icons/remove.ico", true)?.ToBitmap();
        public static Image ICON_SECURITY_ADD = IconUtils.GetIcon(AppStartPath + "Files/Icons/security_add.ico", true)?.ToBitmap();
        public static Image ICON_SECURITY_DEL = IconUtils.GetIcon(AppStartPath + "Files/Icons/security_del.ico", true)?.ToBitmap();
        public static Image ICON_SECURITY_EDIT = IconUtils.GetIcon(AppStartPath + "Files/Icons/security_edit.ico", true)?.ToBitmap();
        public static Image ICON_SHOW = IconUtils.GetIcon(AppStartPath + "Files/Icons/show.ico", true)?.ToBitmap();
        public static Image ICON_THEME = IconUtils.GetIcon(AppStartPath + "Files/Icons/theme.ico", true)?.ToBitmap();
        public static Image ICON_TOP = IconUtils.GetIcon(AppStartPath + "Files/Icons/top.ico", true)?.ToBitmap();
        public static Image ICON_UP = IconUtils.GetIcon(AppStartPath + "Files/Icons/up.ico", true)?.ToBitmap();
        public static Image ICON_UPDATE = IconUtils.GetIcon(AppStartPath + "Files/Icons/update.ico", true)?.ToBitmap();
        public static Image ICON_FRESH = IconUtils.GetIcon(AppStartPath + "Files/Icons/Fresh.ico", true)?.ToBitmap();

        public static Image ICON_MYCOMPUTER = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/MyComputer.ico", true)?.ToBitmap();
        public static Image ICON_MYDOCUMENT = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/MyDocument.ico", true)?.ToBitmap();
        public static Image ICON_CONTROL = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Control.ico", true)?.ToBitmap();
        public static Image ICON_RECYCLE_BIN = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/RecycleBin.ico", true)?.ToBitmap();
        public static Image ICON_IE = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/IE.ico", true)?.ToBitmap();
        public static Image ICON_NETWORK = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Network.ico", true)?.ToBitmap();
        public static Image ICON_WINDOW_EXPLORER = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/WindowExplorer.ico", true)?.ToBitmap();
        public static Image ICON_PRINT_AND_FAX = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/PrintAndFax.ico", true)?.ToBitmap();
        public static Image ICON_REGEDIT = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Regedit.ico", true)?.ToBitmap();
        public static Image ICON_CMD = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Cmd.ico", true)?.ToBitmap();
        public static Image ICON_FOLDER_OPTIONS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/FolderOptions.ico", true)?.ToBitmap();
        public static Image ICON_MSTSC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Mstsc.ico", true)?.ToBitmap();

        public static Image ICON_CLOSE_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/ClosePC.ico", true)?.ToBitmap();
        public static Image ICON_RESTART_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/RestartPC.ico", true)?.ToBitmap();
        public static Image ICON_LOGOUT_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/logout.ico", true)?.ToBitmap();
        public static Image ICON_LOCK_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/LockPC.ico", true)?.ToBitmap();
        public static Image ICON_STANDBY_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/StandbyPC.ico", true)?.ToBitmap();
        public static Image ICON_SLEEP_PC = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/SleepPC.ico", true)?.ToBitmap();
        public static Image ICON_NET_END = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/NetEnd.ico", true)?.ToBitmap();
        public static Image ICON_SCREEN_SAVER = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/ScreenSaver.ico", true)?.ToBitmap();
        public static Image ICON_CD_ROM = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/CdRom.ico", true)?.ToBitmap();
        public static Image ICON_DESKTOP_ICON = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/DeskTopIcon.ico", true)?.ToBitmap();
        public static Image ICON_TASKBAR = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/TaskBar.ico", true)?.ToBitmap();
        public static Image ICON_TURNOFF_MONITOR = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/TurnoffMonitor.ico", true)?.ToBitmap();
        public static Image ICON_MEMORY = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Memory.ico", true)?.ToBitmap();
        public static Image ICON_END_PROCESS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/EndProcess.ico", true)?.ToBitmap();
        public static Image ICON_CUR_WINDOW = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/CurWindow.ico", true)?.ToBitmap();

        public static Image ICON_VOLUME_ADD = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/VolumeAdd.ico", true)?.ToBitmap();
        public static Image ICON_VOLUME_REDUCE = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/VolumeReduce.ico", true)?.ToBitmap();
        public static Image ICON_SILENT_TOGGLE = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/VolumeSilent.ico", true)?.ToBitmap();


        public static Image ICON_ADD_OR_REMOVE_APP = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/AddOrRemoveApp.ico", true)?.ToBitmap();
        public static Image ICON_INTERNET_OPTIONS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/InternetOptions.ico", true)?.ToBitmap();
        public static Image ICON_USER_ACCOUNT = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/UserAccount.ico", true)?.ToBitmap();
        public static Image ICON_REGION_LANGUAGE_OPTIONS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/RegionLanguageOptions.ico", true)?.ToBitmap();
        public static Image ICON_PHONE_MODEM = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/PhoneModemOptions.ico", true)?.ToBitmap();
        public static Image ICON_ACCESSIBILITY_OPTIONS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/AccessibilityOptions.ico", true)?.ToBitmap();
        public static Image ICON_POWER_OPTIONS = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/PowerOptions.ico", true)?.ToBitmap();
        public static Image ICON_GAME_CONTROLLER = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/GameController.ico", true)?.ToBitmap();
        public static Image ICON_SYSTEM_PROPERTIES = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/SystemProperties.ico", true)?.ToBitmap();
        public static Image ICON_ADD_HARDWARE = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/AddHardware.ico", true)?.ToBitmap();
        public static Image ICON_MOUSE = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/Mouse.ico", true)?.ToBitmap();
        public static Image ICON_KEYBOARD = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/KeyBoard.ico", true)?.ToBitmap();
        public static Image ICON_SOUND_AUDIO_EQUIPMENT = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/SoundAudioEquipment.ico", true)?.ToBitmap();
        public static Image ICON_VOLUME_CONTROL = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/VolumeControl.ico", true)?.ToBitmap();
        public static Image ICON_DATE_TIME = IconUtils.GetIcon(AppStartPath + "Files/Icons/System/DateTime.ico", true)?.ToBitmap();


        // 需要初始化，在窗口加载后
        public static Dictionary<string, Image> iconDic = new Dictionary<string, Image>();
        public static int systemAppPage = 0;
        public static bool addMulti = false;

        public static void InitIconDic() {
            Configs.iconDic.Clear();
            #region 系统链接
            Configs.iconDic.Add(MY_COMPUTER, Configs.ICON_MYCOMPUTER);
            Configs.iconDic.Add(MY_DOCUMENT, Configs.ICON_MYDOCUMENT);
            Configs.iconDic.Add(CONTROL, Configs.ICON_CONTROL);
            Configs.iconDic.Add(RECYCLE_BIN, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(IE, Configs.ICON_IE);
            Configs.iconDic.Add(INTERNET, Configs.ICON_NETWORK);
            Configs.iconDic.Add(EXPLORER, Configs.ICON_WINDOW_EXPLORER);
            Configs.iconDic.Add(PRINT_FAX, Configs.ICON_PRINT_AND_FAX);
            Configs.iconDic.Add(REGEDIT, Configs.ICON_REGEDIT);
            Configs.iconDic.Add(CMD, Configs.ICON_CMD);
            Configs.iconDic.Add(FOLDER_OPTIONS, Configs.ICON_FOLDER_OPTIONS);
            Configs.iconDic.Add(MSTSC, Configs.ICON_MSTSC);
            #endregion
            #region 系统操作
            Configs.iconDic.Add(CLOSE_PC, Configs.ICON_CLOSE_PC);
            Configs.iconDic.Add(RESTART_PC, Configs.ICON_RESTART_PC);
            Configs.iconDic.Add(LOG_OUT, Configs.ICON_LOGOUT_PC);
            Configs.iconDic.Add(LOCK_PC, Configs.ICON_LOCK_PC);
            Configs.iconDic.Add(STANDBY_PC, Configs.ICON_STANDBY_PC);
            Configs.iconDic.Add(SLEEP_PC, Configs.ICON_SLEEP_PC);
            Configs.iconDic.Add(NET_END, Configs.ICON_NET_END);
            Configs.iconDic.Add(SCREEN_SAVER, Configs.ICON_SCREEN_SAVER);
            Configs.iconDic.Add(OPEN_CD_ROM, Configs.ICON_CD_ROM);
            Configs.iconDic.Add(CLOSE_CD_ROM, Configs.ICON_CD_ROM);
            Configs.iconDic.Add(SHOW_HIDE_TASKBAR, Configs.ICON_TASKBAR);
            Configs.iconDic.Add(TURN_OFF_MONITOR, Configs.ICON_TURNOFF_MONITOR);
            Configs.iconDic.Add(CLEAR_RECYCLE_BIN, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CLEAR_IE_ADDRESS, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CLEAR_IE_HISTORY, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CLEAR_IE_COOKIES, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CLEAR_RENT, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CLEAR_SOME_DIRECTORY, Configs.ICON_RECYCLE_BIN);
            Configs.iconDic.Add(CONTROL_APP_MEMORY, Configs.ICON_MEMORY);
            Configs.iconDic.Add(END_PROCESS, Configs.ICON_END_PROCESS);
            #endregion

            #region 音量控制
            Configs.iconDic.Add(VOLUME_ADD, Configs.ICON_VOLUME_ADD);
            Configs.iconDic.Add(VOLUME_REDUCE, Configs.ICON_VOLUME_REDUCE);
            Configs.iconDic.Add(VOLUME_SILENT_TOGGLE, Configs.ICON_SILENT_TOGGLE);
            Configs.iconDic.Add(VOLUME_WAVE_ADD, Configs.ICON_VOLUME_ADD);
            Configs.iconDic.Add(VOLUME_WAVE_REDUCE, Configs.ICON_VOLUME_REDUCE);
            Configs.iconDic.Add(VOLUME_WAVE_SILENT_TOGGLE, Configs.ICON_SILENT_TOGGLE);
            Configs.iconDic.Add(VOLUME_MIC_ADD, Configs.ICON_VOLUME_ADD);
            Configs.iconDic.Add(VOLUME_MIC_REDUCE, Configs.ICON_VOLUME_REDUCE);
            Configs.iconDic.Add(VOLUME_MIC_SILENT_TOGGLE, Configs.ICON_SILENT_TOGGLE);
            Configs.iconDic.Add(VOLUME_LINE_IN_ADD, Configs.ICON_VOLUME_ADD);
            Configs.iconDic.Add(VOLUME_LINE_IN_REDUCE, Configs.ICON_VOLUME_REDUCE);
            Configs.iconDic.Add(VOLUME_LINE_IN_SILENT_TOGGLE, Configs.ICON_SILENT_TOGGLE);
            Configs.iconDic.Add(VOLUME_CD_PLAYER_ADD, Configs.ICON_VOLUME_ADD);
            Configs.iconDic.Add(VOLUME_CD_PLAYER_REDUCE, Configs.ICON_VOLUME_REDUCE);
            Configs.iconDic.Add(VOLUME_CD_PLAYER_SILENT_TOGGLE, Configs.ICON_SILENT_TOGGLE);
            #endregion

            #region 控制面板
            Configs.iconDic.Add(ADD_OR_REMOVE_APP, Configs.ICON_ADD_OR_REMOVE_APP);
            Configs.iconDic.Add(INTERNET_OPTIONS, Configs.ICON_INTERNET_OPTIONS);
            Configs.iconDic.Add(USER_ACCOUNT, Configs.ICON_USER_ACCOUNT);
            Configs.iconDic.Add(REGION_LANGUAGE_OPTIONS, Configs.ICON_REGION_LANGUAGE_OPTIONS);
            Configs.iconDic.Add(PHONE_AND_MODEM_OPTIONS, Configs.ICON_PHONE_MODEM);
            Configs.iconDic.Add(ACCESSIBILITY_OPTIONS, Configs.ICON_ACCESSIBILITY_OPTIONS);
            Configs.iconDic.Add(POWER_OPTIONS, Configs.ICON_POWER_OPTIONS);
            Configs.iconDic.Add(GAME_CONTROLLER, Configs.ICON_GAME_CONTROLLER);
            Configs.iconDic.Add(NETWORK_CONNECT, Configs.ICON_NETWORK);
            Configs.iconDic.Add(SCREEN_SHOW, Configs.ICON_SCREEN_SAVER);
            Configs.iconDic.Add(SYSTEM_PROPERTIES, Configs.ICON_SYSTEM_PROPERTIES);
            Configs.iconDic.Add(ADD_HARDWARE, Configs.ICON_ADD_HARDWARE);
            Configs.iconDic.Add(MOUSE, Configs.ICON_MOUSE);
            Configs.iconDic.Add(KEYBOARD, Configs.ICON_KEYBOARD);
            Configs.iconDic.Add(SOUND_AUDIO_EQUIPMENT, Configs.ICON_SOUND_AUDIO_EQUIPMENT);
            Configs.iconDic.Add(VOLUME_CONTROL, Configs.ICON_VOLUME_CONTROL);
            Configs.iconDic.Add(DATE_TIME, Configs.ICON_DATE_TIME);
            #endregion
        }
        public static void Dispose() {
            BindingFlags flag = BindingFlags.Static | BindingFlags.Public;
            System.Type configsType = typeof(Configs);
            var infos = configsType.GetFields(flag);
            foreach (var info in infos) {
                if ("Image".Equals(info.FieldType.Name)) {
                    Image imageInfo = (Image)info.GetValue(new Configs());
                    if (null != imageInfo) {
                        imageInfo.Dispose();
                    }
                }
            }
        }
    }
}
