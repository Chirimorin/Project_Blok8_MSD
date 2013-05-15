using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    class MatchMogelijkViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _showDetailsCommand;
        private readonly RelayCommand _matchenCommand;

        public MatchMogelijkViewModel(IApplicationController app)
        {
            _app = app;
            _showDetailsCommand = new RelayCommand(ShowDetails);
            _matchenCommand = new RelayCommand(Matchen);
        }

        public RelayCommand ShowDetailsCommand { get { return _showDetailsCommand; } }
        public void ShowDetails(object command)
        {
            _app.ShowMatchDetailsView();
        }

        public RelayCommand MatchenCommand { get { return _matchenCommand; } }
        public void Matchen(object command)
        {
            _app.ShowMatchSuccesView();
        }
    }
}
