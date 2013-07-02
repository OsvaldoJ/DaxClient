using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AnalysisServices.AdomdClient;
using System.Configuration;
using System.Data;

namespace DaxClient.Tests
{
    [TestClass]
    public class AdomdTest
    {
        private AdomdConnection GetConnection()
        {
            return new AdomdConnection(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        }

        [TestMethod]
        public void TestConnection()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
            }
        }

        [TestMethod]
        public void TestQuery()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "evaluate Customer";
                    using (var dr = cmd.ExecuteReader())
                    {
                        Console.WriteLine(dr.FieldCount);
                        var schema = dr.GetSchemaTable();
                        foreach (DataRow schemaRow in schema.Rows)
                        {
                            Console.WriteLine(schemaRow[0]);
                        }
                        while (dr.Read())
                        {
                            //Console.WriteLine("New line");
                        }
                    }
                }
            }
        }

    }
}
