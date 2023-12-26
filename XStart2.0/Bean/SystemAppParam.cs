using System.Collections.Generic;

namespace XStart2._0.Bean {
    // 系统功能相关的参数
    static class SystemAppParam {
        public static Dictionary<string, SystemApp> OperateParam = new Dictionary<string, SystemApp>();

        public const string MY_COMPUTER = "#MyComputer";
        public const string MY_DOCUMENT = "#MyDocument";
        public const string CONTROL = "#Control";
        public const string RECYCLE_BIN = "#RecycleBin";
        public const string IE = "#IE";
        public const string INTERNET = "#Internet";
        public const string EXPLORER = "#Explorer";
        public const string PRINT_FAX = "#PrintFax";
        public const string REGEDIT = "#Regedit";
        public const string CMD = "#Cmd";
        public const string FOLDER_OPTIONS = "#FolderOptions";
        public const string MSTSC = "#Mstsc";// 远程桌面

        public const string CLOSE_PC = "#ClosePc";
        public const string RESTART_PC = "#RestartPc";
        public const string LOG_OUT = "#LogOut";
        public const string LOCK_PC = "#LockPc";
        public const string STANDBY_PC = "#StandbyPc";
        public const string SLEEP_PC = "#SleepPc";
        public const string NET_END = "#NetEnd";
        public const string SCREEN_SAVER = "#ScreenSaver";
        public const string OPEN_CD_ROM = "#OpenCdRom";
        public const string CLOSE_CD_ROM = "#CloseCdRom";
        public const string SHOW_HIDE_TASKBAR = "#ShowHideTaskbar";
        public const string TURN_OFF_MONITOR = "#TurnoffMonitor";
        public const string CLEAR_RECYCLE_BIN = "#ClearRecycleBin";
        public const string CLEAR_IE_ADDRESS = "#ClearIeAddress";
        public const string CLEAR_IE_HISTORY = "#ClearIeHistory";
        public const string CLEAR_IE_COOKIES = "#ClearIeCookies";
        public const string CLEAR_RENT = "#ClearRent";
        public const string CLEAR_SOME_DIRECTORY = "#ClearSomeDirectory";
        public const string CONTROL_APP_MEMORY = "#ControlAppMemory";
        public const string END_PROCESS = "#EndProcess";
        public const string VOLUME_ADD = "#VolumeAdd";
        public const string VOLUME_REDUCE = "#VolumeReduce";
        public const string VOLUME_SILENT_TOGGLE = "#VolumeSilentToggle";
        public const string VOLUME_WAVE_ADD = "#VolumeWaveAdd";
        public const string VOLUME_WAVE_REDUCE = "#VolumeWaveReduce";
        public const string VOLUME_WAVE_SILENT_TOGGLE = "#VolumeWaveSilentToggle";
        public const string VOLUME_MIC_ADD = "#VolumeMicAdd";
        public const string VOLUME_MIC_REDUCE = "#VolumeMicReduce";
        public const string VOLUME_MIC_SILENT_TOGGLE = "#VolumeMicSilentToggle";
        public const string VOLUME_LINE_IN_ADD = "#VolumeLineInAdd";
        public const string VOLUME_LINE_IN_REDUCE = "#VolumeLineInReduce";
        public const string VOLUME_LINE_IN_SILENT_TOGGLE = "#VolumeLineInSilentToggle";
        public const string VOLUME_CD_PLAYER_ADD = "#VolumeCDPlayerAdd";
        public const string VOLUME_CD_PLAYER_REDUCE = "#VolumeCDPlayerReduce";
        public const string VOLUME_CD_PLAYER_SILENT_TOGGLE = "#VolumeCDPlayerSilentToggle";
        public const string ADD_OR_REMOVE_APP = "#AddOrRemoveApp";
        public const string INTERNET_OPTIONS = "#InternetOptions";
        public const string USER_ACCOUNT = "#UserAccount";
        public const string REGION_LANGUAGE_OPTIONS = "#RegionLanguageOptions";
        public const string PHONE_AND_MODEM_OPTIONS = "#PhoneAndModemOptions";
        public const string ACCESSIBILITY_OPTIONS = "#AccessibilityOptions";
        public const string POWER_OPTIONS = "#PowerOptions";
        public const string GAME_CONTROLLER = "#GameController";
        public const string NETWORK_CONNECT = "#NetworkConnect";
        public const string SCREEN_SHOW = "#ScreenShow";
        public const string SYSTEM_PROPERTIES = "#SystemProperties";
        public const string ADD_HARDWARE = "#AddHardware";
        public const string MOUSE = "#Mouse";
        public const string KEYBOARD = "#Keyboard";
        public const string SOUND_AUDIO_EQUIPMENT = "#SoundAudioEquipment";
        public const string VOLUME_CONTROL = "#VolumeControl";
        public const string DATE_TIME = "#DateTime";


