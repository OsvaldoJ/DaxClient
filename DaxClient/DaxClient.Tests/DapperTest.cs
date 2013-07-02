using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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

        [TestMethod]
        public void TestDynamicAsync()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var result = connection.QueryAsync<dynamic>("evaluate Customer").Result.ToList();
                Assert.IsTrue(result.Count() > 1);
                Assert.AreEqual("AW00019728", result.First().CustomerId);
            }
        }

        [TestMethod]
        public void TestGetAllAsync()
        {
            var result = GetAllCustomersAsync().Result.ToList();
            Console.WriteLine(result.Count());
        }

        private async Task<IEnumerable<dynamic>> GetAllCustomersAsync()
        {
            var runs = new int[] { 1, 2 };

            var result = (await Task.WhenAll(runs.Select(r => GetCustomersAsync()))).ToList();

            return result;
        }

        private async Task<IEnumerable<dynamic>> GetCustomersAsync()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                return (await connection.QueryAsync<dynamic>("evaluate Customer"));
            }

            return Enumerable.Empty<dynamic>();
        }

    }
}
