using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            fillEducation();
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
        public void fillEducation()
        {
            MySqlCommand cmd;
            DataTable table;
            MySqlDataAdapter adapter;
            cmd = new MySqlCommand("SELECT * FROM opleiding");
            table = new DataTable();
            adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            _education = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Education[RowNr] = table.Rows[RowNr][1].ToString();
            }

            cmd = new MySqlCommand("SELECT academie_afkorting FROM opleiding GROUP BY academie_afkorting");
            table = new DataTable();
            adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            _academie = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Academie[RowNr] = table.Rows[RowNr][0].ToString();
            }
        }
        public string SelectedAcademie
        {
            get
            {
                if (Teacher.Academie == null) return "";
                return Teacher.Academie;
            }
            set
            {
                Teacher.Academie = value;
                OnPropertyChanged("Academie");
            }

        }
        public string[] _academie;
        public string[] Academie
        {
            get { return _academie; }
            set
            {
                _academie = value;
                OnPropertyChanged("Academie");
            }
        }
        public string SelectedEducation
        {
            get
            {
                if (Teacher.Education == null) return "";
                return Teacher.Education;
            }
            set
            {
                Teacher.Education = value;
                OnPropertyChanged("Education");
            }

        }
        private string[] _education;
        public string[] Education
        {
            get { return _education; }
            set
            {
                _education = value;
                OnPropertyChanged("Education");
            }
        }

        public Teacher Teacher
        {
            get { return _teacher; }
            set { _teacher = value;}
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
