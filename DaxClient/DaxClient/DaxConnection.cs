using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaxClient
{
    public class DaxConnection : DbConnection
    {
        private AdomdConnection _connection;

        public DaxConnection()
        {
            this._connection = new AdomdConnection();
        }

        public DaxConnection(string connectionString)
        {
            this._connection = new AdomdConnection(connectionString);
        }


        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            this._connection.Close();
        }

        public override string ConnectionString
        {
            get
            {
                return this._connection.ConnectionString;
            }
            set
            {
                this._connection.ConnectionString = value;
            }
        }

        internal AdomdConnection InternalConnection
        {
            get
            {
                return this._connection;
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            return new DaxCommand() { Connection = this };
        }

        public override string DataSource
        {
            get { return this._connection.ConnectionString; }
        }

        public override string Database
        {
            get { return this._connection.Database; }
        }

        public override void Open()
        {
            this._connection.Open();
        }

        public override string ServerVersion
        {
            get { return this._connection.ServerVersion; }
        }

        public override System.Data.ConnectionState State
        {
            get { return this._connection.State; }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._connection.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
