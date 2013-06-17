﻿using MSD.Controllers;
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
            _database = ModelFactory.Database;
            _fillquery = "SELECT s.studentnr, s.naam, s.mailadres, o.omschrijving, so.opdrachtnaam, so.opdrachtgoed, so.toestemmingvoorlopig, so.toestemmingdefinitief, b.naam, so.periode_periodenaam, so.type FROM student s JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN stageopdracht so ON so.stagenr = ss.stageopdracht_stagenr JOIN stagebedrijf b ON so.stagebedrijf_bedrijfnr = b.bedrijfnr JOIN opleiding o ON s.opleiding_afkorting = o.afkorting";
            FillTable(_fillquery);
            FillPeriode();
            this.StudentCollection = CollectionViewSource.GetDefaultView(Students);
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
            set { _selectedItem = value;
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
                this.OnPropertyChanged("Period");
                this.StudentCollection.Filter = new Predicate<object>(ContainsPeriod);
            }
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            MatchMogelijkViewModel mm = (MatchMogelijkViewModel)ViewFactory.getViewModel(_app, "matchMogelijkViewModel");

            mm.Student = SelectedItem;
            _stagenr = getexecuteQuery("SELECT stageopdracht_stagenr FROM stageopdracht_has_student WHERE student_studentnr = " + SelectedItem.StudentNo);
            if (SelectedItem.Assignment.Type == "Afstuderen")
            {
                mm.MogelijkeMatchReader(_stagenr);
            }
            mm.MogelijkeMatchTeacher(_stagenr);
            _app.ShowMatchMogelijkView();
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
           
            if (!string.IsNullOrEmpty(Zoektext))
            {
                if (!StudentCollection.IsEmpty)
                {
                    this.StudentCollection.Filter = new Predicate<object>(Contains);
                    this.StudentCollection.Refresh();
                    this.Zoektext = null;
                }
                else
                {
                    this.StudentCollection.Filter = null;
                }
            }
            else
            {
                this.StudentCollection.Filter = null;
            }

        }

        /// <summary>
        /// Maakt een student van het object en kijkt of de waardes overeenkomen met de zoektext
        /// </summary>
        /// <param name="obj">Het student object</param>
        /// <returns>De student rows die overeenkomen met het filter</returns>
        private bool Contains(object obj)
        {
            Student student = obj as Student;
            return (Regex.Match(student.StudentNo, Zoektext, RegexOptions.IgnoreCase).Success ||
                    Regex.Match(student.Name, Zoektext, RegexOptions.IgnoreCase).Success ||
                    Regex.Match(student.Email, Zoektext, RegexOptions.IgnoreCase).Success ||
                    Regex.Match(student.Education, Zoektext, RegexOptions.IgnoreCase).Success);
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
        public void FillTable(string query)
        {
            students.Clear();
            MySqlCommand mycommand = new MySqlCommand(query);
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
    }
}
