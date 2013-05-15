﻿using MSD.Controllers;
using MSD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.ViewModels
{
    public class GebruikerViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private readonly RelayCommand _nieuweGebruikerCommand;
        private readonly RelayCommand _gebruikerAanpassenCommand;

        public GebruikerViewModel(IApplicationController app)
        {
            _app = app;
            _nieuweGebruikerCommand = new RelayCommand(NieuweGebruiker);
            _gebruikerAanpassenCommand = new RelayCommand(GebruikerAanpassen);
        }

        public RelayCommand NieuweGebruikerCommand { get { return _nieuweGebruikerCommand; } }
        public void NieuweGebruiker(object command)
        {
            _app.ShowGebruikerAccountView();
        }

        public RelayCommand GebruikerAanpassenCommand { get { return _gebruikerAanpassenCommand; } }
        public void GebruikerAanpassen(object command)
        {
            _app.ShowGebruikerAccountView();
        }
    }
}
