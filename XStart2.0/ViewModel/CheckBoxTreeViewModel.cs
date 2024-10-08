﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XStart2._0.ViewModel {
    /// <summary>
    ///  这个继承BaseViewModel后，没有效果，先使用原有的逻辑
    /// </summary>
    public class CheckBoxTreeViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool? _IsChecked = false;
        private string _Section;
        private string _Header = string.Empty;
        private object _Data;
        private List<CheckBoxTreeViewModel> _Children = null;
        private CheckBoxTreeViewModel _Parent = null;

        public bool? IsChecked { get => _IsChecked; set { SetIsChecked(value, true, true); } }
        public string Header { get => _Header; set => _Header = value; }
        public object Data { get => _Data; set => _Data = value; }
        public List<CheckBoxTreeViewModel> Children { get => _Children; set { _Children = value; SetParentValue(); } }
        public string Section { get => _Section; set => _Section = value; }
        public CheckBoxTreeViewModel Parent { get => _Parent; private set => _Parent = value; }

        /// <summary>
        /// 设置节点IsChecked的值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="updateChildren"></param>
        /// <param name="updateParent"></param>
        private void SetIsChecked(bool? value, bool updateChildren, bool updateParent) {
            if (value == _IsChecked)
                return;
            _IsChecked = value;
            if (updateChildren && _IsChecked.HasValue && Children != null) {
                Children.ForEach(c => c.SetIsChecked(_IsChecked, true, false));
            }
            if (updateParent && Parent != null) {
                Parent.VerifyCheckState();
            }
            OnPropertyChanged("IsChecked");
        }
        /// <summary>
        /// 验证并设置父级节点的IsChecked的值
        /// </summary>
        private void VerifyCheckState() {
            bool? state = null;
            for (int i = 0; i < Children.Count; ++i) {
                bool? current = Children[i].IsChecked;
                if (i == 0) {
                    state = current;
                } else if (state != current) {
                    state = null;
                    break;
                }
            }
            SetIsChecked(state, false, true);
        }
        /// <summary>
        /// 数据初始化时设置父节点的值
        /// </summary>
        private void SetParentValue() {
            Children?.ForEach(ch => ch.Parent = this);
        }
    }
}
