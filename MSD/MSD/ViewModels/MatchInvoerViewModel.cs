using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace MSD.ViewModels
{
    class MatchInvoerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _matchenCommand;
        private readonly RelayCommand _zoekenCommand;
        private string _zoektext;
        private readonly RelayCommand _resetCommand;
        private readonly RelayCommand _matchmadeCommand;
        private string _selectedPeriod;
        private int _stagenr;
        private string _fillquery;



        private ICollectionView _studentCollection;

        public ICollectionView StudentCollection
        {
            get { return _studentCollection; }
            set { _studentCollection = value; }
        }

        private Database _database;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public MatchInvoerViewModel(IApplicationController app)
        {
            _app = app;
            _resetCommand = new RelayCommand(Reset);
            _matchenCommand = new RelayCommand(Matchen);
            _zoekenCommand = new RelayCommand(Zoeken);
            _matchmadeCommand = new RelayCommand(Matchmade);
            _database = ModelFactory.Database;
            _fillquery = "SELECT s.studentnr, s.naam, s.mailadres, o.omschrijving, so.opdrachtnaam, so.opdrachtgoed, so.toestemmingvoorlopig, so.toestemmingdefinitief, b.naam, so.periode_periodenaam, so.type FROM student s JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN stageopdracht so ON so.stagenr = ss.stageopdracht_stagenr JOIN stagebedrijf b ON so.stagebedrijf_bedrijfnr = b.bedrijfnr JOIN opleiding o ON s.opleiding_afkorting = o.afkorting WHERE so.stagenr NOT IN (SELECT ds.stageopdracht_stagenr FROM docent_has_stageopdracht ds)";
            FillTable();
            FillPeriode();
            this.StudentCollection = CollectionViewSource.GetDefaultView(Students);
            SelectedPeriod = "Alle";
        }

        

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            MatchMogelijkViewModel mm = (MatchMogelijkViewModel)ViewFactory.getViewModel(_app, "matchMogelijkViewModel");

            mm.Student = SelectedItem;
            _stagenr = getexecuteQuery("SELECT stageopdracht_stagenr FROM stageopdracht_has_student WHERE student_studentnr = " + SelectedItem.StudentNo);
            mm.Stagenr = _stagenr;
            mm.SecondReader.Clear();
            mm.SelectedReader = null;
            if (SelectedItem.Assignment.Type == "Afstuderen")
            {
                mm.MogelijkeMatchReader();
                mm.MatchedReader = "";
                mm.Afstuderen = true;
            }
            mm.MatchedTeacher = "";
            mm.MogelijkeMatchTeacher();
            _app.ShowMatchMogelijkView();
        }
        public RelayCommand MatchmadeCommand { get { return _matchmadeCommand; } }
        public void Matchmade(object command)
        {
            _app.ShowMatchGemaaktView();
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

        public RelayCommand ResetCommand { get { return _resetCommand; } }

        public void Reset(object command)
        {
            this.Zoektext = null;
            SelectedPeriod = "Alle";
            this.StudentCollection.Filter = null;
            this.StudentCollection.Refresh();
        }

        public RelayCommand ZoekenCommand { get { return _zoekenCommand; } }

        /// <summary>
        /// Filtert de StudentCollection
        /// </summary>
        /// <param name="command"></param>
        public void Zoeken(object command)
        {

            if (students.Count != 0)
            {
                this.StudentCollection.Filter = new Predicate<object>(ContainsBoth);
                this.StudentCollection.Refresh();
            }

        }

        private bool ContainsBoth(object student)
        {
            if (_selectedPeriod.Equals("Alle"))
            {
                return ContainsSearch(student);
            }
            else
            {
                return ContainsSearch(student) && ContainsPeriod(student);
            }
        }

        /// <summary>
        /// Maakt een student van het object en kijkt of de waardes overeenkomen met de zoektext
        /// </summary>
        /// <param name="obj">Het student object</param>
        /// <returns>De student rows die overeenkomen met het filter</returns>
        private bool ContainsSearch(object obj)
        {
            if (String.IsNullOrEmpty(Zoektext))
            {
                return true;
            }
            else
            {
                Student student = obj as Student;
                return Regex.Match(student.Name, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Email, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.StudentNo, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Education, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Name, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Company, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Type, Zoektext, RegexOptions.IgnoreCase).Success;
            }

        }

        private bool ContainsPeriod(object obj)
        {
            Student student = obj as Student;
            return Regex.Match(student.Assignment.Period, _selectedPeriod, RegexOptions.IgnoreCase).Success;
        }

        /// <summary>
        /// Vult de ObservableCollection met Student objecten
        /// </summary>
        /// <param name="query">De uit te voeren query</param>
        public void FillTable()
        {
            students.Clear();
            MySqlCommand mycommand = new MySqlCommand(_fillquery);
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = _database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                for (int RowNr = 0; RowNr < data.Rows.Count; RowNr++)
                {
                    students.Add(new Student
                    {
                        StudentNo = data.Rows[RowNr][0].ToString(),
                        Name = data.Rows[RowNr][1].ToString(),
                        Email = data.Rows[RowNr][2].ToString(),
                        Education = data.Rows[RowNr][3].ToString(),
                        
                        Assignment = new Assignment
                        {
                            Name = data.Rows[RowNr][4].ToString(),
                            Period = data.Rows[RowNr][9].ToString(),
                            Company = data.Rows[RowNr][8].ToString(),
                            Accepted = (bool)data.Rows[RowNr][5],
                            Permission = (bool)data.Rows[RowNr][6],
                            TempPermission = (bool)data.Rows[RowNr][7],
                            Type = data.Rows[RowNr][10].ToString(),
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Vult periode combobox
        /// </summary>
        public void FillPeriode()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT periodenaam FROM periode");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            _period = new string[table.Rows.Count];
            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Period[RowNr] = table.Rows[RowNr][0].ToString();
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

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                this.OnPropertyChanged("Students");
            }
        }

        private Student _selectedItem;
        public Student SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                this.OnPropertyChanged("SelectedItem");
                OnPropertyChanged("AanpassenEnabled");
            }
        }

        public bool AanpassenEnabled
        {
            get { return (SelectedItem != null); }
        }

        public string Zoektext
        {
            get
            {
                return _zoektext;
            }
            set
            {
                _zoektext = value;
                OnPropertyChanged("Zoektext");
            }
        }

        public string SelectedPeriod
        {
            get { return _selectedPeriod; }
            set
            {
                _selectedPeriod = value;
                this.OnPropertyChanged("SelectedPeriod");
            }
        }

        
    }
}
