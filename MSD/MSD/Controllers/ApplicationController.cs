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
        private LogInView _login;

        public ApplicationController()
        {

            _login = new LogInView();
            _login.Show();
            Models.Database database = new Models.Database();
        }

    }
}
