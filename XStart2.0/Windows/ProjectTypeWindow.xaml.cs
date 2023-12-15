using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using static XStart2._0.Utils.FontAwesome6;

namespace XStart2._0.Windows {
    /// <summary>
    /// ProjectType.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectTypeWindow : Window {
        public ProjectTypeVM vm { get; set; } = new ProjectTypeVM();
        public ProjectTypeWindow() {
            InitializeComponent();
            SetPopularFas();
            DataContext = vm;
        }
        public ProjectTypeWindow(ProjectTypeVM vm) {
            InitializeComponent();
            if (null != vm) {
                this.vm = vm;
            }
            SetPopularFas();
            DataContext = this.vm;
        }

        public void PopularFa_MouseLeftButtonUp(object sender, RoutedEventArgs e) {
            TextBlock selectTextBlock = sender as TextBlock;
            vm.SelectedFa = selectTextBlock.Text;
            vm.SelectedFf = selectTextBlock.FontFamily.ToString();
        }

        private void SetPopularFas() {
            List<string> popularFas = new List<string> { Comments, Check, Xmark, Wheelchair, Bell, Car
                , AngleUp, AngleDown, Gears,FolderOpen, FolderClosed, House,Envelope, Eye
                , EyeSlash, Heart, Film, Globe, Gift, Cloud, FontAwesome6.Calendar,Camera, PhotoFilm};
            vm.PopularFas = popularFas;
        }
        /// <summary>
        /// 保存类别,根据vm里的Section是否为空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectType_Save(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Name)) {
                System.Windows.MessageBox.Show("类别名不能为空!");
                return;
            }

            DialogResult = true;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SelectIconColor(object sender, RoutedEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                vm.SelectedIconColor = System.Drawing.ColorTranslator.ToHtml(colorDialog.Color);
            }
        }
        // 选择更多的图标
        private void SelectMoreFontAwesome(object sender, RoutedEventArgs e) {
            FontAwesomeWindow faw = new FontAwesomeWindow() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this };
            if (true == faw.ShowDialog()) {
                vm.SelectedFa = faw.VM.SelectedFa;
                vm.SelectedFf = faw.VM.SelectedFf;
            }
        }
    }
}
