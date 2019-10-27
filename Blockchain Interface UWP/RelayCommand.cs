using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockchain_Interface_UWP
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;

        private readonly Action _execute;

        public RelayCommand(Action execute)
            : this(execute, null)
        {
            this._execute = execute;
        }

        public RelayCommand(Action execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this._execute = execute;
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            this._execute();
        }

        public void RaiseCanExecuteChanged()
        {
            // ISSUE: reference to a compiler-generated field
            EventHandler canExecuteChanged = this.CanExecuteChanged;
            if (canExecuteChanged == null)
            {
                return;
            }

            canExecuteChanged(this, EventArgs.Empty);
        }
    }
}
