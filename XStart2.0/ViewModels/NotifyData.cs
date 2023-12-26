
namespace XStart2._0.ViewModels {
    class NotifyData : BaseViewModel {

        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged("Title"); } }

        private string background;
        public string Background { get => background; set { background = value; OnPropertyChanged("Background"); } }

        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged("Content"); } }
    }
}
