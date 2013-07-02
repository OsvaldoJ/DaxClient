using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Dapper;
using System.Linq;

namespace DaxClient.Tests
{
    [TestClass]
    public class DapperTest
    {
        private DaxConnection GetConnection()
        {
            return new DaxConnection(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        }

        [TestMethod]
        public void TestDynamic()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var result = connection.Query("evaluate Customer").ToList();
                Assert.IsTrue(result.Count() > 1);
                Assert.AreEqual("AW00019728", result.First().CustomerId);
            }
        }
    }
}
