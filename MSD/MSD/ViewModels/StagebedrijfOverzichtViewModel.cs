using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class StagebedrijfOverzichtViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuwBedrijfCommand;
        private readonly RelayCommand _bedrijfAanpassenCommand;

        public StagebedrijfOverzichtViewModel(IApplicationController app)
        {
            _app = app;
            _nieuwBedrijfCommand = new RelayCommand(NieuwBedrijf);
            _bedrijfAanpassenCommand = new RelayCommand(BedrijfAanpassen);
        }

        public RelayCommand NieuwBedrijfCommand { get { return _nieuwBedrijfCommand;}}
        public void NieuwBedrijf(object command)
        {
            _app.ShowBedrijfView();
        }

        public RelayCommand BedrijfAanpassenCommand { get { return _bedrijfAanpassenCommand; } }
        public void BedrijfAanpassen(object command)
        {
            _app.ShowBedrijfView();
        }
    }
}
