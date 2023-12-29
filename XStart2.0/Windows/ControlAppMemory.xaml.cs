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

namespace XStart2._0.Windows {
    /// <summary>
    /// ControlAppMemory.xaml 的交互逻辑
    /// </summary>
    public partial class ControlAppMemory : Window {

        public string AppName { get; set; }
        public string MinMemory { get; set; }
        public string MaxMemory { get; set; }
        public ControlAppMemory() {
            InitializeComponent();
        }
    }
}
