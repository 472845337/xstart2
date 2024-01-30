using System.Windows;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// AvatarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window {
        public UserViewModel vm = new UserViewModel();
        public UserWindow(string avatarPath, string nickName) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            vm.AvatarPath = avatarPath;
            vm.NickName = nickName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }
        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void SelectAvatar_Click(object sender, RoutedEventArgs e) {
            // 选择图片文件
            using System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "图片文件|*.jpg;*.jpeg;*.bmp;*.png;*.gif" };
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                string avatarFileName = ofd.FileName;
                vm.AvatarPath = avatarFileName;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
