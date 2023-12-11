using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace XStart2._0.ViewModels {
    class NotifyData : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string title;
        public string Title { get => title; set { title = value; OnPropertyChanged("Title"); } }

        private string background;
        public string Background { get => background; set { background = value; OnPropertyChanged("Background"); } }

        private string content;
        public string Content { get => content; set { content = value; OnPropertyChanged("Content"); } }
    }
}
