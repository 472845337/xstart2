using System;
using System.Windows.Input;

namespace XStart2._0.Commands {
    /// <summary>
    /// 主命令
    /// </summary>
    public class RelayCommand : ICommand {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object param) {
            return _canExecute(param);
        }

        public void Execute(object param) {
            _execute(param);
        }

        public static bool DefaultChangeFunc(object param) {
            return true;
        }
    }
}
