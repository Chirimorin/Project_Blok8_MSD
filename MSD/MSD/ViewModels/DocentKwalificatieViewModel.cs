using MSD.Controllers;
using MSD.Entity;
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

        private bool _editing;
        private Teacher _teacher;

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

        public string Hours
        {
            get { return Teacher.Hours; }
            set
            {
                Teacher.Hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public string Preference
        {
            get { return Teacher.Preference; }
            set
            {
                Teacher.Preference = value;
                OnPropertyChanged("Preference");
            }
        }
    }
}
