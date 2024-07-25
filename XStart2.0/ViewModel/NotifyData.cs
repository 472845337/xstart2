using PropertyChanged;

namespace XStart2._0.ViewModel {
    [AddINotifyPropertyChangedInterface]
    class NotifyData : BaseViewModel {

        public string Title { get; set; }

        public string Background { get; set; }
        public int Height { get; set; }
        public string Content { get; set; }
        public int SaveTime { get; set; }
    }
}
