using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchInvoerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _matchenCommand;

        public MatchInvoerViewModel(IApplicationController app)
        {
            _app = app;
            _matchenCommand = new RelayCommand(Matchen);
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            _app.ShowMatchMogelijkView();
        }
    }
}
