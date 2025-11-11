using PropertyChanged;
using System.Windows;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class MstscViewModel : BaseViewModel {
        public bool IsAdd { get; set; } = true;
        public string Address { get; set; }
        public string Port { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string SeePassword { get; set; } = FontAwesome6.Eye;
        [OnChangedMethod(nameof(ChangeShowPassword))]
        public bool ShowPassword { get; set; } = false;

        public Visibility PasswordTextShow { get; set; } = Visibility.Collapsed;
        public Visibility PasswordTextHidden { get; set; } = Visibility.Visible;

        private void ChangeShowPassword() {
            SeePassword = ShowPassword ? FontAwesome6.EyeSlash : FontAwesome6.Eye;
            if (ShowPassword) {
                PasswordTextShow = Visibility.Visible;
                PasswordTextHidden = Visibility.Collapsed;
            } else {
                PasswordTextShow = Visibility.Collapsed;
                PasswordTextHidden = Visibility.Visible;
            }

        }
    }
}
