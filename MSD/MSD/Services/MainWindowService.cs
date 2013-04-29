using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Services
{
    class MainWindowService
    {
        private Views.MainWindow _mainWindow;

        public MainWindowService(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            _mainWindow.Show();
        }
    }
}
