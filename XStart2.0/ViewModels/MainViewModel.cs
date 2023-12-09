using XStart.Bean;
using System.ComponentModel;

using System.Runtime.CompilerServices;


namespace XStart2._0.ViewModels {
    class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #region 应用数据
        private LinkedHashMap<string, Type> types;
        public LinkedHashMap<string, Type> Types {
            get => types;
            set { types = value; OnPropertyChanged("Types"); }
        }
        #endregion

        #region 窗口相关
        // 类别宽度（TabControl的TabItem的宽度）
        private int typeWidth;
        public int TypeWidth { get => typeWidth; set { typeWidth = value;OnPropertyChanged("TypeWidth"); } }

        private bool typeTabExpanded;
        public bool TypeTabExpanded { get => typeTabExpanded; set { typeTabExpanded = value;OnPropertyChanged("ByteTabExpanded"); } }

        private string typeTabToggleIcon;
        public string TypeTabToggleIcon { get => typeTabToggleIcon;set { typeTabToggleIcon = value; OnPropertyChanged("TypeTabToggleIcon"); } }
        #endregion
    }
}
