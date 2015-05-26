using DataBaseProject.DBConnector;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBases.DbConnector
{
    public interface IDbConnector : IDisposable
    {
        DataTable ExecuteCommand(string command);
        DbDataReader ExecuteCommandReader(string command);
        List<string> GetTablesList();
        IDictionary<string, int> GetPrimaryKeys(string tableName);
        List<string> GetColumnsList(string tableName);
        DataTable GetDataTable(string tableName);
        void InsertRow(string tableName, List<DbColumnNamesModel> l);
        void DeleteRow(string tableName, object[] itemArray);

        List<DbColumnInfo> GetColumnsInfo(String tableName);
        DbColumnReference GetColumnReference(DbColumnReference column);
        void AddTable(string tableName, IEnumerable<DbColumnInfo> columns);
        void DropTable(string tableName);
        void AddColumn(string tableName, DbColumnInfo columnInfo);
        void DeleteColumn(string tableName, string columnName);
    }
}
