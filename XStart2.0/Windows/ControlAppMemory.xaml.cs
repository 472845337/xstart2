using System.Windows;

namespace XStart2._0.Windows {
    /// <summary>
    /// ControlAppMemory.xaml 的交互逻辑
    /// </summary>
    public partial class ControlAppMemory : Window {

        public string AppName { get; set; }
        public string MinMemory { get; set; }
        public string MaxMemory { get; set; }
        public ControlAppMemory() {
            InitializeComponent();
        }
    }
}
