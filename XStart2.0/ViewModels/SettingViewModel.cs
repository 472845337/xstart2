
namespace XStart2._0.ViewModels {
    public class SettingViewModel : BaseViewModel {
        private bool topMost;
        public bool TopMost { get => topMost; set { topMost = value; OnPropertyChanged("TopMost"); } }
        private bool audio;
        public bool Audio { get => audio; set { audio = value; OnPropertyChanged("Audio"); } }
        private bool autoRun;
        public bool AutoRun { get => autoRun; set { autoRun = value; OnPropertyChanged("AutoRun"); } }
        private bool exitWarn;
        public bool ExitWarn { get => exitWarn; set { exitWarn = value; OnPropertyChanged("ExitWarn"); } }
        private bool closeBorderHide;
        public bool CloseBorderHide { get => closeBorderHide; set { closeBorderHide = value; OnPropertyChanged("CloseBorderHide"); } }
        private string clickType;
        public string ClickType { get => clickType; set { clickType = value; OnPropertyChanged("ClickType"); } }
        private string urlOpen;
        public string UrlOpen { get => urlOpen; set { urlOpen = value; OnPropertyChanged("UrlOpen"); } }
        private string urlOpenCustomBrowser;
        public string UrlOpenCustomBrowser { get => urlOpenCustomBrowser; set { urlOpenCustomBrowser = value; OnPropertyChanged("UrlOpenCustomBrowser"); } }
    }
}
