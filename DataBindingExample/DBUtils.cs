using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;


namespace DataBindingExample
{
    public class DBUtils
    {
        public static void Add(Person person)
        {
            SQLiteConnection connection = Connection.Instance.connection;
            using (var statement = connection.Prepare("INSERT INTO People (Name, Birthday, Male) VALUES (?, ?, ?)"))
            {
                statement.Bind(1, person.Name == null ? " " : person.Name);
                statement.Bind(2, person.Birthday.ToString());
                statement.Bind(3, person.Male ? 1 : 0);
                statement.Step();
            }
        }

        public static void Delete(Person person)
        {
            SQLiteConnection connection = Connection.Instance.connection;
            using (var statement = connection.Prepare("DELETE FROM People WHERE Id=?"))
            {
                statement.Bind(1, person.Id);
                statement.Step();
            }
        }

        public static void Update(Person person, String FieldToUpdate)
        {
            SQLiteConnection connection = Connection.Instance.connection;
            using (var statement = connection.Prepare("UPDATE People SET " + FieldToUpdate + " = ? WHERE Id=?"))
            {
                if (person.Id == null)
                    return;
                object newValue = person.GetType().GetProperty(FieldToUpdate).GetValue(person);
                if (FieldToUpdate.Equals("Name") && newValue == null)
                {
                    newValue = " ";
                }
                statement.Bind(1, newValue.ToString());
                statement.Bind(2, person.Id);
                statement.Step();
            }
        }

        public static void LoadAllPersons(Persons persons)
        {
            SQLiteConnection connection = Connection.Instance.connection;
            using (var statement = connection.Prepare("SELECT Id, Name, Birthday, Male FROM People"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "id= " + ((long)statement[0]).ToString() + " " +
                        (string)statement[1] + " "
                        + (string)statement[2] + " " +
                        ((long)statement[3]).ToString()
                        + Environment.NewLine);

                    persons.Add(new Person()
                    {
                        Id = (int)(long)statement[0],
                        Avatar = null,
                        Name = (string)statement[1],
                        Male = (long)statement[3] == 1,
                        Birthday = !((string)statement[2]).Equals("") ? (DateTime?)DateTime.Parse((string)statement[2]) : null
                    });

                }
            }
        }
    }
}
