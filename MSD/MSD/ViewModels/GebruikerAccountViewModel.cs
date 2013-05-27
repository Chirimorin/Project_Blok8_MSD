using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class GebruikerAccountViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _opslaanCommand;
        private readonly RelayCommand _terugCommand;

        public GebruikerAccountViewModel(IApplicationController app)
        {
            _app = app;
            _opslaanCommand = new RelayCommand(Opslaan);
            _terugCommand = new RelayCommand(Terug);
        }

        public RelayCommand VerderCommand { get { return _opslaanCommand; } }
        public void Opslaan(object command)
        {
            //opslaan van gegevens
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowGebruikerView();
        }
    }
}
