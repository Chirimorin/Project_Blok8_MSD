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
using System.Windows;

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
        private bool _wijzig;
        private int _stagenr;


        public bool Wijzig
        {
            set { _wijzig = value; }
        }
        

        public StageopdrachtViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Save);
            _terugCommand = new RelayCommand(Back);
            _database = ModelFactory.Database;
            fillBox();
            FillKnowledgeAreas();
            
            if (_wijzig == false)
            {
                _stagenr = getexecuteQuery("SELECT MAX(stagenr) FROM stageopdracht;") + 1;
            }
            if (_wijzig == true)
            {
                _stagenr = getexecuteQuery("SELECT stageopdracht_stagenr FROM stageopdracht_has_student WHERE student_studentnr = " + Student.StudentNo);
               
            }
            UpdateKnowledgeAreas();
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentPersoonView();
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Save(object command)
        {
            if (Assignment.Company == null)
            {
                MessageBox.Show("u heeft het bedrijf niet ingevult");
            }

            int afkorting = getexecuteQuery("SELECT afkorting FROM opleiding WHERE omschrijving = '" + Student.Education + "';");
            int bedrijf = getexecuteQuery("SELECT bedrijfnr FROM stagebedrijf WHERE naam = '" + Assignment.Company + "';");

            if (_wijzig == false)
            {
                
                string studentquery = "INSERT INTO student VALUES(" + Student.StudentNo + ",'" + Student.Name + "','" + Student.Email + "','" + Student.Phone + "'," + afkorting + ",'" + Student.Academie + "');";

                executeQuery(studentquery);
                string opdrachtquery = "INSERT INTO stageopdracht () VALUES (" + _stagenr + ",'" + Assignment.Name + "','" + Assignment.Description + "','" + Assignment.Comments + "'," + Assignment.Accepted + "," + Assignment.TempPermission + "," + Assignment.Permission + "," + bedrijf + ",'" + Assignment.Period + "')";
                executeQuery(opdrachtquery);
                string studentopdrachtquery = "INSERT INTO stageopdracht_has_student VALUES (" + _stagenr + "," + Student.StudentNo + ")";
                executeQuery(studentopdrachtquery);
            }
            if (_wijzig == true)
            {
                string studentquery = "UPDATE student SET naam = '" + Student.Name + "', mailadres = '" + Student.Email + "', telefoonnr = " + Student.Phone + ", opleiding_afkorting = " + afkorting + ", opleiding_academie_afkorting = '" + Student.Academie + "' WHERE studentnr = " + Student.StudentNo;
                executeQuery(studentquery);
                string opdrachtquery = "UPDATE stageopdracht SET stagenr=" + _stagenr + ", opdrachtnaam ='" + Assignment.Name + "', omschrijving ='" + Assignment.Description + "', opmerking ='" + Assignment.Comments + "', opdrachtgoed = " + Assignment.Accepted + ", toestemmingvoorlopig =" + Assignment.TempPermission + ", toestemmingdefinitief =" + Assignment.Permission + ", stagebedrijf_bedrijfnr = " + bedrijf + ", periode_periodenaam = '" + Assignment.Period + "' WHERE stagenr = " + _stagenr;
                executeQuery(opdrachtquery);
            }
            _app.ShowStudentView();

        }
        public int getexecuteQuery(string query)
        {
            Debug.WriteLine(query);
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = _database.getData(mycommand);
            adapter.Fill(data);
            return (int)data.Rows[0][0];
        }
        public void executeQuery(string query)
        {
            Debug.WriteLine(query);
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
        public void FillKnowledgeAreas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            KnowledgeAreas = new string[table.Rows.Count];

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                KnowledgeAreas[RowNr] = table.Rows[RowNr][1].ToString();
            }
        }
        private void UpdateKnowledgeAreas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden k JOIN kennisgebieden_has_stageopdracht ks ON ks.kennisgebieden_kennisnr = k.kennisnr WHERE stageopdracht_stagenr = " + _stagenr);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            int NumRows = table.Rows.Count;

            if (NumRows >= 1)
                SelectedArea1 = table.Rows[0][1].ToString();
            if (NumRows >= 2)
                SelectedArea2 = table.Rows[1][1].ToString();
            if (NumRows >= 3)
                SelectedArea3 = table.Rows[2][1].ToString();
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
        public string SelectedArea1
        {
            get
            {
                if (Assignment.Knowledge == null) return "";
                return Assignment.Knowledge[0];
            }
            set
            {
                Assignment.Knowledge[0] = value;
                OnPropertyChanged("SelectedArea1");
            }

        }
        public string SelectedArea2
        {
            get
            {
                if (Assignment.Knowledge == null) return "";
                return Assignment.Knowledge[1];
            }
            set
            {
                Assignment.Knowledge[1] = value;
                OnPropertyChanged("SelectedArea2");
            }

        }
        public string SelectedArea3
        {
            get
            {
                if (Assignment.Knowledge == null) return "";
                return Assignment.Knowledge[2];
            }
            set
            {
                Assignment.Knowledge[2] = value;
                OnPropertyChanged("SelectedArea3");
            }

        }
        public string[] _knowledgeAreas;
        public string[] KnowledgeAreas
        {
            get { return _knowledgeAreas; }
            set
            {
                _knowledgeAreas = value;
                OnPropertyChanged("KnowledgeAreas");
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

    }
}
