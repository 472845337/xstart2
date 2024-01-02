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
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// SystemProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SystemProjectWindow : Window {
        SystemProjectViewModel vm;
        public bool MultiAdd { get; set; }
        public int OpenPage { get; set; }
        public Project Project { get; set; }
        public SystemProjectWindow() {
            InitializeComponent();
        }

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
            List<ViewModels.SystemProject> systemLinks = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject myComputer = new ViewModels.SystemProject() { Name = "MyComputerButton", Content = "我的电脑", Image = Configs.ICON_MYCOMPUTER };
            ViewModels.SystemProject myDocument = new ViewModels.SystemProject() { Name = "MyDocumentButton", Content = "我的文档", Image = Configs.ICON_MYDOCUMENT };
            ViewModels.SystemProject control = new ViewModels.SystemProject() { Name = "ControlButton", Content = "控制面板", Image = Configs.ICON_CONTROL };
            ViewModels.SystemProject recycleBin = new ViewModels.SystemProject() { Name = "RecycleBinButton", Content = "回收站", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject ieButton = new ViewModels.SystemProject() { Name = "IEButton", Content = "Internet Explorer", Image = Configs.ICON_IE };
            ViewModels.SystemProject internetButton = new ViewModels.SystemProject() { Name = "InternetButton", Content = "网上邻居", Image = Configs.ICON_NETWORK };
            ViewModels.SystemProject explorerButton = new ViewModels.SystemProject() { Name = "ExplorerButton", Content = "资源管理器", Image = Configs.ICON_WINDOW_EXPLORER };
            ViewModels.SystemProject printFaxButton = new ViewModels.SystemProject() { Name = "PrintFaxButton", Content = "打印机和传真", Image = Configs.ICON_PRINT_AND_FAX };
            ViewModels.SystemProject regeditButton = new ViewModels.SystemProject() { Name = "RegeditButton", Content = "注册表编辑器", Image = Configs.ICON_REGEDIT };
            ViewModels.SystemProject cmdButton = new ViewModels.SystemProject() { Name = "CmdButton", Content = "命令提示符", Image = Configs.ICON_CMD };
            ViewModels.SystemProject folderOptionsButton = new ViewModels.SystemProject() { Name = "FolderOptionsButton", Content = "文件夹选项", Image = Configs.ICON_FOLDER_OPTIONS };
            ViewModels.SystemProject mstscButton = new ViewModels.SystemProject() { Name = "MstscButton", Content = "远程桌面", Image = Configs.ICON_MSTSC };
            systemLinks.Add(myComputer);
            systemLinks.Add(myDocument);
            systemLinks.Add(control);
            systemLinks.Add(recycleBin);
            systemLinks.Add(ieButton);
            systemLinks.Add(internetButton);
            systemLinks.Add(explorerButton);
            systemLinks.Add(printFaxButton);
            systemLinks.Add(regeditButton);
            systemLinks.Add(cmdButton);
            systemLinks.Add(folderOptionsButton);
            systemLinks.Add(mstscButton);
            #endregion

            #region 系统操作
            List<ViewModels.SystemProject> systemOperates = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject closePcButton = new ViewModels.SystemProject() { Name = "ClosePcButton", Content = "关闭计算机", Image = Configs.ICON_CLOSE_PC };
            ViewModels.SystemProject restartPcButton = new ViewModels.SystemProject() { Name = "RestartPcButton", Content = "重启计算机", Image = Configs.ICON_RESTART_PC };
            ViewModels.SystemProject logOutButton = new ViewModels.SystemProject() { Name = "LogOutButton", Content = "注销计算机", Image = Configs.ICON_LOGOUT_PC };
            ViewModels.SystemProject lockPcButton = new ViewModels.SystemProject() { Name = "LockPcButton", Content = "锁定计算机", Image = Configs.ICON_LOCK_PC };
            ViewModels.SystemProject standbyPcButton = new ViewModels.SystemProject() { Name = "StandbyPcButton", Content = "待机", Image = Configs.ICON_STANDBY_PC };
            ViewModels.SystemProject sleepPcButton = new ViewModels.SystemProject() { Name = "SleepPcButton", Content = "休眠", Image = Configs.ICON_SLEEP_PC };
            ViewModels.SystemProject netEndButton = new ViewModels.SystemProject() { Name = "NetEndButton", Content = "断开所有拨号网络", Image = Configs.ICON_NET_END };
            ViewModels.SystemProject screenSaverButton = new ViewModels.SystemProject() { Name = "ScreenSaverButton", Content = "屏幕保护", Image = Configs.ICON_SCREEN_SAVER };
            ViewModels.SystemProject openCdRomButton = new ViewModels.SystemProject() { Name = "OpenCdRomButton", Content = "打开光驱", Image = Configs.ICON_CD_ROM };
            ViewModels.SystemProject closeCdRomButton = new ViewModels.SystemProject() { Name = "CloseCdRomButton", Content = "关闭光驱", Image = Configs.ICON_CD_ROM };
            ViewModels.SystemProject showHideTaskbarButton = new ViewModels.SystemProject() { Name = "ShowHideTaskbarButton", Content = "隐藏/显示任务栏", Image = Configs.ICON_TASKBAR };
            ViewModels.SystemProject turnoffMonitorButton = new ViewModels.SystemProject() { Name = "TurnoffMonitorButton", Content = "关闭显示器", Image = Configs.ICON_TURNOFF_MONITOR };
            ViewModels.SystemProject clearRecycleBinButton = new ViewModels.SystemProject() { Name = "ClearRecycleBinButton", Content = "清空回收站", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject clearIeAddressButton = new ViewModels.SystemProject() { Name = "ClearIeAddressButton", Content = "清空IE地址栏记录", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject clearIeHistoryButton = new ViewModels.SystemProject() { Name = "ClearIeHistoryButton", Content = "清空IE历史记录", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject clearIeCookiesButton = new ViewModels.SystemProject() { Name = "ClearIeCookiesButton", Content = "清空Cookies", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject clearRentButton = new ViewModels.SystemProject() { Name = "ClearRentButton", Content = "清空历史文档", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject clearSomeDirectoryButton = new ViewModels.SystemProject() { Name = "ClearSomeDirectoryButton", Content = "清空某目录", Image = Configs.ICON_RECYCLE_BIN };
            ViewModels.SystemProject controlAppMemoryButton = new ViewModels.SystemProject() { Name = "ControlAppMemoryButton", Content = "控制程序内存", Image = Configs.ICON_MEMORY };
            ViewModels.SystemProject endProcessButton = new ViewModels.SystemProject() { Name = "EndProcessButton", Content = "结束程序进程", Image = Configs.ICON_END_PROCESS };
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
            List<ViewModels.SystemProject> systemAudioNormals = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject volumeAddButton = new ViewModels.SystemProject() { Name = "VolumeAddButton", Content = "音量增加", Image = Configs.ICON_VOLUME_ADD };
            ViewModels.SystemProject volumeReduceButton = new ViewModels.SystemProject() { Name = "VolumeReduceButton", Content = "音量减少", Image = Configs.ICON_VOLUME_REDUCE };
            ViewModels.SystemProject volumeSilentToggleButton = new ViewModels.SystemProject() { Name = "VolumeSilentToggleButton", Content = "静音切换", Image = Configs.ICON_SILENT_TOGGLE };
            systemAudioNormals.Add(volumeAddButton);
            systemAudioNormals.Add(volumeReduceButton);
            systemAudioNormals.Add(volumeSilentToggleButton);

            List<ViewModels.SystemProject> systemAudioWaves = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject volumeWaveAddButton = new ViewModels.SystemProject() { Name = "VolumeWaveAddButton", Content = "音量增加", Image = Configs.ICON_VOLUME_ADD };
            ViewModels.SystemProject volumeWaveReduceButton = new ViewModels.SystemProject() { Name = "VolumeWaveReduceButton", Content = "音量减少", Image = Configs.ICON_VOLUME_REDUCE };
            ViewModels.SystemProject volumeWaveSilentToggleButton = new ViewModels.SystemProject() { Name = "VolumeWaveSilentToggleButton", Content = "静音切换", Image = Configs.ICON_SILENT_TOGGLE };
            systemAudioWaves.Add(volumeWaveAddButton);
            systemAudioWaves.Add(volumeWaveReduceButton);
            systemAudioWaves.Add(volumeWaveSilentToggleButton);

            List<ViewModels.SystemProject> systemAudioMics = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject volumeMicAddButton = new ViewModels.SystemProject() { Name = "VolumeMicAddButton", Content = "音量增加", Image = Configs.ICON_VOLUME_ADD };
            ViewModels.SystemProject volumeMicReduceButton = new ViewModels.SystemProject() { Name = "VolumeMicReduceButton", Content = "音量减少", Image = Configs.ICON_VOLUME_REDUCE };
            ViewModels.SystemProject volumeMicSilentToggleButton = new ViewModels.SystemProject() { Name = "VolumeMicSilentToggleButton", Content = "静音切换", Image = Configs.ICON_SILENT_TOGGLE };
            systemAudioMics.Add(volumeMicAddButton);
            systemAudioMics.Add(volumeMicReduceButton);
            systemAudioMics.Add(volumeMicSilentToggleButton);

            List<ViewModels.SystemProject> systemAudioLines = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject volumeLineAddButton = new ViewModels.SystemProject() { Name = "VolumeLineAddButton", Content = "音量增加", Image = Configs.ICON_VOLUME_ADD };
            ViewModels.SystemProject volumeLineReduceButton = new ViewModels.SystemProject() { Name = "VolumeLineReduceButton", Content = "音量减少", Image = Configs.ICON_VOLUME_REDUCE };
            ViewModels.SystemProject volumeLineSilentToggleButton = new ViewModels.SystemProject() { Name = "VolumeLineSilentToggleButton", Content = "静音切换", Image = Configs.ICON_SILENT_TOGGLE };
            systemAudioLines.Add(volumeLineAddButton);
            systemAudioLines.Add(volumeLineReduceButton);
            systemAudioLines.Add(volumeLineSilentToggleButton);

            List<ViewModels.SystemProject> systemAudioCdRoms = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject volumeCdRomAddButton = new ViewModels.SystemProject() { Name = "VolumeCdRomAddButton", Content = "音量增加", Image = Configs.ICON_VOLUME_ADD };
            ViewModels.SystemProject volumeCdRomReduceButton = new ViewModels.SystemProject() { Name = "VolumeCdRomReduceButton", Content = "音量减少", Image = Configs.ICON_VOLUME_REDUCE };
            ViewModels.SystemProject volumeCdRomSilentToggleButton = new ViewModels.SystemProject() { Name = "VolumeCdRomSilentToggleButton", Content = "静音切换", Image = Configs.ICON_SILENT_TOGGLE };
            systemAudioCdRoms.Add(volumeCdRomAddButton);
            systemAudioCdRoms.Add(volumeCdRomReduceButton);
            systemAudioCdRoms.Add(volumeCdRomSilentToggleButton);
            #endregion

            #region 控制面板
            List<ViewModels.SystemProject> systemControls = new List<ViewModels.SystemProject>();
            ViewModels.SystemProject addOrRemoveAppButton = new ViewModels.SystemProject() { Name = "AddOrRemoveAppButton", Content = "添加或删除程序", Image = Configs.ICON_ADD_OR_REMOVE_APP };
            ViewModels.SystemProject internetOptionsButton = new ViewModels.SystemProject() { Name = "InternetOptionsButton", Content = "Internet选项", Image = Configs.ICON_INTERNET_OPTIONS };
            ViewModels.SystemProject userAccountButton = new ViewModels.SystemProject() { Name = "UserAccountButton", Content = "用户账户", Image = Configs.ICON_USER_ACCOUNT };
            ViewModels.SystemProject regionLanguageOptionsButton = new ViewModels.SystemProject() { Name = "RegionLanguageOptionsButton", Content = "区域和语言选项", Image = Configs.ICON_REGION_LANGUAGE_OPTIONS };
            ViewModels.SystemProject phoneAndModemOptionsButton = new ViewModels.SystemProject() { Name = "PhoneAndModemOptionsButton", Content = "电话和调制解调器选项", Image = Configs.ICON_PHONE_MODEM };
            ViewModels.SystemProject accessibilityOptionsButton = new ViewModels.SystemProject() { Name = "AccessibilityOptionsButton", Content = "辅助功能选项", Image = Configs.ICON_ACCESSIBILITY_OPTIONS };
            ViewModels.SystemProject powerOptionsButton = new ViewModels.SystemProject() { Name = "PowerOptionsButton", Content = "电源选项", Image = Configs.ICON_POWER_OPTIONS };
            ViewModels.SystemProject gameControllerButton = new ViewModels.SystemProject() { Name = "GameControllerButton", Content = "游戏控制器", Image = Configs.ICON_GAME_CONTROLLER };
            ViewModels.SystemProject networkConnectButton = new ViewModels.SystemProject() { Name = "NetworkConnectButton", Content = "网络连接", Image = Configs.ICON_NETWORK };
            ViewModels.SystemProject screenShowButton = new ViewModels.SystemProject() { Name = "ScreenShowButton", Content = "屏幕显示", Image = Configs.ICON_SCREEN_SAVER };
            ViewModels.SystemProject systemPropertiesButton = new ViewModels.SystemProject() { Name = "SystemPropertiesButton", Content = "系统属性", Image = Configs.ICON_SYSTEM_PROPERTIES };
            ViewModels.SystemProject addHardwareButton = new ViewModels.SystemProject() { Name = "AddHardwareButton", Content = "添加硬件", Image = Configs.ICON_ADD_HARDWARE };
            ViewModels.SystemProject mouseButton = new ViewModels.SystemProject() { Name = "MouseButton", Content = "鼠标", Image = Configs.ICON_MOUSE };
            ViewModels.SystemProject keyboardButton = new ViewModels.SystemProject() { Name = "KeyboardButton", Content = "键盘", Image = Configs.ICON_KEYBOARD };
            ViewModels.SystemProject soundAudioEquipmentButton = new ViewModels.SystemProject() { Name = "SoundAudioEquipmentButton", Content = "声音和音频设备", Image = Configs.ICON_SOUND_AUDIO_EQUIPMENT };
            ViewModels.SystemProject volumeControlButton = new ViewModels.SystemProject() { Name = "VolumeControlButton", Content = "音量控制", Image = Configs.ICON_VOLUME_CONTROL };
            ViewModels.SystemProject dateTimeButton = new ViewModels.SystemProject() { Name = "DateTimeButton", Content = "日期和时间", Image = Configs.ICON_DATE_TIME };
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
            string path = "#" + buttonName.Replace("Button", string.Empty);
            string arguments = string.Empty;
            if (SystemProjectParam.CLEAR_SOME_DIRECTORY.Equals(path)) {
                Topmost = false;
                // 清空某目录打开目录窗口选择目录
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if (System.Windows.Forms.DialogResult.OK == fbd.ShowDialog()) {
                    string dir = fbd.SelectedPath;
                    name = $"清空{Path.GetFileName(Path.GetDirectoryName(dir))}目录";
                    arguments = Path.GetDirectoryName(dir);
                } else {
                    MessageBox.Show("未选择目录!", "错误");
                    isAdd = false;
                }
                Topmost = true;
            } else if (SystemProjectParam.CONTROL_APP_MEMORY.Equals(path)) {
                Topmost = false;
                // 控制程序内存
                ControlAppMemory cam = new ControlAppMemory() { Topmost = true };
                if (true == cam.ShowDialog()) {
                    name = $"控制{cam.AppName}内存";
                    arguments = $"{cam.AppName}#{cam.MinMemory}#{cam.MaxMemory}";
                } else {
                    isAdd = false;
                }
                Topmost = true;
            } else if (SystemProjectParam.MSTSC.Equals(path)) {
                // 远程桌面
                Topmost = false;
                MstscWindow mstsc = new MstscWindow() { Topmost = true };
                if (true == mstsc.ShowDialog()) {
                    name = $"{mstsc.vm.Address}远程";
                    arguments = $"{mstsc.vm.Address}{Constants.SPLIT_CHAR}{mstsc.vm.Port}{Constants.SPLIT_CHAR}{mstsc.vm.Account}{Constants.SPLIT_CHAR}{mstsc.vm.Password}";
                } else {
                    isAdd = false;
                }
                Topmost = true;
            }

            if (isAdd) {
                // 创建系统功能项目对象（不可自启动）
                Project app = new Project {
                    TypeSection = vm.TypeSection, ColumnSection = vm.ColumnSection,
                    Kind = Project.KIND_SYSTEM, Name = name, Path = path, IconPath = path, Arguments = arguments, CanAutoRun = false
                };
                if (vm.MultiAdd) {
                    // 直接添加应用
                    XStartService.AddNewApp(app);
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
