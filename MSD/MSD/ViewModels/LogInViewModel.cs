using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MSD.ViewModels
{
    class LogInViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        //private MainWindow mainwindow = new MainWindow();
        private string _errorMessage;
        private readonly RelayCommand _ShowMainWindow;
        private readonly RelayCommand _WachtwoordVergeten;

        private string _name;
        private string _password;

        public LogInViewModel(IApplicationController app)
        {
            _app = app;
            this._ShowMainWindow = new RelayCommand(ShowMainWindow);
            this._WachtwoordVergeten = new RelayCommand(ShowWachtwoordView);
        }
        public string Message
        {
            get { return _errorMessage; }
            private set { _errorMessage = value; }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public RelayCommand ShowMainWindowCommand { get { return _ShowMainWindow; } }

        public void ShowMainWindow(object command)
        {
            MainWindowModel mainWindowModel = (MainWindowModel)ViewFactory.getViewModel(_app, "mainWindowModel");
            mainWindowModel.UserName = Name;
            _app.ShowMainWindow();
        }

        public RelayCommand ShowWachtwoordVergeten { get { return _WachtwoordVergeten; } }

        public void ShowWachtwoordView(object command)
        {
            WachtwoordViewModel wachtwoordview = (WachtwoordViewModel)ViewFactory.getViewModel(_app, "wachtwoordViewModel");
            _app.ShowWachtwoordView();
        }
    }
}
