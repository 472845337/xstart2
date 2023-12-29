using System.Windows;
using System.Windows.Media.Imaging;

namespace XStart2._0.Helper {
    // 自定义的属性值
    class ElementParamHelper : DependencyObject {

        #region 类别Section
        public static string GetTypeSection(DependencyObject obj) {
            return (string)obj.GetValue(TypeSectionProperty);
        }

        public static void SetTypeSection(DependencyObject obj, string value) {
            obj.SetValue(TypeSectionProperty, value);
        }
        public static readonly DependencyProperty TypeSectionProperty = DependencyProperty.Register("TypeSection", typeof(string), typeof(ElementParamHelper), new PropertyMetadata(string.Empty));
        #endregion

        #region 栏目Section
        public static string GetColumnSection(DependencyObject obj) {
            return (string)obj.GetValue(ColumnSectionProperty);
        }

        public static void SetColumnSection(DependencyObject obj, string value) {
            obj.SetValue(ColumnSectionProperty, value);
        }

        public static readonly DependencyProperty ColumnSectionProperty = DependencyProperty.Register("ColumnSection", typeof(string), typeof(ElementParamHelper), new PropertyMetadata(string.Empty));
        #endregion

        #region 项目Section
        public static string GetProjectSection(DependencyObject obj) {
            return (string)obj.GetValue(ProjectSectionProperty);
        }

        public static void SetProjectSection(DependencyObject obj, string value) {
            obj.SetValue(ProjectSectionProperty, value);
        }

        public static readonly DependencyProperty ProjectSectionProperty = DependencyProperty.Register("ProjectSection", typeof(string), typeof(ElementParamHelper), new PropertyMetadata(string.Empty));
        #endregion

        #region 按钮图标
        public static BitmapImage GetButtonIcon(DependencyObject obj) {
            return (BitmapImage)obj.GetValue(ButtonIconProperty);
        }

        public static void SetButtonIcon(DependencyObject obj, BitmapImage value) {
            obj.SetValue(ButtonIconProperty, value);
        }

        public static readonly DependencyProperty ButtonIconProperty = DependencyProperty.Register("ButtonIcon", typeof(BitmapImage), typeof(ElementParamHelper), new PropertyMetadata(null));
        #endregion
        #region 按钮名称
        public static string GetButtonName(DependencyObject obj) {
            return (string)obj.GetValue(ButtonIconProperty);
        }

        public static void SetButtonName(DependencyObject obj, string value) {
            obj.SetValue(ButtonIconProperty, value);
        }

        public static readonly DependencyProperty ButtonNameProperty = DependencyProperty.Register("ButtonName", typeof(string), typeof(ElementParamHelper), new PropertyMetadata(string.Empty));
        #endregion
    }
}
