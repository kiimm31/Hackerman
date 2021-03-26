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

        public string EncryptString(string unHashed)
        {
            byte[] b = System.Text.Encoding.ASCII.GetBytes(unHashed);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public string DecryptString(string hashed)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(hashed);
                decrypted = System.Text.Encoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }
            return decrypted;
        }
    }
}
