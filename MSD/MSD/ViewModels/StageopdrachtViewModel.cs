using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StageopdrachtViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _saveCommand;

        public StageopdrachtViewModel(IApplicationController app)
        {
            _app = app;
            _saveCommand = new RelayCommand(Save);
        }

        public RelayCommand SaveCommand { get { return _saveCommand; } }
        public void Save(object command)
        {
            _app.ShowStudentView();
        }
    }
}
