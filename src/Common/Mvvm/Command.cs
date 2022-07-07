using System;
using System.Windows.Input;

namespace Common.Mvvm
{
    public class Command : ICommand
    {
        private readonly Action _action;
        private readonly Action<object> _argAction;

        public Command(Action action)
        {
            _action = action;
        }

        public Command(Action<object> action)
        {
            _argAction = action;
        }
#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning disable

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
            _argAction?.Invoke(parameter);
        }
    }
}
