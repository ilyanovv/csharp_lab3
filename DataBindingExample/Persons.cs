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
            /*Add(new Person()
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

            SQLiteConnection connection = Connection.Instance.connection;
            using (var statement = connection.Prepare("INSERT INTO People (Name, Birthday, Sex) VALUES (?, ?, ?)"))
            {
                statement.Bind(1, this[0].Name);
                statement.Bind(2, this[0].Birthday.ToString());
                statement.Bind(3, this[0].Male ? 1 : 0);
                statement.Step();
            }*/


            DBUtils.LoadAllPersons(this);

           


        }


    }
}
