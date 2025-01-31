using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Workshop.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeWithParameter;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> executeWithParameter, Func<bool> canExecute = null)
        {
            _executeWithParameter = executeWithParameter;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else if (_executeWithParameter != null)
            {
                _executeWithParameter(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
