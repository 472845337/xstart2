﻿using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace XStart2._0.View {
    public class ImageButton : Button {
        public BitmapImage ButtonIcon {
            get {
                return (BitmapImage)GetValue(ButtonIconProperty);
            }
            set {
                SetValue(ButtonIconProperty, value);
            }
        }

        public static readonly System.Windows.DependencyProperty ButtonIconProperty = System.Windows.DependencyProperty.Register("ButtonIcon", typeof(BitmapImage), typeof(ImageButton), new System.Windows.PropertyMetadata(null));


    }
}
