using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using static XStart2._0.Utils.FontAwesome6;

namespace XStart2._0.Windows {
    /// <summary>
    /// ProjectType.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectTypeWindow : Window {
        public ProjectTypeVM VM { get; set; }
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
                System.Windows.MessageBox.Show("类别名不能为空!", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }

            DialogResult = true;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SelectIconColor(object sender, RoutedEventArgs e) {
            using ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                VM.SelectedIconColor = System.Drawing.ColorTranslator.ToHtml(colorDialog.Color);
            }
        }
        // 选择更多的图标
        private void SelectMoreFontAwesome(object sender, RoutedEventArgs e) {
            FontAwesomeWindow faw = new FontAwesomeWindow() { WindowStartupLocation = WindowStartupLocation.CenterOwner, Owner = this };
            if (true == faw.ShowDialog()) {
                VM.SelectedFa = faw.VM.SelectedFa;
                VM.SelectedFf = faw.VM.SelectedFf;
            }
            faw.Close();
        }
    }
}
