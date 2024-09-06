using System.Windows;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// AvatarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window {
        private UserViewModel vm = new UserViewModel();
        public string AvatarPath {  get; set; }
        public double GifSpeedRatio { get; set; }
        public string NickName {  get; set; }
        public int AvatarSize {  get; set; }
        public UserWindow(string avatarPath, double gifSpeedRadio, string nickName, int avatarSize) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            AvatarPath = avatarPath;
            AvatarSize = avatarSize;
            GifSpeedRatio = gifSpeedRadio;
            NickName = nickName;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.AvatarPath = AvatarPath;
            vm.AvatarSize = AvatarSize;
            vm.GifSpeedRatio = GifSpeedRatio;
            vm.NickName = NickName;
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
            AvatarPath = vm.AvatarPath;
            AvatarSize = vm.AvatarSize;
            GifSpeedRatio = vm.GifSpeedRatio;
            NickName = vm.NickName;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
