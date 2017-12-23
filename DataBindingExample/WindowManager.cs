using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBindingExample
{
    static class WindowManager
    {
        static readonly Dictionary<Type, Type> _windows = new Dictionary<Type, Type>();

        public static void Register(Type viewModel, Type view)
        {
            _windows[viewModel] = view;
        }

        public static bool ShowDialog(object viewModel)
        {
            var viewType = _windows[viewModel.GetType()];
            var wnd = Activator.CreateInstance(viewType) as Window;
            wnd.DataContext = viewModel;
            return wnd.ShowDialog() == true;
        }
    }
}
