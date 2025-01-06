using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XStart2._0.Bean;
using XStart2._0.Const;
using XStart2._0.Utils;
using XStart2._0.ViewModel;
using static XStart2._0.Utils.FontAwesome6;

namespace XStart2._0.Windows {
    /// <summary>
    /// FontAwesomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FontAwesomeWindow : Window {
        public FontAwesomeVM VM { get; set; }
        public FontAwesomeWindow() {
            InitializeComponent();
            Loaded += WindowLoaded;
            Closing += Window_Closing;
        }

        private void WindowLoaded(object sender, RoutedEventArgs e) {
            // 实体
            IEnumerable<FontAwesome> fontAwesomeList = GetFontAwesomeFieldValue<FontAwesome6>(Constants.FA_NAME_SOLID);
            // 品牌
            IEnumerable<FontAwesome> fontAwesomeBrandsList = GetFontAwesomeFieldValue<FontAwesomeBrands>(Constants.FA_NAME_BRANDS);
            // 常用
            IEnumerable<FontAwesome> fontAwesomeRegularList = GetFontAwesomeFieldValue<FontAwesomeRegular>(Constants.FA_NAME_REGULAR);
            // V4
            IEnumerable<FontAwesome> fontAwesome4List = GetFontAwesomeFieldValue<FontAwesome4>(Constants.FA_NAME_V4);
            VM = new FontAwesomeVM {
                CustomFontAwesomes = GetCustomFontAwesome(),
                SolidFontAwesomes = fontAwesomeList,
                RegularFontAwesomes = fontAwesomeRegularList,
                BrandsFontAwesomes = fontAwesomeBrandsList,
                FontAwesomes4 = fontAwesome4List,
                QueryFontAwesomes = new ObservableCollection<FontAwesome>()
            };
            DataContext = VM;
        }

        private void Window_Closing(object sender, System.EventArgs e) {
            VM.CustomFontAwesomes.Clear();
            VM.SolidFontAwesomes.GetEnumerator().Dispose();
            VM.BrandsFontAwesomes.GetEnumerator().Dispose();
            VM.FontAwesomes4.GetEnumerator().Dispose();
            VM.QueryFontAwesomes.Clear();
        }

