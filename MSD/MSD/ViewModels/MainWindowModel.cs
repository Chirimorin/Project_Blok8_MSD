using MSD.Controllers;
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
        private readonly RelayCommand _ShowDocentCommand;
        private readonly RelayCommand _ShowStudentCommand;
        private readonly RelayCommand _ShowMatchCommand;
        private readonly RelayCommand _ShowStageCommand;
        private readonly RelayCommand _ShowGebruikerCommand;

        private PropertyChangedBase contents;

        public MainWindowModel(IApplicationController app)
        {
            _app = app;
            this._ShowDocentCommand = new RelayCommand(ShowDocent);
            this._ShowStudentCommand = new RelayCommand(ShowStudent);
            this._ShowGebruikerCommand = new RelayCommand(ShowGebruiker);
            this._ShowMatchCommand = new RelayCommand(ShowMatchInvoer);
            this._ShowStageCommand = new RelayCommand(ShowStage);
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

        //username komt uit de database
        public string UserName
        {
            get { return ""; }
        }

        public PropertyChangedBase Contents
        {
            get { return contents; }
            set
            {
                contents = value;
                OnPropertyChanged("Contents");
            }
        }

    }
}
