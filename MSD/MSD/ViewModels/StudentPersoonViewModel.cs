using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StudentPersoonViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _verderCommand;
        private readonly RelayCommand _terugCommand;
        private Student _student;
        private string[] _education;

        public StudentPersoonViewModel(IApplicationController app)
        {
            _app = app;
            _verderCommand = new RelayCommand(Verder);
            _terugCommand = new RelayCommand(Back);
            fillEducation();
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentView();
        }

        public RelayCommand VerderCommand { get { return _verderCommand; } }
        public void Verder(object command)
        {
            
            
            _app.ShowStageopdrachtView();
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
                Education[RowNr] =  table.Rows[RowNr][1].ToString();
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
            get { 
                if (Student.Academie == null) return "";
                return Student.Academie;
            }
            set { Student.Academie = value;
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
            get { 
                if (Student.Education == null) return "";
                return Student.Education;
            }
            set { Student.Education = value;
            OnPropertyChanged("Education");
            }
           
        }
        public string[] Education
        {
            get { return _education; }
            set
            {
                _education = value;
                OnPropertyChanged("Education");
            }
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
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
            }
        }

        public string StudentNo
        {
            get { 
                if (Student.StudentNo == null) return "";
                return Student.StudentNo;
            }
            set { Student.StudentNo = value;
            OnPropertyChanged("StudentNo");
            }

        }

        public string Name
        {
            get
            {
                if (Student.Name == null) return "";
                return Student.Name; }
            set { Student.Name = value;
            OnPropertyChanged("Name");
            }
        }

        public string Email
        {
            get
            {
                if (Student.Email == null) return "";
                return Student.Email; }
            set { Student.Email = value;
            OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get
            {
                if (Student.Phone == null) return "";
                return Student.Phone; }
            set { Student.Phone = value;
            OnPropertyChanged("Phone");
            }
        }
    }
}
