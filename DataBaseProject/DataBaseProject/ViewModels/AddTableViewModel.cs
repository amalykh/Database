using Caliburn.Micro;
using DataBaseProject.Models;
using DataBaseProject.Views;
using DataBases.DbConnector;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataBaseProject.ViewModels
{
    public class AddTableViewModel : Screen
    {
        private ObservableCollection<DbColumnInfo> _columnsDataCollection;
        private IWindowManager _windowManager;
        private TablesViewModel.EmptyDelegate _updTables;

        [ImportingConstructor]
        public AddTableViewModel(IWindowManager _windowManager, TablesViewModel.EmptyDelegate updTables)
        {
            this._windowManager = _windowManager;
            ColumnsDataCollection = new ObservableCollection<DbColumnInfo>();
            _updTables = updTables;
        }

        public ObservableCollection<DbColumnInfo> ColumnsDataCollection
        {
            get { return _columnsDataCollection; }
            set
            {
                _columnsDataCollection = value;
                NotifyOfPropertyChange(() => ColumnsDataCollection);
            }
        }

        private string _tableName;
        private TablesViewModel.EmptyDelegate updTables;

        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                NotifyOfPropertyChange(() => TableName);
            }
        }

        public void Add()
        {
            try
            {
                DB.connector.AddTable(TableName, _columnsDataCollection);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Eror while creating new table", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _updTables();
            TryClose();
        }

        public void Cancel()
        {
            TryClose();
        }
    }
}
