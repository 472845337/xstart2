using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XStart.Services;
using XStart2._0.Utils;
using XStart2._0.ViewModels;

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
            List<string> popularFas = new List<string> { FaIcons.Comments, FaIcons.Check, FaIcons.Xmark, FaIcons.Wheelchair, FaIcons.Bell, FaIcons.Car
                , FaIcons.AngleUp, FaIcons.AngleDown, FaIcons.Gears,FaIcons.FolderOpen, FaIcons.FolderClosed, FaIcons.House,FaIcons.Envelope, FaIcons.Eye
                , FaIcons.EyeSlash, FaIcons.Heart, FaIcons.Film, FaIcons.Globe, FaIcons.Gift, FaIcons.Glass, FaIcons.Cloud, FaIcons.Calendar,FaIcons.Camera, FaIcons.PhotoFilm};
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
            FontAwesomeWindow faw = new FontAwesomeWindow();
            faw.Show();
        }
    }
}
