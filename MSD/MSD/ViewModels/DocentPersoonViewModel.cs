using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class DocentPersoonViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _verderCommand;
        private readonly RelayCommand _terugCommand;

        private bool _editing;
        private Teacher _teacher;


        public DocentPersoonViewModel(IApplicationController app)
        {
            _app = app;
            _verderCommand = new RelayCommand(Verder);
            _terugCommand = new RelayCommand(Terug);
        }

        public RelayCommand VerderCommand { get { return _verderCommand; } }
        public void Verder(object command)
        {
            _app.ShowDocentKwalificatieView();
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowDocentView();
        }

        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                OnPropertyChanged(Title);
            }
        }

        public string Title
        {
            get
            {
                if (Editing) return "Docent Aanpassen";
                else return "Nieuwe Docent";
            }
        }

        public Teacher Teacher
        {
            get { return _teacher; }
            set { _teacher = value; }
        }

        public string Name
        {
            get { return Teacher.Name; }
            set
            {
                Teacher.Name = value;
                OnPropertyChanged("FirstName");
            }
        }

        public DateTime Birthday
        {
            get { return Teacher.Birthday; }
            set
            {
                Teacher.Birthday = value;
                OnPropertyChanged("Birthday");
            }
        }

        public string Email
        {
            get { return Teacher.Email; }
            set
            {
                Teacher.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get { return Teacher.Phone; }
            set
            {
                Teacher.Phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string Adress
        {
            get { return Teacher.Adress; }
            set
            {
                Teacher.Adress = value;
                OnPropertyChanged("Adress");
            }
        }

        public string City
        {
            get { return Teacher.City; }
            set
            {
                Teacher.City = value;
                OnPropertyChanged("City");
            }
        }

    }
}
