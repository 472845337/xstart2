using System.Windows;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// AvatarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window {
        public UserViewModel vm = new UserViewModel();
        public UserWindow(string avatar, string nickName) {
            InitializeComponent();
            Loaded += Window_Loaded;
            vm.Avatar = avatar;
            vm.NickName = nickName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }

        private void SelectAvatar_Click(object sender, RoutedEventArgs e) {
            // 选择图片文件
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "图片文件|*.jpg;*.jpeg;*.bmp;*.png;*.gif" };
            if (System.Windows.Forms.DialogResult.OK == ofd.ShowDialog()) {
                string avatarFileName = ofd.FileName;
                vm.Avatar = avatarFileName;
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
