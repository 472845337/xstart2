using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// ThemeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ThemeWindow : Window {
        public ThemeViewModel vm = new ThemeViewModel();
        public ThemeWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(WindowTheme.Instance.ThemeCustom)) {
                string[] colors = WindowTheme.Instance.ThemeCustom.Split(Constants.SPLIT_CHAR);
                vm.ConfirmButtonBackGroundColor = ColorUtils.GetBrush(colors[0]);
                vm.ConfirmButtonForeGroundColor = ColorUtils.GetBrush(colors[1]);
                vm.ConfirmButtonMouseOverBackGroundColor = ColorUtils.GetBrush(colors[2]);
                vm.ConfirmButtonMouseOverForeGroundColor = ColorUtils.GetBrush(colors[3]);

                vm.CancelButtonBackGroundColor = ColorUtils.GetBrush(colors[4]);
                vm.CancelButtonForeGroundColor = ColorUtils.GetBrush(colors[5]);
                vm.CancelButtonMouseOverBackGroundColor = ColorUtils.GetBrush(colors[6]);
                vm.CancelButtonMouseOverForeGroundColor = ColorUtils.GetBrush(colors[7]);

                vm.ToggleButtonCheckedBackGroundColor = ColorUtils.GetBrush(colors[8]);
                vm.ToggleButtonCheckedForeGroundColor = ColorUtils.GetBrush(colors[9]);
            }
            DataContext = vm;
        }

        private void Window_Closing(object sender, EventArgs e) {
            DataContext = null;
        }

        private void GroundColor_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            TextBlock groundColor = sender as TextBlock;
            string type = groundColor.Tag as string;
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                SolidColorBrush brush = ColorUtils.GetBrush(colorDialog.Color);
                if ("ConfirmButtonBg".Equals(type)) {
                    vm.ConfirmButtonBackGroundColor = brush;
                } else if ("ConfirmButtonFg".Equals(type)) {
                    vm.ConfirmButtonForeGroundColor = brush;
                } else if ("ConfirmButtonMouseOverBg".Equals(type)) {
                    vm.ConfirmButtonMouseOverBackGroundColor = brush;
                } else if ("ConfirmButtonMouseOverFg".Equals(type)) {
                    vm.ConfirmButtonMouseOverForeGroundColor = brush;
                } else if ("CancelButtonBg".Equals(type)) {
                    vm.CancelButtonBackGroundColor = brush;
                } else if ("CancelButtonFg".Equals(type)) {
                    vm.CancelButtonForeGroundColor = brush;
                } else if ("CancelButtonMouseOverBg".Equals(type)) {
                    vm.CancelButtonMouseOverBackGroundColor = brush;
                } else if ("CancelButtonMouseOverFg".Equals(type)) {
                    vm.CancelButtonMouseOverForeGroundColor = brush;
                } else if ("ToggleButtonCheckedBg".Equals(type)) {
                    vm.ToggleButtonCheckedBackGroundColor = brush;
                } else if ("ToggleButtonCheckedFg".Equals(type)) {
                    vm.ToggleButtonCheckedForeGroundColor = brush;
                }
            }
            e.Handled = true;
        }
        // 生成随机颜色
        private void RandomTheme_Click(object sender, RoutedEventArgs e) {
            Tuple<SolidColorBrush, SolidColorBrush> confirmGroundRanColorTuple = GetRanColor();
            Tuple<SolidColorBrush, SolidColorBrush> confirmMouseOverGroundRanColorTuple = GetRanColor();
            Tuple<SolidColorBrush, SolidColorBrush> cancelGroundRanColorTuple = GetRanColor();
            Tuple<SolidColorBrush, SolidColorBrush> cancelMouseOverGroundRanColorTuple = GetRanColor();
            Tuple<SolidColorBrush, SolidColorBrush> toggleButtonCheckedGroundRanColorTuple = GetRanColor();
            vm.ConfirmButtonBackGroundColor = confirmGroundRanColorTuple.Item1;
            vm.ConfirmButtonForeGroundColor = confirmGroundRanColorTuple.Item2;
            vm.ConfirmButtonMouseOverBackGroundColor = confirmMouseOverGroundRanColorTuple.Item1;
            vm.ConfirmButtonMouseOverForeGroundColor = confirmMouseOverGroundRanColorTuple.Item2;
            vm.CancelButtonBackGroundColor = cancelGroundRanColorTuple.Item1;
            vm.CancelButtonForeGroundColor = cancelGroundRanColorTuple.Item2;
            vm.CancelButtonMouseOverBackGroundColor = cancelMouseOverGroundRanColorTuple.Item1;
            vm.CancelButtonMouseOverForeGroundColor = cancelMouseOverGroundRanColorTuple.Item2;
            vm.ToggleButtonCheckedBackGroundColor = toggleButtonCheckedGroundRanColorTuple.Item1;
            vm.ToggleButtonCheckedForeGroundColor = toggleButtonCheckedGroundRanColorTuple.Item2;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            if (null == vm.ConfirmButtonBackGroundColor || null == vm.ConfirmButtonForeGroundColor || null == vm.ConfirmButtonMouseOverBackGroundColor || null == vm.ConfirmButtonMouseOverForeGroundColor
               || null == vm.CancelButtonBackGroundColor || null == vm.CancelButtonForeGroundColor || null == vm.CancelButtonMouseOverBackGroundColor || null == vm.CancelButtonMouseOverForeGroundColor
               || null == vm.ToggleButtonCheckedBackGroundColor || null == vm.ToggleButtonCheckedForeGroundColor) {
                MessageBox.Show("未配置的颜色，背景色默认为白色，文字默认为黑色", Constants.MESSAGE_BOX_TITLE_INFO, MessageBoxButton.OK);
            }
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        private Tuple<SolidColorBrush, SolidColorBrush> GetRanColor() {
            // 使用随机种子，否则短时间内调用的随机数是一样的
            Random ran = new Random(Guid.NewGuid().ToString().GetHashCode());
            int r = ran.Next(0, 255);
            int g = ran.Next(0, 255);
            int b = ran.Next(0, 255);
            return new Tuple<SolidColorBrush, SolidColorBrush>(ColorUtils.GetBrush(r, g, b), ColorUtils.GetBrush(255 - r, 255 - g, 255 - b));
        }
    }
}
