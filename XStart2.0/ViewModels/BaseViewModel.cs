using PropertyChanged;

namespace XStart2._0.ViewModels {
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel {
        public bool TopMost { get; set; }// 当前窗口是否置顶
    }
}
