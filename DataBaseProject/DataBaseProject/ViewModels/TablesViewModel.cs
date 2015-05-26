using Caliburn.Micro;
using DataBaseProject.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBaseProject.ViewModels
{
    public class TablesViewModel : PropertyChangedBase
    {
        private int _selectedTableIndex;
        private int _selectedDataTableIndex;
        private readonly IWindowManager _windowManager;
        private ObservableCollection<string> _tablesList;
        private ObservableCollection<DbColumnInfo> _columnsInfo;
        private DataTable _dataTable;
        private const string oracleDeleteError = "Cannot delete";

        public delegate void EmptyDelegate();
        public delegate void TableNameDelegate(string tableName);

        [ImportingConstructor]
        public TablesViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            _tablesList = new ObservableCollection<string>();
            _columnsInfo = new ObservableCollection<DbColumnInfo>();

            UpdateTablesList();
        }

        public void UpdateTablesList()
        {
            List<string> tablesList = DB.connector.GetTablesList();

            Tables.Clear();
            foreach (var el in tablesList)
            {
                Tables.Add(el);
            }
        }

        public DataTable TableDataTable
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                NotifyOfPropertyChange(() => TableDataTable);
            }
        }

        public string SelectedTableName
        {
            get
            {
                try
                {
                    return _tablesList[_selectedTableIndex];
                }
                catch(Exception e)
                {
                    return null;
                }
            }
        }

        public int SelectedTableIndex
        {
            get { return _selectedTableIndex; }
            set
            {
                if (_selectedTableIndex == value) return;
                _selectedTableIndex = value;

                #region tablestabl
                Columns.Clear();
                List<DbColumnInfo> l = DB.connector.GetColumnsInfo(SelectedTableName);
                foreach(var el in l)
                {
                    Columns.Add(el);
                }
                #endregion

                #region datatab

                FillSelectedTableRows(SelectedTableName);

                #endregion

                NotifyOfPropertyChange(() => _selectedTableIndex);
            }
        }

        public int SelectedDataTableIndex
        {
            get { return _selectedDataTableIndex; }
            set
            {
                _selectedDataTableIndex = value;
                NotifyOfPropertyChange(() => SelectedDataTableIndex);
            }
        }

        private void FillSelectedTableRows(string selectedTableName)
        {
            TableDataTable = DB.connector.ExecuteCommand("select * from " + SelectedTableName);
        }

        public void InsertRow()
        {
            TableNameDelegate updData = FillSelectedTableRows;

            _windowManager.ShowWindow(new CreateRowViewModel(_windowManager, SelectedTableName, updData));
        }

        public void DeleteRow()
        {

            try
            {
                DataRow row = TableDataTable.Rows[SelectedDataTableIndex];
                DB.connector.DeleteRow(SelectedTableName, row.ItemArray);
            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("You need to select row to delete", oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (OracleException e)
            {
                MessageBox.Show(e.Message, oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("You need to select row to delete", oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            FillSelectedTableRows(SelectedTableName);
        }

        public void EditRow()
        {
            try
            {
                DataRow row = TableDataTable.Rows[SelectedDataTableIndex];
                DB.connector.DeleteRow(SelectedTableName, row.ItemArray);
                _windowManager.ShowWindow(new CreateRowViewModel(_windowManager, SelectedTableName, FillSelectedTableRows, row.ItemArray));

            }
            catch (IndexOutOfRangeException e)
            {
                MessageBox.Show("You need to select row to edit", oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (OracleException e)
            {
                MessageBox.Show(e.Message, oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show("You need to select row to edit", oracleDeleteError, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public void InsertColumn()
        {

        }

        public void DeleteColumn()
        {

        }

        public void EditColumn()
        {

        }



        public void AddTable()
        {
            EmptyDelegate updTables = UpdateTablesList;

            _windowManager.ShowWindow(new AddTableViewModel(_windowManager, updTables));
        }

        public void DropTable()
        {
            DB.connector.DropTable(SelectedTableName);
            SelectedTableIndex--;
            UpdateTablesList();
        }

        public ObservableCollection<string> Tables { get { return _tablesList; } }
        public ObservableCollection<DbColumnInfo> Columns { get { return _columnsInfo; } }
    }
}
