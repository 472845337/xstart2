using System.Windows;
using System.Windows.Controls;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for OpacityWindow.xaml
    /// </summary>
    public partial class OpacityWindow : Window {
        OpacityViewModel vm = new OpacityViewModel();
        public double opacity;
        public OpacityWindow(double opacity) {
            InitializeComponent();
            this.opacity = opacity;
            DataContext = vm;
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.Opacity = opacity*10;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            opacity = vm.ShowOpacity;
            DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }
}
