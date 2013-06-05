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

            vm.Email = "";
            vm.FirstName = "";
            vm.LastName = "";
            vm.StudentNo = "";
            vm.Year = "";

            vm2.Accepted = false;
            vm2.Assignment = "";
            vm2.Comments = "";
            vm2.Permission = false;
            vm2.TempPermission = false;

            _app.ShowStudentPersoonView();
        }

        public RelayCommand StudentAanpassenCommand { get { return _studentAanpassenCommand; } }
        public void StudentAanpassen(object command)
        {
            StudentPersoonViewModel vm = (StudentPersoonViewModel)ViewFactory.getViewModel(_app, "studentPersoonViewModel");
            StageopdrachtViewModel vm2 = (StageopdrachtViewModel)ViewFactory.getViewModel(_app, "stageopdrachtViewModel");

            vm.Title = "Student Aanpassen";

            //TODO: Gegevens invullen zoals in de database!
            vm.Email = "";
            vm.FirstName = "";
            vm.LastName = "";
            vm.StudentNo = "";
            vm.Year = "";

            vm2.Accepted = false;
            vm2.Assignment = "";
            vm2.Comments = "";
            vm2.Permission = false;
            vm2.TempPermission = false;

            _app.ShowStudentPersoonView();
        }

        /// <summary>
        /// Vult de Student tabel met student uit de Database
        /// </summary>
        public void fillStudentTable()
        {
            Students.Clear();
            MySqlCommand cmd = new MySqlCommand("select * from Student");
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = Database.getData(cmd);
            adapter.Fill(table);

            for (int RowNr = 0; RowNr < table.Rows.Count; RowNr++)
            {
                Students.Add(new Student
                {
                    Name = table.Rows[RowNr][2].ToString(),
                });
            }
        }

        public string Name { get { return FirstName + " " + LastName; } }

        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
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
