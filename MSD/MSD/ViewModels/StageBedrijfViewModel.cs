using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    public class StageBedrijfViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _saveCommand;



        public StageBedrijfViewModel(IApplicationController app)
        {
            _app = app;
            _saveCommand = new RelayCommand(Save);
        }

        public RelayCommand SaveCommand { get { return _saveCommand; } }
        public void Save(object command)
        {

        }
    }
}
