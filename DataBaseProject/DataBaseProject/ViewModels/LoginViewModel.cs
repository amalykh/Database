using Caliburn.Micro;
using DataBaseProject.Models;
using DataBaseProject.Views;
using DataBases.DbConnector;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataBaseProject.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : Screen
    {
        private LoginInfoModel _loginInfo;
        private object _view;

        private const string oracleConnectionErrorTitle = "Cannot connect to the database";

        private readonly IWindowManager _windowManager;
 
        [ImportingConstructor]
        public LoginViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _loginInfo = new LoginInfoModel();
            _loginInfo.Username = "12201";
            IP = "10.4.0.119";
            Tablespace = "USERS";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _view = view;
        }

        public string Username
        {
            get { return _loginInfo.Username; }
            set
            {
                _loginInfo.Username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        public string IP
        {
            get { return _loginInfo.IP; }
            set
            {
                _loginInfo.IP = value;
                NotifyOfPropertyChange(() => IP);
            }
        }

        public string Tablespace
        {
            get { return _loginInfo.Tablespace; }
            set
            {
                _loginInfo.Tablespace = value;
                NotifyOfPropertyChange(() => Tablespace);
            }
        }

        public void Login()
        {
            string password = ((LoginView)_view).textPassword.Password;

            try
            {
                DB.connector = new OracleDbConnector(IP, _loginInfo.Username, password, Tablespace);
            }
            catch (OracleException e)
            {
                MessageBox.Show(e.Message, oracleConnectionErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _windowManager.ShowWindow(new MainControlViewModel(_windowManager));

            TryClose();
        }
    }
}
