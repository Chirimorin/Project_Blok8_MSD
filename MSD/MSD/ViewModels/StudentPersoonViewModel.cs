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

        public StudentPersoonViewModel(IApplicationController app)
        {
            _app = app;
            _verderCommand = new RelayCommand(Verder);
        }
        public RelayCommand VerderCommand { get { return _verderCommand; } }
        public void Verder(object command)
        {
            _app.ShowStudentBedrijfView();
        }
    }
}
