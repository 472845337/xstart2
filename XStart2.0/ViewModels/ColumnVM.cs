using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart2._0.ViewModels {
    public class ColumnVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // 窗口的标题
        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged("Title"); } }
        // section
        private string section;
        public string Section { get => section; set { section = value; OnPropertyChanged("Section"); } }
        private string typeSection;
        public string TypeSection { get => typeSection; set { typeSection = value; OnPropertyChanged("TypeSection"); } }
        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
    }
}
