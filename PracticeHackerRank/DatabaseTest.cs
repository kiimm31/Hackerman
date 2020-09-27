using DatabasePractice;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeHackerRank
{
    public class DatabaseTest
    {

        [Test]
        public void sqliteTest()
        {
            SQLite sQLite = new SQLite(@"C:\Users\KimHung\Documents\Test.db");

            string password = "thisIsMyPassword";

            string hashed = sQLite.EncryptString(password);

            string unHashed = sQLite.DecryptString(hashed);

            Assert.AreEqual(unHashed, password);
        }
    }
}
