using PropertyChanged;
using System.Windows.Media.Imaging;
using XStart2._0.Utils;

namespace XStart2._0.ViewModel {
    public class UserViewModel : BaseViewModel {
        [OnChangedMethod(nameof(SetAvatar))]
        public string AvatarPath { get; set; }
        public BitmapImage Avatar { get; set; }
        public string NickName { get; set; }

        private void SetAvatar() {
            // gif读取的流不能关闭（始终保持占用文件）
            Avatar = ImageUtils.GetImageOutMs(AvatarPath);
        }
    }
}
