﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DataBaseProject.Models;
using DataBaseProject.DBConnector;

namespace DataBases.DbConnector
{
    public class OracleDbConnector : IDbConnector
    {

        private OracleConnection _databaseConnection;
        private string _username;
        private string _tablespace;

        public OracleDbConnector(string address, string username, string userPassword, string tablespace)
        {
            _username = username;
            _tablespace = tablespace;

            var connectionString = new OracleConnectionStringBuilder();
            connectionString.DataSource = address;
            connectionString.UserID = username;
            connectionString.Password = userPassword;
            connectionString.ValidateConnection = true;

            _databaseConnection = new OracleConnection(connectionString.ToString());
            _databaseConnection.Open();

        }

        public DataTable ExecuteCommand(string command)
        {
            if (command == "" || command == null || command == "select * from ") return new DataTable();

            /*using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = command;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }*/

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = command;

                DataTable dt = new DataTable();
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);

                adapter.Fill(dt);
                adapter.Dispose();

                return dt;
            }
        }

        public DbDataReader ExecuteCommandReader(string command)
        {
            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = command;
                return cmd.ExecuteReader();
            }
            return null;
        }

        public void InsertRow(string tableName, List<DbColumnNamesModel> l)
        {
            if (tableName == null || tableName == "") return;

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = "select * from " + tableName;

                DataTable table = new DataTable();
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(table);

                    DataRow dataRow = table.NewRow();

                    for (int i = 0; i < l.Count; i++)
                    {
                        dataRow[l[i].Name] = l[i].Value;
                    }

                    table.Rows.Add(dataRow);

                    OracleCommandBuilder commandBuilder = new OracleCommandBuilder(adapter);

                    //adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                    adapter.InsertCommand = commandBuilder.GetInsertCommand();
                    adapter.Update(table);
                }
            }
        }

        public void DeleteRow(string tableName, object[] itemArray)
        {
            if (tableName == "" || tableName == null) return;

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = "select * from " + tableName;

                DataTable table = new DataTable();
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    adapter.Fill(table);

                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        bool equal = true;
                        for (int j = 0; j < itemArray.Length; j++)
                        {
                            if (!itemArray[j].Equals(table.Rows[i].ItemArray[j]))
                            {
                                equal = false;
                                break;
                            }
                        }

                        if (equal)
                        {
                            table.Rows[i].Delete();
                            break;
                        }
                    }

                    OracleCommandBuilder commandBuilder = new OracleCommandBuilder(adapter);

                    //adapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                    adapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                    adapter.Update(table);
                }
            }
        }

        public List<string> GetTablesList()
        {
            List<string> res = new List<string>();
            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = "select * from all_tables";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    var e = from DbDataRecord row in reader
                            where row[row.GetOrdinal("owner")].ToString() != "SYS" && row[row.GetOrdinal("tablespace_name")].ToString() == _tablespace
                            && row[row.GetOrdinal("owner")].ToString() == _username
                            orderby row[row.GetOrdinal("table_name")]
                            select row[row.GetOrdinal("table_name")].ToString();
                    res = e.ToList();
                }
            }
            return res;
        }

        public IDictionary<string, int> GetPrimaryKeys(string tableName)
        {
            if (tableName == "" || tableName == null) return new Dictionary<string, int>();

            #region QUERY
            const string query = @"SELECT cols.column_name, cols.position " + @"FROM all_constraints cons" +
@" INNER JOIN all_cons_columns cols" +
" ON cons.constraint_type = \'P\' AND cons.constraint_name = cols.constraint_name AND cons.owner = cols.owner AND cols.Table_Name = \'{0}\' " +
            "AND cons.owner = \'{1}\'";
            #endregion

            IDictionary<string, int> res = new Dictionary<string, int>();

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = String.Format(query, tableName.ToUpper(), _username.ToUpper());
                    
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader == null) return res;
                    while (reader.Read())
                    {
                        res.Add(reader.GetString(0), reader.GetInt32(1));
                    }
                }
            }

            return res;
        }

        public List<string> GetColumnsList(string tableName)
        {
            if (tableName == "" || tableName == null) return new List<string>();

            List<string> res = new List<string>();
            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = "select * from " + tableName + " where 1 = 2";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string s = reader.GetName(i);
                        res.Add(s);
                    }
                }
            }
            return res;
        }

        public DataTable GetDataTable(string tableName)
        {
            if (tableName == "" || tableName == null)
                return new DataTable();
            return ExecuteCommand("select * from " + tableName);
        }

        public DbColumnReference GetColumnReference(DbColumnReference column)
        {

            #region QUERY
            const string query = "SELECT c.owner, c_pk.table_name r_table_name,  b.column_name r_column_name " +
  "FROM user_cons_columns a JOIN user_constraints c ON a.owner = c.owner AND a.constraint_name = c.constraint_name JOIN user_constraints c_pk ON c.r_owner = c_pk.owner " +
       "AND c.r_constraint_name = c_pk.constraint_name " +
  "JOIN user_cons_columns b ON C_PK.owner = b.owner AND  C_PK.CONSTRAINT_NAME = b.constraint_name " +
            "WHERE c.constraint_type = 'R' AND c.owner = \'{0}\' AND a.table_name = \'{1}\' AND a.column_name = \'{2}\'";
            #endregion

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = String.Format(query, column.Owner.ToUpper(), column.TableName.ToUpper(), column.ColumnName.ToUpper());
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader == null) return null;
                    if (!reader.Read()) return null;

                    DbColumnReference res = new DbColumnReference();
                    res.Owner = reader.GetString(0);
                    res.TableName = reader.GetString(1);
                    res.ColumnName = reader.GetString(2);
                    return res;
                }
            }
        }

        public IDictionary<string, DbColumnReference> GetTableReferences(string tableName)
        {
            if (tableName == "" || tableName == null) return new Dictionary<string, DbColumnReference>();

            #region QUERY
            const string query = "SELECT c.owner, c_pk.table_name r_table_name,  b.column_name r_column_name " +
  "FROM user_cons_columns a JOIN user_constraints c ON a.owner = c.owner AND a.constraint_name = c.constraint_name JOIN user_constraints c_pk ON c.r_owner = c_pk.owner " +
       "AND c.r_constraint_name = c_pk.constraint_name " +
  "JOIN user_cons_columns b ON C_PK.owner = b.owner AND  C_PK.CONSTRAINT_NAME = b.constraint_name " +
            "WHERE c.constraint_type = 'R' AND a.table_name = \'{0}\'";
            #endregion

            IDictionary<string, DbColumnReference> res = new Dictionary<string, DbColumnReference>();

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = String.Format(query, tableName.ToUpper());
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader == null) return null;
                    while (reader.Read())
                    {
                        DbColumnReference cur = new DbColumnReference();
                        cur.Owner = reader.GetString(0);
                        cur.TableName = reader.GetString(1);
                        cur.ColumnName = reader.GetString(2);
                        res[cur.ColumnName] = cur;
                    }
                    return res;
                }
            }

            return res;
        }

        public List<DbColumnInfo> GetColumnsInfo(string tableName)
        {
            if (tableName == "" || tableName == null) return new List<DbColumnInfo>();


            string query = "SELECT COLUMN_NAME, DATA_TYPE, NULLABLE, DATA_DEFAULT FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = \'{0}\' " +
                "AND owner = \'{1}\'";

            List<DbColumnInfo> res = new List<DbColumnInfo>();

            var primaryKeys = GetPrimaryKeys(tableName);

            using (var cmd = _databaseConnection.CreateCommand())
            {
                cmd.CommandText = String.Format(query, tableName.ToUpper(), _username.ToUpper());
                using (DbDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        DbColumnInfo curColumn = new DbColumnInfo();
                        curColumn.Name = reader.GetString(0);
                        curColumn.Type = reader.GetString(1);
                        curColumn.Nullable = (reader.GetString(2) == "Y");
                        curColumn.PrimaryKey = (primaryKeys.ContainsKey(curColumn.Name));

                        var references = GetTableReferences(tableName);

                        curColumn.Reference = (references.ContainsKey(curColumn.Name)) ? (references[curColumn.Name].ToString()) : ("-");
                        res.Add(curColumn);
                    }

                }
            }

            return res;
        }

        public void AddTable(string tableName, IEnumerable<DbColumnInfo> columns)
        {
            #region QUERY
            const string query = "CREATE TABLE {0} ( {1} )";
            #endregion

            StringBuilder inserting = new StringBuilder("");

            List<string> toAdd = new List<string>();

            List<string> primaries = new List<string>();

            foreach (var el in columns)
            {
                toAdd.Add(el.CreateString());
                if (el.PrimaryKey)
                    primaries.Add(el.Name);
            }

            for (int i = 0; i < toAdd.Count; i++)
            {
                inserting.Append(toAdd[i]);
                if (i != toAdd.Count - 1)
                    inserting.Append(", ");
                else
                    inserting.Append(" ");
            }

            if (primaries.Count > 0)
            {
                inserting.Append(", ");
                string s = "";
                for (int i = 0; i < primaries.Count; i++)
                {
                    s += primaries[i];
                    if (i != primaries.Count - 1)
                        s += ", ";
                }
                inserting.Append(string.Format("PRIMARY KEY ({0})", s));
            }

            while (inserting.Length > 0 && inserting[inserting.Length - 1] == ' ')
                inserting.Remove(inserting.Length - 1, 1);

            ExecuteCommand(string.Format(query, tableName, inserting.ToString()));

        }

        public void DropTable(string tableName)
        {
            #region QUERY
            const string query = "DROP TABLE {0}";
            #endregion

            ExecuteCommand(string.Format(query, tableName));
        }

        public void AddColumn(string tableName, DbColumnInfo columnInfo)
        {
            #region QUERY
            const string query = "ALTER TABLE {0} ADD {1}";
            #endregion

            StringBuilder inserting = new StringBuilder("");

            inserting.Append(columnInfo.Name);
            inserting.Append(" ");
            inserting.Append(columnInfo.Type);
            inserting.Append(" ");
            if (!columnInfo.Nullable)
                inserting.Append("NOT NULL ");
            if (columnInfo.PrimaryKey)
                inserting.Append("PRIMARY KEY ");
            if (columnInfo.Reference != null && columnInfo.Reference != "")
            {
                DbColumnReference reference = DbColumnReference.TryParse(columnInfo.Reference);
                inserting.Append(string.Format("REFERENCES {0}({1})", reference.TableName, reference.ColumnName));
                inserting.Append(" ");
            }

            while (inserting.Length > 0 && inserting[inserting.Length - 1] == ' ')
                inserting.Remove(inserting.Length - 1, 1);

            ExecuteCommandReader(string.Format(query, tableName, inserting.ToString()));
        }

        public void DeleteColumn(string tableName, string columnName)
        {
            #region QUERY
            const string query = "ALTER TABLE {0} DROP COLUMN {1}";
            #endregion

            ExecuteCommandReader(string.Format(query, tableName, columnName));
        }

        public void Dispose()
        {
            _databaseConnection.Close();
            _databaseConnection.Dispose();
        }
    }
}
