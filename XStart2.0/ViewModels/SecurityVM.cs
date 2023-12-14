using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XStart2._0.ViewModels {
    public class SecurityVM : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // 种类，type，column
        public string Kind { get; set; }
        public string Section { get; set; }
        // 操作类型，create/update/remove
        public string Operate { get; set; }
        private string title;
        public string Title { get => title; set { title = value;OnPropertyChanged("Title"); } }
        // 当前口令
        public string CurSecurity { get; set; }
        // 原口令
        private string priSecurity;
        public string PriSecurity { get => priSecurity; set { priSecurity = value;OnPropertyChanged("PriSecurity"); } }
        // 口令
        private string security;
        public string Security { get => security; set { security = value; OnPropertyChanged("Security"); } }
        // 确认口令
        private string confirmSecurity;
        public string ConfirmSecurity { get => confirmSecurity; set { confirmSecurity = value; OnPropertyChanged("ConfirmSecurity"); } }
    }
}
