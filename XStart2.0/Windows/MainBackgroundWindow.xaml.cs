using System.Windows;
using System.Windows.Forms;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for MainBackgroundWindow.xaml
    /// </summary>
    public partial class MainBackgroundWindow : Window {
        MainBackgroundViewModel vm = new MainBackgroundViewModel();
        public string Bg { get; set; }
        public MainBackgroundWindow(string background) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Bg = background;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.Background = Bg;
        }

        private void CancelBackground_Click(object sender, RoutedEventArgs e) {
            vm.Background = string.Empty;
        }

        private void ColorBackground_Click(object sender, RoutedEventArgs e) {
            // 颜色选择框
            ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                vm.Background = ColorUtils.GetHtml(colorDialog.Color);
            }
        }

        private void ImageBackground_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (System.Windows.Forms.DialogResult.OK == openFileDialog.ShowDialog()) {
                vm.Background = openFileDialog.FileName;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            Bg = vm.Background;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }


    }
}
