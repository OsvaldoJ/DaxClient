using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace DaxClient.Tests
{
    [TestClass]
    public class DaxCommandTest
    {
        private DaxConnection GetConnection()
        {
            return new DaxConnection(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        }

        [TestMethod]
        public void TestExecuteReader()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "evaluate Customer";
                var dr = cmd.ExecuteReader();
            }
        }
    }
}
