using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExample
{
    public class YCommand : System.Windows.Input.ICommand
    {
        Action<object> _action = null;

        public YCommand(Action<object> action)
        {
            _action = action;
        }

        bool _canExecute = true;

        public bool CanExecuteProperty
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}
