using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace lab6_7.Commands.Base
{
    abstract class Command : ICommand
    {
        // Передаем управление WPF (автоматически). Virtual чтоб если чо поменять
        public virtual event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}
