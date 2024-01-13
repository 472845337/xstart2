using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DragDropAssist {
    /// <summary>
    /// 可选择列表拖放附加属性
    /// </summary>
    public class SelectorDragDropAttach {
        public static bool GetIsItemsDragDropEnabled(ItemsControl scrollViewer) {
            return (bool)scrollViewer.GetValue(IsItemsDragDropEnabledProperty);
        }

        public static void SetIsItemsDragDropEnabled(ItemsControl scrollViewer, bool value) {
            scrollViewer.SetValue(IsItemsDragDropEnabledProperty, value);
        }

        public static readonly DependencyProperty IsItemsDragDropEnabledProperty =
            DependencyProperty.RegisterAttached("IsItemsDragDropEnabled", typeof(bool), typeof(SelectorDragDropAttach), new PropertyMetadata(false, OnIsItemsDragDropEnabledChanged));

        private static readonly DependencyProperty SelectorDragDropProperty =
            DependencyProperty.RegisterAttached("SelectorDragDrop", typeof(SelectorDragDrop), typeof(SelectorDragDropAttach), new PropertyMetadata(null));

        private static void OnIsItemsDragDropEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            bool b = (bool)e.NewValue;
            ItemsControl selector = d as ItemsControl;
            var selectorDragDrop = selector?.GetValue(SelectorDragDropProperty) as SelectorDragDrop;
            if (selectorDragDrop != null)
                selectorDragDrop.Selector = null;
            if (b == false) {
                selector?.SetValue(SelectorDragDropProperty, null);
                return;
            }
            selector?.SetValue(SelectorDragDropProperty, new SelectorDragDrop(selector));

        }

    }
}
