using MSD.Controllers;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MSD.Entity;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Windows.Data;

namespace MSD.ViewModels
{
    class StudentViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuweStudentCommand;
        private readonly RelayCommand _studentAanpassenCommand;
        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        private ObservableCollection<Education> _educations = new ObservableCollection<Education>();
        private ObservableCollection<Academy> _academies = new ObservableCollection<Academy>();
        private Student _selectedItem;
        private string _selectedPeriod;
        private Academy _selectedAcademy;
        private Education _selectedEducation;
        private string _zoektext;
        private readonly RelayCommand _filterCommand;
        private readonly RelayCommand _resetCommand;

        private ICollectionView _studentCollection;
        public ICollectionView StudentCollection
        {
            get { return _studentCollection; }
            set { _studentCollection = value; }
        }

        private ICollectionView _eduCollection;
        public ICollectionView EduCollection
        {
            get { return _eduCollection; }
            set { _eduCollection = value; }
        }

        private ICollectionView _acaCollection;
        public ICollectionView AcaCollection
        {
            get { return _acaCollection; }
            set { _acaCollection = value; }
        }


        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }

        public ObservableCollection<Education> Educations
        {
            get { return _educations; }
            set
            {
                _educations = value;
                OnPropertyChanged("Educations");
            }
        }

        public ObservableCollection<Academy> Academies
        {
            get { return _academies; }
            set
            {
                _academies = value;
                OnPropertyChanged("Academies");
            }
        }

        public Student SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("StudentName");
                OnPropertyChanged("StudentNo");
                OnPropertyChanged("Email");
                OnPropertyChanged("Company");
                OnPropertyChanged("Period");
                OnPropertyChanged("Accepted");
                OnPropertyChanged("TempPermission");
                OnPropertyChanged("Permission");
                OnPropertyChanged("StageType");
                OnPropertyChanged("Teacher");
                OnPropertyChanged("Education");
                OnPropertyChanged("SecondReader");
                OnPropertyChanged("Academy");
                OnPropertyChanged("AanpassenEnabled");

                if (value == null)
                {
                    _selectedItem = generateStudent();
                }
                else
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                    OnPropertyChanged("StudentName");
                    OnPropertyChanged("StudentNo");
                    OnPropertyChanged("Email");
                    OnPropertyChanged("Company");
                    OnPropertyChanged("Period");
                    OnPropertyChanged("Accepted");
                    OnPropertyChanged("TempPermission");
                    OnPropertyChanged("Permission");
                    OnPropertyChanged("StageType");
                    OnPropertyChanged("Teacher");
                    OnPropertyChanged("Education");
                    OnPropertyChanged("SecondReader");
                    OnPropertyChanged("Academy");
                }
            }
        }

        public bool AanpassenEnabled
        {
            get { return (SelectedItem != null); }
        }

        public StudentViewModel(IApplicationController app)
        {
            _app = app;
            _nieuweStudentCommand = new RelayCommand(NieuweStudent);
            _studentAanpassenCommand = new RelayCommand(StudentAanpassen);
            _filterCommand = new RelayCommand(Filter);
            _resetCommand = new RelayCommand(Reset);
            FillPeriode();
            FillAcademies();
            Fillopleiding();
            this.StudentCollection = CollectionViewSource.GetDefaultView(Students);
            this.EduCollection = CollectionViewSource.GetDefaultView(Educations);
            if (SelectedItem == null)
            {
                SelectedItem = generateStudent();
            }
            SelectedPeriod = "Alle";
            this.SelectedAcademy = Academies.First();
            this.SelectedEducation = Educations.First();
            this.StudentCollection.Filter = new Predicate<object>(ContainsAll);
            this.StudentCollection.Refresh();
        }

        private Student generateStudent()
        {
            return new Student
            {
                Academie = "Niet bekend",
                StudentNo = "Niet bekend",
                Email = "Niet bekend",
                Name = "Niet bekend",
                Phone = "Niet bekend",
                Education = "Niet bekend",
                Assignment = new Assignment
                {
                    Company = "Niet bekend",
                    Description = "Niet bekend",
                    Name = "Niet bekend",
                    Period = "Niet bekend",
                    Secondreader = "Niet bekend",
                    Supervisor = "Niet bekend",
                    Type = "Niet bekend"
                }
            };
        }

        private Database Database
        {
            get { return ModelFactory.Database; }
        }

        public RelayCommand NieuweStudentCommand { get { return _nieuweStudentCommand; } }
        public void NieuweStudent(object command)
        {
            StudentPersoonViewModel vm = (StudentPersoonViewModel)ViewFactory.getViewModel(_app, "studentPersoonViewModel");
            StageopdrachtViewModel vm2 = (StageopdrachtViewModel)ViewFactory.getViewModel(_app, "stageopdrachtViewModel");

            vm.Title = "Nieuwe Student";
            vm2.Title = "Nieuwe Student";
            vm.Student = new Student();
            vm2.Student = vm.Student;
            vm2.Student.Assignment = new Assignment();
            vm2.Wijzig = false;
            vm2.setStagenr();
            _app.ShowStudentPersoonView();
        }

        public RelayCommand StudentAanpassenCommand { get { return _studentAanpassenCommand; } }
        public void StudentAanpassen(object command)
        {
            StudentPersoonViewModel vm = (StudentPersoonViewModel)ViewFactory.getViewModel(_app, "studentPersoonViewModel");
            StageopdrachtViewModel vm2 = (StageopdrachtViewModel)ViewFactory.getViewModel(_app, "stageopdrachtViewModel");

            vm.Title = "Student Aanpassen";
            vm2.Title = "Student Aanpassen";
            vm.Student = SelectedItem;
            vm2.Student = vm.Student;
            makeAssignment(vm2);
            vm2.Wijzig = true;
            vm2.setStagenr();
            vm2.UpdateKnowledgeAreas();
            _app.ShowStudentPersoonView();
        }
        /// <summary>
        /// maakt het assignment object dat bij de student hoort
        /// </summary>
        public void makeAssignment(StageopdrachtViewModel vm2)
        {
            string query = "SELECT o.opdrachtnaam, o.omschrijving, o.opmerking, o.opdrachtgoed, o.toestemmingvoorlopig, o.toestemmingdefinitief, o.periode_periodenaam, b.naam, o.type FROM stageopdracht o JOIN stageopdracht_has_student s ON o.stagenr = s.stageopdracht_stagenr JOIN stagebedrijf b ON o.stagebedrijf_bedrijfnr = b.bedrijfnr WHERE s.student_studentnr = " + SelectedItem.StudentNo + ";";
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = Database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                vm2.Student.Assignment = new Assignment
                {
                    Name = data.Rows[0][0].ToString(),
                    Description = data.Rows[0][1].ToString(),
                    Comments = data.Rows[0][2].ToString(),
                    Accepted = (bool)data.Rows[0][3],
                    Permission = (bool)data.Rows[0][5],
                    TempPermission = (bool)data.Rows[0][4],
                    //Knowledge = data.Rows[0][5].ToString(),
                    Period = data.Rows[0][6].ToString(),
                    Company = data.Rows[0][7].ToString(),
                    Type = data.Rows[0][8].ToString(),
                };
            }
            else
            {
                vm2.Student.Assignment = null;
            }
        }

        /// <summary>
        /// Vult de Student tabel met student uit de Database
        /// </summary>
        public void fillStudentTable()
        {
            Students.Clear();
            MySqlCommand cmd = new MySqlCommand("select s.studentnr, s.naam, s.mailadres, s.telefoonnr, o.omschrijving, s.opleiding_academie_afkorting, so.periode_periodenaam, b.naam, so.opdrachtgoed, so.toestemmingvoorlopig, so.toestemmingdefinitief, so.type , so.stagenr FROM student s JOIN opleiding o ON s.opleiding_afkorting = o.afkorting JOIN stageopdracht_has_student ss ON s.studentnr = ss.student_studentnr JOIN stageopdracht so ON so.stagenr = ss.stageopdracht_stagenr JOIN stagebedrijf b ON so.stagebedrijf_bedrijfnr = b.bedrijfnr");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Students.Add(new Student
                {
                    StudentNo = table.Rows[RowNr][0].ToString(),
                    Name = table.Rows[RowNr][1].ToString(),
                    Email = table.Rows[RowNr][2].ToString(),
                    Phone = table.Rows[RowNr][3].ToString(),
                    Education = table.Rows[RowNr][4].ToString(),
                    Academie = table.Rows[RowNr][5].ToString(),

                    Assignment = new Assignment
                    {
                        Period = table.Rows[RowNr][6].ToString(),
                        Company = table.Rows[RowNr][7].ToString(),
                        Accepted = (bool)table.Rows[RowNr][8],
                        TempPermission = (bool)table.Rows[RowNr][9],
                        Permission = (bool)table.Rows[RowNr][10],
                        Type = table.Rows[RowNr][11].ToString(),
                        Supervisor = getSupervisor(Convert.ToInt32(table.Rows[RowNr][12])),
                        Secondreader = getSecondreader(Convert.ToInt32(table.Rows[RowNr][12]))
                    }
                });
            }
        }

        /// <summary>
        /// Pakt de bijbehorende begeleider van de stage als die er is
        /// </summary>
        /// <param name="stagenr"></param>
        /// <returns>Docent naam</returns>
        public string getSupervisor(int stagenr)
        {
            string supervisorquery = "SELECT d.naam FROM docent d JOIN docent_has_stageopdracht ds ON ds.docent_docentnr = d.docentnr WHERE ds.stageopdracht_stagenr = " + stagenr + " AND soort = 'Begeleider'";
            if (String.IsNullOrEmpty(getexecuteQuery(supervisorquery)))
            {
                return "-";
            }
            else
            {
                return getexecuteQuery(supervisorquery);
            }
        }

        /// <summary>
        /// Pakt de bijbehorende tweede lezer van de stage als die er is
        /// </summary>
        /// <param name="stagenr"></param>
        /// <returns>Docent naam</returns>
        public string getSecondreader(int stagenr)
        {
            string secondreaderquery = "SELECT d.naam FROM docent d JOIN docent_has_stageopdracht ds ON ds.docent_docentnr = d.docentnr WHERE ds.stageopdracht_stagenr = " + stagenr + " AND soort = 'Tweede lezer'";
            if (String.IsNullOrEmpty(getexecuteQuery(secondreaderquery)))
            {
                return "-";
            }
            else
            {
                return getexecuteQuery(secondreaderquery);
            }
        }

        public string getexecuteQuery(string query)
        {
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = ModelFactory.Database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
                return data.Rows[0][0].ToString();
            else
                return "-";
        }

        public RelayCommand ResetCommand { get { return _resetCommand; } }

        public void Reset(object command)
        {
            this.Zoektext = null;
            this.StudentCollection.Filter = null;
            this.StudentCollection.Refresh();
        }

        public RelayCommand FilterCommand { get { return _filterCommand; } }

        /// <summary>
        /// Filtert de StudentCollection
        /// </summary>
        /// <param name="command"></param>
        public void Filter(object command)
        {
            if (Students.Count != 0)
            {
                
                this.StudentCollection.Filter = new Predicate<object>(ContainsAll);
                this.StudentCollection.Refresh();
            }
            SelectedItem = generateStudent();
        }

        private bool ContainsAll(object student)
        {
            return ContainsSearch(student) && ContainsPeriod(student) && ContainsAcademy(student) && ContainsOpleiding(student);
        }

        private bool ContainsOpleiding(object obj)
        {
            if (SelectedEducation == null)
            {
                return true;
            }
            else
            {
                Student student = obj as Student;
                return Regex.Match(student.Education, SelectedEducation.Description, RegexOptions.IgnoreCase).Success;
            }
        }

        private bool ContainsAcademy(object obj)
        {
            if (SelectedAcademy == null)
            {
                return true;
            }
            else
            {
                Student student = obj as Student;
                return Regex.Match(student.Academie, SelectedAcademy.Abbreviation, RegexOptions.IgnoreCase).Success;
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
                        Regex.Match(student.Assignment.Company, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Supervisor, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Secondreader, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Academie, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Assignment.Type, Zoektext, RegexOptions.IgnoreCase).Success ||
                        Regex.Match(student.Education, Zoektext, RegexOptions.IgnoreCase).Success;
            }
        }

        private bool ContainsPeriod(object obj)
        {
            if (SelectedPeriod.Equals("Alle"))
            {
                return true;
            }
            else
            {
                Student student = obj as Student;
                return Regex.Match(student.Assignment.Period, _selectedPeriod, RegexOptions.IgnoreCase).Success;
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

        public Education SelectedEducation
        {
            get { return _selectedEducation; }
            set
            {
                _selectedEducation = value;
                this.OnPropertyChanged("SelectedEducation");
            }
        }

        public Academy SelectedAcademy
        {
            get { return _selectedAcademy; }
            set
            {
                _selectedAcademy = value;
                this.OnPropertyChanged("SelectedAcademy");
                filterEducation();
                this.SelectedEducation = Educations.First();
            }
        }

        public void filterEducation()
        {
            this.EduCollection.Filter = new Predicate<object>(hasEducation);
        }

        public bool hasEducation(object e)
        {
            Education edu = e as Education;
            return Regex.Match(edu.Abbreviation, SelectedAcademy.Abbreviation, RegexOptions.IgnoreCase).Success;
        }

        private Assignment _assignment;
        public Assignment Assignment
        {
            get
            {
                return _assignment;
            }
            set
            {
                _assignment = value;
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string StudentName
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Name;
                return "";
            }
        }

        public string Academy
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Academie;
                return "";
            }
        }

        public string StageType
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Type;
                return "";
            }
        }
        public string StudentNo
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.StudentNo;
                return "";
            }

        }

        public string Email
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Email;
                return "";
            }
        }

        public string Education
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Education;
                return "";
            }
        }

        public string Teacher
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Supervisor;
                return "";
            }
        }

        public string SecondReader
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Secondreader;
                return "";
            }
        }

        public string Company
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Company;
                else return "";
            }
            set
            {
                SelectedItem.Assignment.Company = value;
            }
        }

        public string Period
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Period;
                else return "";
            }
            set
            {
                SelectedItem.Assignment.Period = value;
            }
        }

        public bool Accepted
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Accepted;
                else return false;
            }
            set
            {
                SelectedItem.Assignment.Accepted = value;
            }
        }

        public bool TempPermission
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.TempPermission;
                else return false;
            }
            set
            {
                SelectedItem.Assignment.TempPermission = value;
            }
        }

        public bool Permission
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.Assignment.Permission;
                else return false;
            }
            set
            {
                SelectedItem.Assignment.Permission = value;
            }
        }

        private string[] _period;
        public string[] PeriodCollection
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
                OnPropertyChanged("PeriodCollection");
            }
        }
        /// <summary>
        /// Vult de de academy combobox
        /// </summary>
        public void FillAcademies()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT Afkorting,Omschrijving FROM academie");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Academies.Add(new Academy
                {
                    Abbreviation = table.Rows[RowNr][0].ToString(),
                    Description = table.Rows[RowNr][1].ToString()
                });
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
                PeriodCollection[RowNr] = table.Rows[RowNr][0].ToString();
            }
        }
        /// <summary>
        /// Vult de opleiding combobox
        /// </summary>
        public void Fillopleiding()
        {
            Educations.Clear();
            MySqlCommand cmd = new MySqlCommand("Select omschrijving,Academie_afkorting from opleiding");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = ModelFactory.Database.getData(cmd);
            adapter.Fill(table);
            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Educations.Add(new Education
                {
                    Description = table.Rows[RowNr][0].ToString(),
                    Abbreviation = table.Rows[RowNr][1].ToString()
                });
            }
        }
    }

}
