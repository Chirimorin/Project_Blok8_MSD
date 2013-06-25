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
using System.ComponentModel;
using System.Windows.Data;

namespace MSD.ViewModels
{
    class MatchMogelijkViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _showDetailsCommand;
        private readonly RelayCommand _matchenCommand;
        private readonly RelayCommand _terugCommand;
        private Database _database;
        private int _stagenr;
        private ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();
        private ObservableCollection<Teacher> readers = new ObservableCollection<Teacher>();

        private ICollectionView _teacherCollection;
        public ICollectionView TeacherCollection
        {
            get { return _teacherCollection; }
            set { _teacherCollection = value; }
        }

        private ICollectionView _readerCollection;
        public ICollectionView ReaderCollection
        {
            get { return _readerCollection; }
            set { _readerCollection = value; }
        }

        public ObservableCollection<Teacher> Teachers
        {
            get { return teachers; }
            set
            {
                teachers = value;
                this.OnPropertyChanged("Teachers");
            }
        }
        public ObservableCollection<Teacher> SecondReader
        {
            get { return readers; }
            set
            {
                readers = value;
                this.OnPropertyChanged("SecondReader");
            }
        }
        public int Stagenr
        {
            get { return _stagenr; }
            set { _stagenr = value; }
        }

        public bool _afstuderen;
        public bool Afstuderen
        {
            get { return _afstuderen; }
            set { _afstuderen = value; }
        }
        private Teacher _selectedteacher;
        public Teacher SelectedTeacher
        {
            get { return _selectedteacher; }
            set
            {
                _selectedteacher = value;
                OnPropertyChanged("SelectedTeacher");
            }
        }
        private Teacher _selectedreader;
        public Teacher SelectedReader
        {
            get { return _selectedreader; }
            set
            {
                _selectedreader = value;
                OnPropertyChanged("SelectedReader");
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
            this.ReaderCollection = CollectionViewSource.GetDefaultView(SecondReader);
            this.ReaderCollection.SortDescriptions.Add(new SortDescription("Hours", ListSortDirection.Descending));
            this.TeacherCollection = CollectionViewSource.GetDefaultView(Teachers);
            this.TeacherCollection.SortDescriptions.Add(new SortDescription("Hours", ListSortDirection.Descending));
        }

        public RelayCommand ShowDetailsCommand { get { return _showDetailsCommand; } }
        public void ShowDetails(object command)
        {
            MatchDetailsViewModel vm = (MatchDetailsViewModel)ViewFactory.getViewModel(_app, "matchDetailsViewModel");
            vm.Student.Add(Student);
            vm.Supervisor.Add(SelectedTeacher);
            if (SelectedReader != null)
            {
                vm.Secondreader.Add(SelectedReader);
            }
            vm.Stagenr = _stagenr;
            _app.ShowMatchDetailsView();
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            MatchSuccesViewModel vm = (MatchSuccesViewModel)ViewFactory.getViewModel(_app, "matchSuccesViewModel");
            vm.Student.Add(Student);
            vm.Supervisor.Add(SelectedTeacher);
            Match(SelectedTeacher,7, "Begeleider");
            if (SelectedReader != null)
            {
                vm.Secondreader.Add(SelectedReader);
                Match(SelectedReader,0, "Tweede lezer");
            }

            _app.ShowMatchSuccesView();
        }
        public void Match(Teacher teacher, int uren, string type)
        {
            string deletequery = "DELETE FROM docent_has_stageopdracht WHERE stageopdracht_stagenr = " + _stagenr;
            MySqlCommand mycommand = new MySqlCommand(deletequery);
            ModelFactory.Database.setData(mycommand);
            string query = "INSERT INTO docent_has_stageopdracht VALUES(" + teacher.TeacherNo + "," + _stagenr + ",'" + type + "');";
            mycommand = new MySqlCommand(query);
            ModelFactory.Database.setData(mycommand);
            query = "UPDATE `docent` SET `Uren`= ((SELECT `Uren`)-" + uren + ") WHERE `docentnr` = " + teacher.TeacherNo;
            mycommand = new MySqlCommand(query);
            ModelFactory.Database.setData(mycommand);
        }
        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowMatchInvoerView();
        }

