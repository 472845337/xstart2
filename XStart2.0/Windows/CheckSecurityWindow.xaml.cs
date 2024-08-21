
using System.Windows;
using XStart2._0.Const;
using XStart2._0.ViewModel;


namespace XStart2._0.Windows {
    /// <summary>
    /// Interaction logic for CheckSecurityWindow.xaml
    /// </summary>
    public partial class CheckSecurityWindow : Window {
        SecurityVM vm;
        public CheckSecurityWindow(string title, string priSecurity) {
            InitializeComponent();
            vm = new SecurityVM {
                Title = title,
                PriSecurity = priSecurity
            };
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            DataContext = vm;
        }

        private void CheckSecurity(object sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(vm.Security)) {
                MessageBox.Show("口令不能为空！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            } else if (!vm.Security.Equals(vm.PriSecurity)) {
                MessageBox.Show("口令不一致！", Constants.MESSAGE_BOX_TITLE_ERROR);
                return;
            }
            DialogResult = true;
        }

        private void CancelSecurity(object sender, RoutedEventArgs e) {
            DialogResult = false;
        }
    }


}
