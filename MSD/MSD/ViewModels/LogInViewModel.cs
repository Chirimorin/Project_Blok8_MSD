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


        public MainWindow login
        {
            get { return mainwindow; }
        }

    }
}
