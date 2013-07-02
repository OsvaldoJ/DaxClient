using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaxClient
{
    public class DaxCommand :DbCommand
    {
        private AdomdCommand _command;
        private DaxConnection _connection;

        public DaxCommand()
        {
            this._command = new AdomdCommand();
        }

        public override void Cancel()
        {
            this._command.Cancel();
        }

        public override string CommandText
        {
            get
            {
                return this._command.CommandText;
            }
            set
            {
                this._command.CommandText = value;
            }
        }

        public override int CommandTimeout
        {
            get
            {
                return this._command.CommandTimeout;
            }
            set
            {
                this._command.CommandTimeout = value;
            }
        }

        public override System.Data.CommandType CommandType
        {
            get
            {
                return this._command.CommandType;
            }
            set
            {
                this._command.CommandType = value;
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = (DaxConnection)value;
                this._command.Connection = this._connection.InternalConnection;
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbDataReader ExecuteDbDataReader(System.Data.CommandBehavior behavior)
        {
            return new DaxDataReader(this._command.ExecuteReader(behavior));
        }

        public override int ExecuteNonQuery()
        {
            throw new NotSupportedException();
        }

        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        public override System.Data.UpdateRowSource UpdatedRowSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
