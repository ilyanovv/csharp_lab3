using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataBindingExample
{
    /// <summary>
    /// Interaction logic for WindowAddContact.xaml
    /// </summary>
    public partial class WindowAddContact : Window
    {
        public WindowAddContact()
        {
            InitializeComponent();

            //Person = new Person();
            //DataContext = Person;
        }

        // Person Person { get; set; }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            //if (_addPerson != null)
            //    _addPerson(Person);
            // Close();
            DialogResult = true;
        }

        //Action<Person> _addPerson = null;

        //public static void ShowAddPerson(Action<Person> addPerson)
        //{
        //    WindowAddContact wnd = new WindowAddContact();
        //    wnd._addPerson = addPerson;
        //    wnd.Show();
           
        //}

    }
}
