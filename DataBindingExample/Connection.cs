using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLitePCL;

namespace DataBindingExample
{
    public class Connection
    {
        public SQLiteConnection connection { get; }
        protected Connection() {
            connection = new SQLiteConnection("Storage.db");
            string sql = @"CREATE TABLE IF NOT EXISTS People (
                                                Id INTEGER NOT NULL PRIMARY KEY,
                                                Name TEXT,
                                                Birthday TEXT,
                                                Male INTEGER CHECK (Male IN (0, 1)) DEFAULT 1);";
            using (var statement = connection.Prepare(sql))
            {
                statement.Step();
            }
            System.Diagnostics.Debug.WriteLine("connection = " + connection.ToString());
        }

        private sealed class SingletonCreator
        {
            private static readonly Connection instance = new Connection();
            public static Connection Instance { get { return instance; } }
        }

        public static Connection Instance
        {
            get { return SingletonCreator.Instance; }
        }

    }
}
