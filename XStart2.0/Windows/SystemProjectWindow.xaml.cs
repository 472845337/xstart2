using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Helper;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModel;
using static XStart2._0.Bean.SystemProjectParam;

namespace XStart2._0.Windows {
    /// <summary>
    /// SystemProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SystemProjectWindow : Window {
        readonly SystemProjectViewModel vm;
        public bool MultiAdd { get; set; }
        public int OpenPage { get; set; }
        public Project Project { get; set; }

        public SystemProjectWindow(string typeSection, string columnSection, bool addMulti, int openPage) {
            vm = new SystemProjectViewModel {
                TypeSection = typeSection,
                ColumnSection = columnSection,
                MultiAdd = addMulti,
                OpenPage = openPage
            };

            InitializeComponent();
            Loaded += Window_Loaded;
        }


        private void Window_Loaded(object sender, EventArgs e) {
            DataContext = vm;

            #region 系统链接
            List<ViewModel.SystemProject> systemLinks = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject myComputer = new ViewModel.SystemProject() { Name = "MyComputerButton", Content = "我的电脑", Image = Configs.iconDic32[MY_COMPUTER] };
            ViewModel.SystemProject myDocument = new ViewModel.SystemProject() { Name = "MyDocumentButton", Content = "我的文档", Image = Configs.iconDic32[MY_DOCUMENT] };
            ViewModel.SystemProject control = new ViewModel.SystemProject() { Name = "ControlButton", Content = "控制面板", Image = Configs.iconDic32[CONTROL] };
            ViewModel.SystemProject recycleBin = new ViewModel.SystemProject() { Name = "RecycleBinButton", Content = "回收站", Image = Configs.iconDic32[RECYCLE_BIN] };
            ViewModel.SystemProject ieButton = new ViewModel.SystemProject() { Name = "IEButton", Content = "Internet Explorer", Image = Configs.iconDic32[IE] };
            ViewModel.SystemProject internetButton = new ViewModel.SystemProject() { Name = "InternetButton", Content = "网上邻居", Image = Configs.iconDic32[INTERNET] };
            ViewModel.SystemProject taskmgrButton = new ViewModel.SystemProject() { Name = "TaskmgrButton", Content = "任务管理器", Image = Configs.iconDic32[TASKMGR] };
            ViewModel.SystemProject explorerButton = new ViewModel.SystemProject() { Name = "ExplorerButton", Content = "资源管理器", Image = Configs.iconDic32[EXPLORER] };
            ViewModel.SystemProject printFaxButton = new ViewModel.SystemProject() { Name = "PrintFaxButton", Content = "打印机和传真", Image = Configs.iconDic32[PRINT_FAX] };
            ViewModel.SystemProject regeditButton = new ViewModel.SystemProject() { Name = "RegeditButton", Content = "注册表编辑器", Image = Configs.iconDic32[REGEDIT] };
            ViewModel.SystemProject cmdButton = new ViewModel.SystemProject() { Name = "CmdButton", Content = "命令提示符", Image = Configs.iconDic32[CMD] };
            ViewModel.SystemProject folderOptionsButton = new ViewModel.SystemProject() { Name = "FolderOptionsButton", Content = "文件夹选项", Image = Configs.iconDic32[FOLDER_OPTIONS] };
            ViewModel.SystemProject mstscButton = new ViewModel.SystemProject() { Name = "MstscButton", Content = "远程桌面", Image = Configs.iconDic32[MSTSC] };
            systemLinks.Add(myComputer);
            systemLinks.Add(myDocument);
            systemLinks.Add(control);
            systemLinks.Add(recycleBin);
            systemLinks.Add(ieButton);
            systemLinks.Add(internetButton);
            systemLinks.Add(taskmgrButton);
            systemLinks.Add(explorerButton);
            systemLinks.Add(printFaxButton);
            systemLinks.Add(regeditButton);
            systemLinks.Add(cmdButton);
            systemLinks.Add(folderOptionsButton);
            systemLinks.Add(mstscButton);
            #endregion

            #region 系统操作
            List<ViewModel.SystemProject> systemOperates = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject closePcButton = new ViewModel.SystemProject() { Name = "ClosePcButton", Content = "关闭计算机", Image = Configs.iconDic32[CLOSE_PC] };
            ViewModel.SystemProject restartPcButton = new ViewModel.SystemProject() { Name = "RestartPcButton", Content = "重启计算机", Image = Configs.iconDic32[RESTART_PC] };
            ViewModel.SystemProject logOutButton = new ViewModel.SystemProject() { Name = "LogOutButton", Content = "注销计算机", Image = Configs.iconDic32[LOG_OUT] };
            ViewModel.SystemProject lockPcButton = new ViewModel.SystemProject() { Name = "LockPcButton", Content = "锁定计算机", Image = Configs.iconDic32[LOCK_PC] };
            ViewModel.SystemProject standbyPcButton = new ViewModel.SystemProject() { Name = "StandbyPcButton", Content = "待机", Image = Configs.iconDic32[STANDBY_PC] };
            ViewModel.SystemProject sleepPcButton = new ViewModel.SystemProject() { Name = "SleepPcButton", Content = "休眠", Image = Configs.iconDic32[SLEEP_PC] };
            ViewModel.SystemProject netEndButton = new ViewModel.SystemProject() { Name = "NetEndButton", Content = "断开所有拨号网络", Image = Configs.iconDic32[NET_END] };
            ViewModel.SystemProject screenSaverButton = new ViewModel.SystemProject() { Name = "ScreenSaverButton", Content = "屏幕保护", Image = Configs.iconDic32[SCREEN_SAVER] };
            ViewModel.SystemProject openCdRomButton = new ViewModel.SystemProject() { Name = "OpenCdRomButton", Content = "打开光驱", Image = Configs.iconDic32[OPEN_CD_ROM] };
            ViewModel.SystemProject closeCdRomButton = new ViewModel.SystemProject() { Name = "CloseCdRomButton", Content = "关闭光驱", Image = Configs.iconDic32[CLOSE_CD_ROM] };
            ViewModel.SystemProject showHideTaskbarButton = new ViewModel.SystemProject() { Name = "ShowHideTaskbarButton", Content = "隐藏/显示任务栏", Image = Configs.iconDic32[SHOW_HIDE_TASKBAR] };
            ViewModel.SystemProject turnoffMonitorButton = new ViewModel.SystemProject() { Name = "TurnoffMonitorButton", Content = "关闭显示器", Image = Configs.iconDic32[TURN_OFF_MONITOR] };
            ViewModel.SystemProject clearRecycleBinButton = new ViewModel.SystemProject() { Name = "ClearRecycleBinButton", Content = "清空回收站", Image = Configs.iconDic32[CLEAR_RECYCLE_BIN] };
            ViewModel.SystemProject clearIeAddressButton = new ViewModel.SystemProject() { Name = "ClearIeAddressButton", Content = "清空IE地址栏记录", Image = Configs.iconDic32[CLEAR_IE_ADDRESS] };
            ViewModel.SystemProject clearIeHistoryButton = new ViewModel.SystemProject() { Name = "ClearIeHistoryButton", Content = "清空IE历史记录", Image = Configs.iconDic32[CLEAR_IE_HISTORY] };
            ViewModel.SystemProject clearIeCookiesButton = new ViewModel.SystemProject() { Name = "ClearIeCookiesButton", Content = "清空Cookies", Image = Configs.iconDic32[CLEAR_IE_COOKIES] };
            ViewModel.SystemProject clearRentButton = new ViewModel.SystemProject() { Name = "ClearRentButton", Content = "清空历史文档", Image = Configs.iconDic32[CLEAR_RENT] };
            ViewModel.SystemProject clearSomeDirectoryButton = new ViewModel.SystemProject() { Name = "ClearSomeDirectoryButton", Content = "清空某目录", Image = Configs.iconDic32[CLEAR_SOME_DIRECTORY] };
            ViewModel.SystemProject controlAppMemoryButton = new ViewModel.SystemProject() { Name = "ControlAppMemoryButton", Content = "控制程序内存", Image = Configs.iconDic32[CONTROL_APP_MEMORY] };
            ViewModel.SystemProject endProcessButton = new ViewModel.SystemProject() { Name = "EndProcessButton", Content = "结束程序进程", Image = Configs.iconDic32[END_PROCESS] };
            systemOperates.Add(closePcButton);
            systemOperates.Add(restartPcButton);
            systemOperates.Add(logOutButton);
            systemOperates.Add(lockPcButton);
            systemOperates.Add(standbyPcButton);
            systemOperates.Add(sleepPcButton);
            systemOperates.Add(netEndButton);
            systemOperates.Add(screenSaverButton);
            systemOperates.Add(openCdRomButton);
            systemOperates.Add(closeCdRomButton);
            systemOperates.Add(showHideTaskbarButton);
            systemOperates.Add(turnoffMonitorButton);
            systemOperates.Add(clearRecycleBinButton);
            systemOperates.Add(clearIeAddressButton);
            systemOperates.Add(clearIeHistoryButton);
            systemOperates.Add(clearIeCookiesButton);
            systemOperates.Add(clearRentButton);
            systemOperates.Add(clearSomeDirectoryButton);
            systemOperates.Add(controlAppMemoryButton);
            systemOperates.Add(endProcessButton);
            #endregion

            #region 音量控制
            List<ViewModel.SystemProject> systemAudioNormals = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject volumeAddButton = new ViewModel.SystemProject() { Name = "VolumeAddButton", Content = "音量增加", Image = Configs.iconDic32[VOLUME_ADD] };
            ViewModel.SystemProject volumeReduceButton = new ViewModel.SystemProject() { Name = "VolumeReduceButton", Content = "音量减少", Image = Configs.iconDic32[VOLUME_REDUCE] };
            ViewModel.SystemProject volumeSilentToggleButton = new ViewModel.SystemProject() { Name = "VolumeSilentToggleButton", Content = "静音切换", Image = Configs.iconDic32[VOLUME_SILENT_TOGGLE] };
            systemAudioNormals.Add(volumeAddButton);
            systemAudioNormals.Add(volumeReduceButton);
            systemAudioNormals.Add(volumeSilentToggleButton);

            List<ViewModel.SystemProject> systemAudioWaves = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject volumeWaveAddButton = new ViewModel.SystemProject() { Name = "VolumeWaveAddButton", Content = "音量增加", Image = Configs.iconDic32[VOLUME_WAVE_ADD] };
            ViewModel.SystemProject volumeWaveReduceButton = new ViewModel.SystemProject() { Name = "VolumeWaveReduceButton", Content = "音量减少", Image = Configs.iconDic32[VOLUME_WAVE_REDUCE] };
            ViewModel.SystemProject volumeWaveSilentToggleButton = new ViewModel.SystemProject() { Name = "VolumeWaveSilentToggleButton", Content = "静音切换", Image = Configs.iconDic32[VOLUME_WAVE_SILENT_TOGGLE] };
            systemAudioWaves.Add(volumeWaveAddButton);
            systemAudioWaves.Add(volumeWaveReduceButton);
            systemAudioWaves.Add(volumeWaveSilentToggleButton);

            List<ViewModel.SystemProject> systemAudioMics = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject volumeMicAddButton = new ViewModel.SystemProject() { Name = "VolumeMicAddButton", Content = "音量增加", Image = Configs.iconDic32[VOLUME_MIC_ADD] };
            ViewModel.SystemProject volumeMicReduceButton = new ViewModel.SystemProject() { Name = "VolumeMicReduceButton", Content = "音量减少", Image = Configs.iconDic32[VOLUME_MIC_REDUCE] };
            ViewModel.SystemProject volumeMicSilentToggleButton = new ViewModel.SystemProject() { Name = "VolumeMicSilentToggleButton", Content = "静音切换", Image = Configs.iconDic32[VOLUME_MIC_SILENT_TOGGLE] };
            systemAudioMics.Add(volumeMicAddButton);
            systemAudioMics.Add(volumeMicReduceButton);
            systemAudioMics.Add(volumeMicSilentToggleButton);

            List<ViewModel.SystemProject> systemAudioLines = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject volumeLineAddButton = new ViewModel.SystemProject() { Name = "VolumeLineAddButton", Content = "音量增加", Image = Configs.iconDic32[VOLUME_LINE_IN_ADD] };
            ViewModel.SystemProject volumeLineReduceButton = new ViewModel.SystemProject() { Name = "VolumeLineReduceButton", Content = "音量减少", Image = Configs.iconDic32[VOLUME_LINE_IN_REDUCE] };
            ViewModel.SystemProject volumeLineSilentToggleButton = new ViewModel.SystemProject() { Name = "VolumeLineSilentToggleButton", Content = "静音切换", Image = Configs.iconDic32[VOLUME_LINE_IN_SILENT_TOGGLE] };
            systemAudioLines.Add(volumeLineAddButton);
            systemAudioLines.Add(volumeLineReduceButton);
            systemAudioLines.Add(volumeLineSilentToggleButton);

            List<ViewModel.SystemProject> systemAudioCdRoms = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject volumeCdRomAddButton = new ViewModel.SystemProject() { Name = "VolumeCdRomAddButton", Content = "音量增加", Image = Configs.iconDic32[VOLUME_CD_PLAYER_ADD] };
            ViewModel.SystemProject volumeCdRomReduceButton = new ViewModel.SystemProject() { Name = "VolumeCdRomReduceButton", Content = "音量减少", Image = Configs.iconDic32[VOLUME_CD_PLAYER_REDUCE] };
            ViewModel.SystemProject volumeCdRomSilentToggleButton = new ViewModel.SystemProject() { Name = "VolumeCdRomSilentToggleButton", Content = "静音切换", Image = Configs.iconDic32[VOLUME_CD_PLAYER_SILENT_TOGGLE] };
            systemAudioCdRoms.Add(volumeCdRomAddButton);
            systemAudioCdRoms.Add(volumeCdRomReduceButton);
            systemAudioCdRoms.Add(volumeCdRomSilentToggleButton);
            #endregion

            #region 控制面板
            List<ViewModel.SystemProject> systemControls = new List<ViewModel.SystemProject>();
            ViewModel.SystemProject addOrRemoveAppButton = new ViewModel.SystemProject() { Name = "AddOrRemoveAppButton", Content = "添加或删除程序", Image = Configs.iconDic32[ADD_OR_REMOVE_APP] };
            ViewModel.SystemProject internetOptionsButton = new ViewModel.SystemProject() { Name = "InternetOptionsButton", Content = "Internet选项", Image = Configs.iconDic32[INTERNET_OPTIONS] };
            ViewModel.SystemProject userAccountButton = new ViewModel.SystemProject() { Name = "UserAccountButton", Content = "用户账户", Image = Configs.iconDic32[USER_ACCOUNT] };
            ViewModel.SystemProject regionLanguageOptionsButton = new ViewModel.SystemProject() { Name = "RegionLanguageOptionsButton", Content = "区域和语言选项", Image = Configs.iconDic32[REGION_LANGUAGE_OPTIONS] };
            ViewModel.SystemProject phoneAndModemOptionsButton = new ViewModel.SystemProject() { Name = "PhoneAndModemOptionsButton", Content = "电话和调制解调器选项", Image = Configs.iconDic32[PHONE_AND_MODEM_OPTIONS] };
            ViewModel.SystemProject accessibilityOptionsButton = new ViewModel.SystemProject() { Name = "AccessibilityOptionsButton", Content = "辅助功能选项", Image = Configs.iconDic32[ACCESSIBILITY_OPTIONS] };
            ViewModel.SystemProject powerOptionsButton = new ViewModel.SystemProject() { Name = "PowerOptionsButton", Content = "电源选项", Image = Configs.iconDic32[POWER_OPTIONS] };
            ViewModel.SystemProject gameControllerButton = new ViewModel.SystemProject() { Name = "GameControllerButton", Content = "游戏控制器", Image = Configs.iconDic32[GAME_CONTROLLER] };
            ViewModel.SystemProject networkConnectButton = new ViewModel.SystemProject() { Name = "NetworkConnectButton", Content = "网络连接", Image = Configs.iconDic32[NETWORK_CONNECT] };
            ViewModel.SystemProject screenShowButton = new ViewModel.SystemProject() { Name = "ScreenShowButton", Content = "屏幕显示", Image = Configs.iconDic32[SCREEN_SHOW] };
            ViewModel.SystemProject systemPropertiesButton = new ViewModel.SystemProject() { Name = "SystemPropertiesButton", Content = "系统属性", Image = Configs.iconDic32[SYSTEM_PROPERTIES] };
            ViewModel.SystemProject addHardwareButton = new ViewModel.SystemProject() { Name = "AddHardwareButton", Content = "添加硬件", Image = Configs.iconDic32[ADD_HARDWARE] };
            ViewModel.SystemProject mouseButton = new ViewModel.SystemProject() { Name = "MouseButton", Content = "鼠标", Image = Configs.iconDic32[MOUSE] };
            ViewModel.SystemProject keyboardButton = new ViewModel.SystemProject() { Name = "KeyboardButton", Content = "键盘", Image = Configs.iconDic32[KEYBOARD] };
            ViewModel.SystemProject soundAudioEquipmentButton = new ViewModel.SystemProject() { Name = "SoundAudioEquipmentButton", Content = "声音和音频设备", Image = Configs.iconDic32[SOUND_AUDIO_EQUIPMENT] };
            ViewModel.SystemProject volumeControlButton = new ViewModel.SystemProject() { Name = "VolumeControlButton", Content = "音量控制", Image = Configs.iconDic32[VOLUME_CONTROL] };
            ViewModel.SystemProject dateTimeButton = new ViewModel.SystemProject() { Name = "DateTimeButton", Content = "日期和时间", Image = Configs.iconDic32[DATE_TIME] };
            systemControls.Add(addOrRemoveAppButton);
            systemControls.Add(internetOptionsButton);
            systemControls.Add(userAccountButton);
            systemControls.Add(regionLanguageOptionsButton);
            systemControls.Add(phoneAndModemOptionsButton);
            systemControls.Add(accessibilityOptionsButton);
            systemControls.Add(powerOptionsButton);
            systemControls.Add(gameControllerButton);
            systemControls.Add(networkConnectButton);
            systemControls.Add(screenShowButton);
            systemControls.Add(systemPropertiesButton);
            systemControls.Add(addHardwareButton);
            systemControls.Add(mouseButton);
            systemControls.Add(keyboardButton);
            systemControls.Add(soundAudioEquipmentButton);
            systemControls.Add(volumeControlButton);
            systemControls.Add(dateTimeButton);
            #endregion

            vm.SystemLinks = systemLinks;
            vm.SystemOperates = systemOperates;
            vm.SystemAudioNormals = systemAudioNormals;
            vm.SystemAudioWaves = systemAudioWaves;
            vm.SystemAudioMics = systemAudioMics;
            vm.SystemAudioLines = systemAudioLines;
            vm.SystemAudioCdRoms = systemAudioCdRoms;
            vm.SystemControls = systemControls;
        }

