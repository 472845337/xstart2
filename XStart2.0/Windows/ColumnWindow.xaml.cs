﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// ColumnWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColumnWindow : Window {
        public ColumnVM vm;
        public ColumnWindow(ColumnVM columnVm) {
            InitializeComponent();
            vm = columnVm;
            DataContext = vm;
        }

        private void Window_Close(object sender, RoutedEventArgs e) {
            Close();
        }

        private void Save_Column(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Name)) {
                MessageBox.Show("名称不能为空！");
                return;
            } else {
                DialogResult = true;
            }
        }
    }
}