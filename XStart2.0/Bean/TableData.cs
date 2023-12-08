
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart.Bean {
    /// <summary>
    /// 快捷对象公共的属性
    /// </summary>
    /// 
    [Serializable]
    public abstract class TableData : INotifyPropertyChanged{

        public const string KEY_NAME = "Name";
        public const string KEY_SORT = "Sort";
        public const string KEY_ICON_INDEX = "IconIndex";
        // section
        [TableParam(true, "section", "VARCHAR")]
        public string Section { get; set; }
        private string name;
        // 名称
        [TableParam("name", "VARCHAR")]
        public string Name { get => name; set { name = value;OnPropertyChanged("Name"); } }
        // 排序
        [TableParam("sort", "INT")]
        public int? Sort { get; set; }
        // 图标（symbol或exe 多图标的下标）
        [TableParam("icon_index", "INT")]
        public int? IconIndex { get; set; }
        public string OrderBy { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
