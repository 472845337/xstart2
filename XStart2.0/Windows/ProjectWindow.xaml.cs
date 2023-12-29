using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// ProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectWindow : Window {
        ProjectViewModel vm = new ProjectViewModel();
        public Project Project { get; set; }
        public ProjectWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        public ProjectWindow(string title, string typeSection, string columnSection) {
            InitializeComponent();
            vm.Title = title;
            vm.TypeSection = typeSection;
            vm.ColumnSection = columnSection;
            Loaded += Window_Loaded;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {

            if (null != Project) {
                vm.Project = Project;
                vm.Name = Project.Name;
                vm.Path = Project.Path;
                vm.Kind = Project.Kind;
                vm.IconIndex = Project.IconIndex;
                vm.IconPath = Project.IconPath;
                vm.Arguments = Project.Arguments;
                vm.RunStartPath = Project.RunStartPath;
                vm.HotKey = Project.HotKey;
                vm.Remark = Project.Remark;
                if (Project.Path.StartsWith("#")) {
                    // 系统功能路径不可以变更
                    vm.PathEnable = false;
                }
            } else {
                // 新增的时候不会传对象过来
                Project = new Project();
                Project.TypeSection = vm.TypeSection;
                Project.ColumnSection = vm.ColumnSection;
            }
            DataContext = vm;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e) {
            Project.Name = vm.Name;
            Project.Path = vm.Path;
            Project.IconIndex = vm.IconIndex;
            Project.IconPath = vm.IconPath;
            Project.Arguments = vm.Arguments;
            Project.RunStartPath = vm.RunStartPath;
            Project.HotKey = vm.HotKey;
            Project.Remark = vm.Remark;
            DialogResult = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
        // 选择文件
        private void FileBtn_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "所有文件|*.*|可执行文件|.exe|音频文件|*.mp3;*.wav;*.wma;*.ape;*.flac|视频文件|*.avi;*.mp4;*.wmv;*.mkv;*.rmvb;*.mov;*.flv|图片文件|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.jfif|文档文件|*.doc;*.xls;*.ppt;*.docx;*.xlsx;*pptx;*.rtf;*.txt;*.pdf" };
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                string filePath = ofd.FileName;
                vm.PathEnable = true;
                vm.ArgumentsEnable = true;
                vm.Path = filePath;
                // 自动判断kind
                vm.Kind = Project.KIND_FILE;
                // 名称自动赋值
                if (string.IsNullOrEmpty(vm.Name)) {
                    vm.Name = Path.GetFileNameWithoutExtension(filePath);
                }
                if (string.IsNullOrEmpty(vm.IconPath)) {
                    // 图标自动赋值
                    vm.IconPath = filePath;
                    Project project = new Project {
                        Path = filePath,
                        IconPath = filePath,
                        Kind = vm.Kind
                    };
                    vm.Project = project;
                }
            }
        }
        // 选择目录
        private void FolderBtn_Click(object sender, RoutedEventArgs e) {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (System.Windows.Forms.DialogResult.OK == fbd.ShowDialog()) {
                string folderPath = fbd.SelectedPath;
                string dirName = Path.GetDirectoryName(folderPath);
                vm.PathEnable = true;
                vm.ArgumentsEnable = true;
                vm.Path = dirName;
                vm.Kind = Project.KIND_DIRECTORY;
                // 名称自动赋值,普通目录和磁盘名不同的处理方式
                if (string.IsNullOrEmpty(vm.Name)) {
                    if (string.IsNullOrEmpty(dirName)) {
                        // 选择的磁盘
                        DriveInfo driveInfo = WinUtils.GetDriveInfoByName(folderPath);
                        vm.Name = $"{driveInfo.VolumeLabel}({folderPath.Substring(0, folderPath.IndexOf(":"))})";
                    } else {
                        // 选择的普通目录
                        vm.Name = Path.GetFileName(dirName);
                    }
                }
                // 图标自动赋值
                if (string.IsNullOrEmpty(vm.IconPath)) {
                    vm.IconPath = dirName;
                    Project project = new Project {
                        Path = dirName,
                        IconPath = dirName,
                        Kind = vm.Kind
                    };
                    vm.Project = project;
                }
            }
        }
        // 选择系统功能
        private void SystemBtn_Click(object sender, RoutedEventArgs e) {
            SystemProjectWindow spw = new SystemProjectWindow(vm.TypeSection, vm.ColumnSection, Configs.systemAppAddMulti, Configs.systemAppOpenPage) { };
            if (true == spw.ShowDialog()) {
                vm.PathEnable = false;
                vm.ArgumentsEnable = false;
                vm.Name = spw.Project.Name;
                vm.Path = spw.Project.Path;
                vm.Kind = spw.Project.Kind;
                vm.IconIndex = spw.Project.IconIndex;
                vm.IconPath = spw.Project.IconPath;
                vm.Arguments = spw.Project.Arguments;
                vm.RunStartPath = spw.Project.RunStartPath;
                vm.HotKey = spw.Project.HotKey;
                vm.Remark = spw.Project.Remark;
                Project = spw.Project;
            }
            if (Configs.systemAppAddMulti != spw.MultiAdd) {
                Configs.systemAppAddMulti = spw.MultiAdd;
                IniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_ADD_MULTI, Convert.ToString(spw.MultiAdd));
            }
            if (Configs.systemAppOpenPage != spw.OpenPage) {
                Configs.systemAppOpenPage = spw.OpenPage;
                IniUtils.IniWriteValue(Constants.SET_FILE, Constants.SECTION_SYSTEM_APP, Constants.KEY_SYSTEM_PROJECT_OPEN_PAGE, Convert.ToString(spw.OpenPage));
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "带图标文件|*.exe;*.ico" };
            if(System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                vm.IconPath = ofd.FileName;
                vm.Project = new Project() { IconPath = ofd.FileName };
            }
        }
    }
}
