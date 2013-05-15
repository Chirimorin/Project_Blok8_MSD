using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchDetailsViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _terugCommand;
        private readonly RelayCommand _matchMakenCommand;

        public MatchDetailsViewModel(IApplicationController app)
        {
            _app = app;
            _terugCommand = new RelayCommand(Terug);
            _matchMakenCommand = new RelayCommand(MatchMaken);
        }

        public RelayCommand TerugCommand { get { return _terugCommand; } }
        public void Terug(object command)
        {
            _app.ShowMatchMogelijkView();
        }

        public RelayCommand MatchMakenCommand { get { return _matchMakenCommand; } }
        public void MatchMaken(object command)
        {
            _app.ShowMatchSuccesView();
        }

    }
}
