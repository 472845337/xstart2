using Microsoft.Win32;
using NAudio.CoreAudioApi;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using static XStart2._0.Bean.SystemProjectParam;

namespace XStart2._0.Utils {
    public static class ProjectUtils {

        public static string ExecuteApp(Project project) {
            string result = string.Empty;
            // 根据应用类型，区分执行
            switch (project.Kind) {
                case Project.KIND_FILE:
                    if (project.Path.EndsWith(".exe", true, CultureInfo.CurrentCulture) || project.Path.EndsWith(".bat", true, CultureInfo.CurrentCulture) || project.Path.EndsWith(".cmd", true, CultureInfo.CurrentCulture)) {
                        CliWrapUtils.ExecuteApp(project.Path, string.IsNullOrEmpty(project.RunStartPath) ? Path.GetDirectoryName(project.Path) : project.RunStartPath, project.Arguments);
                    } else {
                        Process.Start(project.Path);
                    }
                    break;
                case Project.KIND_DIRECTORY:
                    Process.Start(project.Path);
                    break;
                case Project.KIND_SYSTEM:
                    // 系统功能
                    switch (project.Path) {
                        case MSTSC:
                            string rdpFilePath = Configs.AppStartPath + @$"rdp\{project.Section}.rdp";
                            Process p = new Process();
                            ProcessStartInfo pInfo = new ProcessStartInfo("mstsc", $"\"{rdpFilePath}\"");
                            p.StartInfo = pInfo;
                            p.Start();
                            break;
                        case NET_END: AsdlUtils.Disconnect(); break;
                        case OPEN_CD_ROM: CdRomUtils.OpenCdRom(); break;
                        case CLOSE_CD_ROM: CdRomUtils.CloseCdRom(); break;
                        case SHOW_HIDE_TASKBAR:
                            ushort toggle = Configs.taskbarIsShow ? WinApi.SW_HIDE : WinApi.SW_SHOW;
                            DllUtils.ShowWindow(Configs.taskbarHandler, toggle);
                            Configs.taskbarIsShow = !Configs.taskbarIsShow;
                            break;
                        case TURN_OFF_MONITOR: DllUtils.SendMessage(WinApi.HWND_BROADCAST, WinApi.WM_SYSCOMMAND, WinApi.SC_MONITORPOWER, 2); break;
                        case CLEAR_RECYCLE_BIN: DllUtils.SHEmptyRecycleBin(Configs.Handler, null, WinApi.SHERB_NOCONFIRMATION); break;
                        case CLEAR_IE_ADDRESS: {
                                using RegistryKey userKey = Registry.CurrentUser.CreateSubKey(@"Software/Microsoft/Internet Explorer/TypedURLs");
                                foreach (string mru in userKey.GetValueNames()) {
                                    if (mru != "MRUList") {
                                        userKey.DeleteValue(userKey.GetValue(mru) as string, false);
                                    }
                                }
                            }
                            break;
                        case CLEAR_RENT: DllUtils.SHAddToRecentDocs(WinApi.ShellAddToRecentDocsFlags.Pidl, null); break;
                        case CLEAR_SOME_DIRECTORY: {
                                string[] files = Directory.GetFiles(project.Arguments); if (files.Length > 0) {
                                    foreach (string filename in files) { File.Delete(filename); }
                                }
                            }
                            break;
                        case CONTROL_APP_MEMORY: {
                                string[] arguments = project.Arguments.Split('#');
                                int minSize = arguments.Length > 1 ? Convert.ToInt32(arguments[1]) : 0;
                                int maxSize = arguments.Length > 1 ? Convert.ToInt32(arguments[2]) : Convert.ToInt32(arguments[1]);
                                Process[] processes = Process.GetProcessesByName(arguments[0]);
                                foreach (Process process in processes) {
                                    try {
                                        DllUtils.SetProcessWorkingSetSize(process.MainWindowHandle, minSize * 1024 * 1024, maxSize * 104 * 1024);
                                    } catch (Exception) {

                                    }
                                }
                            }
                            break;
                        case END_PROCESS: {
                                Process[] processes = Process.GetProcessesByName(project.Arguments);
                                foreach (Process process in processes) {
                                    try {
                                        process.Kill();
                                    } catch (Exception) {

                                    }
                                }
                            }
                            break;
                        case VOLUME_ADD: DllUtils.SendMessage(Configs.Handler, WinApi.WM_APPCOMMAND, Configs.Handler.ToInt32(), WinApi.APPCOMMAND_VOLUME_UP); break;
                        case VOLUME_REDUCE: DllUtils.SendMessage(Configs.Handler, WinApi.WM_APPCOMMAND, Configs.Handler.ToInt32(), WinApi.APPCOMMAND_VOLUME_DOWN); break;
                        case VOLUME_SILENT_TOGGLE: DllUtils.SendMessage(Configs.Handler, WinApi.WM_APPCOMMAND, Configs.Handler.ToInt32(), WinApi.APPCOMMAND_VOLUME_MUTE); break;
                        case VOLUME_WAVE_ADD: {
                                uint left = Configs.volume > (0xffff - Constants.VOLUME_WAVE_STEP) ? 0xffff : (Configs.volume + Constants.VOLUME_WAVE_STEP);//左声道音量
                                uint right = Configs.volume > (0xffff - Constants.VOLUME_WAVE_STEP) ? 0xffff : (Configs.volume + Constants.VOLUME_WAVE_STEP);//右
                                Configs.volume = Configs.volume > (0xffff - Constants.VOLUME_WAVE_STEP) ? 0xffff : (Configs.volume + Constants.VOLUME_WAVE_STEP);
                                DllUtils.waveOutSetVolume(0, left & 0x0000FFFF | right << 16);
                            }
                            break;
                        case VOLUME_WAVE_REDUCE: {
                                uint left = Configs.volume < Constants.VOLUME_WAVE_STEP ? 0x0000 : (Configs.volume - Constants.VOLUME_WAVE_STEP);//左声道音量
                                uint right = Configs.volume < Constants.VOLUME_WAVE_STEP ? 0x0000 : (Configs.volume - Constants.VOLUME_WAVE_STEP);//右
                                Configs.volume = Configs.volume < Constants.VOLUME_WAVE_STEP ? 0x0000 : (Configs.volume - Constants.VOLUME_WAVE_STEP);
                                DllUtils.waveOutSetVolume(0, left & 0x0000FFFF | right << 16);
                            }
                            break;
                        case VOLUME_WAVE_SILENT_TOGGLE: {
                                if (Configs.waveMuted) {
                                    DllUtils.waveOutSetVolume(0, Configs.volume & 0x0000FFFF | Configs.volume << 16);
                                } else {
                                    DllUtils.waveOutSetVolume(0, 0x0000 & 0x0000FFFF | 0x0000 << 16);
                                }
                                Configs.waveMuted = !Configs.waveMuted;
                            }
                            break;
                        case VOLUME_MIC_ADD:
                        case VOLUME_LINE_IN_ADD:
                        case VOLUME_CD_PLAYER_ADD: {
                                string deviceName = string.Empty; int volume = 0; DataFlow df = DataFlow.Capture;
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_MIC; volume = Configs.micVolume; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_LINE_IN; volume = Configs.lineInVolume; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_CD_PLAYER; volume = Configs.cdPlayerVolume; df = DataFlow.Render; }
                                if (volume == 100) throw new Exception("已达最高值！");
                                volume = (volume + Constants.VOLUME_STEP > 100) ? 100 : (volume + Constants.VOLUME_STEP);
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { Configs.micVolume = volume; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { Configs.lineInVolume = volume; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { Configs.cdPlayerVolume = volume; }

                                AudioUtils.SetDeviceVolume(deviceName, volume, df);
                                result = "当前设备音量:" + volume;
                            }
                            break;
                        case VOLUME_MIC_REDUCE:
                        case VOLUME_LINE_IN_REDUCE:
                        case VOLUME_CD_PLAYER_REDUCE: {
                                string deviceName = string.Empty; int volume = 0; DataFlow df = DataFlow.Capture;
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_MIC; volume = Configs.micVolume; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_LINE_IN; volume = Configs.lineInVolume; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_CD_PLAYER; volume = Configs.cdPlayerVolume; df = DataFlow.Render; }
                                if (volume <= 2) throw new Exception("已达最低值！");
                                volume = (volume < Constants.VOLUME_STEP + 2) ? 2 : (volume - Constants.VOLUME_STEP);
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { Configs.micVolume = volume; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { Configs.lineInVolume = volume; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { Configs.cdPlayerVolume = volume; }

                                AudioUtils.SetDeviceVolume(deviceName, volume, df);
                                result = "当前设备音量:" + volume;
                            }
                            break;
                        case VOLUME_MIC_SILENT_TOGGLE:
                        case VOLUME_LINE_IN_SILENT_TOGGLE:
                        case VOLUME_CD_PLAYER_SILENT_TOGGLE: {
                                string deviceName = string.Empty; bool muted = false; DataFlow df = DataFlow.Capture;
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_MIC; muted = Configs.micMuted; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_LINE_IN; muted = Configs.lineInMuted; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { deviceName = Constants.DEVICE_NAME_CD_PLAYER; muted = Configs.cdPlayerMuted; df = DataFlow.Render; }
                                muted = !muted;
                                if (VOLUME_MIC_ADD.Equals(project.Path)) { Configs.micMuted = muted; } else if (VOLUME_LINE_IN_ADD.Equals(project.Path)) { Configs.lineInMuted = muted; } else if (VOLUME_CD_PLAYER_ADD.Equals(project.Path)) { Configs.cdPlayerMuted = muted; }

                                AudioUtils.SetDeviceMute(deviceName, muted, df);
                                result = "当前设备:" + (muted ? "静音" : "非静音");
                            }
                            break;
                        default:
                            // 调用exe执行的功能
                            if (OperateParam.TryGetValue(project.Path, out SystemProject appOperateParam) && !string.IsNullOrEmpty(appOperateParam.Execute)) {
                                Process.Start(appOperateParam.Execute, appOperateParam.Param);
                                break;
                            } else {
                                throw new Exception("功能失效！");
                            }

                    }
                    break;
                case Project.KIND_URL:
                    switch (Configs.urlOpen) {
                        case Constants.URL_OPEN_DEFAULT:
                            Process.Start(project.Path);
                            break;
                        case Constants.URL_OPEN_EDGE:
                            Process.Start(Constants.EDGE_PATH, project.Path);
                            break;
                        case Constants.URL_OPEN_CUSTOM:
                            if (!string.IsNullOrEmpty(Configs.urlOpenCustomBrowser)) {
                                Process.Start(Configs.urlOpenCustomBrowser, project.Path);
                            } else {
                                Process.Start(project.Path);
                            }
                            break;
                        default:
                            Process.Start(project.Path);
                            break;
                    }

                    break;
                default:
                    break;
            }
            return result;
        }

        public static Project Copy(Project p) {
            Project newP = new Project {
                Section = p.Section, TypeSection = p.TypeSection, ColumnSection = p.ColumnSection, Kind = p.Kind
                , FontColor = p.FontColor, Arguments = p.Arguments, Path = p.Path, Name = p.Name, AutoRun = p.AutoRun
                , IconPath = p.IconPath, IconIndex = p.IconIndex, RunStartPath = p.RunStartPath, HotKey = p.HotKey, Remark = p.Remark
            };
            return newP;
        }

    }
}
