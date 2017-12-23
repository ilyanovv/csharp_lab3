using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataBindingExample.ViewModel;

namespace DataBindingExample
{
    /// <summary>
    /// Interaction logic for SimpleWindow.xaml
    /// </summary>
    public partial class SimpleWindow : Window
    {
        private PersonViewModel _personViewModel;

        public SimpleWindow()
        {
            InitializeComponent();

            Person testPerson = new Person()
            {
                Name = "Сергей",
                // Birthday = new DateTime(1992, 5, 1),
                Male = true
            };

            _personViewModel = new PersonViewModel(testPerson);
            DataContext = _personViewModel;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _personViewModel.Name = "111";
            _personViewModel.Birthday = DateTime.Today;
        }

        private void TextBoxNewName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Так делать нежелательно, это не совсем MVVM
            _personViewModel.UpdateCommand.RaiseCanExecuteChanged();
        }
    }
}
