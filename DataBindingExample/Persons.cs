using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.IO;

namespace DataBindingExample
{
    public class Persons : ObservableCollection<Person>
    {
        public void Load()
        {
            Add(new Person()
            {
                Avatar = null,
                Name = "Петр Сидоров",
                Male = true,
                Birthday = new DateTime(1980, 5, 19), 
                HomeNumber = "88005553535",
            });
            Add(new Person()
            {
                Avatar = null,
                Name = "Екатерина Иванова",
                Male = false,
                Birthday = new DateTime(1985, 6, 7),
            });
        }
    }
}
