using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace XStart2._0.ViewModel {
    public class SecurityVM : BaseViewModel {
        // 种类，type，column
        [DoNotNotify]
        public string Kind { get; set; }
        [DoNotNotify]
        public string Section { get; set; }
        // 操作类型，create/update/remove
        [DoNotNotify]
        public string Operate { get; set; }
        public string Title { get; set; }
        public string ExitMsg { get; set; }
        // 当前口令
        [DoNotNotify]
        public string CurSecurity { get; set; }
        // 原口令
        public string PriSecurity { get; set; }
        // 口令
        public string Security { get; set; }
        // 确认口令
        public string ConfirmSecurity { get; set; }
        public bool RunTypeShow { get; set; } = false;
        public List<string> AutoRunTypes { get; set; } = new List<string>() { "自启动", "手工启动", "不启动" };
        public string AutoRunType { get; set; } = "自启动";
    }
}
