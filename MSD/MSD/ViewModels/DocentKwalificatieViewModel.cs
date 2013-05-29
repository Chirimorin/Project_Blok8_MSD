﻿using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class DocentKwalificatieViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;

        public DocentKwalificatieViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
            _terugCommand = new RelayCommand(Terug);
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Opslaan(object command)
        {
            _app.ShowDocentView();
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowDocentPersoonView();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(Title);
            }
        }

        private string _hours;
        public string Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
                OnPropertyChanged("Hours");
            }
        }

        private string _preference;
        public string Preference
        {
            get
            {
                return _preference;
            }
            set
            {
                _preference = value;
                OnPropertyChanged("Preference");
            }
        }
    }
}
