using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace XStart2._0.ViewModels {
    public class ProjectTypeVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // 窗口的标题
        private string title;
        public string Title { get => title; set { title = value;OnPropertyChanged("Title"); } }
        // 类别的section
        private string section;
        public string Section { get => section; set { section = value;OnPropertyChanged("Section"); } }
        // 类别名称
        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        // 类别所选FontAwesome Icon
        private string selectedFa;
        public string SelectedFa { get => selectedFa; set { selectedFa = value; OnPropertyChanged("SelectedFa"); } }
        // 类别Icon颜色
        private string selectedIconColor = "LightSeaGreen";
        public string SelectedIconColor { get => selectedIconColor; set { selectedIconColor = value; OnPropertyChanged("SelectedIconColor"); } }
        // 类别所选FontAwesome字体
        private string selectedFf = "{pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid}";
        public string SelectedFf { get => selectedFf; set { selectedFf = value; OnPropertyChanged("SelectedFf"); } }

        private List<string> popularFas;
        public List<string> PopularFas { get => popularFas; set { popularFas = value;OnPropertyChanged("PopularFas"); } }
    }
}
