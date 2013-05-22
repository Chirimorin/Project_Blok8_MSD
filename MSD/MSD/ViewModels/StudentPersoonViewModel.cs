using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
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
    }
}
