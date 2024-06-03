using System.Windows.Media.Imaging;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class UserViewModel : BaseViewModel {
        private string avatarPath;
        public string AvatarPath { get => avatarPath; set { avatarPath = value; SetAvatar(); } }
        public BitmapImage Avatar { get; set; }
        public string NickName { get; set; }

        private void SetAvatar() {
            Avatar = ImageUtils.File2BitmapImage(avatarPath);
        }
    }
}
