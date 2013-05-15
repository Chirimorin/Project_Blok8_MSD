using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
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
    }
}
