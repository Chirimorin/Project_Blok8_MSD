using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            if (!String.IsNullOrEmpty(Teacher.Name) &&
                !String.IsNullOrEmpty(Teacher.Email) &&
                !String.IsNullOrEmpty(Teacher.Phone) &&
                !String.IsNullOrEmpty(Teacher.Adress) &&
                !String.IsNullOrEmpty(Teacher.City))
            {
                string query;
                if (Editing)
                {
                    query = "UPDATE docent SET Naam = '" + Teacher.Name + "', Mailadres = '" + Teacher.Email + "', Plaats = '" + Teacher.City + "', Adres = '" + Teacher.Adress + "', Telefoonnr = '" + Teacher.Phone + "', Voorkeur = '" + Teacher.Preference + "', Uren = '" + Teacher.Hours + "' WHERE Docentnr = '" + Teacher.TeacherNo + "';";
                }
                else
                {
                    query = "";
                }
                MySqlCommand mycommand = new MySqlCommand(query);
                ModelFactory.Database.setData(mycommand);
                _app.ShowDocentView();
            }
            else
            {
                string message = "De volgende gegevens zijn niet ingevuld: \n";
                if (String.IsNullOrEmpty(Teacher.Name))
                {
                    message += " - Naam \n";
                }
                if (String.IsNullOrEmpty(Teacher.Email))
                {
                    message += " - Email \n";
                }
                if (String.IsNullOrEmpty(Teacher.Phone))
                {
                    message += " - Telefoonnummer \n";
                }
                if (String.IsNullOrEmpty(Teacher.Adress))
                {
                    message += " - Adres \n";
                }
                if (String.IsNullOrEmpty(Teacher.Phone))
                {
                    message += " - Woonplaats \n";
                }

                MessageBox.Show(message);
            }
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
                OnPropertyChanged("Title");
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

        public int Hours
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
