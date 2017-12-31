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
        private static SQLiteConnection connection = null;

        public static SQLiteConnection getConnection()
        {
            if (connection != null)
                return connection; 

            connection = new SQLiteConnection("Storage.db");
            string sql = @"CREATE TABLE IF NOT EXISTS People (
                                                Id INTEGER NOT NULL PRIMARY KEY,
                                                Name TEXT,
                                                Birthday TEXT,
                                                Male INTEGER CHECK (Male IN (0, 1)) DEFAULT 1, 
                                                HomeNumber TEXT,
                                                WorkNumber TEXT,
                                                Skype TEXT,
                                                Email TEXT,
                                                Comment TEXT,
                                                Avatar BLOB);";
            using (var statement = connection.Prepare(sql))
            {
                statement.Step();
            }
            System.Diagnostics.Debug.WriteLine("connection = " + connection.ToString());
            return connection; 
        }

        public static void Add(Person person)
        {
            using (var statement = getConnection().Prepare("" +
                "INSERT INTO People (Name, Birthday, Male, WorkNumber, HomeNumber, Skype, Email, Comment, Avatar) " +
                "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"))
            {
                statement.Bind(1, person.Name == null ? " " : person.Name);
                statement.Bind(2, person.Birthday.ToString());
                statement.Bind(3, person.Male ? 1 : 0);
                statement.Bind(4, person.WorkNumber);
                statement.Bind(5, person.HomeNumber);
                statement.Bind(6, person.Skype);
                statement.Bind(7, person.Email);
                statement.Bind(8, person.Comment);
                statement.Bind(9, person.Avatar);
                statement.Step();
            }
        }

        public static void Delete(Person person)
        {
            using (var statement = getConnection().Prepare("DELETE FROM People WHERE Id=?"))
            {
                statement.Bind(1, person.Id);
                statement.Step();
            }
        }

        public static void Update(Person person, String FieldToUpdate)
        {
            using (var statement = getConnection().Prepare("UPDATE People SET " + FieldToUpdate + " = ? WHERE Id=?"))
            {
                if (person.Id == null)
                    return;
                object newValue = person.GetType().GetProperty(FieldToUpdate).GetValue(person);
                if (FieldToUpdate.Equals("Name") && newValue == null)
                {
                    newValue = " ";
                }
                if (FieldToUpdate.Equals("Male"))
                {
                    newValue = (bool)newValue ? 1 : 0;
                }
                //TODO: с Avatar нужно по-другому
                statement.Bind(1, newValue.ToString());
                statement.Bind(2, person.Id);
                statement.Step();
            }
        }

        public static void LoadAllPersons(Persons persons)
        {
            using (var statement = getConnection().Prepare("" +
                "SELECT Id, Name, Birthday, Male, Skype, WorkNumber, HomeNumber, Comment, Email " +
                "FROM People"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    /* System.Diagnostics.Debug.WriteLine(
                        "id= " + ((long)statement[0]).ToString() + " " +
                        (string)statement[1] + " "
                        + (string)statement[2] + " " +
                        ((long)statement[3]).ToString()
                        + Environment.NewLine); */
     
                    persons.Add(new Person()
                    {
                        Id = (int)(long)statement[0],
                        Avatar = null,
                        Name = (string)statement[1],
                        Male = (long)statement[3] == 1,
                        Birthday = !((string)statement[2]).Equals("") ? (DateTime?)DateTime.Parse((string)statement[2]) : null, 
                        Skype = (string)statement[4], 
                        WorkNumber = (string)statement[5], 
                        HomeNumber = (string)statement[6], 
                        Comment = (string)statement[7], 
                        Email = (string)statement[8]
                        //TODO: получить Avatar
                    });

                }
            }
        }
    }
}
