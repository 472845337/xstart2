using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XStart2._0.Utils;
using XStart2._0.ViewModels;
using static XStart2._0.Utils.FontAwesome6;

namespace XStart2._0.Windows {
    /// <summary>
    /// FontAwesomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FontAwesomeWindow : Window {
        public FontAwesomeVM VM { get; set; }
        public FontAwesomeWindow() {
            InitializeComponent();
            // 实体
            List<string> fontAwesomeList = GetFontAwesomeFieldValue<FontAwesome6>();
            // 品牌
            List<string> fontAwesomeBrandsList = GetFontAwesomeFieldValue<FontAwesomeBrands>();
            // 常用
            List<string> fontAwesomeRegularList = GetFontAwesomeFieldValue<FontAwesomeRegular>();

            VM = new FontAwesomeVM {
                CustomFontAwesomes = new List<string> { Comments, Check, Xmark, Wheelchair, Bell, Car
                , AngleUp, AngleDown, Gears,FolderOpen, FolderClosed, House,Envelope, Eye
                , EyeSlash, Heart, Film, Globe, Gift, Cloud, FontAwesome6.Calendar, Camera, PhotoFilm, ScaleBalanced, Cube, FontAwesome6.Key, Map
                , Pencil, Rocket, FaceSmile, Star, FontAwesome6.Tag, FontAwesome6.Table, Tv},
                SolidFontAwesomes = fontAwesomeList,
                RegularFontAwesomes = fontAwesomeRegularList,
                BrandsFontAwesomes = fontAwesomeBrandsList
            };
            Loaded += WindowLoaded;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            DataContext = VM;
        }

        private void SelectIconColor(object sender, RoutedEventArgs e) {
            ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                VM.SelectedIconColor = System.Drawing.ColorTranslator.ToHtml(colorDialog.Color);
            }
        }
        private void SelectedFontAwesome(object sender, RoutedEventArgs e) {
            TextBlock text = sender as TextBlock;
            VM.SelectedFf = text.FontFamily.ToString();
            VM.SelectedFa = text.Text;
        }
        private void ConfirmSelectFontAwesome(object sender, RoutedEventArgs e) {
            if (string.Empty.Equals(VM.SelectedFa)) {
                System.Windows.MessageBox.Show("未选择图标");
                return;
            }
            DialogResult = true;
        }

        private List<string> GetFontAwesomeFieldValue<T>() {
            T t = System.Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            List<string> fontAwesomeList = new List<string>();
            foreach (FieldInfo info in fields) {
                fontAwesomeList.Add(info.GetValue(t) as string);
            }
            return fontAwesomeList;
        }
    }
}
