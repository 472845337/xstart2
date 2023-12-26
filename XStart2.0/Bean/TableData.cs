
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart2._0.Bean {
    /// <summary>
    /// 快捷对象公共的属性
    /// </summary>
    /// 
    [Serializable]
    public abstract class TableData : INotifyPropertyChanged {

        public const string KEY_NAME = "Name";
        public const string KEY_SORT = "Sort";
        public const string KEY_ICON_INDEX = "IconIndex";
        // section
        [TableParam(true, "section", "VARCHAR")]
        public string Section { get; set; }
        private string name;
        // 名称
        [TableParam("name", "VARCHAR")]
        public string Name { get => name; set { name = value; OnPropertyChanged("Name"); } }
        // 排序
        [TableParam("sort", "INT")]
        public int? Sort { get; set; }
        // 图标（symbol或exe 多图标的下标）
        [TableParam("icon_index", "INT")]
        public int? IconIndex { get; set; }
        // 加密
        [TableParam("password", "VARCHAR")]
        public string Password { get; set; }
        public string OrderBy { get; set; }

        #region 窗口中使用的属性
        // 是否有密码
        private bool hasPassword = false;
        public bool HasPassword { get => hasPassword; set { hasPassword = value; OnPropertyChanged("HasPassword"); SetCanLock(); } }
        // 用于判定是否锁定状态，展示锁定页面
        private bool locked = false;
        public bool Locked { get => locked; set { locked = value; OnPropertyChanged("Locked"); SetUnlocked(); SetCanLock(); } }
        // 是否非锁定状态，展示内容页面,则Locked控制
        private bool unlocked = true;
        public bool Unlocked { get => unlocked; set { unlocked = value; OnPropertyChanged("Unlocked"); } }
        // 是否可锁, HasPassword && !Locked
        private bool canLock = false;
        public bool CanLock { get => canLock; set { canLock = value; OnPropertyChanged("CanLock"); } }
        // 解锁口令
        private string unlockSecurity;
        public string UnlockSecurity { get => unlockSecurity; set { unlockSecurity = value; OnPropertyChanged("UnlockSecurity"); } }
        // 记住口令
        private bool rememberSecurity;
        public bool RememberSecurity { get => rememberSecurity; set { rememberSecurity = value; OnPropertyChanged("RememberSecurity"); } }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void SetUnlocked() {
            Unlocked = !Locked;
        }

        private void SetCanLock() {
            CanLock = HasPassword && !Locked;
        }
    }
}