        private void SytemProjectBtn_Click(object sender, RoutedEventArgs e) {
            bool isAdd = true;
            Button systemAppButton = (Button)sender;
            string name = systemAppButton.Content as string;// 按钮内容
            string buttonName = systemAppButton.GetValue(ElementParamHelper.ButtonNameProperty) as string;
            string path = Constants.SYSTEM_PROJECT_CHAR + buttonName.Replace("Button", string.Empty);
            string arguments = string.Empty;
            if (CLEAR_SOME_DIRECTORY.Equals(path)) {
                // 清空某目录打开目录窗口选择目录
                using System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if (System.Windows.Forms.DialogResult.OK == fbd.ShowDialog()) {
                    string dir = fbd.SelectedPath;
                    name = $"清空{Path.GetFileName(Path.GetDirectoryName(dir))}目录";
                    arguments = Path.GetDirectoryName(dir);
                } else {
                    MsgBoxUtils.ShowError("未选择目录!");
                    isAdd = false;
                }
            } else if (CONTROL_APP_MEMORY.Equals(path)) {
                // 控制程序内存
                ControlAppMemoryWindow cam = new ControlAppMemoryWindow() { Owner = this };
                if (true == cam.ShowDialog()) {
                    name = $"控制{cam.vm.AppName}内存";
                    arguments = $"{cam.vm.AppName}{Constants.SPLIT_CHAR}{cam.vm.MinMemory}{Constants.SPLIT_CHAR}{cam.vm.MaxMemory}";
                } else {
                    isAdd = false;
                }
                cam.Close();
            } else if (MSTSC.Equals(path)) {
                // 远程桌面
                MstscWindow mstsc = new MstscWindow() { Owner = this };
                if (true == mstsc.ShowDialog()) {
                    name = $"{mstsc.vm.Address}远程";
                    arguments = $"{mstsc.vm.Address}{Constants.SPLIT_CHAR}{mstsc.vm.Port}{Constants.SPLIT_CHAR}{mstsc.vm.Account}{Constants.SPLIT_CHAR}{mstsc.vm.Password}";
                } else {
                    isAdd = false;
                }
                mstsc.Close();
            }

            if (isAdd) {
                // 创建系统功能项目对象（不可自启动）
                Project app = new Project {
                    TypeSection = vm.TypeSection, ColumnSection = vm.ColumnSection,
                    Kind = Project.KIND_SYSTEM, Name = name, Path = path, IconPath = path, Arguments = arguments, CanAutoRun = false
                };
                if (MSTSC.Equals(path)) {
                    app.CanAutoRun = true;
                }
                if (vm.MultiAdd) {
                    // 直接添加应用
                    XStartService.AddNewData(app);
                    NotifyUtils.ShowNotification("系统应用添加成功");
                } else {
                    // 返回
                    Project = app;
                    DialogResult = true;
                }
                MultiAdd = vm.MultiAdd;
                OpenPage = vm.OpenPage;
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