        public void MogelijkeMatchTeacher()
        {
            teachers.Clear();
            string query = "SELECT d.naam, d.uren, o.omschrijving, d.plaats, d.docentnr, d.mailadres FROM docent d JOIN docent_has_kennisgebieden dk ON d.docentnr = dk.docent_docentnr JOIN kennisgebieden k ON dk.kennisgebieden_kennisnr = k.kennisnr JOIN kennisgebieden_has_stageopdracht ks ON k.kennisnr = ks.kennisgebieden_kennisnr JOIN docent_has_opleiding dho ON d.docentnr = dho.docent_docentnr JOIN opleiding o ON dho.opleiding_afkorting = o.afkorting JOIN stageuren u ON o.stageuren_stageurennr = u.stageurennr WHERE ks.stageopdracht_stagenr = '" + _stagenr + "' AND d.uren > u.urenvergoedingstagesingle GROUP BY d.naam";
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
                        TeacherNo = (int)data.Rows[RowNr][4],
                        Email = data.Rows[RowNr][5].ToString(),

                    });
                    UpdateTeacherKnowledgeAreas(teachers[RowNr]);
                }
            }
        }

        public void MogelijkeMatchReader()
        {
            readers.Clear();
            string query = "SELECT d.naam, d.uren, o.omschrijving, d.plaats, d.docentnr, d.mailadres FROM docent d JOIN docent_has_kennisgebieden dk ON d.docentnr = dk.docent_docentnr JOIN kennisgebieden k ON dk.kennisgebieden_kennisnr = k.kennisnr JOIN kennisgebieden_has_stageopdracht ks ON k.kennisnr = ks.kennisgebieden_kennisnr JOIN docent_has_opleiding dho ON d.docentnr = dho.docent_docentnr JOIN opleiding o ON dho.opleiding_afkorting = o.afkorting JOIN stageuren u ON o.stageuren_stageurennr = u.stageurennr WHERE ks.stageopdracht_stagenr = '" + _stagenr + "' AND d.uren > u.urenvergoedingstagesingle  GROUP BY d.naam";
            Debug.WriteLine(query);
            MySqlCommand mycommand = new MySqlCommand(query);
            DataTable data = new DataTable();
            MySqlDataAdapter adapter = _database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                for (int RowNr = 0; RowNr < data.Rows.Count; RowNr++)
                {
                    readers.Add(new Teacher
                    {
                        Name = data.Rows[RowNr][0].ToString(),
                        Hours = (int)data.Rows[RowNr][1],
                        Education = data.Rows[RowNr][2].ToString(),
                        City = data.Rows[RowNr][3].ToString(),
                        TeacherNo = (int)data.Rows[RowNr][4],
                        Email = data.Rows[RowNr][5].ToString(),

                    });
                    UpdateTeacherKnowledgeAreas(readers[RowNr]);
                }
            }

        }
        private void UpdateTeacherKnowledgeAreas(Teacher teacher)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM kennisgebieden WHERE KennisNr IN (SELECT Kennisgebieden_KennisNr FROM docent_has_kennisgebieden WHERE Docent_Docentnr = " + teacher.TeacherNo + ")");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            int NumRows = table.Rows.Count;

            if (NumRows >= 1)
            {
                teacher.KnowledgeAreas[0] = table.Rows[0][1].ToString();
                teacher.Knowledge += table.Rows[0][1].ToString() + ", ";
            }
            else
                teacher.KnowledgeAreas[0] = "";
            if (NumRows >= 2)
            {
                teacher.KnowledgeAreas[1] = table.Rows[1][1].ToString();
                teacher.Knowledge += table.Rows[1][1].ToString();
            }
            else
                teacher.KnowledgeAreas[1] = "";
            if (NumRows >= 3)
            {
                teacher.KnowledgeAreas[2] = table.Rows[2][1].ToString();
                teacher.Knowledge += ", " + table.Rows[2][1].ToString();
            }
            else
                teacher.KnowledgeAreas[2] = "";
            if (NumRows >= 4)
            {
                teacher.KnowledgeAreas[3] = table.Rows[3][1].ToString();
                teacher.Knowledge += ", " + table.Rows[0][1].ToString();
            }
            else
                teacher.KnowledgeAreas[3] = "";
            if (NumRows >= 5)
            {
                teacher.KnowledgeAreas[4] = table.Rows[4][1].ToString();
                teacher.Knowledge += ", " + table.Rows[0][1].ToString();
            }
            else
                teacher.KnowledgeAreas[4] = "";
        }

        public string StudentNumber
        {
            get { return Student.StudentNo; }
            set { Student.StudentNo = value; }
        }

        public string Name
        {
            get { return Student.Name; }
            set { Student.Name = value; }
        }

        public string Education
        {
            get { return Student.Education; }
            set { Student.Education = value; }
        }

        public string Type
        {
            get { return Student.Assignment.Type; }
            set { Student.Assignment.Type = value; }
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
        private string _matchedTeacher;
        public string MatchedTeacher
        {
            get { return _matchedTeacher; }
            set { _matchedTeacher = value; }
        }
        private string _matchedReader;

        public string MatchedReader
        {
            get { return _matchedReader; }
            set { _matchedReader = value; }
        }
    }
}

