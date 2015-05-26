using Caliburn.Micro;
using DataBaseProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DataBaseProject.ViewModels
{
    public class CreateColumnViewModel : Screen
    {
        private DbColumnInfo _columnInfo;
        private IWindowManager _windowManager;
        private TablesViewModel.TableNameDelegate _updateDelegate;
        private string _tableName;

        public string ColumnName
        {
            get { return _columnInfo.Name; }
            set
            {
                _columnInfo.Name = value;
                NotifyOfPropertyChange(() => ColumnName);
            }
        }

        public string ColumnType
        {
            get { return _columnInfo.Type; }
            set
            {
                _columnInfo.Type = value;
                NotifyOfPropertyChange(() => ColumnType);
            }
        }

        public bool Nullable
        {
            get { return _columnInfo.Nullable; }
            set
            {
                _columnInfo.Nullable = value;
                NotifyOfPropertyChange(() => Nullable);
            }
        }

        public bool PrimaryKey
        {
            get { return _columnInfo.PrimaryKey; }
            set
            {
                _columnInfo.PrimaryKey = value;
                NotifyOfPropertyChange(() => PrimaryKey);
            }
        }
        
        public string Reference
        {
            get { return _columnInfo.Reference; }
            set
            {
                _columnInfo.Reference = value;
                NotifyOfPropertyChange(() => Reference);
            }
        }

        [ImportingConstructor]
        public CreateColumnViewModel(IWindowManager windowManager, string tableName, TablesViewModel.TableNameDelegate updateDelegate)
        {
            _windowManager = windowManager;
            _columnInfo = new DbColumnInfo();
            _updateDelegate = updateDelegate;
            _tableName = tableName;
        }

        public void Insert()
        {
            try
            {
                DB.connector.AddColumn(_tableName, _columnInfo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error while adding/creating column", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _updateDelegate(_tableName);
            TryClose();
        }

        public void Cancel()
        {
            TryClose();
        }
    }
}
