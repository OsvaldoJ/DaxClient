using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace DaxClient.Tests
{
    [TestClass]
    public class DaxConnectionTest
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
        }

        [TestMethod]
        public void TestDaxConnectionOpen()
        {
            var connection = new DaxConnection(GetConnectionString());

            connection.Open();
        }

        [TestMethod]
        public void TestDaxConnectionOpenClose()
        {
            var connection = new DaxConnection(GetConnectionString());

            connection.Open();
            connection.Close();
        }

        [TestMethod]
        public void TestDaxConnectionDispose()
        {
            using (var connection = new DaxConnection(GetConnectionString()))
            {
                connection.Open();
            }
        }

        [TestMethod]
        public void TestDaxConnectionCreateCommand()
        {
            using (var connection = new DaxConnection(GetConnectionString()))
            {
                var cmd = connection.CreateCommand();
                Assert.AreEqual(typeof(DaxCommand), cmd.GetType());
                Assert.AreEqual(connection, cmd.Connection);
            }
        }
    }
}
