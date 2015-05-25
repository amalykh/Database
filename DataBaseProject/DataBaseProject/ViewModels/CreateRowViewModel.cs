using Caliburn.Micro;
using DataBaseProject.Models;
using DataBaseProject.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DataBaseProject.ViewModels
{
    class CreateRowViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private object _view;
        private ObservableCollection<DbColumnNamesModel> _columnNamesCollection;
        private string _tableName;

        public ObservableCollection<DbColumnNamesModel> ColumnNamesCollection
        {
            get { return _columnNamesCollection; }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _view = view;

            DataGrid dg = (((CreateRowView)_view).ColumnNamesDataGrid);

            dg.CanUserSortColumns = false;
            dg.CanUserAddRows = false;

            if (dg.Columns.Count > 0)
            {
                dg.Columns[0].IsReadOnly = true;
            }
            if (dg.Columns.Count > 1)
                dg.Columns[1].MinWidth = 300;

            
        }
        
        [ImportingConstructor]
        public CreateRowViewModel(IWindowManager windowManager, string tableName, object[] itemValues = null)
        {
            _windowManager = windowManager;
            _columnNamesCollection = new ObservableCollection<DbColumnNamesModel>();

            List<string> l = DB.connector.GetColumnsList(tableName);
            for (int i = 0; i < l.Count; i++)
            {
                DbColumnNamesModel cur = new DbColumnNamesModel(l[i]);
                if (itemValues != null)
                    cur.Value = itemValues[i].ToString();
                _columnNamesCollection.Add(cur);
            }
            _tableName = tableName;
        }

        public void Insert()
        {

            DataGrid dg = (((CreateRowView)_view).ColumnNamesDataGrid);

            DB.connector.InsertRow(_tableName, ColumnNamesCollection.ToList());

            TryClose();
        }

        public void Cancel()
        {
            TryClose();
        }

    }
}
