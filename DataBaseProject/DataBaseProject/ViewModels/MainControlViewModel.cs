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

namespace DataBaseProject.ViewModels
{
    public class MainControlViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private object _view;

        public RunCommandViewModel RunControl
        {
            get;
            set;
        }

        public TablesViewModel TablesControl
        {
            get;
            set;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _view = view;
        }
 
        [ImportingConstructor]
        public MainControlViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;

            RunControl = new RunCommandViewModel();
            TablesControl = new TablesViewModel(windowManager);
        }

        public void LogOut()
        {
            DB.connector.Dispose();

            _windowManager.ShowWindow(new LoginViewModel(_windowManager));

            TryClose();
        }

    }
}
