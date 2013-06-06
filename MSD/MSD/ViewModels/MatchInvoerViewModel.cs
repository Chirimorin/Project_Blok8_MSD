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

namespace MSD.ViewModels
{
    class MatchInvoerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _matchenCommand;
        private readonly RelayCommand _zoekenCommand;
        private string _zoektext;

        private Database _database;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();

        public MatchInvoerViewModel(IApplicationController app)
        {
            _app = app;
            _matchenCommand = new RelayCommand(Matchen);
            _zoekenCommand = new RelayCommand(Zoeken);
            _database = ModelFactory.Database;
            string query = "SELECT s.studentnr, s.naam, s.mailadres, o.omschrijving FROM student s JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN opleiding o ON s.opleiding_afkorting = o.afkorting";
            FillTable(query);
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
        public string Zoektext
        {
            get
            {
                return _zoektext;
            }
            set
            {
                _zoektext = value;
                OnPropertyChanged("Email");
            }
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            _app.ShowMatchMogelijkView();
        }
        public RelayCommand ZoekenCommand { get { return _zoekenCommand; } }
        public void Zoeken(object command)
        {
            string query = "SELECT s.studentnr, s.naam, s.mailadres, o.omschrijving FROM student s JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN opleiding o ON s.opleiding_afkorting = o.afkorting WHERE s.naam LIKE '%" + Zoektext + "%' OR o.omschrijving LIKE '%" + Zoektext + "%';";
            FillTable(query);
        }
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

                    });
                }
            }
        }
    }
}
