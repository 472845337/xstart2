using System;
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
    /// MstscWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MstscWindow : Window {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public MstscWindow() {
            InitializeComponent();
        }
    }
}
