using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;
using System.Data;

namespace DaxClient.Tests
{
    [TestClass]
    public class DaxDataReaderTest
    {
        private DaxConnection GetConnection()
        {
            return new DaxConnection(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        }

        private DaxCommand GetCommand()
        {
            var connection = GetConnection();

            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "evaluate Customer";

            return (DaxCommand)cmd;
        }

        [TestMethod]
        public void TestRead()
        {
            var dr = GetCommand().ExecuteReader();
            var count = 0;
            while (dr.Read())
            {
                count++;
            }

            Assert.IsTrue(count > 1);
        }

        [TestMethod]
        public void TestFieldCount()
        {
            var dr = GetCommand().ExecuteReader();
            Assert.AreEqual(25, dr.FieldCount);
        }

        [TestMethod]
        public void TestGetValue()
        {
            var dr = GetCommand().ExecuteReader();
            dr.Read();
            Assert.IsNotNull(dr.GetValue(0));
        }

        [TestMethod]
        public void TestIsDBNull()
        {
            var dr = GetCommand().ExecuteReader();
            dr.Read();
            Assert.IsTrue(dr.IsDBNull(3));
        }

        [TestMethod]
        public void TestSchema()
        {
            var dr = GetCommand().ExecuteReader();
            var schema = dr.GetSchemaTable();

            Assert.AreEqual("CustomerKey", schema.Rows[0][0]);
            Assert.IsNotNull(schema.Rows.Cast<DataRow>().FirstOrDefault(r => r[0].ToString() == "Title"));
            Assert.IsNotNull(schema.Rows.Cast<DataRow>().FirstOrDefault(r => r[0].ToString() == "CustomerId"));
        }

        [TestMethod]
        public void TestGetOrdinal()
        {
            var dr = GetCommand().ExecuteReader();
            Assert.AreEqual(3, dr.GetOrdinal("Title"));
        }
    }
}
