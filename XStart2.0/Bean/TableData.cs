
using PropertyChanged;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart2._0.Bean {
    /// <summary>
    /// 快捷对象公共的属性
    /// </summary>
    /// 
    [Serializable]
    [AddINotifyPropertyChangedInterface]
    public abstract class TableData {

        public const string KEY_NAME = "Name";
        public const string KEY_SORT = "Sort";
        public const string KEY_ICON_INDEX = "IconIndex";
        // section
        [DoNotNotify]
        [TableParam(true, "section", "VARCHAR")]
        public string Section { get; set; }
        // 名称
        [TableParam("name", "VARCHAR")]
        public string Name { get; set;  }
        // 排序
        [DoNotNotify]
        [TableParam("sort", "INT")]
        public int? Sort { get; set; }
        // 图标（symbol或exe 多图标的下标）
        [DoNotNotify]
        [TableParam("icon_index", "INT")]
        public int? IconIndex { get; set; }
        // 加密
        [DoNotNotify]
        [TableParam("password", "VARCHAR")]
        public string Password { get; set; }
        [DoNotNotify]
        public string OrderBy { get; set; }

        #region 窗口中使用的属性
        // 是否有密码
        [AlsoNotifyFor("CanLock")]
        public bool HasPassword { get; set; }
        // 用于判定是否锁定状态，展示锁定页面
        [AlsoNotifyFor("CanLock", "Unlocked")]
        public bool Locked { get; set; }
        // 是否非锁定状态，展示内容页面,则Locked控制
        public bool Unlocked { get => !Locked; }
        // 是否可锁, HasPassword && !Locked
        public bool CanLock { get => HasPassword && !Locked; }
        // 解锁口令
        public string UnlockSecurity { get; set; }
        // 记住口令
        public bool RememberSecurity { get; set; }
        #endregion
    }
}
