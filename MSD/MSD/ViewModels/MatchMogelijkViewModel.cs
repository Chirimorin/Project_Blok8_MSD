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

namespace MSD.ViewModels
{
    class MatchMogelijkViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _showDetailsCommand;
        private readonly RelayCommand _matchenCommand;
        private readonly RelayCommand _terugCommand;
        private Database _database;
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();

        public ObservableCollection<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = value;
                this.OnPropertyChanged("Teachers");
            }
        }
        private Student _student;
        public Student Student
        {
            get { return _student; }
            set
            {

                _student = value;
            }
        }

        public MatchMogelijkViewModel(IApplicationController app)
        {
            _app = app;
            _showDetailsCommand = new RelayCommand(ShowDetails);
            _matchenCommand = new RelayCommand(Matchen);
            _terugCommand = new RelayCommand(Terug);
            _database = ModelFactory.Database;
        }

        public RelayCommand ShowDetailsCommand { get { return _showDetailsCommand; } }
        public void ShowDetails(object command)
        {
            _app.ShowMatchDetailsView();
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            _app.ShowMatchSuccesView();
        }
        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowMatchInvoerView();
        }

        public void MogelijkeMatch(int stagenr, string periodenaam)
        {
            teachers.Clear();
            string query = "SELECT d.naam, d.uren, o.omschrijving, d.plaats FROM docent d JOIN docent_has_kennisgebieden dk ON d.docentnr = dk.docent_docentnr JOIN kennisgebieden k ON dk.kennisgebieden_kennisnr = k.kennisnr JOIN kennisgebieden_has_stageopdracht ks ON k.kennisnr = ks.kennisgebieden_kennisnr JOIN docent_has_opleiding dho ON d.docentnr = dho.docent_docentnr JOIN opleiding o ON dho.opleiding_afkorting = o.afkorting WHERE ks.stageopdracht_stagenr = '"+ stagenr + "' AND ks.stageopdracht_periode_periodenaam = '" + periodenaam + "' AND d.uren > 7";
            Debug.WriteLine(query);
            MySqlCommand mycommand = new MySqlCommand(query);
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = _database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                for (int RowNr = 0; RowNr < data.Rows.Count; RowNr++)
                {
                    teachers.Add(new Teacher
                    {
                        Name = data.Rows[RowNr][0].ToString(),
                        Hours = (int)data.Rows[RowNr][1],
                        Education = data.Rows[RowNr][2].ToString(),
                        City = data.Rows[RowNr][3].ToString(),
                        
                    });
                }
            }

        }

        public string StudentNumber
        {
            get { return Student.StudentNo; }
            set { Student.StudentNo = value; }
        }

        public string Name
        {
            get { return Student.Name; }
            set { Student.Name= value; }
        }

        public string Education
        {
            get { return Student.Education; }
            set { Student.Education = value; }
        }

        public string Academie
        {
            get { return Student.Academie; }
            set { Student.Academie = value; }
        }

        public string Company
        {
            get { return Student.Assignment.Company; }
            set { Student.Assignment.Company = value; }
        }

        public string AssignmentName
        {
            get { return Student.Assignment.Name; }
            set { Student.Assignment.Name = value; }
        }
    }
}
