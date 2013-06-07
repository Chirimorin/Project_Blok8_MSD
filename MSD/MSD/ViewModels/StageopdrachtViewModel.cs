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
    class StageopdrachtViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;
        private Assignment _assignment;
        private Student _student;
        private Database _database;
        private int _stagenr;

        public StageopdrachtViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Save);
            _terugCommand = new RelayCommand(Back);
            _database = ModelFactory.Database;
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentPersoonView();
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Save(object command)
        {
            string studentquery = "INSERT INTO student (studentnr, naam, mailadres, telefoonnr) VALUES ("+ Student.StudentNo +",'"+ Student.Name+"','"+ Student.Email + "','"+ Student.Phone + "');";
            executeQuery(studentquery);
            //string opdrachtquery = "INSERT INTO stageopdracht () VALUES ()";
            //executeQuery(opdrachtquery);
            //string studentopdrachtquery = "INSERT INTO stageopdracht_has_student VALUES (" + _stagenr + "," + Student.StudentNo + ")";
            //executeQuery(studentopdrachtquery);
            _app.ShowStudentView();
        }
        public void executeQuery(string query)
        {
            MySqlCommand mycommand;
            /*if (_stagenr == null)
            {
                mycommand = new MySqlCommand("SELECT MAX(stagenr) FROM stageopdracht;");
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable data = new DataTable();
                adapter = _database.getData(mycommand);
                adapter.Fill(data);
                _stagenr = (int)data.Rows[0][0];
            }*/
            mycommand = new MySqlCommand(query);
            _database.setData(mycommand);
        }

        public Assignment Assignment
        {
            get { return _assignment; }
            set
            {
                
                _assignment = value;
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

        public string Description
        {
            get
            {
                if(Assignment != null) 
                    return Assignment.Description;
                return "";
            }
            set
            {
                Assignment.Description = value;
            }
        }
        public string Name
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Name;
                return "";
            }
            set
            {
                Assignment.Name = value;
            }
        }

        public string Comments
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Comments;
                return "";
            }
            set
            {
                Assignment.Comments = value;
            }
        }
        public Company Company
        {
            get
            {
                return Assignment.Company;
            }
            set
            {
                Assignment.Company = value;
            }
        }
        public string Period
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Period;
                return "";
            }
            set
            {
                Assignment.Period = value;
            }
        }

        public bool Accepted
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Accepted;
                return false;
            }
            set
            {
                Assignment.Accepted = value;
            }
        }

        public bool TempPermission
        {
            get
            {
                if (Assignment != null)
                    return Assignment.TempPermission;
                return false;
            }
            set
            {
                Assignment.TempPermission = value;
            }
        }

        public bool Permission
        {
            get
            {
                if (Assignment != null)
                    return Assignment.Permission;
                return false;
            }
            set
            {
                Assignment.Permission = value;
            }
        }
        public string KnowLedgeItem
        {
            get
            {
                return Assignment.KnowLedgeItem;
            }
            set
            {
                Assignment.KnowLedgeItem = value;
                Assignment.Knowledge.Add(Assignment.KnowLedgeItem);
            }
        }

    }
}
