﻿using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    public class DocentViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _NieuweDocentCommand;
        private readonly RelayCommand _DocentAanpassenCommand;

        public DocentViewModel(IApplicationController app)
        {
            _app = app;
            _NieuweDocentCommand = new RelayCommand(NieuweDocent);
            _DocentAanpassenCommand = new RelayCommand(DocentAanpassen);
        }

        public RelayCommand NieuweDocentCommand { get { return _NieuweDocentCommand; } }
        public void NieuweDocent(object command)
        {
            DocentPersoonViewModel vm = (DocentPersoonViewModel)ViewFactory.getViewModel(_app, "docentPersoonViewModel");
            DocentKwalificatieViewModel vm2 = (DocentKwalificatieViewModel)ViewFactory.getViewModel(_app, "docentKwalificatieViewModel");

            vm.Title = "Nieuwe Docent";
            vm2.Title = "Nieuwe Docent";

            vm.Adress = "";
            vm.Birthday = new DateTime(1990, 1, 1);
            vm.City = "";
            vm.Email = "";
            vm.FirstName = "";
            vm.LastName = "";
            vm.Phone = "";

            vm2.Hours = "";
            vm2.Preference = "";

            _app.ShowDocentPersoonView();
        }

        public RelayCommand DocentAanpassenCommand { get { return _DocentAanpassenCommand; } }
        public void DocentAanpassen(object command)
        {
            DocentPersoonViewModel vm = (DocentPersoonViewModel)ViewFactory.getViewModel(_app, "docentPersoonViewModel");
            DocentKwalificatieViewModel vm2 = (DocentKwalificatieViewModel)ViewFactory.getViewModel(_app, "docentKwalificatieViewModel");

            vm.Title = "Docent Aanpassen";
            vm2.Title = "Docent Aanpassen";

            vm.Adress = "";
            vm.Birthday = new DateTime(1990, 1, 1);
            vm.City = "";
            vm.Email = "";
            vm.FirstName = "";
            vm.LastName = "";
            vm.Phone = "";

            vm2.Hours = "";
            vm2.Preference = "";

            _app.ShowDocentPersoonView();
        }

        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string _adress;
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                OnPropertyChanged("Adress");
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
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

    }
}
