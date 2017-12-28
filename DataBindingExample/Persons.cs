using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using SQLitePCL;


namespace DataBindingExample
{
    public class Persons : ObservableCollection<Person>
    {
        public void Load()
        {
            DBUtils.LoadAllPersons(this);
        }


    }
}
