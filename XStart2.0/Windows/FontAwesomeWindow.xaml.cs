using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using XStart2._0.Const;
using XStart2._0.Bean;
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
            IEnumerable<FontAwesome> fontAwesomeList = GetFontAwesomeFieldValue<FontAwesome6>();
            // 品牌
            IEnumerable<FontAwesome> fontAwesomeBrandsList = GetFontAwesomeFieldValue<FontAwesomeBrands>();
            // 常用
            IEnumerable<FontAwesome> fontAwesomeRegularList = GetFontAwesomeFieldValue<FontAwesomeRegular>();
            // V4
            IEnumerable<FontAwesome> fontAwesome4List = GetFontAwesomeFieldValue<FontAwesome4>();
            VM = new FontAwesomeVM {
                CustomFontAwesomes = GetCustomFontAwesome(),
                SolidFontAwesomes = fontAwesomeList,
                RegularFontAwesomes = fontAwesomeRegularList,
                BrandsFontAwesomes = fontAwesomeBrandsList,
                FontAwesomes4 = fontAwesome4List,
                QueryFontAwesomes = new ObservableCollection<FontAwesome>()
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
            if (string.IsNullOrEmpty(VM.SelectedFa)) {
                System.Windows.MessageBox.Show("未选择图标", "错误");
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
            if(System.Windows.Input.Key.Enter == e.Key) {
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
                foreach (FontAwesome fa in VM.SolidFontAwesomes) {
                    if (fa.Name.ToLower().Contains(VM.QueryFontAwesomeName.ToLower())) {
                        fa.FfName = Constants.FONT_FAMILY_FA_SOLID;
                        VM.QueryFontAwesomes.Add(fa);
                    }
                }
                foreach (FontAwesome fa in VM.RegularFontAwesomes) {
                    if (fa.Name.ToLower().Contains(VM.QueryFontAwesomeName.ToLower())) {
                        fa.FfName = Constants.FONT_FAMILY_FA_REGULAR;
                        VM.QueryFontAwesomes.Add(fa);
                    }
                }
                foreach (FontAwesome fa in VM.BrandsFontAwesomes) {
                    if (fa.Name.ToLower().Contains(VM.QueryFontAwesomeName.ToLower())) {
                        fa.FfName = Constants.FONT_FAMILY_FA_BRANDS;
                        VM.QueryFontAwesomes.Add(fa);
                    }
                }
                foreach (FontAwesome fa in VM.FontAwesomes4) {
                    if (fa.Name.ToLower().Contains(VM.QueryFontAwesomeName.ToLower())) {
                        fa.FfName = Constants.FONT_FAMILY_FA_V4;
                        VM.QueryFontAwesomes.Add(fa);
                    }
                }
                VM.QueryFontAwesomeResult = $"查询到{VM.QueryFontAwesomes.Count}个图标";
                // 展开查询选项
                if(5 != FontAwesomeTabControl.SelectedIndex) {
                    FontAwesomeTabControl.SelectedIndex = 5;
                }
            }
        }

        /// <summary>
        /// 常用图标
        /// </summary>
        /// <returns></returns>
        private List<FontAwesome> GetCustomFontAwesome() {
            return new List<FontAwesome> { 
                new FontAwesome{Name="Comments", Value =  Comments }
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

        private IEnumerable<FontAwesome> GetFontAwesomeFieldValue<T>() {
            T t = System.Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo info in fields) {
                yield return new FontAwesome { Name = info.Name, Value = info.GetValue(t) as string };
            }
        }
    }
}
