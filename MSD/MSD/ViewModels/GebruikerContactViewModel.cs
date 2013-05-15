using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class GebruikerContactViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;

        public GebruikerContactViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
        }

        public RelayCommand OpslaanCommand { get { return _opslaanCommand; } }
        public void Opslaan(object command)
        {
            _app.ShowGebruikerView();
        }
    }
}
