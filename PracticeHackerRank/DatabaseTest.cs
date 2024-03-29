﻿using DatabasePractice;
using NUnit.Framework;

namespace PracticeHackerRank
{
    public class DatabaseTest
    {
        [Test]
        [Ignore("wrong FP")]
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