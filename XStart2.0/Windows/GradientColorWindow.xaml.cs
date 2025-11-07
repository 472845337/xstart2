using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Utils;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for GradientColorWindow.xaml
    /// </summary>
    public partial class GradientColorWindow : Window {
        readonly GradientColorViewModel vm = new GradientColorViewModel();
        public string GradientColor { get; set; }
        public GradientColorWindow(string gradientColor) {
            InitializeComponent();
            DataContext = vm;
            if (IsGradient(gradientColor)) {
                GradientColor = gradientColor;
            } else {
                GradientColor = Constants.DEFAULT_GRADIENT_COLOR;
            }
            Loaded += Window_Loaded;
        }

        private bool IsGradient(string gradientColor) {
            bool isGradient = false;
            if (!string.IsNullOrEmpty(gradientColor) && gradientColor.StartsWith("#") && gradientColor.Contains(";")) {
                string[] gradientColorArray = gradientColor.Split(';');
                int colorCount = 0;
                foreach (string singleColor in gradientColorArray) {
                    if (singleColor.StartsWith("#") && singleColor.Contains(":")) {
                        colorCount++;
                    }
                }
                if (colorCount > 1) {
                    isGradient = true;
                }
            }
            return isGradient;
        }

        private void Window_Loaded(object sender, EventArgs e) {
            vm.GradientBackground = GradientColor;
            vm.GradientColorList = new ObservableCollection<GradientColor>(GradientColorUtils.GetList(GradientColor));
            vm.Angle = GradientColorUtils.GetAngle(GradientColor);
            vm.ChangeGradient();
        }

        private void CheckPoint_TextChanged(object sender, RoutedEventArgs e) {
            TextBox pointTextBox = (TextBox)sender;
            string point = pointTextBox.Text;
            if (string.IsNullOrEmpty(point)) {
                return;
            }
            bool isNumeric = NumberUtils.IsNumeric(point, out double pointD);
            if (!isNumeric || pointD < 0 || pointD > 1) {
                MsgBoxUtils.ShowError("请输入0-1之间的数值");
            }

        }
        private void ChangeColor_MouseLeftButtonUp(object sender, RoutedEventArgs e) {
            FrameworkElement ele = sender as FrameworkElement;
            System.Windows.Forms.ColorDialog cd = new System.Windows.Forms.ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == cd.ShowDialog()) {
                GradientColor gradientColor = ele.Tag as GradientColor;
                gradientColor.Color = ColorUtils.GetHtml(cd.Color);
                vm.ChangeGradient();
            }
        }

        /// <summary>
        /// 删除渐变点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteGradientColor_Click(object sender, RoutedEventArgs e) {
            if (vm.GradientColorList.Count < 3) {
                MsgBoxUtils.ShowError("渐变色不能小于2种！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            Button deleteButton = sender as Button;
            GradientColor deleteColor = deleteButton.Tag as GradientColor;
            vm.GradientColorList.Remove(deleteColor);
            vm.ChangeGradient();
        }

        /// <summary>
        /// 添加渐变色块
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGradientColor_Click(object sender, RoutedEventArgs e) {
            vm.GradientColorList.Add(new GradientColor("#FFFFFF", 1));
            vm.ChangeGradient();
        }

        private void PointSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            vm.ChangeGradient();
        }

        private void AngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            vm.ChangeGradient();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            vm.GradientColorList.Clear();
            vm.GradientBackground = string.Empty;
            DataContext = null;
            DialogResult = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            foreach (GradientColor color in vm.GradientColorList) {
                if (color.Point < 0 || color.Point > 1) {
                    MsgBoxUtils.ShowInfo("请输入0-1之间的数值");
                    return;
                }
            }
            List<GradientColor> gradientColorList = new List<GradientColor>(vm.GradientColorList);
            gradientColorList.Sort(delegate (GradientColor c1, GradientColor c2) {
                return c1.Point > c2.Point ? 1 : -1;
            });
            GradientColor = GradientColorUtils.GetString(gradientColorList, vm.Angle);
            vm.GradientColorList.Clear();
            vm.GradientBackground = string.Empty;
            DataContext = null;
            DialogResult = true;
        }
    }
}
