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
using System.Windows;

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
            
            if (Secondreader.Count != 0 && Secondreader[0].TeacherNo == Supervisor[0].TeacherNo)
            {
                MessageBox.Show("De begeleider en tweede lezer mag niet hetzelfde zijn");
            }
            else
            {
                MatchSuccesViewModel vm = (MatchSuccesViewModel)ViewFactory.getViewModel(_app, "matchSuccesViewModel");
                vm.Student.Add(Student[0]);
                vm.Supervisor.Add(Supervisor[0]);
                string deletequery = "DELETE FROM docent_has_stageopdracht WHERE stageopdracht_stagenr = " + _stagenr;
                MySqlCommand mycommand = new MySqlCommand(deletequery);
                ModelFactory.Database.setData(mycommand);
                Match(Supervisor[0], Supervisor[0].Education.Hours, "Begeleider");
                if (Secondreader.Count != 0)
                {
                    vm.Secondreader.Add(Secondreader[0]);
                    Match(Secondreader[0], 0, "Tweede lezer");
                }
                _app.ShowMatchSuccesView();
            }
            
        }
        public void Match(Teacher teacher, int uren, string type)
        {
            
            string query = "INSERT INTO docent_has_stageopdracht VALUES(" + teacher.TeacherNo + "," + _stagenr + ",'" + type + "');";
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
