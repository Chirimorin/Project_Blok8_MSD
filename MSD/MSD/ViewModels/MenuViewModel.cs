using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MenuViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _ShowDocentCommand;
        private readonly RelayCommand _ShowStudentCommand;
        private readonly RelayCommand _ShowMatchCommand;
        private readonly RelayCommand _ShowStageCommand;
        private readonly RelayCommand _ShowGebruikersCommand;

        

        public MenuViewModel(IApplicationController app)
        {
            _app = app;
            this._ShowDocentCommand = new RelayCommand(ShowDocent);
        }
        
        public RelayCommand ShowDocentCommand { get { return _ShowDocentCommand; } }
        public void ShowDocent(object command)
        {
            _app.ShowDocentView();
        }
        public void ShowStudent(object command)
        {
            _app.ShowStudentView();
        }

        //username komt uit de database
        public string UserName
        {
            get { return UserName; }
        }
        
    }
}
