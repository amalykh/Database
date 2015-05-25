using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DataBaseProject.Models;
using DataBaseProject.Views;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Data.Common;

namespace DataBaseProject.ViewModels
{
    public class RunCommandViewModel : PropertyChangedBase
    {
        private const string oracleRunErrorTitle = "Error while trying to run";
        private string _command;
        public string Command
        {
            get { return _command; }
            set
            {
                _command = value;
                NotifyOfPropertyChange(() => Command);
            }
        }

        private DbDataReader _reader;
        public DbDataReader QueryResultReader
        {
            get { return _reader; }
            set
            {
                _reader = value;
                NotifyOfPropertyChange(() => QueryResultReader);
            }
        }

        public void Run()
        {
            try
            {
                QueryResultReader = DB.connector.ExecuteCommandReader(Command);
            }
            catch (OracleException e)
            {
                MessageBox.Show(e.Message, oracleRunErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message, oracleRunErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
