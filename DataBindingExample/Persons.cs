using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

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
                Birthday = new DateTime(1980, 5, 19)
            });
            Add(new Person()
            {
                Avatar = null,
                Name = "Екатерина Иванова",
                Male = false,
                Birthday = new DateTime(1985, 6, 7)
            });
        }
    }
}
