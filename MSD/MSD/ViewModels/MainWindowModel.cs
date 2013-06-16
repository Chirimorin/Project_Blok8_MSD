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
        private readonly RelayCommand _ShowAfstuderenCommand;
        private readonly RelayCommand _ShowGebruikerCommand;
        private readonly RelayCommand _LogoutCommand;
        private string _username;
        private string _display;
        private PropertyChangedBase _contents;

        public MainWindowModel(IApplicationController app)
        {
            _app = app;
            _ShowBedrijfCommand = new RelayCommand(ShowBedrijf);
            _ShowDocentCommand = new RelayCommand(ShowDocent);
            _ShowStudentCommand = new RelayCommand(ShowStudent);
            _ShowGebruikerCommand = new RelayCommand(ShowGebruiker);
            _ShowMatchCommand = new RelayCommand(ShowMatchInvoer);
            _ShowStageCommand = new RelayCommand(ShowStage);
            _ShowAfstuderenCommand = new RelayCommand(ShowAfstuderen);
            _LogoutCommand = new RelayCommand(Logout);
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

        public RelayCommand ShowAfstuderenCommand { get { return _ShowAfstuderenCommand; } }
        public void ShowAfstuderen(object command)
        {
            _app.ShowAfstuderenView();
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
            loginViewModel.Email = "";
            _app.ShowLoginView();
        }

        //username komt uit de database
        public string UserName
        {
            get { return _username; }            
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

        public string Display
        {
            get { return _display; }
            set { 
                _display = value;
                OnPropertyChanged("Display");
            }
        }

    }
}
