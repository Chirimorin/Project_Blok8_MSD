using MSD.Controllers;
using MSD.Models;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MSD.ViewModels
{
    class LogInViewModel : PropertyChangedBase
    {
        private readonly IApplicationController _app;
        private MainWindow mainwindow = new MainWindow();
        private string foutmelding;
        private readonly RelayCommand _ShowMainWindow;

        public LogInViewModel(IApplicationController app)
        {
            _app = app;
            foutmelding = "*U heeft niet de juiste gegevens ingevoerd&#xA;probeer het opnieuw";
            this._ShowMainWindow = new RelayCommand(ShowMainWindow);
        }
        public string Melding
        {
            get { return foutmelding; }
        }

        public RelayCommand ShowMainWindowCommand { get { return _ShowMainWindow; } }

        public void ShowMainWindow(object command)
        {
            _app.ShowMainWindow();
        }
    }
}
