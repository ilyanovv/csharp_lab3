﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBindingExample
{
    public class XCommand : ICommand
    {
        Action _action = null;

        public XCommand(Action action)
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
            _action();
        }
    }
}
