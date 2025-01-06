using System;
using System.Windows.Input;

namespace XStart2._0.Commands {
    public class RelayCommand : ICommand {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> _action;
        private readonly Func<bool> _canExecute;
        public RelayCommand(Action<object> action, Func<bool> canExecute = null) {
            _action = action;
            _canExecute = canExecute;
        }
        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter) {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter) {
            _action?.Invoke(parameter);
        }
    }
}
