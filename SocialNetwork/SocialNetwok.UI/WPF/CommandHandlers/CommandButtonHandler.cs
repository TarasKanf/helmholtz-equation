using System;
using System.Windows.Input;

namespace SocialNetwork.UI.WPF
{
    public class CommandButtonHandler : ICommand
    {
        private readonly Action<object> action;
        private readonly bool canExecute;

        public CommandButtonHandler(Action<object> action, bool canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged; // never used?

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }
    }
}