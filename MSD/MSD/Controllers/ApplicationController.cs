using MSD.Services;
using MSD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSD.Controllers
{
    class ApplicationController : IApplicationController
    {
        private MainWindowService _mainWindowService;
        private MainWindow _mainWindow;

        public ApplicationController()
        {
            _mainWindow = new MainWindow();
            _mainWindowService = new MainWindowService(_mainWindow);
        }

    }
}
