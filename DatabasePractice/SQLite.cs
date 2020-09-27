using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DatabasePractice
{
    public class SQLite
    {
        public SQLite(string fp)
        {
            if (!File.Exists(fp))
            {
                SQLiteConnection.CreateFile(fp);
            }

            string cs = $"Data Source={fp}";
            string stm = "SELECT SQLITE_VERSION()";

            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();
                using (SqliteCommand cmd = new SqliteCommand(stm, con))
                {
                    string version = cmd.ExecuteScalar().ToString();
                    Console.WriteLine($"SQLite version: {version}");
                }
            }
        }
    }
}
