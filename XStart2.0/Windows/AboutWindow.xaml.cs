using System;
using System.Windows;
using System.Windows.Interop;
using XStart2._0.Config;

namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window {
        public AboutWindow() {
            InitializeComponent();
            Loaded += AboutWindow_Loaded;
            Closed += AboutWindow_Closed;
        }

        private void AboutWindow_Closed(object sender, EventArgs e) {
            Configs.AboutHandler = IntPtr.Zero;
        }

        private void AboutWindow_Loaded(object sender, RoutedEventArgs e) {
            Configs.AboutHandler = new WindowInteropHelper(this).Handle;
        }
    }
}
