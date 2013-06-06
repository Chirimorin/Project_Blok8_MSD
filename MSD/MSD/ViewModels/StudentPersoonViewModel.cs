using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StudentPersoonViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _verderCommand;
        private readonly RelayCommand _terugCommand;
        private Student _student;

        public StudentPersoonViewModel(IApplicationController app)
        {
            _app = app;
            _verderCommand = new RelayCommand(Verder);
            _terugCommand = new RelayCommand(Back);
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Back(object command)
        {
            _app.ShowStudentView();
        }

        public RelayCommand VerderCommand { get { return _verderCommand; } }
        public void Verder(object command)
        {
            _app.ShowStageopdrachtView();
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
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
            }
        }

        public string StudentNo
        {
            get { 
                if (Student.StudentNo == null) return "";
                return Student.StudentNo;
            }
            set { Student.StudentNo = value;
            OnPropertyChanged("StudentNo");
            }

        }

        public string Name
        {
            get
            {
                if (Student.Name == null) return "";
                return Student.Name; }
            set { Student.Name = value;
            OnPropertyChanged("Name");
            }
        }

        public string Email
        {
            get
            {
                if (Student.Email == null) return "";
                return Student.Email; }
            set { Student.Email = value;
            OnPropertyChanged("Email");
            }
        }

        public string Phone
        {
            get
            {
                if (Student.Phone == null) return "";
                return Student.Phone; }
            set { Student.Phone = value;
            OnPropertyChanged("Phone");
            }
        }

        public string Education
        {
            get
            {
                if (Student.Education == null) return "";
                return Student.Education; }
            set { Student.Education = value;
            OnPropertyChanged("Education");
            }
        }

    }
}
