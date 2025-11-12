using System.Windows;
using System.Windows.Input;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// AvatarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Window {

        public readonly UserViewModel vm = new UserViewModel();

        public UserWindow(string avatarPath, double gifSpeedRadio, string nickName, int avatarSize
            , string timeFormat, string dateFormat
            , string yearFormat, string monthFormat, string dayFormat, string weekFormat) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            vm.AvatarPath = avatarPath;
            vm.AvatarSize = avatarSize;
            vm.GifSpeedRatio = gifSpeedRadio;
            vm.NickName = nickName;
            vm.TimeFormat = timeFormat;
            vm.DateFormat = dateFormat;
            vm.YearFormat = yearFormat;
            vm.MonthFormat = monthFormat;
            vm.DayFormat = dayFormat;
            vm.WeekFormat = weekFormat;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

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

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                // 确认按钮
                Confirm_Click(sender, e);
            }
        }
    }
}