        private void SelectIconColor(object sender, RoutedEventArgs e) {
            using ColorDialog colorDialog = new ColorDialog();
            if (System.Windows.Forms.DialogResult.OK == colorDialog.ShowDialog()) {
                VM.SelectedIconColor = ColorUtils.GetHtml(colorDialog.Color);
            }
        }
        private void SelectedFontAwesome(object sender, RoutedEventArgs e) {
            TextBlock text = sender as TextBlock;
            VM.SelectedFf = text.FontFamily.ToString();
            VM.SelectedFa = text.Text;
            VM.SelectedFaName = text.Tag as string;
            VM.SelectedFaCode = UnicodeRegexToString(text.Text);
        }
        static string UnicodeRegexToString(string source) {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in source.ToCharArray()) {
                sb.Append(((int)ch).ToString("x").PadLeft(4, '0'));
            }
            return sb.ToString();
        }
        private void ConfirmSelectFontAwesome(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(VM.SelectedFa)) {
                MsgBoxUtils.ShowError("未选择图标", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            DialogResult = true;
        }
        /// <summary>
        /// 搜索框回车执行查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterKey(object sender, System.Windows.Input.KeyEventArgs e) {
            if (System.Windows.Input.Key.Enter == e.Key) {
                System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
                // 文本框需要先失去焦点，才可以获取到文本内容
                textBox.MoveFocus(new System.Windows.Input.TraversalRequest(System.Windows.Input.FocusNavigationDirection.Next));
                QueryFontAwesome(sender, e);
            }
        }
        /// <summary>
        /// 根据查询的字符串匹配名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryFontAwesome(object sender, RoutedEventArgs e) {
            if (!string.IsNullOrEmpty(VM.QueryFontAwesomeName)) {

                VM.QueryFontAwesomes.Clear();
                VM.QueryFontAwesomeResult = string.Empty;
                // 匹配名称
                QueryNameFromFa(Constants.FONT_FAMILY_FA_SOLID);
                QueryNameFromFa(Constants.FONT_FAMILY_FA_REGULAR);
                QueryNameFromFa(Constants.FONT_FAMILY_FA_BRANDS);
                QueryNameFromFa(Constants.FONT_FAMILY_FA_V4);
                VM.QueryFontAwesomeResult = $"查询到{VM.QueryFontAwesomes.Count}个图标";
                // 展开查询选项
                if (5 != FontAwesomeTabControl.SelectedIndex) {
                    FontAwesomeTabControl.SelectedIndex = 5;
                }
            }
        }

        private void QueryNameFromFa(string ffName) {
            IEnumerable<FontAwesome> faIe = null;
            string faName = string.Empty;
            if (Constants.FONT_FAMILY_FA_SOLID.Equals(ffName)) {
                faIe = VM.SolidFontAwesomes;
                faName = Constants.FA_NAME_SOLID;
            } else if (Constants.FONT_FAMILY_FA_REGULAR.Equals(ffName)) {
                faIe = VM.RegularFontAwesomes;
                faName = Constants.FA_NAME_REGULAR;
            } else if (Constants.FONT_FAMILY_FA_BRANDS.Equals(ffName)) {
                faIe = VM.BrandsFontAwesomes;
                faName = Constants.FA_NAME_BRANDS;
            } else if (Constants.FONT_FAMILY_FA_V4.Equals(ffName)) {
                faIe = VM.FontAwesomes4;
                faName = Constants.FA_NAME_V4;
            }
            foreach (FontAwesome fa in faIe) {
                if (fa.Name.ToLower().Contains(VM.QueryFontAwesomeName.ToLower())) {
                    fa.FfName = ffName;
                    fa.FaName = faName;
                    VM.QueryFontAwesomes.Add(fa);
                }
            }
        }

        /// <summary>
        /// 常用图标
        /// </summary>
        /// <returns></returns>
        private List<FontAwesome> GetCustomFontAwesome() {
            return new List<FontAwesome> {
                new FontAwesome{Name="Comments", Value =  Comments}
                , new FontAwesome{Name="Check", Value =  Check}
                , new FontAwesome{Name="Xmark", Value =  Xmark},
                new FontAwesome{Name="Wheelchair", Value =  Wheelchair},
                new FontAwesome{Name="Bell", Value =  Bell},
                new FontAwesome{Name="Car", Value =  Car},
                new FontAwesome{Name="AngleUp", Value =  AngleUp},
                new FontAwesome{Name="AngleDown", Value =  AngleDown},
                new FontAwesome{Name="Gears", Value =  Gears},
                new FontAwesome{Name="FolderOpen", Value =  FolderOpen},
                new FontAwesome{Name="FolderClosed", Value =  FolderClosed},
                new FontAwesome{Name="House", Value =  House},
                new FontAwesome{Name="Envelope", Value =  Envelope},
                new FontAwesome{Name="Eye", Value =  Eye},
                new FontAwesome{Name="EyeSlash", Value =  EyeSlash},
                new FontAwesome{Name="Heart", Value =  Heart},
                new FontAwesome{Name="Film", Value =  Film},
                new FontAwesome{Name="Globe", Value =  Globe},
                new FontAwesome{Name="Gift", Value =  Gift},
                new FontAwesome{Name="Cloud", Value =  Cloud},
                new FontAwesome{Name="Calendar", Value =  FontAwesome6.Calendar},
                new FontAwesome{Name="Camera", Value =  Camera},
                new FontAwesome{Name="PhotoFilm", Value =  PhotoFilm},
                new FontAwesome{Name="ScaleBalanced", Value =  ScaleBalanced},
                new FontAwesome{Name="Cube", Value =  Cube},
                new FontAwesome{Name="Key", Value =  Key},
                new FontAwesome{Name="Map", Value =  Map},
                new FontAwesome{Name="Pencil", Value =  Pencil},
                new FontAwesome{Name="Rocket", Value =  Rocket},
                new FontAwesome{Name="FaceSmile", Value =  FaceSmile},
                new FontAwesome{Name="Star", Value =  Star},
                new FontAwesome{Name="Tag", Value =  FontAwesome6.Tag},
                new FontAwesome{Name="Table", Value =  FontAwesome6.Table},
                new FontAwesome{Name="Tv", Value =  Tv} };
        }

        private IEnumerable<FontAwesome> GetFontAwesomeFieldValue<T>(string faName) {
            T t = System.Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo info in fields) {
                yield return new FontAwesome { Name = info.Name, Value = info.GetValue(t) as string, FaName = faName };
            }
        }
    }
}
