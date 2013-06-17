using MSD.Controllers;
using MSD.Entity;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchSuccesViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _terugCommand;

        public MatchSuccesViewModel(IApplicationController app)
        {
            _app = app;
            _terugCommand = new RelayCommand(Terug);
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowMatchInvoerView();
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
