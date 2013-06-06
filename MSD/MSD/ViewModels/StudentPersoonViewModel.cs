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
            get { return Student.StudentNo; }
            set { Student.StudentNo = value; }
        }

        public string Name
        {
            get { return Student.Name; }
            set { Student.Name = value; }
        }

        public string Email
        {
            get { return Student.Email; }
            set { Student.Email = value; }
        }

        public string Phone
        {
            get { return Student.Phone; }
            set { Student.Phone = value; }
        }

        public string Education
        {
            get { return Student.Education; }
            set { Student.Education = value; }
        }

    }
}
