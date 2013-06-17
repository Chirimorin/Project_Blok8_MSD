using MSD.Controllers;
using MSD.Entity;
using MSD.Factories;
using MSD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchDetailsViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _terugCommand;
        private readonly RelayCommand _matchMakenCommand;
        private int _stagenr;

        public int Stagenr
        {
            get { return _stagenr; }
            set { _stagenr = value; }
        }

        public MatchDetailsViewModel(IApplicationController app)
        {
            _app = app;
            _terugCommand = new RelayCommand(Terug);
            _matchMakenCommand = new RelayCommand(MatchMaken);
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            student.Clear();
            supervisor.Clear();
            secondreader.Clear();
            _app.ShowMatchMogelijkView();
        }

        public RelayCommand MatchMakenCommand { get { return _matchMakenCommand; } }
        public void MatchMaken(object command)
        {
            MatchSuccesViewModel vm = (MatchSuccesViewModel)ViewFactory.getViewModel(_app, "matchSuccesViewModel");
            vm.Student.Add(Student[0]);
            vm.Supervisor.Add(Supervisor[0]);
            Match(Supervisor[0], 7);
            if (Secondreader.Count != 0)
            {
                vm.Secondreader.Add(Secondreader[0]);
                Match(Secondreader[0], 0);
            }
            _app.ShowMatchSuccesView();
        }
        public void Match(Teacher teacher, int uren)
        {
            string query = "INSERT INTO docent_has_stageopdracht VALUES(" + teacher.TeacherNo + "," + _stagenr + ", 'Begeleider');";
            MySqlCommand mycommand = new MySqlCommand(query);
            ModelFactory.Database.setData(mycommand);
            query = "UPDATE `docent` SET `Uren`= ((SELECT `Uren`)-"+ uren + ") WHERE `docentnr` = " + teacher.TeacherNo;
            mycommand = new MySqlCommand(query);
            ModelFactory.Database.setData(mycommand);
        }
        private ObservableCollection<Student> student = new ObservableCollection<Student>();

        public ObservableCollection<Student> Student
        {
            get { return student; }
            set
            {
                student = value;
                this.OnPropertyChanged("Students");
            }
        }
        private ObservableCollection<Teacher> supervisor = new ObservableCollection<Teacher>();

        public ObservableCollection<Teacher> Supervisor
        {
            get { return supervisor; }
            set
            {
                supervisor = value;
                this.OnPropertyChanged("Supervisor");
            }
        }
        private ObservableCollection<Teacher> secondreader = new ObservableCollection<Teacher>();

        public ObservableCollection<Teacher> Secondreader
        {
            get { return secondreader; }
            set
            {
                secondreader = value;
                this.OnPropertyChanged("Secondreader");
            }
        }

    }
}
