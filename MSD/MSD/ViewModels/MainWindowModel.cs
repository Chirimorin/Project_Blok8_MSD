using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MainWindowModel : PropertyChangedBase
    {

        private readonly IApplicationController _app;
        private readonly RelayCommand _ShowBedrijfCommand;
        private readonly RelayCommand _ShowDocentCommand;
        private readonly RelayCommand _ShowStudentCommand;
        private readonly RelayCommand _ShowMatchCommand;
        private readonly RelayCommand _ShowStageCommand;
        private readonly RelayCommand _ShowGebruikerCommand;
        private readonly RelayCommand _LogoutCommand;

        private string _username;

        private PropertyChangedBase _contents;

        public MainWindowModel(IApplicationController app)
        {
            _app = app;
            this._ShowBedrijfCommand = new RelayCommand(ShowBedrijf);
            this._ShowDocentCommand = new RelayCommand(ShowDocent);
            this._ShowStudentCommand = new RelayCommand(ShowStudent);
            this._ShowGebruikerCommand = new RelayCommand(ShowGebruiker);
            this._ShowMatchCommand = new RelayCommand(ShowMatchInvoer);
            this._ShowStageCommand = new RelayCommand(ShowStage);
            this._LogoutCommand = new RelayCommand(Logout);
        }

        public RelayCommand ShowBedrijfCommand { get { return _ShowBedrijfCommand; } }
        public void ShowBedrijf(object command)
        {
            _app.ShowBedrijfOverzichtView();
        }

        public RelayCommand ShowDocentCommand { get { return _ShowDocentCommand; } }
        public void ShowDocent(object command)
        {
            _app.ShowDocentView();
        }

        public RelayCommand ShowStudentCommand { get { return _ShowStudentCommand; } }
        public void ShowStudent(object command)
        {
            _app.ShowStudentView();
        }

        public RelayCommand ShowStageCommand { get { return _ShowStageCommand; } }
        public void ShowStage(object command)
        {
            _app.ShowStageView();
        }

        public RelayCommand ShowMatchCommand { get { return _ShowMatchCommand; } }
        public void ShowMatchInvoer(object command)
        {
            _app.ShowMatchInvoerView();
        }

        public RelayCommand ShowGebruikerCommand { get { return _ShowGebruikerCommand; } }
        public void ShowGebruiker(object command)
        {
            _app.ShowGebruikerView();
        }

        public RelayCommand LogoutCommand { get { return _LogoutCommand; } }
        public void Logout(object command)
        {
            LogInViewModel loginViewModel = (LogInViewModel)ViewFactory.getViewModel(_app, "logInViewModel");
            loginViewModel.Name = "";
            _app.ShowLoginView();
        }

        //username komt uit de database
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("UserName");
            }
        }

        public PropertyChangedBase Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
                OnPropertyChanged("Contents");
            }
        }

    }
}
