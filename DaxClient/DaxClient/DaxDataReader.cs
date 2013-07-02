using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaxClient
{
    public class DaxDataReader : DbDataReader
    {
        private AdomdDataReader _reader;
        private DataTable _schemaTable;

        internal DaxDataReader(AdomdDataReader reader)
        {
            this._reader = reader;
        }

        public override void Close()
        {
            this._reader.Close();
        }

        public override int Depth
        {
            get { return 0; }
        }

        public override int FieldCount
        {
            get { return this._reader.FieldCount; }
        }

        public override bool GetBoolean(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override short GetInt16(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override int GetInt32(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetInt64(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override string GetName(int ordinal)
        {
            return this.GetSchemaTable().Rows[ordinal]["ColumnName"] as string;
        }

        public override int GetOrdinal(string name)
        {
            var row = this.GetSchemaTable().Rows.Cast<DataRow>().FirstOrDefault(r => r["ColumnName"].ToString() == name);
            return (int)row["ColumnOrdinal"];
        }

        public override System.Data.DataTable GetSchemaTable()
        {
            if (this._schemaTable == null)
            {
                this._schemaTable = this._reader.GetSchemaTable();

                // Add Original ColumnName
                this._schemaTable.Columns.Add("OriginalColumnName", typeof(string));
                foreach (DataRow row in this._schemaTable.Rows)
                {
                    string columnName = row["ColumnName"] as string;
                    row["OriginalColumnName"] = columnName;
                    var idx = columnName.IndexOf("[");
                    if (idx >= 0)
                    {
                        columnName = columnName.Substring(idx + 1);
                    }
                    columnName = columnName.Trim('[', ']').Replace(" ", string.Empty);

                    row["ColumnName"] = columnName;
                }
            }

            return this._schemaTable;
        }

        public override string GetString(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(int ordinal)
        {
            return this._reader.GetValue(ordinal);
        }

        public override int GetValues(object[] values)
        {
            return this._reader.GetValues(values);
        }

        public override bool HasRows
        {
            get { throw new NotImplementedException(); }
        }

        public override bool IsClosed
        {
            get { return this._reader.IsClosed; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return GetValue(ordinal) == null;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            return this._reader.Read();
        }

        public override int RecordsAffected
        {
            get { throw new NotSupportedException(); }
        }

        public override object this[string name]
        {
            get { return GetValue(GetOrdinal(name)); }
        }

        public override object this[int ordinal]
        {
            get { return GetValue(ordinal); }
        }
    }
}
