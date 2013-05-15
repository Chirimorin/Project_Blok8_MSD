using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    public class DocentViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _NieuweDocentCommand;
        private readonly RelayCommand _DocentAanpassenCommand;

        public DocentViewModel(IApplicationController app)
        {
            _app = app;
            _NieuweDocentCommand = new RelayCommand(NieuweDocent);
            _DocentAanpassenCommand = new RelayCommand(DocentAanpassen);
        }

        public RelayCommand NieuweDocentCommand { get { return _NieuweDocentCommand; } }
        public void NieuweDocent(object command)
        {
            _app.ShowDocentPersoonView();
        }

        public RelayCommand DocentAanpassenCommand { get { return _DocentAanpassenCommand; } }
        public void DocentAanpassen(object command)
        {
            _app.ShowDocentPersoonView();
        }
    }
}
