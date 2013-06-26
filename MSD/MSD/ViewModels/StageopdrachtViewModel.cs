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
            
            _type = new string[2];
            _type[0] = "Stage";
            _type[1] = "Afstuderen";
            
        }

        public void setStagenr()
        {
            if (_wijzig == false)
            {
                _stagenr = getexecuteQuery("SELECT MAX(stagenr) FROM stageopdracht;") + 1;
            }
            if (_wijzig == true)
            {
                _stagenr = getexecuteQuery("SELECT stageopdracht_stagenr FROM stageopdracht_has_student WHERE student_studentnr = " + Student.StudentNo);
            }
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentPersoonView();
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Save(object command)
        {
            if (!HasEmptyProperties())
            {
                int afkorting = getexecuteQuery("SELECT afkorting FROM opleiding WHERE omschrijving = '" + Student.Education + "';");
                int bedrijf = getexecuteQuery("SELECT bedrijfnr FROM stagebedrijf WHERE naam = '" + Student.Assignment.Company + "';");

                ModelFactory.Database.setData(new MySqlCommand("DELETE FROM kennisgebieden_has_stageopdracht WHERE stageopdracht_stagenr = " + _stagenr));

                if (_wijzig == false)
                {
                    string studentquery = "INSERT INTO student VALUES(" + Student.StudentNo + ",'" + Student.Name + "','" + Student.Email + "','" + Student.Phone + "'," + afkorting + ",'" + Student.Academie + "');";
                    executeQuery(studentquery);
                    string opdrachtquery = "INSERT INTO stageopdracht () VALUES (" + _stagenr + ",'" + Student.Assignment.Name + "','" + Student.Assignment.Description + "','" + Student.Assignment.Comments + "'," + Student.Assignment.Accepted + "," + Student.Assignment.TempPermission + "," + Student.Assignment.Permission + "," + bedrijf + ",'" + Student.Assignment.Period + "','"+ Student.Assignment.Type +"')";
                    executeQuery(opdrachtquery);
                    string studentopdrachtquery = "INSERT INTO stageopdracht_has_student VALUES (" + _stagenr + "," + Student.StudentNo + ")";
                    executeQuery(studentopdrachtquery);
                }
                if (_wijzig == true)
                {
                    string studentquery = "UPDATE student SET naam = '" + Student.Name + "', mailadres = '" + Student.Email + "', telefoonnr = " + Student.Phone + ", opleiding_afkorting = " + afkorting + ", opleiding_academie_afkorting = '" + Student.Academie + "' WHERE studentnr = " + Student.StudentNo;
                    executeQuery(studentquery);
                    string opdrachtquery = "UPDATE stageopdracht SET stagenr=" + _stagenr + ", opdrachtnaam ='" + Student.Assignment.Name + "', omschrijving ='" + Student.Assignment.Description + "', opmerking ='" + Student.Assignment.Comments + "', opdrachtgoed = " + Student.Assignment.Accepted + ", toestemmingvoorlopig =" + Student.Assignment.TempPermission + ", toestemmingdefinitief =" + Student.Assignment.Permission + ", stagebedrijf_bedrijfnr = " + bedrijf + ", periode_periodenaam = '" + Student.Assignment.Period + "', type = '" + Student.Assignment.Type + "' WHERE stagenr = " + _stagenr;
                    executeQuery(opdrachtquery);
                }
                foreach (string knowledgeArea in Student.Assignment.Knowledge)
                {
                    if (knowledgeArea != "" && knowledgeArea != null)
                        ModelFactory.Database.setData(new MySqlCommand("INSERT INTO kennisgebieden_has_stageopdracht (kennisgebieden_kennisnr, stageopdracht_stagenr) VALUES((SELECT KennisNr FROM kennisgebieden WHERE KennisNaam = '" + knowledgeArea + "')," + _stagenr + ")"));
                }
                _app.ShowStudentView();
            }
        }

        public bool HasEmptyProperties()
        {
            bool HasEmptyProperty = false;
            string message = "De volgende gegevens zijn niet ingevuld: \n";
            if (String.IsNullOrEmpty(Student.StudentNo))
            {
                message += " - nummer \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Student.Name))
            {
                message += " - Naam \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Student.Email))
            {
                message += " - Email \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Student.Academie))
            {
                message += " - Academie \n";
                HasEmptyProperty = true;
            }
            if (String.IsNullOrEmpty(Student.Phone))
            {
                message += " - Telefoonnr \n";
                HasEmptyProperty = true;
            }
            if (HasEmptyProperty)
            {
                MessageBox.Show(message);
            }
            return HasEmptyProperty;
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
            cmd = new MySqlCommand("SELECT periodenaam FROM periode where periodenaam <> 'Alle' ");
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
        public void UpdateKnowledgeAreas()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden k JOIN kennisgebieden_has_stageopdracht ks ON ks.kennisgebieden_kennisnr = k.kennisnr WHERE stageopdracht_stagenr = " + _stagenr);
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            int NumRows = table.Rows.Count;

            if (NumRows >= 1)
                Student.Assignment.Knowledge[0] = table.Rows[0][1].ToString();
            if (NumRows >= 2)
                Student.Assignment.Knowledge[1] = table.Rows[1][1].ToString();
            if (NumRows >= 3)
                Student.Assignment.Knowledge[2] = table.Rows[2][1].ToString();
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
                if (Student.Assignment.Knowledge == null) return "";
                return Student.Assignment.Knowledge[0];
            }
            set
            {
                Student.Assignment.Knowledge[0] = value;
                OnPropertyChanged("SelectedArea1");
            }

        }
        public string SelectedArea2
        {
            get
            {
                if (Student.Assignment.Knowledge == null) return "";
                return Student.Assignment.Knowledge[1];
            }
            set
            {
                Student.Assignment.Knowledge[1] = value;
                OnPropertyChanged("SelectedArea2");
            }

        }
        public string SelectedArea3
        {
            get
            {
                if (Student.Assignment.Knowledge == null) return "";
                return Student.Assignment.Knowledge[2];
            }
            set
            {
                Student.Assignment.Knowledge[2] = value;
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
                if (Student.Assignment.Company == null) return "";
                return Student.Assignment.Company;
            }
            set
            {
                Student.Assignment.Company = value;
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
        public string SelectedType
        {
            get
            {
                if (Student.Assignment.Type == null) return "";
                return Student.Assignment.Type;
            }
            set
            {
                Student.Assignment.Type = value;
                OnPropertyChanged("Type");
            }

        }
        public string[] _type;
        public string[] Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public string SelectedPeriod
        {
            get
            {
                if (Student.Assignment.Period == null) return "";
                return Student.Assignment.Period;
            }
            set
            {
                Student.Assignment.Period = value;
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
                if(Student.Assignment != null) 
                    return Student.Assignment.Description;
                return "";
            }
            set
            {
                Student.Assignment.Description = value;
            }
        }
        public string Name
        {
            get
            {
                if (Student.Assignment != null)
                    return Student.Assignment.Name;
                return "";
            }
            set
            {
                Student.Assignment.Name = value;
            }
        }

        public string Comments
        {
            get
            {
                if (Student.Assignment != null)
                    return Student.Assignment.Comments;
                return "";
            }
            set
            {
                Student.Assignment.Comments = value;
            }
        }
       

        public bool Accepted
        {
            get
            {
                if (Student.Assignment != null)
                    return Student.Assignment.Accepted;
                return false;
            }
            set
            {
                Student.Assignment.Accepted = value;
            }
        }

        public bool TempPermission
        {
            get
            {
                if (Student.Assignment != null)
                    return Student.Assignment.TempPermission;
                return false;
            }
            set
            {
                Student.Assignment.TempPermission = value;
            }
        }

        public bool Permission
        {
            get
            {
                if (Student.Assignment != null)
                    return Student.Assignment.Permission;
                return false;
            }
            set
            {
                Student.Assignment.Permission = value;
            }
        }

    }
}
