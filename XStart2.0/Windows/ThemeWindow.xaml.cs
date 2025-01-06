using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Services;
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
            DataContext = vm;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(WindowTheme.Instance.ThemeCustom)) {
                string[] colors = WindowTheme.Instance.ThemeCustom.Split(Constants.SPLIT_CHAR);

                vm.ConfirmButtonBackGround = colors[0];
                vm.ConfirmButtonForeGround = colors[1];

                vm.ConfirmButtonMouseOverBackGround = colors[2];
                vm.ConfirmButtonMouseOverForeGround = colors[3];

                vm.CancelButtonBackGround = colors[4];
                vm.CancelButtonForeGround = colors[5];
                vm.CancelButtonMouseOverBackGround = colors[6];
                vm.CancelButtonMouseOverForeGround = colors[7];

                vm.ToggleButtonCheckedBackGround = colors[8];
                vm.ToggleButtonCheckedForeGround = colors[9];
            }
            // 查询保存的自定义主题列表
            vm.CustomThemes = new ObservableCollection<CustomTheme>(ServiceFactory.GetCustomThemeService().SelectList(null));
        }

        private void Window_Closing(object sender, EventArgs e) {
            DataContext = null;
        }

        private void Ground_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            TextBlock Ground = sender as TextBlock;
            string type = Ground.Tag as string;
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog() { };
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                string color = ColorUtils.GetHtml(colorDialog.Color);
                if ("ConfirmButtonBg".Equals(type)) {
                    vm.ConfirmButtonBackGround = color;
                } else if ("ConfirmButtonFg".Equals(type)) {
                    vm.ConfirmButtonForeGround = color;
                } else if ("ConfirmButtonMouseOverBg".Equals(type)) {
                    vm.ConfirmButtonMouseOverBackGround = color;
                } else if ("ConfirmButtonMouseOverFg".Equals(type)) {
                    vm.ConfirmButtonMouseOverForeGround = color;
                } else if ("CancelButtonBg".Equals(type)) {
                    vm.CancelButtonBackGround = color;
                } else if ("CancelButtonFg".Equals(type)) {
                    vm.CancelButtonForeGround = color;
                } else if ("CancelButtonMouseOverBg".Equals(type)) {
                    vm.CancelButtonMouseOverBackGround = color;
                } else if ("CancelButtonMouseOverFg".Equals(type)) {
                    vm.CancelButtonMouseOverForeGround = color;
                } else if ("ToggleButtonCheckedBg".Equals(type)) {
                    vm.ToggleButtonCheckedBackGround = color;
                } else if ("ToggleButtonCheckedFg".Equals(type)) {
                    vm.ToggleButtonCheckedForeGround = color;
                }
            }
            e.Handled = true;
        }
        // 生成随机颜色
        private void RandomTheme_Click(object sender, RoutedEventArgs e) {
            Tuple<string, string> confirmGroundRanTuple = GetRanColor();
            Tuple<string, string> confirmMouseOverGroundRanTuple = GetRanColor();
            Tuple<string, string> cancelGroundRanTuple = GetRanColor();
            Tuple<string, string> cancelMouseOverGroundRanTuple = GetRanColor();
            Tuple<string, string> toggleButtonCheckedGroundRanTuple = GetRanColor();
            vm.ConfirmButtonBackGround = confirmGroundRanTuple.Item1;
            vm.ConfirmButtonForeGround = confirmGroundRanTuple.Item2;
            vm.ConfirmButtonMouseOverBackGround = confirmMouseOverGroundRanTuple.Item1;
            vm.ConfirmButtonMouseOverForeGround = confirmMouseOverGroundRanTuple.Item2;
            vm.CancelButtonBackGround = cancelGroundRanTuple.Item1;
            vm.CancelButtonForeGround = cancelGroundRanTuple.Item2;
            vm.CancelButtonMouseOverBackGround = cancelMouseOverGroundRanTuple.Item1;
            vm.CancelButtonMouseOverForeGround = cancelMouseOverGroundRanTuple.Item2;
            vm.ToggleButtonCheckedBackGround = toggleButtonCheckedGroundRanTuple.Item1;
            vm.ToggleButtonCheckedForeGround = toggleButtonCheckedGroundRanTuple.Item2;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.ConfirmButtonBackGround) || string.IsNullOrEmpty(vm.ConfirmButtonForeGround)
               || string.IsNullOrEmpty(vm.ConfirmButtonMouseOverBackGround) || string.IsNullOrEmpty(vm.ConfirmButtonMouseOverForeGround)
               || string.IsNullOrEmpty(vm.CancelButtonBackGround) || string.IsNullOrEmpty(vm.CancelButtonForeGround)
               || string.IsNullOrEmpty(vm.CancelButtonMouseOverBackGround) || string.IsNullOrEmpty(vm.CancelButtonMouseOverForeGround)
               || string.IsNullOrEmpty(vm.ToggleButtonCheckedBackGround) || string.IsNullOrEmpty(vm.ToggleButtonCheckedForeGround)) {
                MsgBoxUtils.ShowInfo("未配置的颜色，背景色默认为白色，文字默认为黑色");
            }
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }

        // 使用随机种子，否则短时间内调用的随机数是一样的
        Random ran = new Random(Guid.NewGuid().ToString().GetHashCode());
        private Tuple<string, string> GetRanColor() {
            int br = GetRanRgb();
            int bg = GetRanRgb();
            int bb = GetRanRgb();

            int fr = GetInvertRgb(br);
            int fg = GetInvertRgb(bg);
            int fb = GetInvertRgb(bb);
            return new Tuple<string, string>($"#{br:X2}{bg:X2}{bb:X2}", $"#{fr:X2}{fg:X2}{fb:X2}");
        }
        private int GetRanRgb() {
            return ran.Next(0, 256);
        }

        private int GetInvertRgb(int rgb) {
            if (rgb >= 103 && rgb <= 152) {
                return 0 == ran.Next(0, 2) ? ran.Next(0, 56) : ran.Next(200, 256);
            } else {
                return 255 - rgb;
            }
        }

        private void OpenCustomThemes_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            CustomThemes_Popup.IsOpen = false;
            CustomThemes_Popup.IsOpen = true;
            e.Handled = true;
        }

        private void SaveCustomTheme_Click(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.LoadCustomName)) {
                MsgBoxUtils.ShowError("请先输入自定义主题名！");
                CustomName_TextBox.Focus();
            } else {
                if (null != vm.LoadCustomTheme && vm.LoadCustomTheme.Name.Equals(vm.LoadCustomName)) {
                    // 选择了数据，并且未修改主题名，修改自定义主题
                    CustomTheme customTheme = vm.LoadCustomTheme;
                    customTheme.Bg = vm.ConfirmButtonBackGround;
                    customTheme.Fg = vm.ConfirmButtonForeGround;
                    customTheme.MouseOverBg = vm.ConfirmButtonMouseOverBackGround;
                    customTheme.MouseOverFg = vm.ConfirmButtonMouseOverForeGround;
                    customTheme.CancelBg = vm.CancelButtonBackGround;
                    customTheme.CancelFg = vm.CancelButtonForeGround;
                    customTheme.CancelMouseOverBg = vm.CancelButtonMouseOverBackGround;
                    customTheme.CancelMouseOverFg = vm.CancelButtonMouseOverForeGround;
                    customTheme.CheckedBg = vm.ToggleButtonCheckedBackGround;
                    customTheme.CheckedFg = vm.ToggleButtonCheckedForeGround;
                    ServiceFactory.GetCustomThemeService().Update(customTheme);
                } else {
                    // 新增自定义主题
                    CustomTheme customTheme = new CustomTheme {
                        Section = Guid.NewGuid().ToString(),
                        Name = vm.LoadCustomName,
                        Bg = vm.ConfirmButtonBackGround,
                        Fg = vm.ConfirmButtonForeGround,
                        MouseOverBg = vm.ConfirmButtonMouseOverBackGround,
                        MouseOverFg = vm.ConfirmButtonMouseOverForeGround,
                        CancelBg = vm.CancelButtonBackGround,
                        CancelFg = vm.CancelButtonForeGround,
                        CancelMouseOverBg = vm.CancelButtonMouseOverBackGround,
                        CancelMouseOverFg = vm.CancelButtonMouseOverForeGround,
                        CheckedBg = vm.ToggleButtonCheckedBackGround,
                        CheckedFg = vm.ToggleButtonCheckedForeGround
                    };
                    ServiceFactory.GetCustomThemeService().Insert(customTheme);
                    vm.CustomThemes.Add(customTheme);
                }
            }
        }

        private void RemoveAllCustomThemes_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            if (vm.CustomThemes.Count > 0) {
                if (MessageBoxResult.Yes == MsgBoxUtils.ShowAsk("确认清空所有的自定义？")) {
                    ServiceFactory.GetCustomThemeService().Clear();
                    vm.CustomThemes.Clear();
                }
            } else {
                MsgBoxUtils.ShowError("无自定义主题！", Constants.MESSAGE_BOX_TITLE_ERROR);
            }
            e.Handled = true;
        }

        private void RemoveCustomTheme_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            FrameworkElement ele = sender as FrameworkElement;
            string section = ele.Tag as string;
            if (section != null && MessageBoxResult.Yes == MsgBoxUtils.ShowAsk("确认删除该自定义？")) {
                ServiceFactory.GetCustomThemeService().Delete(section);
                int index = -1;
                foreach (CustomTheme single in vm.CustomThemes) {
                    index++;
                    if (single.Name == section) {
                        break;
                    }
                }
                if (index > -1) {
                    vm.CustomThemes.RemoveAt(index);
                }
            }
        }

        private void SelectCustomTheme_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            FrameworkElement ele = sender as FrameworkElement;
            CustomTheme customTheme = ele.Tag as CustomTheme;
            if (customTheme != null) {
                vm.ConfirmButtonBackGround = customTheme.Bg;
                vm.ConfirmButtonForeGround = customTheme.Fg;
                vm.ConfirmButtonMouseOverBackGround = customTheme.MouseOverBg;
                vm.ConfirmButtonMouseOverForeGround = customTheme.MouseOverFg;
                vm.CancelButtonBackGround = customTheme.CancelBg;
                vm.CancelButtonForeGround = customTheme.CancelFg;
                vm.CancelButtonMouseOverBackGround = customTheme.CancelMouseOverBg;
                vm.CancelButtonMouseOverForeGround = customTheme.CancelMouseOverFg;
                vm.ToggleButtonCheckedBackGround = customTheme.CheckedBg;
                vm.ToggleButtonCheckedForeGround = customTheme.CheckedFg;
                vm.LoadCustomTheme = customTheme;
                vm.LoadCustomName = customTheme.Name;
                CustomThemes_Popup.IsOpen = false;
            }
        }
    }
}
