using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace MSD.ViewModels
{
    public class StageViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private bool _afstuderen;
        private string _selectedPeriod;
        private string _zoektext;
        private RelayCommand _filterCommand;
        private bool _reader = false;

        private ICollectionView _studentCollection;
        public ICollectionView StudentCollection
        {
            get { return _studentCollection; }
            set { _studentCollection = value; }
        }

        public StageViewModel(IApplicationController app)
        {
            _filterCommand = new RelayCommand(Filter);
            _app = app;
            
        }
        public void setData()
        {
            string type;

            if (Afstuderen)
            {
                type = "Afstuderen";
                _reader = true;
            }
            else
            {
                type = "Stage";
            }
            string query = "SELECT s.studentnr, s.naam, s.mailadres, o.omschrijving, so.opdrachtnaam, b.naam, so.periode_periodenaam, so.stagenr FROM student s JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN stageopdracht so ON ss.stageopdracht_stagenr =  so.stagenr JOIN opleiding o ON s.opleiding_afkorting = o.afkorting JOIN stagebedrijf b ON so.stagebedrijf_bedrijfnr = b.bedrijfnr WHERE so.type = '" + type + "'";
            
            FillPeriode();
            FillTable(query);
            this.StudentCollection = CollectionViewSource.GetDefaultView(Students);
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
            MySqlDataAdapter adapter = ModelFactory.Database.getData(mycommand);
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
                            Company = data.Rows[RowNr][5].ToString(),
                            Period = data.Rows[RowNr][6].ToString(),
                            Supervisor = getSupervisor(Convert.ToInt32(data.Rows[RowNr][7])),
                            Secondreader = getSecondreader(Convert.ToInt32(data.Rows[RowNr][7]))
                            
                        }


                    });
                }
            }
        }
        public string getSupervisor(int stagenr)
        {
            string supervisorquery = "SELECT d.naam FROM docent d JOIN docent_has_stageopdracht ds ON ds.docent_docentnr = d.docentnr WHERE ds.stageopdracht_stagenr = "+ stagenr + " AND soort = 'Begeleider'";
            return getexecuteQuery(supervisorquery);
        }
        public string getSecondreader(int stagenr)
        {
            if (_reader == true)
            {
                string secondreaderquery = "SELECT d.naam FROM docent d JOIN docent_has_stageopdracht ds ON ds.docent_docentnr = d.docentnr WHERE ds.stageopdracht_stagenr = " + stagenr + " AND soort = 'Tweede lezer'";
                return getexecuteQuery(secondreaderquery);
            }
            return "-";
        }
        public string getexecuteQuery(string query)
        {
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = ModelFactory.Database.getData(mycommand);
            adapter.Fill(data);
            if(data.Rows.Count != 0)
                return data.Rows[0][0].ToString();
            else
                return "-";
        }
        public RelayCommand FilterCommand { get { return _filterCommand; } }
        
        /// <summary>
        /// Filtert de StudentCollection
        /// </summary>
        /// <param name="command"></param>
        public void Filter(object command)
        {
            if (!string.IsNullOrEmpty(Zoektext))
            {
                if (!StudentCollection.IsEmpty)
                {
                    this.StudentCollection.Filter = new Predicate<object>(Contains);
                    this.StudentCollection.Refresh();
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
            return (student.Name.Contains(Zoektext)) || (student.Email.Contains(Zoektext)) || (student.Education.Contains(Zoektext));
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

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                this.OnPropertyChanged("Students");
            }
        }

        public bool Afstuderen
        {
            get { return _afstuderen; }
            set 
            {
                _afstuderen = value;
                OnPropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                if (Afstuderen) return "Afstuderen";
                else return "Stages";
            }
        }

        public string SelectedPeriod
        {
            get { return _selectedPeriod; }
            set
            {
                _selectedPeriod = value;
                this.OnPropertyChanged("Period");
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
