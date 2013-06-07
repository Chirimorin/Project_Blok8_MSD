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

        public StageopdrachtViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Save);
            _terugCommand = new RelayCommand(Back);
            _database = ModelFactory.Database;
            fillBox();
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentPersoonView();
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Save(object command)
        {
            int stagenr = getexecuteQuery("SELECT MAX(stagenr) FROM stageopdracht;");
            int afkorting = getexecuteQuery("SELECT afkorting FROM opleiding WHERE omschrijving = '" + Student.Education + "';");
            int bedrijf = getexecuteQuery("SELECT bedrijfnr FROM stagebedrijf WHERE naam = '" + Assignment.Company + "';");
            string studentquery = "INSERT INTO student VALUES (" + Student.StudentNo + ",'" + Student.Name + "','" + Student.Email + "','" + Student.Phone + "'," + afkorting + ",'" + Student.Academie + "');";
            executeQuery(studentquery);
            string opdrachtquery = "INSERT INTO stageopdracht () VALUES (" + stagenr + ",'" + Assignment.Name + "','" + Assignment.Description + "','" + Assignment.Comments + "'," + Assignment.Accepted + "," + Assignment.TempPermission + "," + Assignment.Permission + ","+ bedrijf +",'"+Assignment.Period+"')";
            executeQuery(opdrachtquery);
            string studentopdrachtquery = "INSERT INTO stageopdracht_has_student VALUES (" + stagenr + "," + Student.StudentNo + ")";
            executeQuery(studentopdrachtquery);
            _app.ShowStudentView();

        }
        public int getexecuteQuery(string query)
        {
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = _database.getData(mycommand);
            adapter.Fill(data);
            return (int)data.Rows[0][0];
        }
        public void executeQuery(string query)
        {
            MySqlCommand mycommand = new MySqlCommand(query);
            mycommand = new MySqlCommand(query);
            _database.setData(mycommand);
        }
        public void fillBox()
        {
            MySqlCommand cmd;
            DataTable table;
            MySqlDataAdapter adapter;
            cmd = new MySqlCommand("SELECT naam FROM stagebedrijf");
            table = new DataTable();
            adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            _company = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Company[RowNr] = table.Rows[RowNr][0].ToString();
            }
            cmd = new MySqlCommand("SELECT periodenaam FROM periode");
            table = new DataTable();
            adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            _period = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Period[RowNr] = table.Rows[RowNr][0].ToString();
            }
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
        public string SelectedCompany
        {
            get
            {
                if (Assignment.Company == null) return "";
                return Assignment.Company;
            }
            set
            {
                Assignment.Company = value;
                OnPropertyChanged("Company");
            }

        }
        public string[] _company;
        public string[] Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }
        public string SelectedPeriod
        {
            get
            {
                if (Assignment.Period == null) return "";
                return Assignment.Period;
            }
            set
            {
                Assignment.Period = value;
                OnPropertyChanged("Period");
            }

        }
        private string[] _period;
        public string[] Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
                OnPropertyChanged("Period");
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
