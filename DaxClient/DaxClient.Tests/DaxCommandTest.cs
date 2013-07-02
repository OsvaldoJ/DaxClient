using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

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

        [TestMethod]
        public void TestExecuteReaderAsync()
        {
            var result = GetAllCustomersAsync().Result.ToList();
            Console.WriteLine(result.Count());
        }

        private async Task<IEnumerable<object>> GetAllCustomersAsync()
        {
            var runs = new int[] { 1, 2, 3, 4, 5, 6, 7 };

            var result = (await Task.WhenAll(runs.Select(r => GetCustomersAsync()))).ToList();

            return result;

        }

        private async Task<IEnumerable<object>> GetCustomersAsync()
        {
            var result = new List<object>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "evaluate Customer";
                var dr = await cmd.ExecuteReaderAsync();
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

                while (dr.Read())
                {
                    result.Add(new
                    {
                        Id = dr["CustomerId"],
                        FirstName = dr["FirstName"]
                    });
                }
            }

            return result;
        }
    }
}