        /**
         * IE相关操作
         * //1.History (历史记录)
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 1
        *   2.Cookies
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2
        *   3.Temporary Internet Files (Internet临时文件)
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8
        *   4.Form. Data (表单数据)
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 16
        *   5.Passwords (密码)
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 32
        *   6.Delete All (全部删除)
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255
        *   7.Delete All - "Also delete files and settings stored by add-ons"
        *   RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 4351
        *   
        *   
        *   
        *   Internet选项： inetcpl.cpl
        *   ODBC数据源管理： odbccp32.cpl
        *   电话和调制解调器选项： telephon.cpl
        *   电源选项： powercfg.cpl
        *   辅助功能选项： access.cpl
        *   区域和语言选项： intl.cpl
        *   日期和时间： timedate.cpl
        *   声音和音频设备： mmsys.cpl
        *   鼠标： main.cpl
        *   添加或删除程序： appwiz.cpl
        *   添加硬件： hdwwiz.cpl
        *   网络连接： ncpa.cpl
        *   系统： sysdm.cpl
        *   显示： desk.cpl
        *   用户帐户： nusrmgr.cpl
        *   游戏控制器： joy.cpl
        *   语音： sapi.cpl
        */
        public static void InitOperate() {
            OperateParam.Add(MY_COMPUTER, new SystemApp("explorer.exe", "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"));
            OperateParam.Add(MY_DOCUMENT, new SystemApp("explorer.exe", "::{450D8FBA-AD25-11D0-98A8-0800361B1103}"));
            OperateParam.Add(CONTROL, new SystemApp("rundll32.exe", "shell32.dll,Control_RunDLL"));
            OperateParam.Add(RECYCLE_BIN, new SystemApp("explorer.exe", "::{645FF040-5081-101B-9F08-00AA002F954E}"));
            OperateParam.Add(IE, new SystemApp("iexplorer.exe", string.Empty));
            OperateParam.Add(INTERNET, new SystemApp("explorer.exe", "::{208D2C60-3AEA-1069-A2D7-08002B30309D}"));
            OperateParam.Add(EXPLORER, new SystemApp("explorer.exe", string.Empty));
            OperateParam.Add(PRINT_FAX, new SystemApp("explorer.exe", "::{2227A280-3AEA-1069-A2DE-08002B30309D}"));
            OperateParam.Add(REGEDIT, new SystemApp("regedit.exe", string.Empty));
            OperateParam.Add(CMD, new SystemApp("cmd.exe", string.Empty));
            OperateParam.Add(FOLDER_OPTIONS, new SystemApp("rundll32.exe", "shell32.dll,Options_RunDLL"));
            OperateParam.Add(CLOSE_PC, new SystemApp("shutdown.exe", "/s /t 5", true, "确认关闭计算机吗?"));
            OperateParam.Add(RESTART_PC, new SystemApp("shutdown.exe", "/r /t 5", true, "确认重启计算机吗?"));
            OperateParam.Add(LOG_OUT, new SystemApp("shutdown.exe", "/l /t 5", true, "确认注销当前用户吗?"));
            OperateParam.Add(LOCK_PC, new SystemApp("rundll32.exe", "user32.dll,LockWorkStation"));
            OperateParam.Add(STANDBY_PC, new SystemApp("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0"));
            OperateParam.Add(SLEEP_PC, new SystemApp("shutdown.exe", "/h /t 0"));
            OperateParam.Add(SCREEN_SAVER, new SystemApp("scrnsave.scr", string.Empty));
            OperateParam.Add(CLEAR_RECYCLE_BIN, new SystemApp(true, "确认清空回收站?"));
            OperateParam.Add(CLEAR_IE_HISTORY, new SystemApp("RunDll32.exe", "InetCpl.cpl, ClearMyTracksByProcess 1", true, "确认清空IE历史记录?"));
            OperateParam.Add(CLEAR_IE_COOKIES, new SystemApp("RunDll32.exe", "InetCpl.cpl, ClearMyTracksByProcess 2", true, "确认清空IE cookies?"));
            OperateParam.Add(ADD_OR_REMOVE_APP, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL appwiz.cpl,,1"));
            OperateParam.Add(INTERNET_OPTIONS, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL inetcpl.cpl"));
            OperateParam.Add(CLEAR_SOME_DIRECTORY, new SystemApp(true, "确认清空指定目录?"));
            //OperateParam.Add(USER_ACCOUNT, new SystemApp("netplwiz.exe", ""));
            OperateParam.Add(USER_ACCOUNT, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL nusrmgr.cpl"));
            OperateParam.Add(REGION_LANGUAGE_OPTIONS, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL intl.cpl"));
            OperateParam.Add(PHONE_AND_MODEM_OPTIONS, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL telephon.cpl"));
            OperateParam.Add(ACCESSIBILITY_OPTIONS, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL access.cpl"));
            OperateParam.Add(POWER_OPTIONS, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL powercfg.cpl"));
            OperateParam.Add(GAME_CONTROLLER, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL joy.cpl"));
            OperateParam.Add(NETWORK_CONNECT, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL ncpa.cpl"));
            OperateParam.Add(SCREEN_SHOW, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL desk.cpl"));
            OperateParam.Add(SYSTEM_PROPERTIES, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL sysdm.cpl"));
            OperateParam.Add(ADD_HARDWARE, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL hdwwiz.cpl"));
            OperateParam.Add(MOUSE, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL main.cpl"));
            OperateParam.Add(KEYBOARD, new SystemApp("Control.exe", "keyboard"));
            OperateParam.Add(SOUND_AUDIO_EQUIPMENT, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL mmsys.cpl"));
            OperateParam.Add(VOLUME_CONTROL, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL mmsys.cpl,,1"));
            OperateParam.Add(DATE_TIME, new SystemApp("RunDll32.exe", "shell32.dll,Control_RunDLL timedate.cpl"));
        }
    }

    public class SystemApp {
        /// <summary>
        /// 执行exe文件的构造函数
        /// </summary>
        /// <param name="execute">执行的exe名</param>
        /// <param name="param">执行的参数</param>
        public SystemApp(string execute, string param) {
            Execute = execute;
            Param = param;
        }

        /// <summary>
        /// 执行前的确认信息
        /// </summary>
        /// <param name="confirm">是否确认</param>
        /// <param name="confirmMsg">确认信息</param>
        public SystemApp(bool confirm, string confirmMsg) {
            Confirm = confirm;
            ConfirmMsg = confirmMsg;
        }

        /// <summary>
        /// 可执行exe在执行前进行确认
        /// </summary>
        /// <param name="execute">执行的exe名</param>
        /// <param name="param">执行的参数</param>
        /// <param name="confirm">是否确认</param>
        /// <param name="confirmMsg">确认信息</param>
        public SystemApp(string execute, string param, bool confirm, string confirmMsg) {
            Execute = execute;
            Param = param;
            Confirm = confirm;
            ConfirmMsg = confirmMsg;
        }
        public string Execute { get; set; }

        public string Param { get; set; }

        public bool Confirm { get; set; }

        public string ConfirmMsg { get; set; }
    }
}
