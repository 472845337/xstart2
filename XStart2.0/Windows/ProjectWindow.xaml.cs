using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using Utils;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// ProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectWindow : Window {
        readonly ProjectViewModel vm = new ProjectViewModel();
        public Project Project { get; set; }

        public ProjectWindow(string title, string typeSection, string columnSection) {
            InitializeComponent();
            vm.Title = title;
            vm.TypeSection = typeSection;
            vm.ColumnSection = columnSection;
            vm.TopMost = true;
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if (null != Project) {
                vm.Name = Project.Name;
                vm.Path = Project.Path;
                vm.Kind = Project.Kind;
                vm.IconIndex = Project.IconIndex;
                vm.IconPath = Project.IconPath;
                vm.Arguments = Project.Arguments;
                vm.RunStartPath = Project.RunStartPath;
                vm.HotKey = Project.HotKey;
                vm.Remark = Project.Remark;
                SystemPathSpecialArguments(vm.Path);
            } else {
                // 新增的时候不会传对象过来
                Project = new Project {
                    TypeSection = vm.TypeSection,
                    ColumnSection = vm.ColumnSection
                };
            }
            vm.Project = Project;
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void SystemPathSpecialArguments(string path) {
            if (path.StartsWith(Constants.SYSTEM_PROJECT_CHAR)) {
                // 系统功能路径不可以变更
                vm.PathReadonly = true;
                if (SystemProjectParam.CLEAR_SOME_DIRECTORY.Equals(path)) {
                    // 清空目录
                    vm.ArgumentsReadonly = true;
                    ArgumentsTextBox.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(ClearSomeDirectory_ArgumentsTextBoxMouseLeftButtonUp), true);
                } else if (SystemProjectParam.MSTSC.Equals(path)) {
                    // 远程桌面应用
                    vm.ArgumentsReadonly = true;
                    ArgumentsTextBox.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(MstscProject_ArgumentsTextBoxMouseLeftButtonUp), true);
                } else if (SystemProjectParam.CONTROL_APP_MEMORY.Equals(path)) {
                    // 控制应用内存
                    vm.ArgumentsReadonly = true;
                    ArgumentsTextBox.AddHandler(MouseLeftButtonUpEvent, new MouseButtonEventHandler(ControlAppMemoryProject_ArgumentsTextBoxMouseLeftButtonUp), true);
                }
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e) {
            string errMsg = null;
            if (string.IsNullOrEmpty(vm.Name)) {
                errMsg = "项目名称不能为空！";
            } else if (string.IsNullOrEmpty(vm.Path)) {
                errMsg = "项目路径不能为空！";
            }
            if (string.IsNullOrEmpty(errMsg)) {
                Project.Name = vm.Name;
                Project.Path = vm.Path;
                Project.Kind = vm.Kind;
                Project.IconIndex = vm.IconIndex;
                Project.IconPath = vm.IconPath;
                // 特殊处理的参数排除
                if (SystemProjectParam.MSTSC.Equals(vm.Path) || SystemProjectParam.CLEAR_SOME_DIRECTORY.Equals(vm.Path) || SystemProjectParam.CONTROL_APP_MEMORY.Equals(vm.Path)) {
                    Project.Arguments = vm.Arguments;
                } else {
                    Project.Arguments = vm.ShowArguments;
                }

                Project.RunStartPath = vm.RunStartPath;
                Project.HotKey = vm.HotKey;
                Project.Remark = vm.Remark;
                DialogResult = true;
            } else {
                MsgBoxUtils.ShowError(errMsg);
            }
            e.Handled = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            e.Handled = true;
        }
        // 选择文件
        private void FileBtn_Click(object sender, RoutedEventArgs e) {
            using System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "所有文件|*.*|可执行文件|*.exe|音频文件|*.mp3;*.wav;*.wma;*.ape;*.flac|视频文件|*.avi;*.mp4;*.wmv;*.mkv;*.rmvb;*.mov;*.flv|图片文件|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.jfif|文档文件|*.doc;*.xls;*.ppt;*.docx;*.xlsx;*pptx;*.rtf;*.txt;*.pdf" };
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                string filePath = ofd.FileName;
                vm.PathReadonly = false;
                vm.ArgumentsReadonly = false;
                vm.Path = filePath;
                // 自动判断kind
                vm.Kind = Project.KIND_FILE;
                // 名称自动赋值
                if (string.IsNullOrEmpty(vm.Name)) {
                    vm.Name = Path.GetFileNameWithoutExtension(filePath);
                }
                if (string.IsNullOrEmpty(vm.IconPath)) {
                    vm.IconPath = filePath;
                }
            }
        }
        // 选择目录
        private void FolderBtn_Click(object sender, RoutedEventArgs e) {
            using System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            if (System.Windows.Forms.DialogResult.OK == fbd.ShowDialog()) {
                string folderPath = fbd.SelectedPath;
                string dirName = Path.GetDirectoryName(folderPath);
                vm.PathReadonly = false;
                vm.ArgumentsReadonly = false;
                vm.Path = folderPath;
                vm.Kind = Project.KIND_DIRECTORY;
                // 名称自动赋值,普通目录和磁盘名不同的处理方式
                if (string.IsNullOrEmpty(vm.Name)) {
                    if (string.IsNullOrEmpty(dirName)) {
                        // 选择的磁盘
                        DriveInfo driveInfo = XStartWinUtils.GetDriveInfoByName(folderPath);
                        vm.Name = $"{driveInfo.VolumeLabel}({folderPath.Substring(0, folderPath.IndexOf(":"))})";
                    } else {
                        // 选择的普通目录
                        vm.Name = Path.GetFileName(folderPath);
                    }
                }
                // 图标自动赋值
                if (string.IsNullOrEmpty(vm.IconPath)) {
                    vm.IconPath = folderPath;
                }
            }
        }
        // 选择系统功能
        private void SystemBtn_Click(object sender, RoutedEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            SystemProjectWindow spw = new SystemProjectWindow(vm.TypeSection, vm.ColumnSection, Configs.systemAppAddMulti, Configs.systemAppOpenPage) { Owner = this };
            if (true == spw.ShowDialog()) {
                vm.PathReadonly = true;
                vm.ArgumentsReadonly = true;
                vm.Name = spw.Project.Name;
                vm.Path = spw.Project.Path;
                vm.Kind = spw.Project.Kind;
                vm.IconIndex = spw.Project.IconIndex;
                vm.IconPath = spw.Project.IconPath;
                vm.Arguments = spw.Project.Arguments;
                vm.RunStartPath = spw.Project.RunStartPath;
                vm.HotKey = spw.Project.HotKey;
                vm.Remark = spw.Project.Remark;
                SystemPathSpecialArguments(vm.Path);
            }
            IniParser.Model.IniData iniData = new IniParser.Model.IniData();
            if (Configs.systemAppAddMulti != spw.MultiAdd) {
                Configs.systemAppAddMulti = spw.MultiAdd;
                iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_ADD_MULTI] = Convert.ToString(spw.MultiAdd);
            }
            if (Configs.systemAppOpenPage != spw.OpenPage) {
                Configs.systemAppOpenPage = spw.OpenPage;
                iniData[Constants.SECTION_SYSTEM_APP][Constants.KEY_SYSTEM_PROJECT_OPEN_PAGE] = Convert.ToString(spw.OpenPage);
            }
            if (iniData.Sections.Count > 0) {
                IniParserUtils.SaveIniData(Configs.AppStartPath + Constants.SET_FILE, iniData);
            }
            spw.Close();
            OpenNewWindowUtils.RecoverTopmost(this, vm);
        }

        private void Border_MouseLeftButtonUp(object sender, RoutedEventArgs e) {
            using System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "带图标文件|*.exe;*.ico" };
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                vm.IconPath = ofd.FileName;
            }
        }

        private void ClearSomeDirectory_ArgumentsTextBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            string directory = vm.Arguments;
            using System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog() { SelectedPath = directory };
            if (System.Windows.Forms.DialogResult.OK == fbd.ShowDialog()) {
                string dir = fbd.SelectedPath;
                vm.Name = $"清空{Path.GetFileName(Path.GetDirectoryName(dir))}目录";
                vm.Arguments = Path.GetDirectoryName(dir);
            }
        }

        private void MstscProject_ArgumentsTextBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            string[] argumentArray = vm.Arguments.Split(Constants.SPLIT_CHAR);

            MstscWindow mstsc = new MstscWindow { Owner = this };
            mstsc.vm.Address = argumentArray[0];
            mstsc.vm.Port = argumentArray[1];
            mstsc.vm.Account = argumentArray[2];
            mstsc.vm.Password = argumentArray[3];
            if (true == mstsc.ShowDialog()) {
                vm.Arguments = $"{mstsc.vm.Address}{Constants.SPLIT_CHAR}{mstsc.vm.Port}{Constants.SPLIT_CHAR}{mstsc.vm.Account}{Constants.SPLIT_CHAR}{mstsc.vm.Password}";
            }
            mstsc.Close();
            OpenNewWindowUtils.RecoverTopmost(this, vm);
        }

        private void ControlAppMemoryProject_ArgumentsTextBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            OpenNewWindowUtils.SetTopmost(this);
            string[] argumentArray = vm.Arguments.Split(Constants.SPLIT_CHAR);
            ControlAppMemoryWindow cam = new ControlAppMemoryWindow { Owner = this };
            cam.vm.AppName = argumentArray[0];
            cam.vm.MinMemory = argumentArray[1];
            cam.vm.MaxMemory = argumentArray[2];
            if (true == cam.ShowDialog()) {
                vm.Name = $"控制{cam.vm.AppName}内存";
                vm.Arguments = $"{cam.vm.AppName}{Constants.SPLIT_CHAR}{cam.vm.MinMemory}{Constants.SPLIT_CHAR}{cam.vm.MaxMemory}";
            }
            cam.Close();
            OpenNewWindowUtils.RecoverTopmost(this, vm);
        }
    }
}
