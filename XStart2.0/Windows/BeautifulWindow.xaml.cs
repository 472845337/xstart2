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
        public double Mo { get; private set; }
        public string Ff { get; private set; }
        public string Fg { get; private set; }
        public int Fs { get; private set; }
        public double Op { get; private set; }
        public BeautifulWindow(string background, double mainOpacity, string foreground, string fontFamily, int fontSize, double opacity) {
            InitializeComponent();
            Loaded += Window_Loaded;
            Bg = background;
            Mo = mainOpacity;
            Fg = foreground;
            Ff = fontFamily;
            Fs = fontSize;
            Op = opacity;
            DataContext = vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            vm.SystemFonts = new ObservableCollection<string>(FontUtils.GetSystemFonts());
            vm.Background = Bg;
            vm.MainOpacity = Mo;
            vm.Foreground = Fg;
            vm.TextFontFamily = Ff;
            vm.TextFontSize = Fs;
            vm.Opacity = Op;
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

        private void GradientBackground_Click(object sender, RoutedEventArgs e) {
            // 渐变色框
            GradientColorWindow gradientColorWindow = new GradientColorWindow(vm.Background) { Owner = this };
            if (true == gradientColorWindow.ShowDialog()) {
                vm.Background = gradientColorWindow.GradientColor;
            }
        }

        private void ImageBackground_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "图片文件|*.jpg;*.jpeg;*.bmp;*.png" };
            if (System.Windows.Forms.DialogResult.OK == openFileDialog.ShowDialog()) {
                vm.Background = openFileDialog.FileName;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            Bg = vm.Background;
            Mo = vm.MainOpacity;
            Fg = vm.Foreground;
            Ff = vm.TextFontFamily;
            Fs = vm.TextFontSize;
            Op = vm.Opacity;
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
