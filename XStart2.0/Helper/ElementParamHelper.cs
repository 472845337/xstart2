using System.Windows;

namespace XStart2._0.Bean {
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
    }
}
