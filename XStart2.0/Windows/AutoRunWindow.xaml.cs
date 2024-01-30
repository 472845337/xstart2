using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using XStart2._0.Bean;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// AutoRunWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AutoRunWindow : Window {
        readonly AutoRunViewModel vm = new AutoRunViewModel();
        public List<Project> AutoRunProjects { get; set; }
        public List<Project> Projects { get; set; }

        public bool IsStart { get; set; } = true;
        public bool IsExit { get; set; } = false;
        public AutoRunWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.AutoRunProjects = AutoRunProjects;
            vm.IsStart = IsStart;
            DataContext = vm;
        }
        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e) {
            CheckBox cb = sender as CheckBox;
            foreach (Project p in AutoRunProjects) {
                p.AutoRun = cb.IsChecked;
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e) {
            Projects = AutoRunProjects.Where(item => true == item.AutoRun).ToList();
            if (null != Projects && Projects.Count > 0) {
                // 启动选中的自启项
                DialogResult = true;
            } else {
                MessageBox.Show("未选择启动项目", Const.Constants.MESSAGE_BOX_TITLE_WARN);
            }

        }

        private void Ignore_Click(object sender, RoutedEventArgs e) {
            // 不启动自启项
            DialogResult = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) {
            // 直接退出
            DialogResult = false;
            IsExit = true;
        }
    }
}
