using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XStart2._0.Utils;
using XStart2._0.ViewModel;
using static XStart2._0.Utils.FontAwesome6;

namespace XStart2._0.Windows {
    /// <summary>
    /// ProjectType.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectTypeWindow : Window {
        public ProjectTypeVM VM { get; set; }
        public bool isAdd = true;
        public ProjectTypeWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            SetPopularFas();
            DataContext = VM;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            DataContext = null;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Enter) {
                if (isAdd && (string.IsNullOrEmpty(VM.SelectedFa) || string.IsNullOrEmpty(VM.SelectedFf))) {
                    VM.SelectedFa = VM.PopularFas[new Random().Next(VM.PopularFas.Count)];
                    VM.SelectedFf = "pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid";
                    VM.SelectedIconColor = "#345dab";
                } else {
                    // 保存按钮
                    ProjectType_Save(sender, e);
                }

            }
        }
        public void PopularFa_MouseLeftButtonUp(object sender, RoutedEventArgs e) {
            TextBlock selectTextBlock = sender as TextBlock;
            VM.SelectedFa = selectTextBlock.Text;
            VM.SelectedFf = selectTextBlock.FontFamily.ToString();
        }

        private void SetPopularFas() {
            List<string> popularFas = new List<string> { Comments, Check, Xmark, Wheelchair, Bell, Car
                , AngleUp, AngleDown, Gears,FolderOpen, FolderClosed, House,Envelope, Eye
                , EyeSlash, Heart, Film, Globe, Gift, Cloud, FontAwesome6.Calendar,Camera, PhotoFilm};
            VM.PopularFas = popularFas;
        }
        /// <summary>
        /// 保存类别,根据vm里的Section是否为空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectType_Save(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(VM.Name)) {
                MsgBoxUtils.ShowError("类别名不能为空!");
                return;
            }

            DialogResult = true;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SelectIconColor(object sender, RoutedEventArgs e) {
            using System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                VM.SelectedIconColor = ColorUtils.GetHtml(colorDialog.Color);
            }
        }
        // 选择更多的图标
        private void SelectMoreFontAwesome(object sender, RoutedEventArgs e) {
            FontAwesomeWindow faw = new FontAwesomeWindow() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this };
            if (true == faw.ShowDialog()) {
                VM.SelectedFa = faw.VM.SelectedFa;
                VM.SelectedFf = faw.VM.SelectedFf;
            }
        }
    }
}
