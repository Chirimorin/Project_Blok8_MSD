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

namespace MSD.ViewModels
{
    class StudentViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuweStudentCommand;
        private readonly RelayCommand _studentAanpassenCommand;
        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        private Student _selectedItem;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { 
                _students = value;
                this.OnPropertyChanged("Students");
            }
        }

        public Student SelectedItem
        {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                this.OnPropertyChanged("SelectedItem");
                OnPropertyChanged("StudentName");
                OnPropertyChanged("StudentNo");
                OnPropertyChanged("Email");
               /* Teacher;
                Company;
                SecondReader;
                StageType;
                Period*/
            }
        }

        public StudentViewModel(IApplicationController app)
        {
            _app = app;
            _nieuweStudentCommand = new RelayCommand(NieuweStudent);
            _studentAanpassenCommand = new RelayCommand(StudentAanpassen);
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
            vm2.Assignment = new Assignment();
            vm2.Student = vm.Student;

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
            makeAssignment(vm2);

            


            //vm2.Assignment = get assignment from selected item
            //TODO: Gegevens invullen zoals in de database!

            _app.ShowStudentPersoonView();
        }
        /// <summary>
        /// maakt het assignment object dat bij de student hoort
        /// </summary>
        public void makeAssignment(StageopdrachtViewModel vm2)
        {
            string query = "SELECT o.opdrachtnaam, o.omschrijving, o.opmerking, o.opdrachtgoed, o.toestemmingvoorlopig, o.toestemmingdefinitief, o.periode_periodenaam FROM stageopdracht o JOIN stageopdracht_has_student s ON o.stagenr = s.stageopdracht_stagenr WHERE s.student_studentnr = " + SelectedItem.StudentNo  + ";";
            MySqlCommand mycommand = new MySqlCommand(query);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable data = new DataTable();
            adapter = Database.getData(mycommand);
            adapter.Fill(data);
            if (data.Rows.Count != 0)
            {
                vm2.Assignment = new Assignment
                {
                    Name = data.Rows[0][0].ToString(),
                    Description = data.Rows[0][1].ToString(),
                    Comments = data.Rows[0][2].ToString(),
                    Accepted = (bool)data.Rows[0][3],
                    Permission = (bool)data.Rows[0][5],
                    TempPermission = (bool)data.Rows[0][4],
                    //KnowLedgeItem = data.Rows[0][5].ToString(),
                    Period = data.Rows[0][6].ToString(),
                };
            }
            else
            {
                vm2.Assignment = null;
            }
        }

        /// <summary>
        /// Vult de Student tabel met student uit de Database
        /// </summary>
        public void fillStudentTable()
        {
            Students.Clear();
            MySqlCommand cmd = new MySqlCommand("select s.studentnr, s.naam, s.mailadres, s.telefoonnr, o.omschrijving, s.opleiding_academie_afkorting from student s JOIN opleiding o ON s.opleiding_afkorting = o.afkorting");
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
                    Academie = table.Rows[RowNr][5].ToString()
                    
                });
            }
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
                if(SelectedItem != null) 
                    return SelectedItem.Name;
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

        private string _education;
        public string Education
        {
            get
            {
                return _education;
            }
            set
            {
                _education = value;
                OnPropertyChanged("Education");
            }
        }

        private string _teacher;
        public string Teacher
        {
            get
            {
                return _teacher;
            }
            set
            {
                _teacher = value;
                OnPropertyChanged("Teacher");
            }
        }

        private string _educationYear;
        public string EducationYear
        {
            get
            {
                return _educationYear;
            }
            set
            {
                _educationYear = value;
                OnPropertyChanged("EducationYear");
            }
        }

        private string _company;
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }

        private bool _accepted;
        public bool Accepted
        {
            get
            {
                return _accepted;
            }
            set
            {
                _accepted = value;
                OnPropertyChanged("Accepted");
            }
        }

        private bool _tempPermission;
        public bool TempPermission
        {
            get
            {
                return _tempPermission;
            }
            set
            {
                _tempPermission = value;
                OnPropertyChanged("TempPermission");
            }
        }

        private bool _permission;
        public bool Permission
        {
            get
            {
                return _permission;
            }
            set
            {
                _permission = value;
                OnPropertyChanged("Permission");
            }
        }
    }
}
