using MSD.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MSD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ApplicationController _applicationController;
        private LogInView login;

        public App()
        {
            _applicationController = new ApplicationController(this);
        }
        

    }
}
