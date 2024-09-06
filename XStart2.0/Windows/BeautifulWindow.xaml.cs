using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for MainBackgroundWindow.xaml
    /// </summary>
    public partial class BeautifulWindow : Window {
        readonly BeautifulViewModel vm = new BeautifulViewModel();
        public string Bg { get; private set; }
        public string Ff { get; private set; }
        public string Fg { get; private set; }
        public int Fs { get; private set; }
        public double Op { get; private set; }
        public BeautifulWindow(string background, string foreground, string fontFamily, int fontSize, double opacity) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Bg = background;
            Fg = foreground;
            Ff = fontFamily;
            Fs = fontSize;
            Op = opacity;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.SystemFonts = new ObservableCollection<string>(FontUtils.GetSystemFonts());
            vm.Background = Bg;
            vm.Foreground = Fg;
            vm.TextFontFamily = Ff;
            vm.TextFontSize = Fs;
            vm.Opacity = Op * 10;
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
            Fg = vm.Foreground;
            Ff = vm.TextFontFamily;
            Fs = vm.TextFontSize;
            Op = vm.ShowOpacity;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private void Foreground_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog?.ShowDialog()) {
                vm.Foreground = ColorUtils.GetHtml(colorDialog.Color);
            }
        }
    }
}
