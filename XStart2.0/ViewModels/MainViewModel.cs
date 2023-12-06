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
        // 项目按钮宽度
        private int projectWidth;
        public int ProjectWidth { get => projectWidth; set { projectWidth = value;OnPropertyChanged("ProjectWidth"); } }
        // 类别宽度（TabControl的TabItem的宽度）
        private int typeWidth;
        public int TypeWidth { get => typeWidth; set { typeWidth = value;OnPropertyChanged("TypeWidth"); } }

        #endregion
    }
}
