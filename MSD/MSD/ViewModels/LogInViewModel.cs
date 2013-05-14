using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MSD.ViewModels
{
    class LogInViewModel
    {
        private MainWindow mainwindow = new MainWindow();
        private string foutmelding;

        public LogInViewModel()
        {
            foutmelding = "*U heeft niet de juiste gegevens ingevoerd&#xA;probeer het opnieuw";
        }
        public string Melding
        {
            get { return foutmelding; }
        }

        public MainWindow login
        {
            get { return mainwindow; }
        }

    }
}
