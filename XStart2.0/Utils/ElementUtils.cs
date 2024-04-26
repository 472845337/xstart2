﻿using System.Windows;
using System.Windows.Media;

namespace XStart2._0.Utils {
    class ElementUtils {
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型
                T childType = child as T;
                if (childType == null) {
                    // 在下一级控件中递归查找
                    foundChild = FindChild<T>(child, childName);

                    // 找到控件就可以中断递归操作 
                    if (foundChild != null) break;
                } else if (!string.IsNullOrEmpty(childName)) {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件
                    if (frameworkElement != null && frameworkElement.Name == childName) {
                        foundChild = (T)child;
                        break;
                    }
                    // 在下一级控件中递归查找
                    foundChild = FindChild<T>(child, childName);

                    // 找到控件就可以中断递归操作 
                    if (foundChild != null) break;
                } else {
                    // 查找到了控件
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static T FindChildTag<T>(DependencyObject parent, string tag) where T : DependencyObject {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++) {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型
                T childType = child as T;
                if (childType == null) {
                    // 在下一级控件中递归查找
                    foundChild = FindChildTag<T>(child, tag);

                    // 找到控件就可以中断递归操作 
                    if (foundChild != null) break;
                } else if (!string.IsNullOrEmpty(tag)) {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件
                    if (frameworkElement != null && (string)frameworkElement.Tag == tag) {
                        foundChild = (T)child;
                        break;
                    }
                    // 在下一级控件中递归查找
                    foundChild = FindChildTag<T>(child, tag);

                    // 找到控件就可以中断递归操作 
                    if (foundChild != null) break;
                } else {
                    // 查找到了控件
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

    }
}
